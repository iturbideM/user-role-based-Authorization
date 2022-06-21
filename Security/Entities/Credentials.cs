using System.Security;
namespace Security.Entities
{
    public class Credentials
    {
        public int Id { get; set; }

        protected string? _email;
        public string? Email
        {
            get => GetEmail();
            set => SetEmail(value);
        }

        protected virtual void SetEmail(string? value)
        {
            this._email = value;
        }

        protected virtual string? GetEmail()
        {
            return this._email;
        }

        protected string? _password;
        public string? Password
        {
            get => GetPassword();
            set => SetPassword(value);
        }

        protected virtual void SetPassword(string? value)
        {
            this._password = value;
        }

        protected virtual string? GetPassword()
        {
            return this._password;
        }
    }
}