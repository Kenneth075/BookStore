using System.Linq.Expressions;

namespace BookStoreWebApp.IRepository
{
    public interface IRepository<T> where T : class
    {
        //T-Category
        IEnumerable<T> GetAll();
        T Get(Expression<Func<T, bool>> fitter);
        void Add(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
    }
}
