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
            return TestData.Users;
        }

        //route: api/user/[index]
        [HttpGet("{index}")]
        //public string Get(int index)
        public ActionResult<User?> Get(int index)
        {

            if(index < 0 || index >= TestData.Users.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }
            User? returnedUser = TheUserManager.GetItem(index);
            return returnedUser;
            //return TestData.Users[index];
        }

        //route: api/user/[index]
        [HttpDelete("{index}")]//cal: equivalent to delete
        //public void Delete(int index)
        public ActionResult Delete(int index)
        {
            //TestData.Users.RemoveAt(index);
            if(index<0)
            {
                return NotFound();
            }
            if(TheUserManager.Remove(index))
            {
                return Ok();
            }
            else{
                return NotFound();
            }
        }

        //route:api/user
        [HttpPost]//cal: equivalent to create
        //public void Post([FromBody] string userName)
        public ActionResult<User?> Post([FromBody] User? theUser)
        {
            //TestData.Users.Add(userName);
            if(theUser is null)
            {
                return BadRequest();
            }
            return TheUserManager.Create(theUser);
        }

        [HttpPut("{index}")]//cal: equivalent to update
        public ActionResult Put(int index, [FromBody]UpdateUser? updatedUser)
        {
            if(updatedUser is null)
            {
                return BadRequest();
            }
            User? foundUser = TheUserManager.GetItem(index);
            if(foundUser is not null)
            {
                if(!string.IsNullOrWhiteSpace(updatedUser.FirstName) && !string.IsNullOrWhiteSpace(updatedUser.LastName))
                {
                    foundUser.FirstName = updatedUser.FirstName;
                    foundUser.LastName = updatedUser.LastName;
                }

                TheUserManager.Save(foundUser);
                return Ok();
            }
            else{
                return NotFound();
            }
        }
        

        /*
        [HttpPut("{index}")]//cal: equivalent to update
        public void Put(int index, [FromBody]string userName)
        {
            TestData.Users[index] = userName;
        }
        */
    }
}