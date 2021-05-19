using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlogCar.Models.Entities
{
    public class Message
    {
        [Key]
        public int Id { get; set; }

        [StringLength(30),Required]
        public string Name { get; set; }

        [StringLength(50), Required]
        public string Mail { get; set; }
        public string MessageText { get; set; }
        
        public int CarId { get; set; }

        public DateTime MessageDate { get; set; }

    }
}