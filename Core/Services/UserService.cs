using Core.Database.Entities;
using Core.Utils.Pageable;
using Core.Dtos.Authentication;
using Core.Dtos.Roles;
using Core.Dtos.Users;
using Core.Handlers;
using Core.UnitOfWork;
using Core.Utils;
using Infrastructure.Exceptions;
using System.Numerics;
using ISoft.Travel.Core.Dtos.EmailDtos;
using ISoft.Travel.Core.Services;
using System.Data;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Identity.Client;

namespace Core.Services
{
    public class UserService
    {
        public EfUnitOfWork _unitOfWork { get; set; }
        public AuthTokenHandler _authTokenHandler { get; set; }
        public EmailService _emailService { get; set; }

        public UserService(EfUnitOfWork unitOfWork, AuthTokenHandler authTokenHandler, EmailService emailService)
        {
            _unitOfWork = unitOfWork;
            _authTokenHandler = authTokenHandler;
            _emailService = emailService;
        }

        #region Utils

        private Random random = new Random();

        public string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        #endregion

        public async Task<User> CreateUserAsync(CreateUserDto payload)
        {
            var alreadyExists = await _unitOfWork._usersRepository.CheckIfUserAlreadyExistsAsync(payload.Email);

            if (alreadyExists)
                throw new WrongInputException($"User with email {payload.Email} already exists.");


            var user = new User
            {
                Email = payload.Email,
                Username = payload.Username,
                Password = Base64Encoder.Encode(payload.Password),

            };

            _unitOfWork._usersRepository.Add(user);

            await _unitOfWork.SaveAsync();

            return user;
        }

        public async Task AddRoleToUserAsync(string roleName, int userId)
        {
            var role = await _unitOfWork._rolesRepository.GetRoleByTitle(roleName);
            var user = await _unitOfWork._usersRepository.GetByIdAsync(userId);

            if (role == null)
                throw new WrongInputException($"Role with name={roleName} does not exist.");

            if (role.IsDeleted)
                throw new WrongInputException($"Role with name={roleName} does not exist.");


            if (user == null)
                throw new WrongInputException($"User with id={userId} does not exist.");

            if (user.IsDeleted)
                throw new WrongInputException($"User with id={userId} does not exist.");

            if (role.UserRoles.Any(ur => ur.UserId == userId))
                throw new EntityException("Role is already asigned.");

            role.UserRoles.Add(new UserRole()
            {
                User = user
            });

            await _unitOfWork.SaveAsync();
        }

        public async Task<AuthenticationResponseDto> AuthenticateUserAsync(AuthenticationRequestDto payload)
        {
            User user;

            user = await _unitOfWork._usersRepository.GetUserByCredentialsAsync(payload);
            if (user == null)
                throw new WrongInputException("There is no associated account with this username/password");
            else
                return new AuthenticationResponseDto()
                {
                    Id = user.Id,
                    Username = user.Username,
                    Token = _authTokenHandler.GenerateTokenAsync(user)
                };
        }

        public async Task<UserDto> GetUserDtoByIdAsync(int userId)
        {
            var user = await _unitOfWork._usersRepository.GetUserByIdAsync(userId);

            if (user == null)
                throw new WrongInputException($"User with id={userId} does not exist. ");

            var userDto = new UserDto()
            {
                Id = user.Id,
                Username = user.Username,
                Roles = user.UserRoles.Select(r => new RoleDto()
                {
                    Id = r.Role.Id,
                    Title = r.Role.Title

                }).ToList()
            };
            return userDto;
        }

        public async Task<PageableResponse> GetPaginaAsync(PageablePostModel request)
        {
            var dto = await _unitOfWork
                    ._usersRepository
                    .GetPaginaAsync(request.start, request.length);

            return new PageableResponse()
            {
                draw = request.draw,
                recordTotal = dto.NumarTotalRanduri,
                recordsFiltered = dto.NumarRanduriFiltrate,
                data = dto.Pagina.Select(e => new UserDto
                {
                    Id = e.Id,
                    Username = e.Username,
                    Roles = e.UserRoles.Select(ur => new RoleDto()
                    {
                        Id = ur.Role.Id,
                        Title = ur.Role.Title
                    }).ToList()
                })
            };
        }

