using BlogCar.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogCar.Models
{
    public class AddCarViewModel
    {
        public Car Car { get; set; }

        public IEnumerable<SelectListItem> Brand { get; set; }

        [Required, MinLength(2, ErrorMessage = " En az bir fotoğraf zorunludur.")]
        public IEnumerable<HttpPostedFileBase> ImageUpload { get; set; }
    }
}