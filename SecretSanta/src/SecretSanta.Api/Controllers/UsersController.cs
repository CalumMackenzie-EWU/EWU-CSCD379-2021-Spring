using System.Collections.Generic;
using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SecretSanta.Business;
using SecretSanta.Data;
using SecretSanta.Api.Dto;
using System.Linq;

namespace SecretSanta.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserRepository Repository { get; }

        public UsersController(IUserRepository repository)
        {
            Repository = repository ?? throw new System.ArgumentNullException(nameof(repository));
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<User>), StatusCodes.Status200OK)]
        //public IEnumerable<User> Get()
        public IEnumerable<FullUser> Get()
        {
            //return Repository.List();
            ICollection<User> tempUsers = Repository.List();

            return tempUsers.Select(tUser => new FullUser{
                FirstName = tUser.FirstName,
                LastName = tUser.LastName,
                Id = tUser.Id
            });
            
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(FullUser), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //public ActionResult<User?> Get(int id)
        public ActionResult<FullUser?> Get(int id)
        {
            User? user = Repository.GetItem(id);
            if (user is null) return NotFound();
            //return user;

            return new FullUser()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Id = user.Id
            };
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]//cal: these are status codes to do with ActionResult
        public ActionResult Delete(int id)
        {
            if(id<0)
            {
                return NotFound();
            }
            if (Repository.Remove(id))
            {
                return Ok();
            }
            return NotFound();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(FullUser), StatusCodes.Status200OK)]
        //public ActionResult<User?> Post([FromBody] User? user)
        public ActionResult<FullUser?> Post([FromBody] FullUser? user)
        {
            if (user is null)
            {
                return BadRequest();
            }
            //return Repository.Create(user);
            Repository.Create(new User{
                FirstName = user.FirstName,
                LastName = user.LastName,
                Id = (Repository.List().Select(item => item.Id).Max() + 1)
            });

            return user;
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Put(int id, [FromBody] FullUser? user)
        {
            if (user is null)
            {
                return BadRequest();
            }

            User? foundUser = Repository.GetItem(id);
            if (foundUser is not null)
            {
                foundUser.FirstName = user.FirstName ?? "";
                foundUser.LastName = user.LastName ?? "";

                Repository.Save(foundUser);
                return Ok();
            }
            return NotFound();
        }
    }
}
