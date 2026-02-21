using UniVoting.Model;

namespace UniVoting.Admin;

public interface IAdminSessionService
{
    Comissioner? CurrentAdmin { get; set; }
}

public class AdminSessionService : IAdminSessionService
{
    public Comissioner? CurrentAdmin { get; set; }
}
