﻿using Microsoft.AspNetCore.Http;
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
        [HttpGet]
        public dynamic GetMedicalAppointments()
        {
            return MedicalAppointmentsData.GetMedicalAppointments(Connection);
        }
    }
}