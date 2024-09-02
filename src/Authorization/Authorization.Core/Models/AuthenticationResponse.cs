namespace Authorization.Core.Models
{
    public class AuthenticationResponse
    {
        public string Login { get; set; } = string.Empty;

        public string Token { get; set; } = string.Empty;

        public int ExpiresIn { get; set; }
    }
}