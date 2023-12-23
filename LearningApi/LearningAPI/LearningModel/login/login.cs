using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningAPI.LearningModel.login
{
    public class Login
    {
    
        public class LoginRequest
        {
            public string UserNameOrEmail { get; set; }
            public string Password { get; set; }
        }
        public class RegisterRequest
        {
            public required string CreateFor { get; set; }
            public string? Email { get; set; }
            public required long Mobile { get; set; }
            public string? Password { get; set; }
        }

        public class RegisterResponsewithPassword
        {
            public int registrationid { get; set; }
            public string? firstname { get; set; }
            public long mobile { get; set; }
            public int roleid { get; set; }
            public int isemailvalidated { get; set; }
            public int ismobilevalidated { get; set; }
            public int genderid { get; set; }
            public string? email { get; set; }
            public string? password { get; set; }
            public int boolvalue { get; set; }

        }

        public class RegisterResponse
        {
            public int registrationid { get; set; }
            public int verificationid { get; set; }
            public string? firstname { get; set; }
            public long mobile { get; set; }
            public int roleid { get; set; }
            public int userlogid { get; set; }
            public int isemailvalidated { get; set; }
            public int ismobilevalidated { get; set; }
            public int genderid { get; set; }
            public string? email { get; set; }
            public bool IsSuccess { get; set; }
            public int ResponseCode { get; set; }
            public string ResponseMessage { get; set; }
            public int boolvalue { get; set; }
            public string? JwtToken { get; set; }
            public string? RefreshToken { get; set; }

        }
        public class ResendOTPResponse
        {
            public int registrationid { get; set; }
            public int verificationid { get; set; }
            public string? firstname { get; set; }
            public long mobile { get; set; }
            public int ResponseCode { get; set; }
            public string ResponseMessage { get; set; }

        }
        public class UserResponse
        {
            public int userid { get; set; }
            public int roleid { get; set; }
            public string? username { get; set; }
            public string? email { get; set; }
            public bool IsSuccess { get; set; }
            public int ResponseCode { get; set; }
            public string? ResponseMessage { get; set; }
            public string? JwtToken { get; set; }
            public string? RefreshToken { get; set; }
        }

        public class UserLogRequest
        {
            public int UserLogId { get; set; }
            public int RegistrationId { get; set; }
            public int RoleId { get; set; }

            public int SessionTimeout { get; set; }
        }
        public class UserLogoutResponse
        {
            public int userlogid { get; set; }
            public int roleid { get; set; }
            public string? username { get; set; }
            public string? email { get; set; }
            public bool IsSuccess { get; set; }
            public int ResponseCode { get; set; }
            public string? ResponseMessage { get; set; }
        }
     }

}
