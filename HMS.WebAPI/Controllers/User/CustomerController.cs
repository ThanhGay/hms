using HMS.Auth.ApplicationService.Common;
using HMS.Auth.ApplicationService.UserModule.Abstracts;
using HMS.Auth.Dtos.Customer;
using HMS.Shared.Constant.Permission;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HMS.WebAPI.Controllers.User
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        public CustomerController(ICustomerService customerService )
        {
            _customerService = customerService;
        }

        [Authorize]
        [TypeFilter(typeof(AuthorizationFilter), Arguments = new object[] { PermissionKeys.AddCustomer })]
        [HttpPost("/add-customer")]
        public IActionResult AddCustomers([FromQuery] string email, string password, AddCustomerDto input)
        {
            try
            {
                return Ok(_customerService.CreateCustomer(email, password, input));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [TypeFilter(typeof(AuthorizationFilter), Arguments = new object[] { PermissionKeys.UpdateInfCustomer })]
        [HttpPut("/update-information-customer")]
        public IActionResult UpdateInformationCustomer(int customerId, UpdateCustomerDto input)
        {
            try
            {
                return Ok(_customerService.UpdateInfCustomer(customerId, input));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [TypeFilter(typeof(AuthorizationFilter), Arguments = new object[] { PermissionKeys.DeleteCustomer })]
        [HttpDelete("/delete-customer")]
        public IActionResult DeleteCustomer(int id)
        {
            try
            {
                _customerService.DeleteCustomer(id);
                return Ok("Thành công");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [TypeFilter(typeof(AuthorizationFilter), Arguments = new object[] {PermissionKeys.GetCustomerById})]
        [HttpGet("/get-customer-by-id")]
        public IActionResult GetCustomerById([FromQuery] int id)
        {
            try
            {
                return Ok(_customerService.GetCustomerById(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [TypeFilter(typeof(AuthorizationFilter), Arguments = new object[] { PermissionKeys.GetAllCustomer })]
        [HttpGet("/get-all-customer")]
        public IActionResult GetAllCustomer([FromQuery] FilterDto input)
        {
            try
            {
                return Ok(_customerService.GetAllCustomer(input));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [TypeFilter(typeof(AuthorizationFilter), Arguments = new object[] { PermissionKeys.GetAllVoucherCustomer})]
        [HttpGet("/get-all-voucher-customer")]
        public IActionResult GetAllVoucherCustomer([FromQuery] FilterDto input, int customerId)
        {
            try
            {
                return Ok(_customerService.GetAllVoucherByCustomer(input, customerId));
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
