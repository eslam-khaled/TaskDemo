using AutoMapper;
using DemoTask.Business.BusinessInterface;
using DemoTask.DAL.BaseRepository;
using DemoTask.DAL.Models;
using DemoTask.DAL.UnitOfWork;
using DemoTask.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoTask.Business.BusinessImplementation
{
    public class ProductBusiness : IPlayerBusiness
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _UnitOfWork;

        public ProductBusiness(IBaseRepository<Product> baseRepository, IMapper mapper, IUnitOfWork UnitOfWork)
        {
            _mapper = mapper;
            _UnitOfWork = UnitOfWork;
        }

        /// <inheritdoc/>
        public bool AddProductsList(IEnumerable<PlayerDto> ProductsListDto)
        {
            var mappedList = _mapper.Map<List<Product>>(ProductsListDto);

             _UnitOfWork.Product.AddList(mappedList);
            return true;
        }

        /// <inheritdoc/>
        public bool DeletePlayerById(int Id)
        {
            if (Id == 0)
            {
                return false;
            }
            var player = _UnitOfWork.Product.GetWhere(x => x.Id == Id && x.IsDeleted == false).FirstOrDefault();
            player.IsDeleted = true;
             _UnitOfWork.Product.Update(player);
            return true;
        }

        /// <inheritdoc/>
        public bool DeleteProductsListByCategoryId(int CategoryId)
        {
            try
            {
                if (CategoryId == 0)
                {
                    return false;
                }

                var playersList = _UnitOfWork.Product.GetWhere(x => x.CategoryId == CategoryId && x.IsDeleted == false).ToList();

                foreach (var player in playersList)
                {
                    player.IsDeleted = true;
                    _UnitOfWork.Product.Update(player);
                }
                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        /// <inheritdoc/>
        public IEnumerable<PlayerDto> GetPlayersListByTeamId(int CategoryId)
        {
            var PlayersList = _UnitOfWork.Product.GetWhere(x => x.CategoryId == CategoryId && x.IsDeleted == false);

            var mapped = _mapper.Map<List<PlayerDto>>(PlayersList);

            return mapped;
        }


        /*
a. We have an array which its length is 100 (1 to 100) - sorted
b. All the numbers in the array are unique expect a one duplicate number which can be anywhere in the array
c. Write down a function which take the array as input and return the duplicate number
*/


        //public int GetDuplicated(int[] arr)
        //{
        //    foreach (var item in arr)
        //    {
        //        item.arr
        //        if (item == arr.) return item;
        //    }
        //}








    }
}
