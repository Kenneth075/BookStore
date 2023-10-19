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
            //_db.Products.Update(obj);
            var objFromDb = _db.Products.FirstOrDefault(u=>u.Id == obj.Id);
            if (objFromDb != null)
            {
                objFromDb.Title = obj.Title;
                objFromDb.Description = obj.Description;
                objFromDb.Category = obj.Category;
                objFromDb.Price = obj.Price;
                objFromDb.ListPrice = obj.ListPrice;
                objFromDb.Price100 = obj.Price100;
                objFromDb.Price50 = obj.Price50;
                objFromDb.ISBN = obj.ISBN;
                objFromDb.Author = obj.Author;
                
                if(obj.ImageUrl != null )
                {
                    objFromDb.ImageUrl = obj.ImageUrl;
                }

            }
        }
    }
}
