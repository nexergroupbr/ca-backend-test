using Billing.Application.DTOs;
using Billing.Domain.Entities;
using Billing.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Billing.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _repository;

        public ProductsController(IProductRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProductDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
                return BadRequest("Nome do produto é obrigatório.");

            var product = new Product
            {
                Id = Guid.NewGuid(),
                Name = dto.Name
            };

            await _repository.AddAsync(product);
            return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var product = await _repository.GetByIdAsync(id);
            if (product == null) return NotFound();
            return Ok(product);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] ProductDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
                return BadRequest("Nome do produto é obrigatório.");
            var product = await _repository.GetByIdAsync(id);
            if (product == null) return NotFound();
            product.Name = dto.Name;
            await _repository.UpdateAsync(product);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _repository.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _repository.GetAllAsync();
            return Ok(products);
        }
    }
}
