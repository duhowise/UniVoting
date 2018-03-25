using System;
using System.Threading.Tasks;
using Dapper;
using UniVoting.Model;

namespace UniVoting.Data.Implementations
{
	public class ComissionerRepository:Repository<Comissioner>
	{

		public ComissionerRepository():base("VotingSystem")
		{
			
		}
		public  async Task<Comissioner> LoginChairman(Comissioner comissioner )
		{
			using (var connection = new DbManager(ConnectionName).Connection)
			{
				return await connection.QueryFirstOrDefaultAsync<Comissioner>(@"select * FROM Comissioner c  WHERE   c.Username=@Username AND c.Password=@Password AND c.IsChairman=1", comissioner);
			}
		}
		public  async Task<Comissioner> LoginAdmin(Comissioner comissioner)
		{
			using (var connection = new DbManager(ConnectionName).Connection)
			{
				return await connection.QueryFirstOrDefaultAsync<Comissioner>(@"select * FROM Comissioner c  WHERE   c.Username=@Username AND c.Password=@Password  AND c.isAdmin=1", comissioner);
			}
		}
		public  async Task<Comissioner> LoginPresident(Comissioner comissioner)
		{
			using (var connection = new DbManager(ConnectionName).Connection)
			{
				return await connection.QueryFirstOrDefaultAsync<Comissioner>(@"select * FROM Comissioner c  WHERE   c.Username=@Username AND c.Password=@Password  AND c.IsPresident =1", comissioner);
			}
		}
		public  async Task AddNewConfiguration(Setting setting)
		{
			using (var connection = new DbManager(ConnectionName).Connection)
			{

				await connection.ExecuteAsync(@"UPDATE Settings SET  ElectionName = @ElectionName ,EletionSubTitle = @EletionSubTitle ,logo = @logo  ,Colour = @Colour WHERE  id = 1", setting);
			}

		}
		public  async Task<Comissioner> Login(Comissioner comissioner )
		{
			using (var connection = new DbManager(ConnectionName).Connection)
			{
				return await connection.QueryFirstOrDefaultAsync<Comissioner>(@"select * FROM Comissioner c  WHERE   c.Username=@Username AND c.Password=@Password", comissioner);
			}
		}
		public virtual Setting ConfigureElection()
		{
			try
			{
				using (var connection = new DbManager(ConnectionName).Connection)
				{
					return connection.QueryFirstOrDefault<Setting>(@"SELECT  s.id ,s.ElectionName ,s.EletionSubTitle ,s.logo,s.Colour FROM Settings s WHERE s.id=1");

				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}


		}
	}
}