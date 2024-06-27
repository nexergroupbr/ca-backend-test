using CaBackendTest.Models;
using Microsoft.AspNetCore.Mvc;

namespace CaBackendTest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        public UserController(ILogger<UserController> logger)
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
        public List<User> Save(User user)
        {
            return new List<User>();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public bool Delete(int? id)
        {
            return new bool();
        }
    }
}