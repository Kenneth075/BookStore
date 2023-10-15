using BookStoreWebApp.IRepository;
using BookStoreWebApp.Models;

namespace BookStoreWebApp.IRepositories
{
    public interface ICategoryRepository : IRepository<Category>
    {
        void Update(Category obj);
        void Save();
    }
}
