using BookStoreWebApp.Data;
using BookStoreWebApp.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BookStoreWebApp.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        internal DbSet<T> DbSet;
        public Repository(ApplicationDbContext db)
        {
            _db = db;
            this.DbSet = _db.Set<T>();
            //_db.Categories is as Dbset.
        }

        public void Add(T entity)
        {
            DbSet.Add(entity);
        }

        public T Get(Expression<Func<T, bool>> fitter)
        {
            IQueryable<T> query = DbSet;
            query = query.Where(fitter);
            return query.FirstOrDefault();
        }

        public IEnumerable<T> GetAll()
        {
            IQueryable<T> query = DbSet;
            return query.ToList();
        }

        public void Remove(T entity)
        {
            DbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            DbSet.RemoveRange(entities);
        }
    }
}
