﻿using Microsoft.AspNetCore.Mvc;
using PMS_API.Data;
using PMS_API.Models;

namespace PMS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LabTestResultController
    {
        private IConfiguration _configuration;
        private string Connection;

        public LabTestResultController(IConfiguration configuration)
        {
            _configuration = configuration;
            Connection = _configuration.GetConnectionString("Api_PMSContext");
        }

        [HttpPost]
        public dynamic AddLabTestResult(LabTestResult labTestResult)
        {
            return LabTestResultData.AddLabTestResult(labTestResult, Connection);
        }
        [HttpGet]
        public dynamic GetLabTestResult()
        {
            return LabTestResultData.GetLabTestResults(Connection);
        }
        [HttpGet("{id}")]
        public dynamic GetLabTestResultByPatient(int id)
        {
            return LabTestResultData.GetLabTestResultsByPatient(id,Connection);
        }
        [HttpPut("{id}")]
        public dynamic LabTestResult_PendingResults(int id)
        {
            return LabTestResultData.LabTestResult_PendingResults(id, Connection);
        }
    }
    [Route("api/[controller]")]
    [ApiController]
    public class LabTestResultsController
    {
        private IConfiguration _configuration;
        private string Connection;

        public LabTestResultsController(IConfiguration configuration)
        {
            _configuration = configuration;
            Connection = _configuration.GetConnectionString("Api_PMSContext");
        }
        [HttpGet("{id}")]
        public dynamic GetAllLabTestResultByPatient(int id)
        {
            return LabTestResultData.GetAllLabTestResultsByPatient(id, Connection);
        }
    }
}
