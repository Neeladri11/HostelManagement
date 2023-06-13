using HostelManagement.DAL.Data;
using HostelManagement.DAL.DataAccess.Interface;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HostelManagement.DAL.DataAccess
{
    public class Repo<T> : IRepo<T> where T : class
    {
        private readonly ApplicationDbContext _db;

        public DbSet<T> DbSet { get; set; }

        public Repo(ApplicationDbContext db)
        {
            _db = db;
            DbSet = db.Set<T>();
        }

        public void AddAsync(T entity)
        {
            DbSet.AddAsync(entity);
            _db.SaveChanges();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            IQueryable<T> query = DbSet;
            return await query.ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllListAsync(Expression<Func<T, bool>> filter)
        {
            IQueryable<T> query = DbSet;
            query = query.Where(filter);
            return await query.ToListAsync();
        }

        public async Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> filter)
        {
            IQueryable<T> query = DbSet;
            query = query.Where(filter);
            return await query.FirstOrDefaultAsync();
        }

        public void Remove(T entity)
        {
            DbSet.Remove(entity);
            _db.SaveChanges();
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            DbSet.RemoveRange(entities);
        }

        public void UpdateExisting(T entity)
        {
            DbSet.Update(entity);
            _db.SaveChanges();
        }
    }
}
