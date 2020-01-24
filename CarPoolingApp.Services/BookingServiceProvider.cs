using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using CarPoolingApp.DataRepositories;
using CarPoolingApp.Models;
using CarPoolingApp.StringPool;

namespace CarPoolingApp.Services
{
    public class BookingServiceProvider
    {
        List<Booking> bookings = new List<Booking>();
        List<Offer> offers = new List<Offer>();
        User user;
        Repository<Booking> bookingDatAccess = new Repository<Booking>();
        Repository<User> userDataAccess = new Repository<User>();
        Repository<Offer> offerDataAccess = new Repository<Offer>();
        public BookingServiceProvider(string username)
        {
            user = userDataAccess.FindByName(username);
            bookings = bookingDatAccess.GetAllObjects();
        }
        public bool IsBookingPending(string userName)
        {
            foreach(Booking booking in bookings)
            {
                if(string.Equals(booking.userName, userName) && booking.approvalStatus.Equals(BookingConfirmationType.None))
                {
                    return true;
                }
            }
            return false;
        }
        public void MakeBooking(string offerID)
        {
            var offer = offerDataAccess.FindById(offerID);
            if (offer == null || offer.maxPeople == 0)
            {
                throw new Exception(ExceptionMessages.InvalidID);
            }
            if (user.debt != 0)
            {
                throw new Exception(ExceptionMessages.Outstanding);
            }
            Booking newBooking = new Booking(offerID, offer.startPoint, offer.endPoint, offer.costPerKm);
            offer.maxPeople--;
            user.bookingIDs.Add(newBooking.id);
            bookingDatAccess.Add(newBooking);
            userDataAccess.UpdateByName(user);
        }
        public List<Booking> ViewBookings()
        {
            foreach (string bookingID in user.bookingIDs)
            {
                bookings.Add(bookingDatAccess.FindById(bookingID));
            }
            return bookings;
        }
        public void ConfirmBooking(int response, string bookingID)
        {
            var bookingFound = bookingDatAccess.FindById(bookingID);
            if(bookingFound == null)
            {
                throw new Exception(ExceptionMessages.InvalidID);
            }
            if(response == 1)
            {
                bookingFound.approvalStatus = BookingConfirmationType.Accept;
            }
            else
            {
                bookingFound.approvalStatus = BookingConfirmationType.Reject;
            }
            bookingDatAccess.UpdateById(bookingFound);
        }
        public List<Booking> UsersBookingsGenerator()
        {
            List<Booking> bookingsToReturn = new List<Booking>();
            List < Booking > bookings = bookingDatAccess.GetAllObjects();
            foreach (string offerID in user.offers)
            {
                var Booking = bookings.Find(_ => (string.Equals(_.offerID, offerID)));
                if (Booking != null && Booking.approvalStatus.Equals(BookingConfirmationType.None))
                {
                    bookingsToReturn.Add(Booking);
                }
            }
            return bookingsToReturn;
        }
        public List<Booking> ViewCompletedRides()
        {
            List<Booking> completed = new List<Booking>();
            foreach(string ID in user.bookingIDs)
            {
                var booking = bookingDatAccess.FindById(ID);
                if(booking!=null && booking.approvalStatus.Equals(BookingConfirmationType.Accept))
                {
                    completed.Add(booking);
                }
            }
            return completed;
        }
        public List<Booking> ViewDebtedBookings()
        {
            List<Booking> debtedBookings = new List<Booking>();
            foreach(string bookingID in user.bookingIDs)
            {
                var Booking = bookingDatAccess.FindById(bookingID);
                if (Booking.approvalStatus.Equals(BookingConfirmationType.Accept) && Booking.isPaid.Equals(false))
                {
                    debtedBookings.Add(Booking);
                }
            }
            return debtedBookings;
        }
        public List<Booking> GetPendingBookings()
        {
            List<Booking> pendingBookings = new List<Booking>();
            List<string> ids = user.offers;
            foreach(Booking booking in bookings)
            {
                if (booking.approvalStatus.Equals(BookingConfirmationType.None))
                {
                    foreach(string id in ids)
                    {
                        if (booking.offerID.Equals(id))
                        {
                            pendingBookings.Add(booking);
                            break;
                        }
                    }
                }
            }
            return pendingBookings;
        }
       
    }
}
