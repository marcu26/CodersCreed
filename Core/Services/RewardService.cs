using Core.Database.Entities;
using Core.Dtos.Courses;
using Core.Dtos.Rewards;
using Core.UnitOfWork;
using Core.Utils.Pageable;
using Infrastructure.Exceptions;
using Org.BouncyCastle.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class RewardService
    {
        public EfUnitOfWork _efUnitOfWork { get; set; }
        public RewardService(EfUnitOfWork efUnitOfWork)
        {
            _efUnitOfWork = efUnitOfWork;
        }

        public async Task CreateRewardAsync(CreateRewardDto payload)
        {
            var exist = await _efUnitOfWork._rewardsRepository.DoesNameExist(payload.Name);

            if (exist)
                throw new WrongInputException($"Reward with name {payload.Name} allready exists");

            var reward = new Reward { Name = payload.Name, Description = payload.Description, Cost = payload.cost };
            _efUnitOfWork._rewardsRepository.Add(reward);

            await _efUnitOfWork.SaveAsync();
        }

        public async Task<GetRewardDto> GetRewardsByIdAsync(int rewardId)
        {
            var reward = await _efUnitOfWork._rewardsRepository.GetByIdAsync(rewardId);

            if(reward == null)
                throw new WrongInputException($"Reward with id {rewardId} does not exist.");
            
            if(reward.IsDeleted)
                throw new WrongInputException($"Reward with id {rewardId} does not exist.");

            return new GetRewardDto { Name = reward.Name, Description = reward.Description, Cost = reward.Cost, Id = reward.Id };
        }

        public async Task DeleteRewardAsync(int rewardId)
        {
            var reward = await _efUnitOfWork._rewardsRepository.GetByIdAsync(rewardId);

            if(reward == null)
                throw new WrongInputException($"Reward with id {rewardId} does not exist.");
            if(reward.IsDeleted)
                throw new WrongInputException($"Reward with id {rewardId} does not exist.");

            reward.IsDeleted = true;

            await _efUnitOfWork.SaveAsync();
        }
        public async Task UpdateRewardAsync(UpdateRewardDto payload)
        {
            var reward = await _efUnitOfWork._rewardsRepository.GetByIdAsync(payload.Id);

            if(reward == null)
                throw new WrongInputException($"Reward with id {payload.Id} does not exist.");

            if(reward.IsDeleted)
                throw new WrongInputException($"Reward with id {payload.Id} does not exist.");

            if(payload.Name != null)
            {
                var alreadyExists = await _efUnitOfWork._rewardsRepository.DoesNameExist(payload.Name);

                if (alreadyExists)
                    throw new WrongInputException($"Reward with name {payload.Name} already exists.");
            }

            reward.Name = String.IsNullOrEmpty(payload.Name) ? reward.Name : payload.Name;
            reward.Description = String.IsNullOrEmpty(payload.Description) ? reward.Description : payload.Description;
            reward.Cost = payload.Cost == null ? reward.Cost : payload.Cost.Value;

            await _efUnitOfWork.SaveAsync();
        }

        public async Task<PageableResponse> GetPaginaAsync(PageablePostModel request)
        {
            var dto = await _efUnitOfWork
                    ._rewardsRepository
                    .GetPaginaAsync(request.start, request.length);

            return new PageableResponse()
            {
                draw = request.draw,
                recordTotal = dto.NumarTotalRanduri,
                recordsFiltered = dto.NumarRanduriFiltrate,
                data = dto.Pagina.Select(e => new GetRewardDto
                {
                    Id = e.Id,
                    Name = e.Name,
                    Description = e.Description,
                    Cost = e.Cost,
                })
            };
        }
    }
}
