using BookStoreManagementSystem.Interfaces;
using BookStoreManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace BookStoreManagementSystem.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRL _authRL;
        private readonly ILogger<AuthController> _logger;
        private readonly IConfiguration _configuration;
        
        public AuthController(IAuthRL authRL, IConfiguration configuration, ILogger<AuthController> logger)
        {
            _authRL = authRL;
            _logger = logger;
            _configuration = configuration;
        }


        [HttpPost]
        public async Task<ActionResult> SignUp(SignUpRequest request)
        {
            SignUpResponse response = new SignUpResponse();
            try
            {
                _logger.LogInformation($"SignUp Calling In AdminController.... Time : {DateTime.Now}");
                response = await _authRL.SignUp(request);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
                _logger.LogError("Exception Occur In AuthController : Message : ", ex.Message);
            }

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult> SignIn(SignInRequest request)
        {
            SignInResponse response = new SignInResponse();
            try
            {
                _logger.LogInformation($"SignIn Calling In AdminController.... Time : {DateTime.Now}");
                response = await _authRL.SignIn(request);

            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
                _logger.LogError("Exception Occur In AuthController : Message : ", ex.Message);

            }

            return Ok(response);
        }
    }
}
