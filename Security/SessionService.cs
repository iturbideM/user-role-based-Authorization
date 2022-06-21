using System.Diagnostics;
using Security.Entities;
using DataAccessInterface;
using Exceptions;

namespace Security
{
    public class SessionService : ISessionService
    {
        private readonly IRepository<Credentials> _credentialsRepository;
        private readonly IRepository<RolePermission> _rolPermissionRepository;
        private readonly IRepository<CredentialsRole> _credentialsRoleRepository;

        private int? currentUserId;

        public SessionService(IRepository<Credentials> credentialsRepository, IRepository<RolePermission> rolPermissionRepository, IRepository<CredentialsRole> credentialsRoleRepository)
        {
            this._credentialsRepository = credentialsRepository;
            this._rolPermissionRepository = rolPermissionRepository;
            this._credentialsRoleRepository = credentialsRoleRepository;
        }

        public string Auth(Credentials credentials)
        {
            var credential = _credentialsRepository.Get(c => c.Email.Equals(credentials.Email)
                                                        && c.Password.Equals(credentials.Password));
            if (credential == null)
                throw new NotFoundException("A user does not exists. Wrong email and/or password.");

            return new JwtUtils().GenerateJwtToken(credential);
        }

        public int? ValidateToken(string token)
        {
            var userId = new JwtUtils().ValidateJwtToken(token);
            var user = _credentialsRepository.Get(c => c.Id == userId);
            if (user != null)
                SetCurrentUserId(userId);

            return userId;
        }

        public bool HasPermission(int userId, Permission permission)
        {
            var roles = this._credentialsRoleRepository.GetAll(x => x.UserId == userId);

            foreach (var rol in roles)
            {
                if (this._rolPermissionRepository.Get(x => x.RoleId == rol.RoleId && x.Permission == permission) != null)
                    return true;
            }

            return false;
        }

        private void SetCurrentUserId(int? userId)
        {
            this.currentUserId = userId;
        }

        public Credentials GetCurrentUser()
        {
            return this._credentialsRepository.Get(x => x.Id == this.currentUserId);
        }
    }
}