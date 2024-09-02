using Authorization.Core.Interfaces;
using Authorization.Core.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Authorization.Core.Services
{
    public class JWTService
    {
        public const string JWT_SECURITY_KEY = "UltraSecret7870KEYYANAQOSHIMCHALARNIMADURLAR";
        public const int JWT_TOKEN_VALIDITY_DAYS = 15;
        private readonly IUserRequests _userRequests;

        public JWTService(IUserRequests userRequests)
        {
            _userRequests = userRequests;
        }

        public async Task<AuthenticationResponse>? GenerateToken(AuthenticationRequest request)
        {
            IEnumerable<User> users = await _userRequests.GetAllAsync();

            if (string.IsNullOrWhiteSpace(request.Login) || string.IsNullOrWhiteSpace(request.Password))
                return null;

            /* Validation */
            var userAccount = users
                .Where(x => x.Email == request.Login
                && x.PasswordHash == PasswordHash.ComputeSHA512HashFromString(request.Password))
                .FirstOrDefault();

            if (userAccount == null) return null;

            var tokenExpiryTimeStamp = DateTime.Now.AddDays(JWT_TOKEN_VALIDITY_DAYS);
            var tokenKey = Encoding.ASCII.GetBytes(JWT_SECURITY_KEY);
            var claimsIdentity = new ClaimsIdentity(new List<Claim>
            {
                new (JwtRegisteredClaimNames.Name, request.Login),
            });

            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(tokenKey),
                SecurityAlgorithms.HmacSha256Signature);

            var securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claimsIdentity,
                Expires = tokenExpiryTimeStamp,
                SigningCredentials = signingCredentials
            };

            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var securityToken = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
            var token = jwtSecurityTokenHandler.WriteToken(securityToken);

            return new AuthenticationResponse
            {
                Login = request.Login,
                ExpiresIn = (int)tokenExpiryTimeStamp.Subtract(DateTime.UtcNow).TotalSeconds,
                Token = token
            };
        }
    }
}