using DemoTask.DAL.BaseRepository;
using DemoTask.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DemoTask.DAL.UnitOfWork
{
    public interface IUnitOfWork: IDisposable
    {
        DataContext Context { get; }

        Task<int> CompleteAsync();

        IBaseRepository<Product> Product { get; set; }
        IBaseRepository<Category> Category { get; set; }
    }
}
