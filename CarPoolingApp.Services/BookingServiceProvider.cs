using System;
using System.Collections.Generic;
using CarPoolingApp.Models;
using System.Text;

namespace CarPoolingApp.Services
{
    public class BookingServiceProvider
    {
        public List<Booking> ViewBookings(User User, OverallSupervisor supervisor)
        {
            List<Booking> AllBookings = new List<Booking>();
            foreach (string ID in User.BookingIDs)
            {
                AllBookings.Add(supervisor.Bookings.Find(_ => (string.Equals(ID, _.BookingID))));
            }
            return AllBookings;
        }
        public void ConfirmBooking(string bookingID, ref OverallSupervisor supervisor)
        {
            var Booking = supervisor.Bookings.Find(_ => (string.Equals(_.BookingID, bookingID)));
            supervisor.Bookings.Remove(Booking);
            Booking.ApprovalStatus = true;
            supervisor.Bookings.Add(Booking);
        }
        public void MakeBooking(string offerID, string startPoint, string endPoint, ref OverallSupervisor supervisor, ref User user)
        {
            var Book = supervisor.Offers.Find(_ => (_.ID == offerID));
            Booking NewBooking = new Booking(offerID, startPoint, endPoint, Book.CostPerKm);
            supervisor.Bookings.Add(NewBooking);
            user.BookingIDs.Add("BKN" + DateTime.Now.ToString());
        }
        
    }
}
