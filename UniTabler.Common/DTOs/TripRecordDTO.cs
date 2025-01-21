using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniTabler.Common.DTOs
{
    public class TripRecordDTO
    {
        public DateTime PickUpDateTime { get; set; }
        public DateTime DropOffDateTime { get; set; }
        public int PassengerCount { get; set; }
        public double TripDistance { get; set; }
        public bool StoreAndForwardFlag { get; set; }
        public int PickUpLocationId { get; set; }
        public int DropOffLocationId { get; set; }
        public decimal FareAmount { get; set; }
        public decimal TipAmount { get; set; }
    }
}
