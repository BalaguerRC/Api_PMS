using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PMS_API.Data;
using PMS_API.Models;

namespace PMS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicalAppointmentsController : ControllerBase
    {
        private IConfiguration Configuration;
        private string Connection;
        public MedicalAppointmentsController(IConfiguration configuration)
        {
            Configuration = configuration;
            Connection = Configuration.GetConnectionString("Api_PMSContext");
        }

        [HttpPost]
        public dynamic AddMedicalAppointment([FromBody] MedicalAppointments medical)
        {
            return MedicalAppointmentsData.AddMedicalAppointment(medical, Connection);
        }
        [Route("byName")]
        [HttpPost]
        public dynamic GetMAByPatientOrDoctor([FromBody] MedicalAppointmentsByName medical)
        {
            return MedicalAppointmentsData.GetMedicalAppointmentsByPatientOrDoctor(medical.PatientOrDoctor, Connection);
        }
        [HttpGet]
        public dynamic GetMedicalAppointments()
        {
            return MedicalAppointmentsData.GetMedicalAppointments(Connection);
        }
        [Route("dashboard")]
        [HttpGet]
        public dynamic GetMADashboard()
        {
            return MedicalAppointmentsData.MedicalAppointment_Dashboard(Connection);
        }
        [HttpGet("{id}")]
        public dynamic GetMAById_LabTestResults(int id)
        {
            return MedicalAppointmentsData.GetMAByID_LabTestResult(id,Connection);
        }
        [HttpPut("{id}")]
        public dynamic MA_PendingConsultation(int id)
        {
            return MedicalAppointmentsData.MedicalAppointment_PendingConsultation(id, Connection);
        }
        /*[Route("pendingResults")]
        [HttpPut("{id}")]
        public dynamic MA_PendingResults(int id)
        {
            return MedicalAppointmentsData.MedicalAppointment_PendingResults(id, Connection);
        }*/
    }
    [Route("api/[controller]/pendingResults")]
    [ApiController]
    public class MedicalAppointmentController : ControllerBase
    {
        private IConfiguration Configuration;
        private string Connection;
        public MedicalAppointmentController(IConfiguration configuration)
        {
            Configuration = configuration;
            Connection = Configuration.GetConnectionString("Api_PMSContext");
        }
        [HttpPut("{id}")]
        public dynamic MA_PendingResults(int id)
        {
            return MedicalAppointmentsData.MedicalAppointment_PendingResults(id, Connection);
        }
    }

}
