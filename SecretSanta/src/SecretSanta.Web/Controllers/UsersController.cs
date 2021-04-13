using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using SecretSanta.Web.ViewModels;
using SecretSanta.Web.Data;

namespace SecretSanta.Web.Controllers
{
    public class UsersController : Controller
    {
        //cal:This stuff gets moved to the Data/MockData.cs file so it can be referenced by other classes. Note some references must change to compensate.
        /*
        static List<UserViewModel> Users = new List<UserViewModel>{
            new UserViewModel{FirstName = "Rob", LastName = "Boss"},
            new UserViewModel{FirstName = "Rick", LastName= "Vames"}
        };
        */
        public IActionResult Index()
        {
            //return View();
            //return View(Users);
            return View(MockData.Users);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]//cal:This maps the below version to the html method="post"
        public IActionResult Create(UserViewModel vModel)
        {
            if(ModelState.IsValid)//cal:if data is invalid goes back to create page, but also preserves data.
            {
                //Users.Add(vModel);
                vModel.Id = MockData.Users.Count;
                MockData.Users.Add(vModel);
                return RedirectToAction(nameof(Index));//cal: the nameof(Index) part helps give us a compiler warning if the Index name changes.
            }
            return View(vModel);
        }

        public IActionResult Edit(int id)
        {
            //Users[id].Id = id;
            //return View(Users[id]);
            
            //MockData.Users[id].Id = id;
            return View(MockData.Users[id]);
        }

        [HttpPost]
        public IActionResult Edit(UserViewModel vModel)
        {
            if(ModelState.IsValid)
            {
                MockData.Users[vModel.Id] = vModel;
                return RedirectToAction(nameof(Index));
            }
            return View(vModel);
        }

        public IActionResult Delete(int id)
        {
            MockData.Users.RemoveAt(id);
            return RedirectToAction(nameof(Index));
        }
    }
}