using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC4Microformats.Calendar;

namespace MVC4Microformats_WebDemo.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Microformats example";

            return View();
        }

        public ActionResult Calendar()
        {
            ViewBag.cf=CalendarFormats();
            ViewBag.Message = "Calendar";
            var model = new MFCalendar(DateTime.Now.AddDays(1), "MVC Training with Andrei Ignat", CalendarFormat.TagBuilder);
            model.DtEnd = model.DtStart.Value.AddDays(3);
            model.Url = "http://msprogrammer.serviciipeweb.ro/2013/05/06/mvc-trainingfrom-beginner-to-advanced/";
            model.Description = "mvc training - from beginner to advanced";
            return View(model);
        }
        [HttpPost]
        public ActionResult Calendar(MFCalendar model)
        {
            ViewBag.cf = CalendarFormats();
            ViewBag.Message = "Calendar";
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            return View(model);
        }
        public IEnumerable<SelectListItem> CalendarFormats()
        {
            return Enum.GetValues(typeof(FormatCalendar)).Cast<FormatCalendar>()
                .Select(item => new SelectListItem() { Text = item.ToString(), Value = ((int)item).ToString() }).ToArray();
        }


        [ValidateInput(false)]
        [HttpPost]
        public ActionResult ReturnFile(string resultHtml, string eventName, string nameFile)
        {
            return File(System.Text.UTF8Encoding.Default.GetBytes(resultHtml), "text/calendar", nameFile);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
