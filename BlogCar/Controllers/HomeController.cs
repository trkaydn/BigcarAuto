using BlogCar.Models;
using BlogCar.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogCar.Controllers
{
    public class HomeController : Controller
    {
        Context _context = new Context();
        CarBrandViewModel _model = new CarBrandViewModel();

        public HomeController()
        {
            _model.Car = _context.Car.OrderByDescending(i => i.ListDate).ToList();
            _model.Brand = _context.Brand.ToList();
        }

        public ActionResult Index()
        {
            return View(_model);
        }

        [HttpPost]
        public JsonResult ContactForm(Message message)
        {

            if (message.Mail != null && message.MessageText != null && message.Name != null)
            {
                message.MessageDate = DateTime.Now;
                _context.Message.Add(message);
                _context.SaveChanges();
                return Json(1);
            }
            else
                return Json(0);
        }

        public ActionResult About()
        {
            Admin admin = _context.Admin.Where(i => i.Id == 1).FirstOrDefault();
            return View(admin);
        }

    }
}