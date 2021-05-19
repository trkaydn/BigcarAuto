using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlogCar.Models.Entities
{
    public class Admin
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(30)]
        public string Name { get; set; }

        [Required, StringLength(30)]
        public string Mail { get; set; }

        [Required, StringLength(30)]
        public string UserName { get; set; }

        [Required, StringLength(30)]
        public string Password { get; set; }

        [StringLength(250)]
        public string Photo { get; set; }

        public string About { get; set; }
    }
}