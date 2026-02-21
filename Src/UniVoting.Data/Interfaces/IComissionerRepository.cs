using System.Threading.Tasks;
using UniVoting.Model;

namespace UniVoting.Data.Interfaces;

public interface IComissionerRepository
{
    Task<Comissioner> InsertAsync(Comissioner comissioner);
    Task<Comissioner> UpdateAsync(Comissioner comissioner);
    Task<Comissioner> LoginChairman(Comissioner comissioner);
    Task<Comissioner> LoginAdmin(Comissioner comissioner);
    Task<Comissioner> LoginPresident(Comissioner comissioner);
    Task AddNewConfiguration(Setting setting);
    Task<Comissioner> Login(Comissioner comissioner);
    Task<bool> ResetPasswordAsync(string username, string fullName, string newPassword);
    Setting ConfigureElection();
}
