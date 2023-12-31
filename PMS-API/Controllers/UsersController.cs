﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PMS_API.Data;
using PMS_API.Models;

namespace PMS_API.Controllers
{
    [Authorize]
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
        [HttpPut("{id}")]
        public dynamic EditUser([FromBody] EditUser users, int id) 
        {
            Encryption encryption = new Encryption();

            EditUser newUser= new EditUser();
            newUser.Name_User = users.Name_User;
            newUser.LastName_User = users.LastName_User;
            newUser.Email_User = users.Email_User;
            newUser.UserName = users.UserName;
            newUser.Password_User = encryption.Encryting(users.Password_User);
            newUser.Type_User = users.Type_User;
            

            return UsersData.EditUser(id, newUser, Connection);
        }
        [HttpDelete("{id}")]
        public dynamic DeleteUser(int id)
        {
            return UsersData.DeleteUser(id, Connection);
        }
        [HttpGet]
        public dynamic GetUsers()
        {
            return UsersData.GetUsers(Connection);
        }
        [Route("byName")]
        [HttpPost]
        public dynamic GetUserByName([FromBody] UserByName name)
        {
            return UsersData.GetUserByName(name.Name_User, Connection);
        }
        [HttpGet("{id}")]
        public dynamic GetUsersById(int id) 
        { 
            return UsersData.GetUserById(id, Connection);
        }
    }
    
}
