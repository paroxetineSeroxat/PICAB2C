using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TB.Domain.BE
{
    [Table("SportEvent")]
    public class SportEvent
    {
        public int Id { get; set; }
        public string EventName { get; set; }
        public DateTime Eventdate { get; set; }
        public string EventDescription { get; set; }
        public int TicketsAvailable { get; set; }
        public decimal Price { get; set; }
        public decimal Vat { get; set; }
        public decimal DiscountPercentage { get; set; }
        public string Address { get; set; }
        public int CityId { get; set; }

        public virtual City City { get; set; }
    }
}
