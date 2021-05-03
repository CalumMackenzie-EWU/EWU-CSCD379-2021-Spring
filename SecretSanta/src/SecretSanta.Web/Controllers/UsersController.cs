using Microsoft.AspNetCore.Mvc;
using SecretSanta.Web.Data;
using SecretSanta.Web.ViewModels;
using SecretSanta.Web.Api;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;

namespace SecretSanta.Web.Controllers
{
    public class UsersController : Controller
    {
        //public UsersClient Client{get;}//cal: also gets swapped out after building the interface
        public IUsersClient Client{get;}
        //public UsersController(UsersClient theClient) //cal:adds dependency. Is the what we used before generating an Interface in SS.Api.Client.g.cs
        public UsersController(IUsersClient theClient)
        {
            Client = theClient ?? throw new ArgumentNullException(nameof(theClient));
        }   
        /*
        public IActionResult Index()
        {
            return View(MockData.Users);
        }
        */
        public async Task<IActionResult> Index()//cal: This version is used for multithreading.
        {
            ICollection<User> users =  await Client.GetAllAsync();
            List<UserViewModel> viewModelUsers = new();
            foreach(User u in users)
            {
                viewModelUsers.Add(new UserViewModel
                {
                    Id = u.Id,
                    FirstName = u.FirstName,
                    LastName = u.LastName
                });
            }
            return View(viewModelUsers);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(UserViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                MockData.Users.Add(viewModel);
                return RedirectToAction(nameof(Index));
            }

            return View(viewModel);
        }

        public IActionResult Edit(int id)
        {
            return View(MockData.Users[id]);
        }

        [HttpPost]
        public IActionResult Edit(UserViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                MockData.Users[viewModel.Id] = viewModel;
                return RedirectToAction(nameof(Index));
            }

            return View(viewModel);
        }

        [HttpPost]
        public  async Task<IActionResult> Delete(int id)
        {
            await Client.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}