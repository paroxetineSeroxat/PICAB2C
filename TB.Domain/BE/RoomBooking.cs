using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TB.Domain.BE
{
    public class RoomBooking
    {
        
        public int Id { get; set; }
        public int RoomId { get; set; }
        public int HotelId { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }

        public virtual Room Room { get; set; }
        public virtual Hotel Hotel { get; set; }
    }
}
