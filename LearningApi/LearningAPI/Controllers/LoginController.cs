using Microsoft.AspNetCore.Mvc;
using LearningAPI.Repositories;
using static LearningAPI.LearningModel.login.Login;
using LearningAPI.JWT;
using System.Net;
namespace LearningAPI.Controllers
{
    [Route("api/{v:apiVersion}/[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
        private readonly IGenericRepository<LoginRequest> _repository;
        private readonly ILogger<LoginController> _seriLogger;
        private readonly IConfiguration _configuration;
        private string myConnectionString = string.Empty;
        public readonly ITokenRefresher _tokenRefresher;
        public readonly IJwtAuthenticationManager _jwtAuthenticationManager;
        public readonly IRefreshTokenGenerator _refreshTokenGenerator;
        public LoginController(IGenericRepository<LoginRequest> repository, IJwtAuthenticationManager jwtAuthenticationManager, ITokenRefresher tokenRefresher, IRefreshTokenGenerator refreshTokenGenerator, ILogger<LoginController> seriLogger, IConfiguration configuration)
        {
            _repository = repository;
            _seriLogger = seriLogger;
            _configuration = configuration;
            myConnectionString = _configuration["ConnectionStrings:DBConnectionStringDEV"];
            _jwtAuthenticationManager = jwtAuthenticationManager;
            _refreshTokenGenerator = refreshTokenGenerator;
            _tokenRefresher = tokenRefresher;
        }

        [HttpPost]
        [Route("LoginUser")]
        [ProducesResponseType(typeof(IAsyncEnumerable<dynamic>), (int)HttpStatusCode.OK)] // Use typeof(IEnumerable<dynamic>) to specify the response type
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<IEnumerable<RegisterResponse>>> LoginUser([FromBody] LoginRequest login)
        {
            try
            {
               // CustomSeriloggerConfiguration.LogHeaderInfo("Login", this.HttpContext.Request, this.HttpContext.Connection.RemoteIpAddress, _seriLogger);
                _seriLogger.LogInformation("Calling Login Method of LoginController");

                var sqlLogin = _configuration["Queries:Login"];
                var userData = await Task.Run(() => _repository.FetchDataAsync<dynamic, dynamic>(
                    sqlLogin,
                    new { UserNameOrEmail = login.UserNameOrEmail, Email = login.UserNameOrEmail }, myConnectionString));

                if (userData.Count > 0 && BCrypt.Net.BCrypt.Verify(login.Password, userData[0].PasswordHash))
                {
                    // Authentication successful
                    return Ok("Login successful");
                }
                else
                {
                    // Authentication failed
                    return BadRequest("Invalid username/email or password");
                }
            }
            catch (Exception ex)
            {
                _seriLogger.LogError("Exception while running LoginUser: {id}", ex.Message);
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}
