
using BlogCar.Models;
using BlogCar.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;

namespace BlogCar.Controllers
{
    public class CarController : Controller
    {
        Context _context = new Context();
        CarBrandViewModel _model = new CarBrandViewModel();

        public CarController()
        {
            _model.Car = _context.Car.OrderByDescending(i => i.ListDate).ToList();
            _model.Brand = _context.Brand.ToList();
        }

        public ActionResult Index(int page = 1)
        {
            _model.PagedCar = (PagedList<Car>)_model.Car.ToList().ToPagedList(page, 6);
            return View(_model);
        }

        [HttpPost]
        public ActionResult Index(string brandId, string fueltype, string gearBox, int page = 1)
        {
            CarBrandViewModel model = new CarBrandViewModel();
            model.Brand = _model.Brand;

            if (brandId != "0")
            {
                model.PagedCar = (PagedList<Car>)_model.Car.Where(i => i.BrandId.ToString() == brandId).ToList().ToPagedList(page, 6);
                TempData["brandId"] = brandId;

                if (fueltype != "0")
                {
                    model.PagedCar = (PagedList<Car>)model.PagedCar.Where(i => i.FuelType == fueltype).ToList().ToPagedList(page, 6);
                    TempData["fuelType"] = fueltype;
                }
                if (gearBox != "0")
                {
                    model.PagedCar = (PagedList<Car>)model.PagedCar.Where(i => i.GearBox == gearBox).ToList().ToPagedList(page, 6);
                    TempData["gearBox"] = gearBox;

                }
            }
            else
            {
                model.PagedCar = (PagedList<Car>)_model.Car.ToList().ToPagedList(page, 6);

                if (fueltype != "0")
                {
                    model.PagedCar = (PagedList<Car>)model.PagedCar.Where(i => i.FuelType == fueltype).ToList().ToPagedList(page, 6);
                    TempData["fuelType"] = fueltype;
                }
                if (gearBox != "0")
                {
                    model.PagedCar = (PagedList<Car>)model.PagedCar.Where(i => i.GearBox == gearBox).ToList().ToPagedList(page, 6);
                    TempData["gearBox"] = gearBox;
                }
            }

            if (model.PagedCar.Count() == 0)
                ViewBag.Message = "Hiçbir sonuç bulunamadı.";
            return View(model);
        }



        public ActionResult Detail(int id)
        {
            CarDetailViewModel model = new CarDetailViewModel();
            model.Car = _model.Car.Find(i => i.Id == id);
            return View(model);
        }

        [HttpPost]
        public JsonResult Detail(Message message)
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
    }
}