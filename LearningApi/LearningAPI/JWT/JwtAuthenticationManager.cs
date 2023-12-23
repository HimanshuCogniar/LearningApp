using Microsoft.Extensions.Caching.Distributed;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace LearningAPI.JWT
{
    public class JwtAuthenticationManager : IJwtAuthenticationManager
    {

        private readonly IConfiguration _configuration;
        private readonly IDistributedCache cache;
        private readonly string _key;
        private readonly IRefreshTokenGenerator _refreshTokenGenerator;
        private IConfiguration? configuration;
        private string? value;
        private ITokenRefresher? tokenRefresher;

        //private readonly IOptions<> _jwtoptions;

        public IDictionary<string, string> UserRefreshTokens { get; set; }
        public JwtAuthenticationManager(IConfiguration configuration, string keys, IRefreshTokenGenerator refreshTokenGenerator)
        {
            //cache= _cache;
            _configuration = configuration;
            _key = keys;
            _refreshTokenGenerator = refreshTokenGenerator;
            UserRefreshTokens = new Dictionary<string, string>();
        }

        public JwtAuthenticationManager(IConfiguration? configuration, string? value, ITokenRefresher? tokenRefresher)
        {
            this.configuration = configuration;
            this.value = value;
            this.tokenRefresher = tokenRefresher;
        }

        public AuthenticationResponse Authenticate(string username, Claim[] claims)
        {
            var token = GenerateTokenString(username, DateTime.UtcNow, claims);
            var refreshtoken = _refreshTokenGenerator.GenerateRefreshToken();
            if (UserRefreshTokens.ContainsKey(username))
            {
                UserRefreshTokens[username] = refreshtoken;
            }
            else
            {
                UserRefreshTokens.Add(username, refreshtoken);
            }

            return new AuthenticationResponse { JwtToken = token, RefreshToken = refreshtoken };

        }

        public string GenerateTokenString(string loginName, DateTime expires, Claim[] claims = null)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenkey = Encoding.ASCII.GetBytes(_key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                   claims ?? new Claim[]
                {
                        new Claim(ClaimTypes.Name, loginName)
                }),
                Expires = DateTime.UtcNow.AddMinutes(Convert.ToInt32(_configuration["JWT:SessionTimeOut"])),
                SigningCredentials =
                new SigningCredentials(
                    new SymmetricSecurityKey(tokenkey),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var refreshtoken = _refreshTokenGenerator.GenerateRefreshToken();
            return tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
        }

        //public async Task DeactivateAsync(string token)  => await cache.SetStringAsync(GetKey(token),
        //    " ", new DistributedCacheEntryOptions
        //    {
        //        AbsoluteExpirationRelativeToNow =
        //            TimeSpan.FromMinutes(_jwtoptions.Value.)
        //    });

        private static string GetKey(string token) => $"tokens:{token}:deactivated";
    }
}