namespace Authorization.Core.Models
{
    public class AuthenticationRequest
    {
        public string Login { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;
    }
}