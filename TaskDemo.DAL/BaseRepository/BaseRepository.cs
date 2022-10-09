using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DemoTask.DAL.BaseRepository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly DataContext _context;
        public BaseRepository(DataContext dataContext)
        {
            _context = dataContext;
        }


        public T AddNew(T obj)
        {
            _context.Set<T>().Add(obj);
            _context.SaveChanges();
            return obj;
        }

        public bool AddList(IEnumerable<T> obj)
        {
            _context.Set<T>().AddRange(obj);
            _context.SaveChanges();
            return true;
        }

        public T FindById(int id)
        {
           return _context.Set<T>().Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public T GetById(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public bool Update(T obj)
        {
            _context.Set<T>().Attach(obj);
            _context.Entry(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return true;
        }
        public bool Delete(T obj)
        {
            _context.Set<T>().Remove(obj);
            _context.SaveChanges();
            return true;
        }

        public IQueryable<T> GetWhere(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().Where(predicate);
        }


    }
}
