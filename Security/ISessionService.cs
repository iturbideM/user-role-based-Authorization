using Security.Entities;

namespace Security
{
    public interface ISessionService
    {
        string Auth(Credentials credentials);
        int? ValidateToken(string token);
        bool HasPermission(int userId, Permission permission);
        Credentials GetCurrentUser();
    }
}