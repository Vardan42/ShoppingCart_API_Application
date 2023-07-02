using Microsoft.EntityFrameworkCore;
using ShoppingCart_API_Application.Data;
using ShoppingCart_API_Application.Models;
using ShoppingCart_API_Application.Repository.Interfaces;
using System.Linq;
using System.Linq.Expressions;

namespace ShoppingCart_API_Application.Repository.Classes
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _db;
        public ProductRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task Create(Product product)
        {
            await  _db.Products.AddAsync(product);
            await Save();
        }
        public async Task<Product> Get(Expression<Func<Product,bool>> filter = null, bool tracked = true)
        {
            IQueryable<Product> query = _db.Products;
            if (!tracked)
            {
                query = query.AsNoTracking();
            }

            if (filter!=null)
            {
                query = query.Where(filter);
            }
            return await query.FirstOrDefaultAsync();
        }
        public async Task<List<Product>> GetAll(Expression<Func<Product,bool>> filter = null)
        {
            IQueryable<Product> query=_db.Products;
            if (filter!=null)
            {
               query= query.Where(filter);
            }
           return await query.ToListAsync();
        }
        public async Task Remove(Product product)
        {
            _db.Products.Remove(product);
            await Save();
        }
        public async Task Save()
        {
            await _db.SaveChangesAsync();
        }
        public async Task Update(Product product)
        {
            _db.Products.Update(product);
            await Save();
        }
    }
}
