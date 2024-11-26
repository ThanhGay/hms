using HMS.Auth.ApplicationService.UserModule.Abstracts;
using HMS.Auth.Dtos;
using HMS.Noti.ApplicationService.NotificationModule.Abstracts;
using HMS.Shared.Constant.Permission;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace HMS.WebAPI.Controllers.User
{
    [Route("api/auth")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly INotificationService _notificationService;
        public UserController(IUserService userService, INotificationService notificationService)
        {
            _userService = userService;
            _notificationService = notificationService;
        }

        [HttpPost("/Login")]
        public IActionResult Login([FromQuery] LoginDto input)
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

        [HttpPost("/test-serilog")]
        public IActionResult TestSeriLog()
        {
            try
            {
                bool result = true;
                if (result)
                {
                    Log.Warning("Test serlog");
                }
                return Ok("Success");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("/send-email")]
        public async Task<IActionResult> TestSendEmail(string receptor, string subject, string body)
        {
            try
            {
                await _notificationService.SendEmail(receptor, subject, body);
                return Ok("Thành công");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
