using HMS.Auth.ApplicationService.UserModule.Abstracts;
using HMS.Auth.Dtos;
using HMS.Shared.Constant.Permission;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
    }
}
