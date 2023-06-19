using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PMS_API.Data;
using PMS_API.Models;

namespace PMS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        
        private IConfiguration _configuration;
        private string Connection;

        public PatientsController(IConfiguration configuration)
        {
            _configuration = configuration;
            Connection= _configuration.GetConnectionString("Api_PMSContext");
        }
        [HttpPost]
        public dynamic AddPatient([FromBody] Patients patients)
        {
            return PatientsData.AddPatient(patients, Connection);
        }
        [HttpGet]
        public dynamic GetPatients()
        {
            return PatientsData.GetPatients(Connection);
        }
    }
}
