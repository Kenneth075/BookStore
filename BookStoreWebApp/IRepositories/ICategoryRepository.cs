using BookStore.Model;
using BookStoreWebApp.IRepository;

namespace BookStoreWebApp.IRepositories
{
    public interface ICategoryRepository : IRepository<Category>
    {
        void Update(Category obj);
        void Save();
        void Add(Category obj);
    }
}
