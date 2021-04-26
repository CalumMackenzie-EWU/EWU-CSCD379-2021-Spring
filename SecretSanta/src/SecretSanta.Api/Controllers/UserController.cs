using Microsoft.AspNetCore.Mvc;
using SecretSanta.Data;
using System.Collections.Generic;
using SecretSanta.Business;
using System;
using SecretSanta.Api.Dto;

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
            //return TestData.Users;
            return TheUserManager.List();
        }

        //route: api/user/[id]
        [HttpGet("{id}")]
        public ActionResult<User?> Get(int id)
        {

            if(id < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(id));
            }
            User? returnedUser = TheUserManager.GetItem(id);
            return returnedUser;
            //return TestData.Users[id];
        }

        //route: api/user/[id]
        [HttpDelete("{id}")]//cal: equivalent to delete
        public ActionResult Delete(int id)
        {
            //TestData.Users.RemoveAt(id);
            if(id<0)
            {
                return NotFound();
            }
            if(TheUserManager.Remove(id))
            {
                return Ok();
            }
            else{
                return NotFound();
            }
        }

        //route:api/user
        [HttpPost]//cal: equivalent to create
        public ActionResult<User?> Post([FromBody] User? theUser)
        {
            //TestData.Users.Add(userName);
            if(theUser is null)
            {
                return BadRequest();
            }
            return TheUserManager.Create(theUser);
        }

        [HttpPut("{id}")]//cal: equivalent to update
        public ActionResult Put(int id, [FromBody]UpdateUser? updatedUser)
        {
            if(updatedUser is null)
            {
                return BadRequest();
            }
            User? foundUser = TheUserManager.GetItem(id);
            if(foundUser is not null)
            {
                if(!string.IsNullOrWhiteSpace(updatedUser.FirstName) && !string.IsNullOrWhiteSpace(updatedUser.LastName))
                {
                    foundUser.FirstName = updatedUser.FirstName;
                    foundUser.LastName = updatedUser.LastName;
                    foundUser.Id = updatedUser.Id;
                }

                TheUserManager.Save(foundUser);
                return Ok();
            }
            else{
                return NotFound();
            }
        }
        

        /*
        //Cal: Below is the basic version of how things work. It became deprecated when we added UserManager.
        
        //route: api/user/
        [HttpGet]//cal: This is equivalent to a Read
        public IEnumerable<User> Get()
        {
            return TestData.Users;   
        }

        //route: api/user/[id]
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return TestData.Users[id];
        }

        //route: api/user/[id]
        [HttpDelete("{id}")]//cal: equivalent to delete
        public void Delete(int id)
        {
            TestData.Users.RemoveAt(id);
        }

        //route:api/user
        [HttpPost]//cal: equivalent to create
        public void Post([FromBody] string userName)
        {
            TestData.Users.Add(userName);
        }


        [HttpPut("{id}")]//cal: equivalent to update
        public void Put(int id, [FromBody]string userName)
        {
            TestData.Users[id] = userName;
        }
        */
    }
}