using System;
using System.Collections.Generic;
using System.Text;

namespace CarPoolingApp.Models
{
    public class Booking
    {
        public string BookingID { get; set; }
        public bool ApprovalStatus { get; set; }
        public DateTime DateCreated { get; }
        public string OfferID { get; set; }

        public Booking(string offerID)
        {
            this.OfferID = offerID;
        }
    }
}