        public async Task UpdateUserByIdAsync(UpdateUserDto payload)
        {
            var user = await _unitOfWork._usersRepository.GetUserByIdAsync(payload.UserId);
            if (user == null)
                throw new WrongInputException($"User with id={payload.UserId} doesn't exist.");

            if (user.IsDeleted)
                throw new WrongInputException($"User with id={payload.UserId} doesn't exist.");

            if (payload.Username != null)
            {
                var alreadyExists = await _unitOfWork._usersRepository.CheckIfUserAlreadyExistsAsync(payload.Username);

                if (alreadyExists)
                    throw new WrongInputException($"User with username {payload.Username} already exists.");
            }


            user.Username = String.IsNullOrEmpty(payload.Username) ? user.Username : payload.Username;

            user.UserRoles.RemoveAll(ur => ur.UserId == user.Id);


            await _unitOfWork.SaveAsync();

            foreach (var roleDto in payload.Roles)
            {
                user.UserRoles.Add(new UserRole { UserId = user.Id, RoleId = roleDto.roleId });
            }

            await _unitOfWork.SaveAsync();
        }


        public async Task UpdatePasswordAsync(int userId, ChangeUserPasswordDto payload)
        {
            var user = _unitOfWork._usersRepository.GetById(userId);

            if (user == null)
                throw new WrongInputException($"User with id={userId} does not exist.");

            if (user.IsDeleted)
                throw new WrongInputException($"User with id={userId} does not exist.");

            if (user.Password != Base64Encoder.Encode(payload.OldPassword))
                throw new Exception("Password does not match.");

            user.Password = Base64Encoder.Encode(payload.NewPassword);
            await _unitOfWork.SaveAsync();
        }

        public async Task SendForgetPasswordEmail(ForgetPasswordEmailDto payload)
        {
            var user = await _unitOfWork._usersRepository.GetUserByEmailAsync(payload.Email);

            if (user == null)
                throw new WrongInputException($"User with email {payload.Email} does not exist.");

            do
            {
                user.ResetPasswordCode = RandomString(6);
            } while (await _unitOfWork._usersRepository.CheckResetPasswordCodeExistence(user.ResetPasswordCode));

            await _unitOfWork.SaveAsync();

            EmailDto dto = new EmailDto()
            {
                Body = "Hello!\n This is your code to reset your meili password: " + user.ResetPasswordCode,
                To = payload.Email,
                Subject = "ResetPasswordEmail"
            };

            await _emailService.sendEmailAsync(dto);
        }

        public async Task ResetPasswordAsync(ResetPasswordDto payload)
        {
            var user = await _unitOfWork._usersRepository.GetUserByResetPasswordCodeAsync(payload.ResetPasswordCode);

            if (user == null)
                throw new WrongInputException("The code is not available");

            user.Password = Base64Encoder.Encode(payload.Password);
            user.ResetPasswordCode = null;

            await _unitOfWork.SaveAsync();
        }

        public async Task AddRewardToUser(int userId, int rewardId)
        {
            var user = await _unitOfWork._usersRepository.GetUserByIdAsyncWithProperties(userId);

            if (user == null)
                throw new WrongInputException($"User with id={userId} does not exist.");

            if (user.IsDeleted)
                throw new WrongInputException($"User with id={userId} does not exist.");

            var reward = await _unitOfWork._rewardsRepository.GetByIdAsync(rewardId);

            if (reward == null)
                throw new WrongInputException($"Reward with id {rewardId} does not exist.");

            if (reward.IsDeleted)
                throw new WrongInputException($"Reward with id {rewardId} does not exist.");

            var exists = await _unitOfWork._usersRepository.IsUserHavingTheReward(userId, rewardId);

            if (exists)
                throw new WrongInputException($"User with id {userId} allready has the reward {rewardId}");

            if(user.Points - reward.Cost< 0)
                throw new WrongInputException($"User with id {userId} has not enough points to buy reward with id: {rewardId}");

            user.Points -= reward.Cost;
            user.Rewards.Add(reward);

            await _unitOfWork.SaveAsync();
        }

        public async Task AddXpToUserAsync(int userId, int xp)
        {
            var user  = await _unitOfWork._usersRepository.GetUserByIdAsync(userId);

            if(user == null)
                throw new WrongInputException($"User with id={userId} does not exist.");

            if(xp < 0)
                throw new WrongInputException($"The number of Xp must be a positive number");

            user.Experience += xp;

            await _unitOfWork.SaveAsync();
        }

    }
}
