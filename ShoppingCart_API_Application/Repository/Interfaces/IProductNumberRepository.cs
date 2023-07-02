using ShoppingCart_API_Application.Models;
using System.Linq.Expressions;

namespace ShoppingCart_API_Application.Repository.Interfaces
{
    public interface IProductNumberRepository
    {
        Task<List<ProductNumber>> GetAll(Expression<Func<ProductNumber, bool>> filter = null);
        Task<ProductNumber> Get(Expression<Func<ProductNumber, bool>> filter = null, bool tracked = true);
        Task Create(ProductNumber entity);
        Task Remove(ProductNumber entity);
        Task Update(ProductNumber entity);
        Task Save();
    }
}
