using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Transactions;
using Dapper;
using UniVoting.Data.Interfaces;
using UniVoting.Model;

namespace UniVoting.Data.Implementations
{
	public abstract class Repository<T> :IRepository<T> where T: class 
	{
		protected DbManager _dbManager;

		protected Repository(DbManager dbManager)
		{
			_dbManager = dbManager;
		}
		public virtual async Task<IEnumerable<T>> GetAllAsync()
		{
			using (var connection = new DbManager("VotingSystem").Connection)
			{
				return await connection.GetListAsync<T>();
			}

		}
		public virtual  IEnumerable<T> GetAll()
		{
			using (var connection = new DbManager("VotingSystem").Connection)
			{
				return connection.GetList<T>();
			}

		}
		public virtual T Insert(T member)
		{
			using (var connection = new DbManager("VotingSystem").Connection)
			{
				var insert = Convert.ToInt32(connection.Insert(member));
				return GetById(insert);
			}
		}

		public virtual async Task<T> InsertAsync(T member)
		{
			using (var connection = new DbManager("VotingSystem").Connection)
			{
				var insert = Convert.ToInt32(await connection.InsertAsync(member));
				return await GetByIdAsync(insert);
			}

		}
		
		public  T GetById(int member)
		{
			using (var connection = new DbManager("VotingSystem").Connection)
			{
				return  connection.Get<T>(member);
			}
		}
		public  virtual async Task<T> GetByIdAsync(int member)
		{
			using (var connection = new DbManager("VotingSystem").Connection)
			{
				return await connection.GetAsync<T>(member);
			}
		}

		public virtual T Update(T member)
		{
			throw new NotImplementedException();
		}

		public virtual async Task<T> UpdateAsync(T member)
		{
			using (var connection = new DbManager("VotingSystem").Connection)
			{
			return	await GetByIdAsync(Convert.ToInt32(await connection.UpdateAsync(member)));
			}
		}

		
		public virtual void Delete(T member)
		{
			using (var connection = new DbManager("VotingSystem").Connection)
			{
				connection.Delete(member);
			}
		}

		public virtual async Task DeleteAsync(T member)
		{
			using (var connection = new DbManager("VotingSystem").Connection)
			{
			await	connection.DeleteAsync(member);
			}
		}
	}

	
}