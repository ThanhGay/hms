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

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }
        [Authorize]
        [AuthorizationFilter("1,2")]
        [HttpPost("/add-customer")]
        public IActionResult AddCustomers([FromQuery] string email, string password, AddCustomer input)
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
        [AuthorizationFilter("1,2")]
        [HttpPut("/update-information-customer")]
        public IActionResult UpdateInformationCustomer(int customerId, AddCustomer input)
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
        [AuthorizationFilter("2")]
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
    }
}
