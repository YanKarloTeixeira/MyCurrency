using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVCMyCurrency.Models;
using MVCMyCurrency.Utilities;

namespace MVCMyCurrency.Controllers
{
    //[Route("[controller]")]
    //[ApiController]
    public class MVCAlertsController : Controller
    {

        public ActionResult Index()
        {
            string email = HttpContext.User.Identity.Name;

            IEnumerable<MVCAlert> alerts;

            HttpResponseMessage reponse = GlobalVariables.WebApiClient.GetAsync("Alerts/"+email).Result;
            alerts = reponse.Content.ReadAsAsync<IEnumerable<MVCAlert>>().Result;

            return View(alerts);
        }

        public ActionResult AddOrEdit(int id=0)
        {
            if (id == 0)
                return View(new MVCAlert());
            else
            {
                IEnumerable<MVCAlert> alert;
                HttpResponseMessage reponse = GlobalVariables.WebApiClient.GetAsync("Alerts/" + id.ToString()).Result;
                alert = reponse.Content.ReadAsAsync<IEnumerable<MVCAlert>>().Result;
                return View(alert.First());
            }
        }

        [HttpPost]
        public ActionResult AddOrEdit(MVCAlert alert)
        {
            alert.Email = HttpContext.User.Identity.Name;

            HttpResponseMessage reponse = GlobalVariables.WebApiClient.PostAsJsonAsync("Alerts", alert).Result;
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            HttpResponseMessage reponse = GlobalVariables.WebApiClient.DeleteAsync("Alerts/"+id).Result;
            return RedirectToAction("Index");
        }

    }
}