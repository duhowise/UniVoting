using System.Threading.Tasks;
using UniVoting.Model;

namespace UniVoting.Data.Interfaces;

public interface IComissionerRepository : IRepository<Comissioner>
{
    Task<Comissioner> LoginChairman(Comissioner comissioner);
    Task<Comissioner> LoginAdmin(Comissioner comissioner);
    Task<Comissioner> LoginPresident(Comissioner comissioner);
    Task AddNewConfiguration(Setting setting);
    Task<Comissioner> Login(Comissioner comissioner);
    Setting ConfigureElection();
}
