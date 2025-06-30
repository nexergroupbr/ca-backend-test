using Billing.Application.DTOs;
using Billing.Domain.Entities;
using Billing.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Billing.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerRepository _repository;

        public CustomersController(ICustomerRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CustomerDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name) || string.IsNullOrWhiteSpace(dto.Email) || string.IsNullOrWhiteSpace(dto.Address))
                return BadRequest("Todos os campos são obrigatórios.");

            var customer = new Customer
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Email = dto.Email,
                Address = dto.Address
            };

            await _repository.AddAsync(customer);
            return CreatedAtAction(nameof(GetById), new { id = customer.Id }, customer);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var customer = await _repository.GetByIdAsync(id);
            if (customer == null) return NotFound();
            return Ok(customer);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] CustomerDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name) || string.IsNullOrWhiteSpace(dto.Email) || string.IsNullOrWhiteSpace(dto.Address))
                return BadRequest("Todos os campos são obrigatórios.");
            var customer = await _repository.GetByIdAsync(id);
            if (customer == null) return NotFound();
            customer.Name = dto.Name;
            customer.Email = dto.Email;
            customer.Address = dto.Address;
            await _repository.UpdateAsync(customer);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var customer = await _repository.GetByIdAsync(id);
            if (customer == null) return NotFound();
            await _repository.DeleteAsync(id);
            return NoContent();
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var customers = await _repository.GetAllAsync();
            return Ok(customers);
        }
    }
}
