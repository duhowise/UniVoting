using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace UniVoting.Data.Implementations
{
    public abstract class Repository<T> : Interfaces.IRepository<T> where T : class
    {
        protected readonly IDbContextFactory<ElectionDbContext> DbFactory;

        protected Repository(IDbContextFactory<ElectionDbContext> dbFactory)
        {
            DbFactory = dbFactory;
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            await using var db = await DbFactory.CreateDbContextAsync();
            return await db.Set<T>().ToListAsync();
        }

        public virtual IEnumerable<T> GetAll()
        {
            using var db = DbFactory.CreateDbContext();
            return db.Set<T>().ToList();
        }

        public virtual T Insert(T member)
        {
            using var db = DbFactory.CreateDbContext();
            db.Set<T>().Add(member);
            db.SaveChanges();
            return member;
        }

        public virtual async Task<T> InsertAsync(T member)
        {
            await using var db = await DbFactory.CreateDbContextAsync();
            await db.Set<T>().AddAsync(member);
            await db.SaveChangesAsync();
            return member;
        }

        public virtual T GetById(int id)
        {
            using var db = DbFactory.CreateDbContext();
            return db.Set<T>().Find(id)!;
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            await using var db = await DbFactory.CreateDbContextAsync();
            return (await db.Set<T>().FindAsync(id))!;
        }

        public virtual T Update(T member)
        {
            using var db = DbFactory.CreateDbContext();
            db.Set<T>().Update(member);
            db.SaveChanges();
            return member;
        }

        public virtual async Task<T> UpdateAsync(T member)
        {
            await using var db = await DbFactory.CreateDbContextAsync();
            db.Set<T>().Update(member);
            await db.SaveChangesAsync();
            return member;
        }

        public virtual void Delete(T member)
        {
            using var db = DbFactory.CreateDbContext();
            db.Set<T>().Remove(member);
            db.SaveChanges();
        }

        public virtual async Task DeleteAsync(T member)
        {
            await using var db = await DbFactory.CreateDbContextAsync();
            db.Set<T>().Remove(member);
            await db.SaveChangesAsync();
        }
    }
}
