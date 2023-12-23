using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningAPI.JWT
{
    public class AuthenticationResponse
    {
        public string? JwtToken { get; set; }
        public string? RefreshToken { get; set; }

        public int ResponseCode { get; set; }
        public string? ResponseMessage { get; set; }
    }
}
