using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using CarPoolingApp.Models;

namespace CarPoolingApp.Services
{
    public class BookingServiceProvider
    {
        public bool IsBookingPending(OverallSupervisor supervisor, string userName)
        {
            var UserFound = supervisor.Accounts.FirstOrDefault(_ => (string.Equals(_.UserName, userName)));
            foreach (Booking booking in supervisor.Bookings)
            {
                if (booking.ApprovalStatus.Equals("NA"))
                {
                    foreach (string offerID in UserFound.Offers)
                    {
                        if (offerID.Equals(booking.OfferID))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        public void MakeBooking(string offerID, string userName, ref OverallSupervisor supervisor)
        {
            var UserFound = supervisor.Accounts.FirstOrDefault(_ => (string.Equals(_.UserName, userName)));
            var Offer = supervisor.Offers.FirstOrDefault(_ => (string.Equals(_.ID, offerID)));
            if(Offer == null || Offer.MaximumPeople == 0)
            {
                throw new Exception("Offer ID invalid");
            }
            if (UserFound.Debt != 0)
            {
                throw new Exception("You have debts, please clear it");
            }
            Booking NewBooking = new Booking(offerID, Offer.StartPoint, Offer.EndPoint, Offer.CostPerKm);
            Offer.MaximumPeople--;
            supervisor.Accounts.Remove(UserFound);
            UserFound.BookingIDs.Add(NewBooking.BookingID);
            supervisor.Bookings.Add(NewBooking);
            supervisor.Accounts.Add(UserFound);
        }
        public List<Booking> ViewBookings(string userName, ref OverallSupervisor supervisor)
        {
            var UserFound = supervisor.Accounts.FirstOrDefault(_ => (string.Equals(_.UserName, userName)));
            List<Booking> Bookings = new List<Booking>();
            foreach (string bookingID in UserFound.BookingIDs)
            {
                Bookings.Add(supervisor.Bookings.FirstOrDefault(_ => (string.Equals(_.BookingID, bookingID))));
            }
            return Bookings;
        }
        public void ConfirmBooking(int response, string bookingID, OverallSupervisor supervisor)
        {
            var BookingFound = supervisor.Bookings.FirstOrDefault(_ => (string.Equals(_.BookingID, bookingID)));
            if(BookingFound == null)
            {
                throw new Exception("Invalid Booking ID");
            }
            BookingFound.ApprovalStatus = response == 1 ? BookingConfirmationTypes.Accept.ToString() : BookingConfirmationTypes.Reject.ToString();
        }
        public static List<Booking> UsersBookingsGenerator(string userName, OverallSupervisor supervisor)
        {
            var UserFound = supervisor.Accounts.FirstOrDefault(_ => (string.Equals(_.UserName, userName)));
            List<Booking> BookingsToReturn = new List<Booking>();
            foreach(string offerID in UserFound.Offers)
            {
                var Booking = supervisor.Bookings.Find(_ => (string.Equals(_.OfferID, offerID)));
                if (Booking != null && Booking.ApprovalStatus.Equals("NA"))
                {
                    BookingsToReturn.Add(Booking);
                }
            }
            return BookingsToReturn;
        }
        public List<Booking> ViewCompletedRides(string userName, OverallSupervisor supervisor)
        {
            var UserFound = supervisor.Accounts.Find(_ => (string.Equals(_.UserName, userName)));
            List<Booking> Completed = new List<Booking>();
            foreach(string ID in UserFound.BookingIDs)
            {
                var Booking = supervisor.Bookings.Find(_ => (string.Equals(_.BookingID, ID)));
                if(Booking!=null && Booking.ApprovalStatus.Equals("Accept"))
                {
                    Completed.Add(Booking);
                }
            }
            return Completed;
        }
       
    }
}
