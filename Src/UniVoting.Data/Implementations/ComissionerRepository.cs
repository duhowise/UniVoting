using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UniVoting.Data.Interfaces;
using UniVoting.Model;

namespace UniVoting.Data.Implementations
{
    public class ComissionerRepository : IComissionerRepository
    {
        private readonly IDbContextFactory<ElectionDbContext> _dbFactory;

        public ComissionerRepository(IDbContextFactory<ElectionDbContext> dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public async Task<Comissioner> InsertAsync(Comissioner comissioner)
        {
            await using var db = await _dbFactory.CreateDbContextAsync();
            await db.Comissioners.AddAsync(comissioner);
            await db.SaveChangesAsync();
            return comissioner;
        }

        public async Task<Comissioner> UpdateAsync(Comissioner comissioner)
        {
            await using var db = await _dbFactory.CreateDbContextAsync();
            db.Comissioners.Update(comissioner);
            await db.SaveChangesAsync();
            return comissioner;
        }

        public async Task<Comissioner> LoginChairman(Comissioner comissioner)
        {
            await using var db = await _dbFactory.CreateDbContextAsync();
            return await db.Comissioners.FirstOrDefaultAsync(c =>
                c.UserName == comissioner.UserName &&
                c.Password == comissioner.Password &&
                c.IsChairman) ?? new Comissioner();
        }

        public async Task<Comissioner> LoginAdmin(Comissioner comissioner)
        {
            await using var db = await _dbFactory.CreateDbContextAsync();
            return await db.Comissioners.FirstOrDefaultAsync(c =>
                c.UserName == comissioner.UserName &&
                c.Password == comissioner.Password &&
                c.IsAdmin) ?? new Comissioner();
        }

        public async Task<Comissioner> LoginPresident(Comissioner comissioner)
        {
            await using var db = await _dbFactory.CreateDbContextAsync();
            return await db.Comissioners.FirstOrDefaultAsync(c =>
                c.UserName == comissioner.UserName &&
                c.Password == comissioner.Password &&
                c.IsPresident) ?? new Comissioner();
        }

        public async Task AddNewConfiguration(Setting setting)
        {
            await using var db = await _dbFactory.CreateDbContextAsync();
            await db.Settings.Where(s => s.Id == 1).ExecuteUpdateAsync(s => s
                .SetProperty(x => x.ElectionName, setting.ElectionName)
                .SetProperty(x => x.EletionSubTitle, setting.EletionSubTitle)
                .SetProperty(x => x.Logo, setting.Logo)
                .SetProperty(x => x.Colour, setting.Colour));
        }

        public async Task<Comissioner> Login(Comissioner comissioner)
        {
            await using var db = await _dbFactory.CreateDbContextAsync();
            return await db.Comissioners.FirstOrDefaultAsync(c =>
                c.UserName == comissioner.UserName &&
                c.Password == comissioner.Password) ?? new Comissioner();
        }

        public Setting ConfigureElection()
        {
            using var db = _dbFactory.CreateDbContext();
            return db.Settings.FirstOrDefault(s => s.Id == 1) ?? new Setting();
        }
    }
}
