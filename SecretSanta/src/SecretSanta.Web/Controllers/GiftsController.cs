using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using SecretSanta.Web.ViewModels;
using SecretSanta.Web.Data;

namespace SecretSanta.Web.Controllers
{
    public class GiftsController : Controller
    {
        //cal:This stuff gets moved to the Data/MockData.cs file so it can be referenced by other classes. Note some references must change to compensate.
        /*
        static List<GiftViewModel> Gifts = new List<GiftViewModel>{
            new GiftViewModel{Title = "Lootbox", Description = "Let your friends know that you would rather leave their gift up to chance rather than give a bad gift.", Url="https://www.ea.com", Priority = 4},
            new GiftViewModel{Title = "Giftcard", Description = "For when you either dont know your friend enough. Or know them a little too well.", Url="http://NotASketchyShop.com.", Priority = 1}
        };
        */
        public IActionResult Index()
        {
            //return View();
            //return View(Gifts);
            return View(MockData.Gifts);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]//cal:This maps the below version to the html method="post"
        public IActionResult Create(GiftViewModel vModel)
        {
            if(ModelState.IsValid)//cal:if data is invalid goes back to create page, but also preserves data.
            {
                //Gifts.Add(vModel);
                
                MockData.Gifts.Add(vModel);
                return RedirectToAction(nameof(Index));//cal: the nameof(Index) part helps give us a compiler warning if the Index name changes.
            }
            return View(vModel);
        }

        public IActionResult Edit(int id)
        {
            //Gifts[id].Id = id;
            //return View(Gifts[id]);


            //MockData.Gifts[id].Id = id;
            return View(MockData.Gifts[id]);
        }

        [HttpPost]
        public IActionResult Edit(GiftViewModel vModel)
        {
            if(ModelState.IsValid)
            {
                //Gifts[vModel.Id] = vModel;
                MockData.Gifts[vModel.Id] = vModel;
                return RedirectToAction(nameof(Index));
            }
            return View(vModel);
        }

        public IActionResult Delete(int id)
        {
            //Gifts.RemoveAt(id);
            MockData.Gifts.RemoveAt(id);
            return RedirectToAction(nameof(Index));
        }
    }
}