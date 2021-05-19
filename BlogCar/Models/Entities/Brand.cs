using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlogCar.Models.Entities
{
    public class Brand
    {
        [Key]
        public int Id { get; set; }

        [StringLength(30),Required]
        public string Name { get; set; }
        public virtual List<Car> Car { get; set; }
    }
}