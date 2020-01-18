using System;
using System.Collections.Generic;

namespace CarPoolingApp.Models
{
    public class OverallSupervisor
    {
        public List<User> Accounts = new List<User>();
        public List<Offer> Offers = new List<Offer>();
        public List<Booking> Bookings = new List<Booking>();
    }
}
