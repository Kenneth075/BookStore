using BookStoreWebApp.Data;
using BookStoreWebApp.IRepositories;
using BookStoreWebApp.Models;
using BookStoreWebApp.Repository;
using System.Linq.Expressions;

namespace BookStoreWebApp.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext _db;

        public CategoryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(Category obj)
        {
            _db.Update(obj);
        }
    }
}
