using LearningAPI.JWT;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningAPI.JWT
{
    public class TokenRefresher : ITokenRefresher
    {
        private readonly string key;
        private readonly IJwtAuthenticationManager jwtAuthenticationManager;
        public TokenRefresher(string key, IJwtAuthenticationManager jwtAuthenticationManager)
        {
            this.key = key;
            this.jwtAuthenticationManager = jwtAuthenticationManager;
        }

        public AuthenticationResponse Refresh(RefreshCred refreshCred)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken validatedToken;
            var principal = tokenHandler.ValidateToken(refreshCred.JwtToken,
                new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                }, out validatedToken);
            var jwtToken = validatedToken as JwtSecurityToken;

            if (jwtToken == null || !jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token passed,Please reverify jwttoken and refresh token");
            }
            var userName = principal.Identity.Name;

            if (refreshCred.RefreshToken != jwtAuthenticationManager.UserRefreshTokens[userName])
            {
                throw new SecurityTokenException("Invalid token passed, Please reverify jwttoken and refresh token");
            }

            return jwtAuthenticationManager.Authenticate(userName, principal.Claims.ToArray());

        }
    }
}
