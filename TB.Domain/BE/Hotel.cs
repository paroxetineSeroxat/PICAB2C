using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TB.Domain.BE
{
    [Table("Hotel")]
    public class Hotel
    {

        public int Id { get; set; }
        public String Name { get; set; }
        public String Address { get; set; }
        public int RoomsAvailable { get; set; }
        public int RoomsBooked { get; set; }
        public int CityId { get; set; }

        public virtual City City { get; set; }
        public virtual List<Room> Rooms { get; set; }
    }
}
