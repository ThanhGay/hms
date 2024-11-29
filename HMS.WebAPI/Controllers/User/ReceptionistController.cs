using HMS.Auth.ApplicationService.Common;
using HMS.Auth.ApplicationService.UserModule.Abstracts;
using HMS.Auth.ApplicationService.UserModule.Implements;
using HMS.Auth.Dtos.Receptionist;
using HMS.Shared.Constant.Permission;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HMS.WebAPI.Controllers.User
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReceptionistController : ControllerBase
    {
        private readonly IReceptionistService _receptionistService;
        private readonly IHttpContextAccessor _contextAccessor;
        public ReceptionistController(IReceptionistService receptionistService, IHttpContextAccessor contextAccessor)
        {
            _receptionistService = receptionistService;
            _contextAccessor = contextAccessor;
        }

        [Authorize]
        [TypeFilter(typeof(AuthorizationFilter), Arguments = new object[] { PermissionKeys.AddReceptionist })]
        [HttpPost("/add-receptionist")]
        public IActionResult AddReceptionist(AddReceptionistDto input)
        {
            try
            {
                return Ok(_receptionistService.CreateReceptionist(input));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [TypeFilter(typeof(AuthorizationFilter), Arguments = new object[] { PermissionKeys.GetReceptionistById })]
        [HttpGet("/get-receptionist-by-id")]
        public IActionResult GetReceptionistById([FromForm] int receptionistId)
        {
            try
            {
                return Ok(_receptionistService.GetReceptionistById(receptionistId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [TypeFilter(typeof(AuthorizationFilter), Arguments = new object[] { PermissionKeys.GetAllReceptionist })]
        [HttpGet("/get-all-receptionist")]
        public IActionResult GetAllReceptionist([FromQuery] FilterDto input)
        {
            try
            {
                return Ok(_receptionistService.GetAllReceptionist(input));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [TypeFilter(typeof(AuthorizationFilter), Arguments = new object[] { PermissionKeys.UpdateInfReceptionist })]
        [HttpPut("/update-information-receptionist")]
        public IActionResult UpdateInformationReceptionist([FromBody] UpdateReceptionistDto input)
        {
            try
            {
                return Ok(_receptionistService.UpdateInfReceptionist(input));
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [TypeFilter(typeof(AuthorizationFilter), Arguments = new object[] { PermissionKeys.DeleteReceptionist })]
        [HttpDelete("/delete-receptionist")]
        public IActionResult Delete(int receptionistId)
        {
            try
            {
                _receptionistService.DeleteReceptionist(receptionistId);
                return Ok("Đã xóa thành công");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
