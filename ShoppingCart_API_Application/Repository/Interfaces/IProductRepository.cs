using ShoppingCart_API_Application.Models;
using System.Linq.Expressions;

namespace ShoppingCart_API_Application.Repository.Interfaces
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAll(Expression<Func<Product,bool>> filter = null);
        Task<Product> Get(Expression<Func<Product,bool>> filter = null,bool tracked=true);
        Task Create(Product product);
        Task Remove(Product product);
        Task Update(Product product);
        Task Save(); 
    }
}
