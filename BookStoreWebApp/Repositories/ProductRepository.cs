using BookStore.DataAccess.Data;
using BookStore.Model;
using BookStoreWebApp.IRepositories;
using BookStoreWebApp.Repository;
using System.Linq.Expressions;

namespace BookStoreWebApp.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _db;

        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(Product obj)
        {
            _db.Products.Update(obj);
        }
    }
}
