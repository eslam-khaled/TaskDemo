using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DemoTask.DAL.BaseRepository
{
    public interface IBaseRepository<T> where T : class
    {
        T AddNew(T obj);
        bool AddList(IEnumerable<T> obj);
        IEnumerable<T> GetAll();
        T FindById(int id);
        bool Delete(T id);
        T GetById(int id);
        bool Update(T obj);
        IQueryable<T> GetWhere(Expression<Func<T, bool>> predicate);
    }
}
