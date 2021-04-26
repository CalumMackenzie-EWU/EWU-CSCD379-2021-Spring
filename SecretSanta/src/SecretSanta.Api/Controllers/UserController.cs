using Microsoft.AspNetCore.Mvc;
using SecretSanta.Data;
using System.Collections.Generic;

namespace SecretSanta.Api.Controllers
{
    [Route("api/[controller]")]//cal: the things inside the brakets are called attributes
    [ApiController]//cal: just trust that this is needed.
    public class UserController: ControllerBase//cal: use ControllerBase when the controller doesent need to return a view.
    {
        
        [HttpGet]//cal: This is equivalent to a Read
        public IEnumerable<string> Get()
        {
            return TestData.Users;
        }

        
        //[Route("api/[controller]/{index}")]
        [HttpGet("{index}")]
        public string Get(int index)
        {
            return TestData.Users[index];
        }
        
    }
}