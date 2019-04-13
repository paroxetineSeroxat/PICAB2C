using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TB.Domain.BE
{
    public class Room
    {
        
        public int Id { get; set; }
        //RoomName
        public string Name { get; set; }
        public string RoomNumber { get; set; }
        public string Description { get; set; }
        public int Beds { get; set; }
        public decimal Price { get; set; }
        public decimal Vat { get; set; }
        public decimal Discount { get; set; }
        public int HotelId { get; set; }

        public virtual Hotel Hotel { get; set; }
    }
}
