using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PlaywrightSharp;
using System.Linq;

namespace SecretSanta.EndToEnd.Tests
{
    [TestClass]
    public class EndToEndTests
    {
        private static WebHostServerFixture<SecretSanta.Web.Startup, SecretSanta.Api.Startup> theServer;
        //cal: The WebHostServer had multiple steps to it. First creating the WebHostServerFixture.cs file
        //Then copying from the lectures as suggested. Then adding project references into the EndToEnd project.
        //All of this allows us to not have to run the Web project from the command line during the testing.
        [ClassInitialize]//cal: this only run once during Class initialization, so we only start the server once.
        public static void InitializeClass(TestContext testContext)
        {
            theServer = new();
        }

        [TestMethod]
        public async Task LaunchHomepage()
        {
            var localhost = theServer.WebRootUri.AbsoluteUri.Replace("127.0.0.1","localhost");//cal: due to the Lazy part we wrote. It isnt till here where the code actually gets used.
            using var playwright = await Playwright.CreateAsync();
            await using var browser = await playwright.Chromium.LaunchAsync(new LaunchOptions
            {
                Headless = false
            });

            var page = await browser.NewPageAsync();
            //var response = await page.GoToAsync("https://localhost:5001");
            var response = await page.GoToAsync(localhost);//cal: This changes after we added the WebHostServer stuff.

            Assert.IsTrue(response.Ok);

            var headerContent = await page.GetTextContentAsync("body>header>div>a");
            Assert.AreEqual("Secret Santa", headerContent);
        }

        
        [TestMethod]
        public async Task CreateGift()
        {
            using var playwright = await Playwright.CreateAsync();
            await using var browser = await playwright.Chromium.LaunchAsync(new LaunchOptions
            {
                Headless = true     
            });

            var page = await browser.NewPageAsync();
            var response = await page.GoToAsync("https://localhost:5001");

            Assert.IsTrue(response.Ok);

            

            //cal: these cannot distinguish between two "Gifts" in text. So make them unique.
            //await page.ClickAsync("text=Gifts");
            await page.ClickAsync("text=Users");

            //cal:testing to see if we go from our 5 users in the Api. To 6 after we create a user.
            var users = await page.QuerySelectorAllAsync("body > section > section > section");
            Assert.AreEqual(5, users.Count());
            
            await page.ClickAsync("text=Create");

            /*
            //cal: at this point we should have navigate to the create page.
            await page.TypeAsync("input#Title", "Weighted Blanket");//cal: the #value is equivalent to the id in html
            await page.TypeAsync("input#Description", "Helps you sleep.");
            await page.TypeAsync("input#Url", "amazon.com");
            await page.TypeAsync("input#Priority", "3");
            //cal: commented out till we figure out selector.
            */
            await page.TypeAsync("input#FirstName", "Rory");
            await page.TypeAsync("input#LastName", "Roringson");

            await page.ClickAsync("text=Create");//cal: create the Gift.

            //cal: now we see if we created a new gift.
            users = await page.QuerySelectorAllAsync("body > section > section > section");
            Assert.AreEqual(6, users.Count());
        }

        
    }//end class
}