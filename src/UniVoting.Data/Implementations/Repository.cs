using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniVoting.Data.Interfaces;

namespace UniVoting.Data.Implementations
{
	public abstract class Repository<T> : IRepository<T> where T : class 
	{
		protected readonly VotingDbContext _context;

		protected Repository(VotingDbContext context)
		{
			_context = context;
		}

		public virtual async Task<IEnumerable<T>> GetAllAsync()
		{
			return await _context.Set<T>().ToListAsync();
		}

		public virtual IEnumerable<T> GetAll()
		{
			return _context.Set<T>().ToList();
		}

		public virtual T Insert(T member)
		{
			_context.Set<T>().Add(member);
			_context.SaveChanges();
			return member;
		}

		public virtual async Task<T> InsertAsync(T member)
		{
			await _context.Set<T>().AddAsync(member);
			await _context.SaveChangesAsync();
			return member;
		}
		
		public T GetById(int id)
		{
			return _context.Set<T>().Find(id);
		}

		public virtual async Task<T> GetByIdAsync(int id)
		{
			return await _context.Set<T>().FindAsync(id);
		}

		public virtual T Update(T member)
		{
			_context.Set<T>().Update(member);
			_context.SaveChanges();
			return member;
		}

		public virtual async Task<T> UpdateAsync(T member)
		{
			_context.Set<T>().Update(member);
			await _context.SaveChangesAsync();
			return member;
		}

		public virtual void Delete(T member)
		{
			_context.Set<T>().Remove(member);
			_context.SaveChanges();
		}

		public virtual async Task DeleteAsync(T member)
		{
			_context.Set<T>().Remove(member);
			await _context.SaveChangesAsync();
		}
	}
}