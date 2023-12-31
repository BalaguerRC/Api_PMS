﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PMS_API.Data;
using PMS_API.Models;

namespace PMS_API.Controllers
{
    [Authorize]
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
        [Route("byName")]
        [HttpPost]
        public dynamic GetLabTestByName([FromBody] LabTestByName labTest)
        {
            return LabTestData.GetLabTestByName(labTest.Name_LabTest, Connection);
        }
        [HttpPut("{id}")]
        public dynamic EditLabTest([FromBody] LabTest labTest, int id)
        {
            return LabTestData.EditLabTest(id, labTest, Connection);
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
        [HttpDelete("{id}")]
        public dynamic DeleteLabTest(int id)
        {
            return LabTestData.DeleteLabTest(id,Connection);
        }
    }
}
