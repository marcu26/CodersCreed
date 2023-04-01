using Core.Database.Entities;
using Core.Dtos.Badges;
using Core.Dtos.Courses;
using Core.UnitOfWork;
using Core.Utils.Pageable;
using Infrastructure.Exceptions;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class BadgeService
    {
        public EfUnitOfWork _efUnitOfWork { get; set; }
        public BadgeService(EfUnitOfWork efUnitOfWork)
        {
            _efUnitOfWork = efUnitOfWork;
        }

        public async Task CreateBadgeAsync(CreateBadgeDto payload)
        {
            var existName = await _efUnitOfWork._badgesRepository.DoesNameExist(payload.Name);
            var existImage = await _efUnitOfWork._badgesRepository.DoesImageExist(payload.Image);
            var existDescription = await _efUnitOfWork._badgesRepository.DoesDescriptionExist(payload.Description);


            if (existName)
                throw new WrongInputException($"Bage with name {payload.Name} allready exists");

            if (existImage)
                throw new WrongInputException($"Bage with this image {payload.Image} allready exists");

            if (existDescription)
                throw new WrongInputException($"Bage with this description: {payload.Description} allready exists");


            if (payload.Points <= 0)
                throw new WrongInputException($"The number of points of a badge must be greater than 0");


            var badge = new Badge { Name = payload.Name, Description = payload.Description, Image = payload.Image, Points = payload.Points };

            _efUnitOfWork._badgesRepository.Add(badge);

            await _efUnitOfWork.SaveAsync();
        }

        public async Task<GetBadgeDto> GetBadgeByIdAsync(int badgeId)
        {
            var badge = await _efUnitOfWork._badgesRepository.GetByIdAsync(badgeId);

            if (badge == null)
                throw new WrongInputException($"badge with id {badgeId} does not exist.");


            if (badge.IsDeleted)
                throw new WrongInputException($"badge with id {badgeId} does not exist.");

            return new GetBadgeDto {Id = badge.Id, Name = badge.Name, Description = badge.Description, Image = badge.Image, Points = badge.Points };
        }

        public async Task DeleteBadgeAsync(int badgeId)
        {
            var badge = await _efUnitOfWork._badgesRepository.GetByIdAsync(badgeId);

            if(badge == null)
                throw new WrongInputException($"Badge with id {badgeId} does not exist.");

            if(badge.IsDeleted)
                throw new WrongInputException($"Badge with id {badgeId} does not exist.");

            badge.IsDeleted = true;

            await _efUnitOfWork.SaveAsync();
        }

        public async Task UpdateBadgeAsync(UpdateBadgeDto payload)
        {
            var badge = await _efUnitOfWork._badgesRepository.GetByIdAsync(payload.Id);

            if(badge == null)
                throw new WrongInputException($"Badge with id {payload.Id} does not exist.");
            if(badge.IsDeleted)
                throw new WrongInputException($"Badge with id {payload.Id} does not exist.");

            if(badge.Name != null)
            {
                var nameExists = await _efUnitOfWork._badgesRepository.DoesNameExist(payload.Name);

                if (nameExists)
                    throw new WrongInputException($"Bage with name {payload.Name} allready exists");
            }

            if(badge.Description != null) 
            {
                var descriptionExists = await _efUnitOfWork._badgesRepository.DoesDescriptionExist(payload.Description);

                if (descriptionExists)
                    throw new WrongInputException($"Bage with description: {payload.Description} allready exists");
            }

            if(badge.Image!= null) 
            {
                var imageExists = await _efUnitOfWork._badgesRepository.DoesImageExist(payload.Image);

                if (imageExists)
                    throw new WrongInputException($"Bage with image: {payload.Image} allready exists");
            }

            badge.Name = String.IsNullOrEmpty(payload.Name) ? badge.Name : payload.Name;
            badge.Description = String.IsNullOrEmpty(payload.Description) ? badge.Description : payload.Description;
            badge.Image = String.IsNullOrEmpty(payload.Image) ? badge.Image : payload.Image;
            badge.Points = payload.Points == null ? badge.Points : payload.Points.Value;

            await _efUnitOfWork.SaveAsync();
        }

        public async Task<PageableResponse> GetPaginaAsync(PageablePostModel request)
        {
            var dto = await _efUnitOfWork
                    ._badgesRepository
                    .GetPaginaAsync(request.start, request.length);

            return new PageableResponse()
            {
                draw = request.draw,
                recordTotal = dto.NumarTotalRanduri,
                recordsFiltered = dto.NumarRanduriFiltrate,
                data = dto.Pagina.Select(e => new GetBadgeDto
                {
                    Id = e.Id,
                    Name = e.Name,
                    Description= e.Description,
                    Points= e.Points,
                    Image= e.Image

                })
            };
        }

    }
}
