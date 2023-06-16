using Microsoft.AspNetCore.Mvc;
using PMS_API.Data;
using PMS_API.Models;

namespace PMS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IConfiguration _configuration;
        private string Connection;

        public UsersController(IConfiguration configuration)
        {
            _configuration = configuration;
            Connection = _configuration.GetConnectionString("Api_PMSContext");
        }
        [HttpPost]
        public dynamic AddUser([FromBody] Users users)
        {
            return UsersData.AddUsers(users, Connection);
        }
        [HttpGet]
        public dynamic GetUsers()
        {
            return UsersData.GetUsers(Connection);
        }
    }
}
