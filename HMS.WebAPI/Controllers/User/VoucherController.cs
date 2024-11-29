using HMS.Auth.ApplicationService.Common;
using HMS.Auth.ApplicationService.UserModule.Abstracts;
using HMS.Auth.Dtos.Voucher;
using HMS.Shared.Constant.Permission;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HMS.WebAPI.Controllers.User
{
    [Route("api/[controller]")]
    [ApiController]
    public class VoucherController : ControllerBase
    {
        private readonly IVoucherService _voucherService;
        public VoucherController(IVoucherService voucherService)
        {
            _voucherService = voucherService;
        }
        [Authorize]
        [TypeFilter(typeof(AuthorizationFilter), Arguments = new object[] { PermissionKeys.CreateVoucher } )]
        [HttpPost("/create-voucher")]
        public IActionResult CreateVoucher([FromBody] CreateVoucherDto input)
        {
            try
            {
                return Ok(_voucherService.CreateVoucher(input));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [TypeFilter(typeof(AuthorizationFilter), Arguments = new object[] {PermissionKeys.SetVoucherToCustomer})]
        [HttpPost("/set-voucher-to-customer")]
        public IActionResult SetVoucherCustomer([FromForm] int customerId, [FromForm] int voucherId)
        {
            try
            {
                _voucherService.SetVoucherToCustomer(voucherId, customerId);
                return Ok("Thành công");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [TypeFilter(typeof(AuthorizationFilter), Arguments = new object[] { PermissionKeys.GetVoucherById})]
        [HttpGet("/get-voucher-by-id")]
        public IActionResult GetVoucherById([FromQuery] int voucherId)
        {
            try
            {
                return Ok(_voucherService.GetVoucherById(voucherId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [TypeFilter(typeof(AuthorizationFilter), Arguments = new object[] { PermissionKeys.GetAllVoucher})]
        [HttpGet("/get-all-voucher")]
        public IActionResult GetAllVoucher([FromQuery] FilterDto input)
        {
            try
            {
                return Ok(_voucherService.GetAllVoucher(input));
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [TypeFilter(typeof(AuthorizationFilter), Arguments = new object[] { PermissionKeys.UpdateVoucher})]
        [HttpPut("/update-voucher")]
        public IActionResult UpdateVoucher([FromBody] UpdateVoucherDto input)
        {
            try
            {
                return Ok(_voucherService.UpdateVoucher(input));
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [TypeFilter(typeof (AuthorizationFilter), Arguments = new object[] { PermissionKeys.DeleteVoucher})]
        [HttpDelete("/delete-voucher")]
        public IActionResult DeleteVoucher(int voucherId)
        {
            try
            {
                _voucherService.DeleteVoucher(voucherId);
                return Ok("Đã xóa voucher thành công");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
