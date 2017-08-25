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
    public class ErrorRepository : IRepository<Error>, IDisposable
    {

            private ApplicationDbContext db = null;

            public ErrorRepository()
            {
                db = new ApplicationDbContext();
            }

            public ErrorRepository(ApplicationDbContext _db)
            {
                db = _db;
            }

            public async Task<List<Error>> GetAllAsync()
            {

                return await db.Errors.ToListAsync();
            }

            public async Task<Error> GetByIDAsync(int? id)
            {
                Error error = await db.Errors.FindAsync(id);
                return error;
            }

            public async Task AddAsync(Error obj)
            {
                db.Errors.Add(obj);
                await db.SaveChangesAsync();
            }

            public async Task RemoveAsync(int? id)
            {
                Error existing = await db.Errors.FindAsync(id);
                db.Errors.Remove(existing);
                await db.SaveChangesAsync();
            }

            public async Task<List<Error>> GetByPropertyAsync(Expression<Func<Error, bool>> expression)
            {
                return await db.Errors.Where(expression).ToListAsync();
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

            public async Task UpdateStateAsync(Error error)
            {
                db.Entry(error).State = EntityState.Modified;
                await db.SaveChangesAsync();
            }
    }
}