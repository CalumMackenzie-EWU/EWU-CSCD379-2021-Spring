using Microsoft.AspNetCore.Mvc;
using SecretSanta.Data;
using System.Collections.Generic;
using SecretSanta.Business;
using System;

namespace SecretSanta.Api.Controllers
{
    [Route("api/[controller]")]//cal: the things inside the brakets are called attributes
    [ApiController]//cal: just trust that this is needed.
    public class UserController: ControllerBase//cal: use ControllerBase when the controller doesent need to return a view.
    {
        private IUserRepository TheUserManager{get;}
        public UserController(IUserRepository userManager)
        {
            TheUserManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }


        //route: api/user/
        [HttpGet]//cal: This is equivalent to a Read
        public IEnumerable<User> Get()
        {
            return TestData.Users;
        }

        //route: api/user/[index]
        [HttpGet("{index}")]
        public string Get(int index)
        {
            return "";
            //return TestData.Users[index];
        }

        //route: api/user/[index]
        [HttpDelete("{index}")]//cal: equivalent to delete
        public void Delete(int index)
        {
            TestData.Users.RemoveAt(index);
        }

        //route:api/user
        [HttpPost]//cal: equivalent to create
        public void Post([FromBody] string userName)
        {
            //TestData.Users.Add(userName);
        }

        [HttpPut("{index}")]//cal: equivalent to update
        public void Put(int index, [FromBody]string userName)
        {
            //TestData.Users[index] = userName;
        }
        
    }
}