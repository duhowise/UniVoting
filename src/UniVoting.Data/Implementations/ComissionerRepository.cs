using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using UniVoting.Model;

namespace UniVoting.Data.Implementations
{
	public class ComissionerRepository
	{
		private readonly VotingDbContext _context;

		public ComissionerRepository(VotingDbContext context)
		{
			_context = context;
		}

		public async Task<Comissioner> LoginChairman(Comissioner comissioner)
		{
			return await _context.Comissioners
				.FirstOrDefaultAsync(c => c.UserName == comissioner.UserName 
										&& c.Password == comissioner.Password 
										&& c.IsChairman);
		}

		public async Task<Comissioner> LoginAdmin(Comissioner comissioner)
		{
			return await _context.Comissioners
				.FirstOrDefaultAsync(c => c.UserName == comissioner.UserName 
										&& c.Password == comissioner.Password 
										&& c.IsAdmin);
		}

		public async Task<Comissioner> LoginPresident(Comissioner comissioner)
		{
			return await _context.Comissioners
				.FirstOrDefaultAsync(c => c.UserName == comissioner.UserName 
										&& c.Password == comissioner.Password 
										&& c.IsPresident);
		}

		public async Task<Comissioner> Login(Comissioner comissioner)
		{
			return await _context.Comissioners
				.FirstOrDefaultAsync(c => c.UserName == comissioner.UserName 
										&& c.Password == comissioner.Password);
		}

		public async Task AddNewConfiguration(Setting setting)
		{
			var existingSetting = await _context.Settings.FirstOrDefaultAsync(s => s.Id == 1);
			if (existingSetting != null)
			{
				existingSetting.ElectionName = setting.ElectionName;
				existingSetting.EletionSubTitle = setting.EletionSubTitle;
				existingSetting.Logo = setting.Logo;
				existingSetting.Colour = setting.Colour;
				await _context.SaveChangesAsync();
			}
		}

		public Setting ConfigureElection()
		{
			return _context.Settings.FirstOrDefault(s => s.Id == 1);
		}

		public async Task<Comissioner> AddAsync(Comissioner comissioner)
		{
			_context.Comissioners.Add(comissioner);
			await _context.SaveChangesAsync();
			return comissioner;
		}

		public async Task<Comissioner> UpdateAsync(Comissioner comissioner)
		{
			_context.Comissioners.Update(comissioner);
			await _context.SaveChangesAsync();
			return comissioner;
		}

		public async Task DeleteAsync(Comissioner comissioner)
		{
			_context.Comissioners.Remove(comissioner);
			await _context.SaveChangesAsync();
		}
	}
}