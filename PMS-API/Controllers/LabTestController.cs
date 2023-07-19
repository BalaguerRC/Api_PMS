using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PMS_API.Data;
using PMS_API.Models;

namespace PMS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LabTestController : ControllerBase
    {
        private IConfiguration Configuration;
        private string Connection;

        public LabTestController(IConfiguration configuration)
        {
            Configuration = configuration;
            Connection = Configuration.GetConnectionString("Api_PMSContext");
        }

        [HttpPost]
        public dynamic AddLabTest([FromBody] LabTest labTest)
        {
            return LabTestData.AddLabTest(labTest, Connection);
        }

        [HttpGet]
        public dynamic GetLabTest()
        {
            return LabTestData.GetLabTest(Connection);
        }
        [HttpGet("{id}")]
        public dynamic GetLabTestById(int id)
        {
            return LabTestData.GetLabTestById(id,Connection);
        }
    }
}
