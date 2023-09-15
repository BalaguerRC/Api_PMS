using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PMS_API.Data;
using PMS_API.Models;

namespace PMS_API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {

        private IConfiguration _configuration;
        private string Connection;

        public PatientsController(IConfiguration configuration)
        {
            _configuration = configuration;
            Connection = _configuration.GetConnectionString("Api_PMSContext");
        }
        [HttpPost]
        public dynamic AddPatient([FromBody] Patients patients)
        {
            return PatientsData.AddPatient(patients, Connection);
        }
        [Route("byName")]
        [HttpPost]
        public dynamic GetPatientByNameOrIdentity([FromBody] PatientsByNameOrIdenetity patients)
        {
            return PatientsData.GetPatientsByNameOrIdentity(patients.Name_Patient, Connection);
        }
        [HttpPut("{id}")]
        public dynamic EditPatient([FromBody] Patients patients, int id)
        {
            return PatientsData.EditPatient(id, patients, Connection);
        }
        [HttpDelete("{id}")]
        public dynamic DeletePatient(int id)
        {
            return PatientsData.DeletePatient(id, Connection);
        }
        [HttpGet]
        public dynamic GetPatients()
        {
            return PatientsData.GetPatients(Connection);
        }
       
        [HttpGet("{id}")]
        public dynamic GetPatientById(int id)
        {
            return PatientsData.GetPatientById(id, Connection);
        }
        [Route("getPInMA")]
        [HttpGet]
        public dynamic GetPatientsInMA()
        {
            return PatientsData.GetPatientsInMA(Connection);
        }
        /*[Route("getPInMA")]
        [HttpGet("{id}")]
        public dynamic GetPatientsInMAById(int id)
        {
            return PatientsData.GetPatientsInMAById(id,Connection);
        }*/
    }
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsMaController : ControllerBase
    {
        private IConfiguration _configuration;
        private string Connection;

        public PatientsMaController(IConfiguration configuration)
        {
            _configuration = configuration;
            Connection = _configuration.GetConnectionString("Api_PMSContext");
        }
        [HttpGet]
        public dynamic GetPatientsInMA()
        {
            return PatientsData.GetPatientsInMA(Connection);
        }
        [HttpGet("{id}")]
        public dynamic GetPatientsInMAById(int id)
        {
            return PatientsData.GetPatientsInMAById(id,Connection);
        }
    }
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsTop4Controller : ControllerBase
    {
        private IConfiguration _configuration;
        private string Connection;

        public PatientsTop4Controller(IConfiguration configuration)
        {
            _configuration = configuration;
            Connection = _configuration.GetConnectionString("Api_PMSContext");
        }
        [HttpGet]
        public dynamic GetTop4Patients()
        {
            return PatientsData.GetTop4Patients(Connection);
        }
    }
}
