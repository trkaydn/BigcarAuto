using BlogCar.Models;
using BlogCar.Models.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace BlogCar.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        Context _context = new Context();
        CarBrandViewModel _model = new CarBrandViewModel();
        AddCarViewModel _addModel = new AddCarViewModel();
        public AdminController()
        {
            _model.Car = _context.Car.OrderByDescending(i => i.ListDate).ToList();
            _model.Brand = _context.Brand.ToList();
            _addModel.Brand = new SelectList(_model.Brand, "Id", "Name");
        }

        [HttpPost, OverrideAuthorization]
        public ActionResult Login(Admin admin)
        {

            var activeAdmin = _context.Admin.Where(x => x.UserName == admin.UserName && x.Password == admin.Password).FirstOrDefault();
            if (activeAdmin != null)
            {
                Session["admin"] = activeAdmin.UserName;
                FormsAuthentication.SetAuthCookie(activeAdmin.UserName, false);
                return Json(1, JsonRequestBehavior.AllowGet);
            }
            else
                return Json(0, JsonRequestBehavior.AllowGet);
        }

        [OverrideAuthorization]
        public ActionResult Login(int? post)
        {
            if (Session["admin"] == null && post == 1)
                return Json(0, JsonRequestBehavior.AllowGet);
            else if (Session["admin"] == null)
                return RedirectToAction("UnAuthorized", "Error");
            return Json(1, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Remove("admin");
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Index()
        {
            return View(_model);
        }

        public JsonResult CarDetail(int id)
        {
            List<string> properties = new List<string>();
            var car = _context.Car.Find(id);
            properties.Add(car.Brand.Name);
            properties.Add(car.Model);
            properties.Add(car.Price.ToString());
            properties.Add(car.CarPhoto1);
            properties.Add(car.Color);
            properties.Add(car.FuelType);
            properties.Add(car.GearBox);
            properties.Add(car.Kilometer.ToString());
            properties.Add(car.ModelYear.ToString());
            properties.Add(car.Power.ToString());
            return Json(properties, JsonRequestBehavior.AllowGet);

        }

        public JsonResult DeleteCar(int id)
        {
            var car = _context.Car.Find(id);
            string photo1 = null, photo2 = null, photo3 = null;

            photo1 = car.CarPhoto1.Substring(21);
            if (car.CarPhoto2 != null)
                photo2 = car.CarPhoto2.Substring(21);
            if (car.CarPhoto3 != null)
                photo3 = car.CarPhoto3.Substring(21);
            string uploadPath = Path.Combine(Server.MapPath("/Content/images/cars/"));
            DirectoryInfo directoryInfo = new DirectoryInfo(uploadPath);
            var files = directoryInfo.GetFiles();
            foreach (var file in files)
            {
                if (file.Name == photo1)
                    file.Delete();
                else if (photo2 != null && file.Name == photo2)
                    file.Delete();
                else if (photo3 != null && file.Name == photo3)
                    file.Delete();
            }

            _context.Car.Remove(car);
            _context.SaveChanges();
            return Json(1);

        }

        [HttpPost]
        public JsonResult AddBrand(string name)
        {
            var control = _context.Brand.Where(i => i.Name == name).FirstOrDefault();
            if (control != null)
                return Json(2);
            if (name.Count()>2)
            {
                Brand brand = new Brand() { Name = name };
                _context.Brand.Add(brand);
                _context.SaveChanges();
                return Json(1);
            }
            else
                return Json(0);
        }

        public ActionResult Messages()
        {
            var model = _context.Message.OrderByDescending(i => i.MessageDate).ToList();
            return View(model);
        }

        public JsonResult DelMessage(int id)
        {
            var message = _context.Message.Find(id);
            _context.Message.Remove(message);
            _context.SaveChanges();
            return Json(1);
        }

        public ActionResult AddCar()
        {

            return View(_addModel);
        }

        [HttpPost]
        public ActionResult AddCar(AddCarViewModel model)
        {
            Car car = new Car();
            car = model.Car;

            if (Request.Files[0].FileName == "" || car.BrandId < 1 || car.ModelYear < 1900 || car.Power < 1 || car.Price < 1 || car.Color == null || car.FuelType == null || car.FuelType == null || car.GearBox == null || car.Model == null || model.ImageUpload.Count() < 1)
            {
                TempData["AddCarError"] = "Lütfen tüm bilgileri eksiksiz doldurup tekrar deneyin.";
                return View(_addModel);
            }

            List<string> imagePaths = new List<string>();
            foreach (var image in model.ImageUpload)
            {
                if (image.ContentType != "image/jpeg" && image.ContentType != "image/jpg" && image.ContentType != "image/png")
                {
                    TempData["AddCarError"] = "İlan fotoğrafları '.jpg', '.jpeg' veya '.png' dosya türünde olmalıdır.";
                    return View(_addModel);
                }
                var path = Path.Combine(Server.MapPath("/Content/images/cars/"), image.FileName);
                string uploadPath = ConfigurationManager.AppSettings["ImagePath"].ToString();
                imagePaths.Add(uploadPath + image.FileName);
                image.SaveAs(path);
            }

            if (imagePaths.Count() > 0)
                car.CarPhoto1 = imagePaths[0];
            if (imagePaths.Count() > 1)
                car.CarPhoto2 = imagePaths[1];
            if (imagePaths.Count() > 2)
                car.CarPhoto3 = imagePaths[2];
            car.ListDate = DateTime.Now;

            _context.Car.Add(car);
            _context.SaveChanges();
            TempData["AddCarMessage"] = "İlan başarıyla yayınlandı.";
            return RedirectToAction("Index");

        }
    }
}