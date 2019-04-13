using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TB.Domain.BE
{
    [Table("Product")]
    public class Product
    {

        public int Id { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public int SportEventId { get; set; }
        public int? HotelId { get; set; }
        public int? TransportId { get; set; }
        public decimal Price { get; set; }
        public decimal Vat { get; set; }
        public int ItemsAvailable { get; set; }
        public int ItemsBooked { get; set; }
        public bool? IsCampain { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public decimal? CampainDiscount { get; set; }
        public string ImagePath1 { get; set; }
        public string ImagePath2 { get; set; }
        public string ImagePath3 { get; set; }
        public decimal? TotalValue { get; set; }
        public string ProductCode { get; set; }

        public virtual Hotel Hotel { get; set; }
        public virtual Transport Transport { get; set; }
        public virtual SportEvent SportEvent { get; set; }

        [NotMapped]
        public int SoldQuantity { get; set; }



    }
}
