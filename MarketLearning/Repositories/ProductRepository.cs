using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web;
using MarketLearning.Models;
using MarketLearning.Models.Entities;

namespace MarketLearning.Repositories
{
    public class ProductRepository : IRepository<Product>
    {
        private ApplicationDbContext db = null;

        public ProductRepository()
        {
            db = new ApplicationDbContext();
        }

        public ProductRepository(ApplicationDbContext _db)
        {
            db = _db;
        }

        public async Task<List<Product>> GetAllAsync()
        {
            return await db.Products.ToListAsync();
        }

        public Product GetByID(int? id)
        {
            Product program = db.Products.Find(id);
            return program;
        }

        public async Task<Product> GetByIDAsync(int? id)
        {
            Product program = await db.Products.FindAsync(id);
            return program;
        }

        public async Task AddAsync(Product program)
        {
            db.Products.Add(program);
            await db.SaveChangesAsync();
        }

        public async Task RemoveAsync(int? id)
        {
            Product existing = await db.Products.FindAsync(id);
            db.Products.Remove(existing);
            await db.SaveChangesAsync();
        }

        public async Task<List<Product>> GetByPropertyAsync(Expression<Func<Product, bool>> expression)
        {
            return await db.Products.Where(expression).ToListAsync();
        }

        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    db.Dispose();
                }

                disposedValue = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async Task UpdateStateAsync(Product program)
        {
            db.Entry(program).State = EntityState.Modified;
            await db.SaveChangesAsync();
        }
    }
}