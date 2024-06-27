using CaBackendTest.Models;
using Microsoft.AspNetCore.Mvc;

namespace CaBackendTest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        public ProductController(ILogger<ProductController> logger)
        {
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public List<int> Get(int? id)
        {
            return new List<int>();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public List<Product> Save(Product user)
        {
            return new List<Product>();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public bool Delete(int? id)
        {
            return new bool();
        }
    }
}