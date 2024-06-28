using BillingApi.Domain.Models;
using BillingApi.Service.Interfaces;
using BillingApi.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BillingApi.Controllers
{
    [Route("api/customer")]
    [ApiController]
    [Authorize]
    public class CustomerController(ICustomerService customerService) : ControllerBase
    {
        private readonly ICustomerService _customerService = customerService;

        [HttpGet]
        [Route("GetCustomers")]
        [ProducesDefaultResponseType(typeof(List<Customer>))]
        public async Task<ActionResult> GetCustomers()
        {
            try
            {
                var response = await _customerService.GetCustomers();
                if (response != null)
                    return Ok(response);
                else
                    return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}")]
        [ProducesDefaultResponseType(typeof(Customer))]
        public async Task<ActionResult> GetCustomer(Guid id)
        {
            try
            {
                var response = await _customerService.GetCustomer(id);
                if (response != null)
                    return Ok(response);
                else
                    return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [ProducesDefaultResponseType(typeof(Customer))]
        public async Task<ActionResult> PostCustomer(CustomerViewModel customer)
        {
            try
            {
                var response = await _customerService.PostCustomer(customer);
                if (response != null)
                    return CreatedAtAction(nameof(GetCustomer), new { id = response }, customer);
                else
                    return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer(Guid id, CustomerViewModel customer)
        {
            try
            {
                if (id != customer.Id)
                    return BadRequest();

                await _customerService.PutCustomer(customer);
                
                return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(Guid id)
        {
            try
            {
                await _customerService.DeleteCustomer(id);

                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
