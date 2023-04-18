using Authentication.Business.Users;
using Authentication.Domain.DTOs.User;
using Authentication.Shared;
using Azure;
using Microsoft.AspNetCore.Mvc;

namespace Authentication.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        #region Properties

        private readonly IUserService _userService;

        private readonly ILogger<UserController> _logger;

        #endregion

        #region Constructor

        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        #endregion

        #region Actions

        [HttpPost("register")]
        public async Task<ActionResult<ServiceResponse<int>>> Register(UserCreateDto user)
        {
            _logger.LogInformation("Request for Register received!");

            var response = await _userService.Register(
                new UserCreateDto
                {
                    Email = user.Email,
                },
                user.Password);

            if (!response.Success)
            {
                _logger.LogError("Something went wrong while trying to register user.");

                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPost("changePassword")]
        public async Task<ActionResult<bool>> ChangePassword(UserChangePasswordDto userRequest)
        {
            _logger.LogInformation("Request for ChangePassword received!");

            var response = await _userService.ChangePassword(userRequest);

            if (!response.Success)
            {
                _logger.LogError("Something went wrong while trying to register user.");

                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<ActionResult<ServiceResponse<string>>> Login(UserLoginDto request) 
        {
            var response = await _userService.Login(request.Email, request.Password);
            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }

        #endregion
    }
}
