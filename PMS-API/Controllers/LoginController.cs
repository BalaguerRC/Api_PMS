﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using PMS_API.Data;
using PMS_API.Models;

namespace PMS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IConfiguration Configuration;
        private string Connection;

        public LoginController(IConfiguration configuration)
        {
            Configuration = configuration;
            Connection = Configuration.GetConnectionString("Api_PMSContext");
        }
        [HttpPost]
        public dynamic Login([FromBody] Login login) 
        {
            return LoginData.SignIn(login, Connection,Configuration);
        }
    }
}
