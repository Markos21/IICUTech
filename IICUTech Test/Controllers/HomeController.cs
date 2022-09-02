using IICUTech_Test.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace IICUTech_Test.Controllers
{
    public class HomeController : Controller
    {
        // GET: HomeController
        public ActionResult Index()
        {
            
            ViewBag.status = 0;
            ViewBag.user =false;
            return View();
        }

        // POST: HomeController/LogIn
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(IFormCollection collection)
        {
            var username= collection["UserName"];
            var password= collection["Password"];
            var ip = "";

          
            try
            {
               IICUTechservice.ICUTechClient client= new IICUTechservice.ICUTechClient();
             
               var response=client.LoginAsync(username, password, ip).Result.@return;
                
                dynamic data = JsonConvert.DeserializeObject(response);

                //If we want to return the resposne with model we can also use this
                //var result = JsonConvert.DeserializeObject<ResponseModel>(response);

               
               
                
                 if(!response.Contains("EntityId"))
                 {
                     ViewBag.status = 0;
                     ViewBag.user = true;
                 }
                 else
                 {
                     ViewBag.status = 1;
                     ViewBag.user = true;

                    //Send User Data to View
                    ViewBag.userData = data;
                    
                 }

                return View("index");
             

            }
            catch
            {
                 return View();
               
            }
        }


    }
}





    

