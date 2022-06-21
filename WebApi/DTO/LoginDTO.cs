using Security.Entities;

namespace WebApi.DTO
{
    public class LoginDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
        
        public Credentials CreateCredentials( )
        {
            return new Credentials(){
                Email = this.Email,
                Password = this.Password
            };
        }
    }
}