using BlogCar.Models.Entities;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogCar.Models
{
    public class CarBrandViewModel
    {
        public List<Car> Car { get; set; }
        public List<Brand> Brand { get; set; }

        public PagedList<Car> PagedCar { get; set; }

    }
}