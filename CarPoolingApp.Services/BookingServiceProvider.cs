using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using CarPoolingApp.Models;

namespace CarPoolingApp.Services
{
    public class BookingServiceProvider
    {

        // Design Guide: 
        // Have OverallSupervisor and User object as properites and initialize them in constructor. That avoids passing them as parameters
        // each and every method. Do the same with all service. This avoids fetching user each and every time.
        // * Fetching of user cannot be done directly in Booking service provider, it has to go through the account service.
        //        Similarly other entities and actions on them to be moved to the respective service provider.
        // ** Avoid repitative code. you could rather move the code into a method and use it.
        // # Avoid usage of direct strings or integers instead use Enums and constants which ever is appropriate
        // Style Guide - Have a line break after every closing brace unless it is followed by another closing brace
        // @ Improper Case



        public bool IsBookingPending(OverallSupervisor supervisor, string userName)
        {
            // * @
            var UserFound = supervisor.Accounts.FirstOrDefault(_ => (string.Equals(_.UserName, userName)));
            foreach (Booking booking in supervisor.Bookings)
            {
                if (booking.ApprovalStatus.Equals(BookingConfirmationTypes.None))
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
            // * @
            var UserFound = supervisor.Accounts.FirstOrDefault(_ => (string.Equals(_.UserName, userName)));
            
            // * @
            var Offer = supervisor.Offers.FirstOrDefault(_ => (string.Equals(_.ID, offerID)));

            // # **
            if (Offer == null || Offer.MaximumPeople == 0)
            {
                throw new Exception("Offer ID invalid");
            }

            // # **
            if (UserFound.Debt != 0)
            {
                throw new Exception("You have debts, please clear it");
            }
            Booking NewBooking = new Booking(offerID, Offer.StartPoint, Offer.EndPoint, Offer.CostPerKm);

            // * **
            Offer.MaximumPeople--;
            // * **
            supervisor.Accounts.Remove(UserFound);

            // *
            UserFound.BookingIDs.Add(NewBooking.BookingID);
            
            // **
            supervisor.Bookings.Add(NewBooking);

            // ** *
            supervisor.Accounts.Add(UserFound);
        }

        // All objects are by default passed by reference. No need of ref keyword. ref is used for primitive data types.
        public List<Booking> ViewBookings(string userName, ref OverallSupervisor supervisor)
        {
            // * @
            var UserFound = supervisor.Accounts.FirstOrDefault(_ => (string.Equals(_.UserName, userName)));
            List<Booking> Bookings = new List<Booking>();
            foreach (string bookingID in UserFound.BookingIDs)
            {
                // **
                Bookings.Add(supervisor.Bookings.FirstOrDefault(_ => (string.Equals(_.BookingID, bookingID))));
            }
            return Bookings;
        }
        public void ConfirmBooking(int response, string bookingID, OverallSupervisor supervisor)
        {
            // ** @
            var BookingFound = supervisor.Bookings.FirstOrDefault(_ => (string.Equals(_.BookingID, bookingID)));
            if(BookingFound == null)
            {
                throw new Exception("Invalid Booking ID");
            }

            // #
            BookingFound.ApprovalStatus = response == 1 ? BookingConfirmationTypes.Accept : BookingConfirmationTypes.Reject;
        }
        public static List<Booking> UsersBookingsGenerator(string userName, OverallSupervisor supervisor)
        {
            // * @
            var UserFound = supervisor.Accounts.FirstOrDefault(_ => (string.Equals(_.UserName, userName)));
            List<Booking> BookingsToReturn = new List<Booking>();
            foreach(string offerID in UserFound.Offers)
            {
                // ** @
                var Booking = supervisor.Bookings.Find(_ => (string.Equals(_.OfferID, offerID)));
                if (Booking != null && Booking.ApprovalStatus.Equals(BookingConfirmationTypes.None))
                {
                    BookingsToReturn.Add(Booking);
                }
            }
            return BookingsToReturn;
        }
        public List<Booking> ViewCompletedRides(string userName, OverallSupervisor supervisor)
        {
            // * @
            var UserFound = supervisor.Accounts.Find(_ => (string.Equals(_.UserName, userName)));
            List<Booking> Completed = new List<Booking>();
            foreach(string ID in UserFound.BookingIDs)
            {
                // ** @
                var Booking = supervisor.Bookings.Find(_ => (string.Equals(_.BookingID, ID)));
                if(Booking!=null && Booking.ApprovalStatus.Equals(BookingConfirmationTypes.Accept))
                {
                    Completed.Add(Booking);
                }
            }
            return Completed;
        }
        public List<Booking> ViewDebtedBookings(ref OverallSupervisor supervisor, string userName)
        {
            var UserFound = supervisor.Accounts.Find(_ => (string.Equals(_.UserName, userName)));
            List<Booking> DebtedBookings = new List<Booking>();
            foreach(string bookingID in UserFound.BookingIDs)
            {
                var Booking = supervisor.Bookings.Find(_ => (string.Equals(_.BookingID, bookingID)));
                if (Booking.ApprovalStatus.Equals(BookingConfirmationTypes.Accept) && Booking.IsPaid.Equals(false))
                {
                    DebtedBookings.Add(Booking);
                }
            }
            return DebtedBookings;
        }
       
    }
}
