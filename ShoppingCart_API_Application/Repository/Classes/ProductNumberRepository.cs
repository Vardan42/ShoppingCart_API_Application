using Microsoft.EntityFrameworkCore;
using ShoppingCart_API_Application.Data;
using ShoppingCart_API_Application.Models;
using ShoppingCart_API_Application.Repository.Interfaces;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
namespace ShoppingCart_API_Application.Repository.Classes
{
    public class ProductNumberRepository : IProductNumberRepository
    {
        private readonly ApplicationDbContext _db;
        public ProductNumberRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task Create(ProductNumber entity)
        {
            await _db.ProductNumbers.AddAsync(entity);
            await Save();
        }
        public async Task<ProductNumber> Get(Expression<Func<ProductNumber, bool>> filter = null, bool tracked = true)
        {
            IQueryable<ProductNumber> query = _db.ProductNumbers;
            if (!tracked)
            {
                query = query.AsNoTracking();
            }
            if (query != null)
            {
                query = query.Where(filter);
            }
            return await _db.ProductNumbers.FirstOrDefaultAsync();
        }
        public async Task<List<ProductNumber>> GetAll(Expression<Func<ProductNumber, bool>> filter = null)
        {
            IQueryable<ProductNumber> query = _db.ProductNumbers;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            return await query.ToListAsync();
        }
        public async Task Remove(ProductNumber entity)
        {

            _db.ProductNumbers.Remove(entity);
            await Save();
        }

        public async Task Save()
        {
            await _db.SaveChangesAsync();
        }
        public async Task Update(ProductNumber entity)
        {
           _db.ProductNumbers.Update(entity);  
            await Save();
        }
    }
}
