using BookStore.Model;
using BookStoreWebApp.IRepository;

namespace BookStoreWebApp.IRepositories
{
    public interface IProductRepository : IRepository<Product>
    {
        void Update(Product obj);
        void Save();
        void Add(Product obj);
    }
}
