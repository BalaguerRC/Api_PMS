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
    public class DoctorsController : ControllerBase
    {
        private IConfiguration _configuration;
        private string Connection;

        public DoctorsController(IConfiguration configuration)
        {
            _configuration = configuration;
            Connection = _configuration.GetConnectionString("Api_PMSContext");
        }

        [HttpPost]
        public dynamic AddDoctor([FromBody] Doctors doctors)
        {
            return DoctorsData.AddDoctor(doctors, Connection);
        }
        [HttpGet]
        public dynamic GetDoctors()
        {
            return DoctorsData.GetDoctos(Connection);
        }
        [HttpGet("{id}")]
        public dynamic GetDoctorById(int id)
        {
            return DoctorsData.GetDoctorById(id, Connection);
        }
    }
}
