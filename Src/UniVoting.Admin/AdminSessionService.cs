using UniVoting.Model;
using UniVoting.Services;

namespace UniVoting.Admin;

public class AdminSessionService : IAdminSessionService
{
    public Comissioner? CurrentAdmin { get; set; }
}
