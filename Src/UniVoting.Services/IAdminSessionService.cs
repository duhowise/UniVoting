using UniVoting.Model;

namespace UniVoting.Services;

public interface IAdminSessionService
{
    Comissioner? CurrentAdmin { get; set; }
}
