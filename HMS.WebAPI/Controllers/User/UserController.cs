using HMS.Auth.ApplicationService.UserModule.Abstracts;
using HMS.Auth.Dtos;
using HMS.Auth.Dtos.Receptionist;
using HMS.Shared.Constant.Permission;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace HMS.WebAPI.Controllers.User
{
    [Route("api/auth")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("/Login")]
        public IActionResult Login([FromBody] LoginDto input)
        {
            try
            {
                
                return Ok(_userService.Login(input));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPost("/logout")]
        public IActionResult LogOut()
        {
            try
            {
                var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                _userService.AddToBlacklist(token);
                return Ok("Đã đăng xuất thành công");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPost("/forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromForm] string email)
        {
            try
            {
                await _userService.ForgotPassword(email);
                return Ok("Đã gửi otp");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [TypeFilter(typeof(AuthorizationFilter), Arguments = new object[] { PermissionKeys.GetAllFunctionCustomer })]
        [HttpGet("/get-all-function-customer")]
        public IActionResult GetFunctionCustomer()
        {
            try
            {
                return Ok(_userService.GetFunctionCustomer());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [TypeFilter(typeof(AuthorizationFilter), Arguments = new object[] { PermissionKeys.GetAllFunctionReceptionist })]
        [HttpGet("/get-all-function-receptionist")]
        public IActionResult GetFunctionReceptionist()
        {
            try
            {
                return Ok(_userService.GetFunctionReceptionist());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [TypeFilter(typeof(AuthorizationFilter), Arguments = new object[] { PermissionKeys.GetAllFunctionManager })]
        [HttpGet("/get-all-function-manager")]
        public IActionResult GetFunctionManager()
        {
            try
            {
                return Ok(_userService.GetFunctionManager());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [Authorize]
        [HttpPut("/update-password")]
        public IActionResult UpdatePassword([FromBody] UpdatePassWordDto input)
        {
            try
            {
                _userService.ResetPassword(input);
                return Ok("Đã đổi mật khẩu thành công");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
