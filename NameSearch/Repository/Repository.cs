using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Data.Entity;

namespace Repository
{
    public interface IRepository<T>
    {
        void Add(T _item);
        void Remove(T _item);
        void Update(T _item);
        IQueryable<T> GetAllItems();
        T GetItemById(int _id);
        IQueryable<T> FilterItems(Expression<Func<T, bool>> _predicate);
    }

    public class Repository<T> : IRepository<T> where T : class
    {
        protected DbContext dbContext;
        protected DbSet<T> dbSet;

        public Repository(DbContext _dbContext)
        {
            dbContext = _dbContext;
            dbSet = dbContext.Set<T>();
        }

        public void Add(T _item)
        {
            dbSet.Add(_item);
        }

        public void Remove(T _item)
        {
            dbSet.Remove(_item);
        }

        public void Update(T _item)
        {
            dbSet.Attach(_item);
            dbContext.Entry(_item).State = EntityState.Modified;
        }

        public IQueryable<T> GetAllItems()
        {
            return dbSet;
        }

        public T GetItemById(int _id)
        {
            return dbSet.Find(_id);
        } 

        public IQueryable<T> FilterItems(Expression<Func<T, bool>> _predicate)
        {
            return dbSet.Where(_predicate);
        }
    }
}
