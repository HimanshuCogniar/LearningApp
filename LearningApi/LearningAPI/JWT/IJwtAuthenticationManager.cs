
using LearningAPI.JWT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LearningAPI.JWT
{
    public interface IJwtAuthenticationManager
    {

        AuthenticationResponse Authenticate(string username, Claim[] claims);
        public string GenerateTokenString(string loginName, DateTime expires, Claim[] claims = null);
        IDictionary<string, string> UserRefreshTokens { get; set; }
        //Task DeactivateAsync(string token);

    }
}
