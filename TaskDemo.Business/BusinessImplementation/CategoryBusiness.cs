using AutoMapper;
using DemoTask.Business.BusinessInterface;
using DemoTask.DAL;
using DemoTask.DAL.BaseRepository;
using DemoTask.DAL.Models;
using DemoTask.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DemoTask.Business.BusinessImplementation
{
    public class CategoryBusiness : ITeamBusiness
    {
        private readonly IBaseRepository<Category> _baseRepository;
        private readonly IMapper _mapper;
        private readonly DataContext _dataContext;
        private readonly IPlayerBusiness _playerBusiness;
        public CategoryBusiness(IBaseRepository<Category> baseRepository, IMapper mapper
            , DataContext dataContext, IPlayerBusiness playerBusiness)
        {
            _baseRepository = baseRepository;
            _mapper = mapper;
            _dataContext = dataContext;
            _playerBusiness = playerBusiness;
        }

        /// <inheritdoc/>
        public TeamDto AddTeam(TeamDto categoryDto)
        {
            using (_dataContext)
            {
                using (var transaction = _dataContext.Database.BeginTransaction())
                {
                    try
                    {

                        if (categoryDto == null || string.IsNullOrEmpty(categoryDto.Name))
                        {
                            categoryDto.Success = false;
                            categoryDto.Message = "Team is null or empty";
                            return categoryDto;
                        }

                        var mapped = _mapper.Map<Category>(categoryDto);

                        var addedTeam = _baseRepository.AddNew(mapped);

                        if (addedTeam.Id < 0)
                        {
                            categoryDto.Success = false;
                            categoryDto.Message = "Couldn't Add";
                            return categoryDto;
                        }

                        categoryDto.Id = addedTeam.Id;
                        categoryDto.Success = true;
                        categoryDto.Message = "Added successfully";

                        foreach (var player in categoryDto.playerListDto)
                        {
                            player.CategoryId = addedTeam.Id;
                        }

                        _playerBusiness.AddProductsList(categoryDto.playerListDto);


                        transaction.Commit();

                        return categoryDto;

                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        Console.WriteLine("Error occurred." + ex);
                        categoryDto.Success = false;
                        categoryDto.Message = "Couldn't add";
                        return categoryDto;
                    }
                }
            }


        }

        /// <inheritdoc/>
        public bool DeleteTeamById(int Id)
        {
            using (_dataContext)
            {
                using (var transaction = _dataContext.Database.BeginTransaction())
                {
                    try
                    {

                        var team = _baseRepository.GetWhere(x => x.Id == Id).FirstOrDefault();
                        team.IsDeleted = true;
                        _baseRepository.Update(team);

                        _playerBusiness.DeleteProductsListByCategoryId(Id);

                        transaction.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        Console.WriteLine("Error occurred." + ex);
                    }

                }

            }
            return false;
        }

        /// <inheritdoc/>
        public TeamDto GetCategoryById(int Id)
        {
            var Category = _baseRepository.GetWhere(x => x.Id == Id && x.IsDeleted == false).FirstOrDefault();
            var mapped = _mapper.Map<TeamDto>(Category);
            return mapped;
        }

        /// <inheritdoc/>
        public ICollection<TeamDto> GetTeamsList()
        {
            var AllTeams = _baseRepository.GetWhere(x => x.IsDeleted == false);

            var mapped = _mapper.Map<List<TeamDto>>(AllTeams);
 
            return mapped;
        }

        /// <inheritdoc/>
        public TeamDto UpdateTeam(TeamDto CategoryDto)
        {
            if (CategoryDto == null)
            {
                CategoryDto.Success = false;
                CategoryDto.Message = "Team is null or empty";
                return CategoryDto;
            }

            var mapped = _mapper.Map<Category>(CategoryDto);
            _baseRepository.Update(mapped);

            CategoryDto.Success = true;
            CategoryDto.Message = "Updated successfully";

            return CategoryDto;
        }
    }
}
