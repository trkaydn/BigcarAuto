using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlogCar.Models.Entities
{
    public class Car
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Marka zorunludur.")]
        public int BrandId { get; set; }

        [Required(ErrorMessage = "Model adı zorunludur."), StringLength(30)]
        public string Model { get; set; }

        [Required(ErrorMessage = "Model yılı zorunludur."), Range(1900, 2100, ErrorMessage = "Lütfen geçerli bir yıl girin. (Örnek:2021)")]
        public int ModelYear { get; set; }

        [Required(ErrorMessage = "Kilometre zorunludur."), Range(0, 5000000, ErrorMessage = "Lütfen geçerli bir kilometre girin. (Örnek:120000)")]
        public int Kilometer { get; set; }

        [Required(ErrorMessage = "Yakıt türü zorunludur."), MaxLength(10)]
        public string FuelType { get; set; }

        [Required(ErrorMessage = "Vites türü zorunludur."), MaxLength(10)]
        public string GearBox { get; set; }

        [Required(ErrorMessage = "Renk zorunludur."), MaxLength(15)]
        public string Color { get; set; }

        [Required(ErrorMessage = "Motor gücü zorunludur."), Range(0, 10000, ErrorMessage = "Lütfen geçerli bir motor gücü girin. (Örnek:120)")]
        public int Power { get; set; }

        [MaxLength(300)]
        public string CarPhoto1 { get; set; }

        [MaxLength(300)]
        public string CarPhoto2 { get; set; }

        [MaxLength(300)]
        public string CarPhoto3 { get; set; }

        [Required(ErrorMessage = "Fiyat zorunludur."), Range(0, 5000000, ErrorMessage = "Lütfen geçerli bir fiyat girin. (Örnek:150000)")]
        public double Price { get; set; }

        public string Description { get; set; }

        public DateTime ListDate { get; set; }

        public virtual Brand Brand { get; set; }



    }
}