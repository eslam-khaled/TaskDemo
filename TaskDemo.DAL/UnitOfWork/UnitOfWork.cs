using DemoTask.DAL.BaseRepository;
using DemoTask.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DemoTask.DAL.UnitOfWork
{
    public class UnitOfWork: IUnitOfWork
    {
        public DataContext Context { get; }

        public IBaseRepository<Product> Product { get; set; }
        public IBaseRepository<Category> Category { get; set; }

        public UnitOfWork(DataContext context)
        {
            Context = context;
            Product = new BaseRepository<Product>(Context);
            Category = new BaseRepository<Category>(Context);
        }


        #region Unit of work methods

        public async Task<int> CompleteAsync()
        {
            return await Context.SaveChangesAsync();
        }

        public int Complete()
        {
            return Context.SaveChanges();
        }

        public virtual void Dispose(bool disposing)
        {
            if(disposing)
            {
                Context.Dispose();
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
