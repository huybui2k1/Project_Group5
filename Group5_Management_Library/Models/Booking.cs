using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group5_Management_Library.Models
{
    public  class Booking
    {
        public int BookingId { get; set; }
        public int UserId { get; set; }
        public string Destination { get; set; }
        public DateTime Departure { get; set; }
        public string Flightdetails { get; set; }
        public string Accommodationdetails { get; set; }
        public string TotalCost { get; set; }
        public bool Bookingstatus { get; set; }

    }
}
