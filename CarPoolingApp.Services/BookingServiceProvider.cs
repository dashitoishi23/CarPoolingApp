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
            user = userDataAccess.FindByProperty("UserName", username);
            bookings = bookingDatAccess.GetAllObjects();
        }
        public bool IsBookingPending(string userName)
        {
            foreach(Booking booking in bookings)
            {
                if(string.Equals(booking.UserName, userName) && booking.ApprovalStatus.Equals(BookingConfirmationType.None))
                {
                    return true;
                }
            }
            return false;
        }
        public void MakeBooking(string offerId)
        {
            var offer = offerDataAccess.FindByProperty("Id", offerId);
            if (offer == null || offer.maxPeople == 0)
            {
                throw new Exception(ExceptionMessages.InvalidID);
            }
            if (user.Debt != 0)
            {
                throw new Exception(ExceptionMessages.Outstanding);
            }
            Booking newBooking = new Booking(offerId, offer.startPoint, offer.EndPoint, offer.costPerKm);
            offer.maxPeople--;
            user.BookingIDs.Add(newBooking.Id);
            bookingDatAccess.Add(newBooking);
            userDataAccess.UpdateByProps((newObj) =>
            {
                newObj.BookingIDs = user.BookingIDs;
            }, user.Id);
        }
        public List<Booking> ViewBookings()
        {
            foreach (string bookingID in user.BookingIDs)
            {
                bookings.Add(bookingDatAccess.FindByProperty("Id", bookingID));
            }
            return bookings;
        }
        public void ConfirmBooking(int response, string bookingID)
        {
            var bookingFound = bookingDatAccess.FindByProperty("Id", bookingID);
            if(bookingFound == null)
            {
                throw new Exception(ExceptionMessages.InvalidID);
            }
            if(response == 1)
            {
                bookingFound.SetApprovalStatus(BookingConfirmationType.Accept);
            }
            else
            {
                bookingFound.SetApprovalStatus(BookingConfirmationType.Reject);
            }
            bookingDatAccess.UpdateByProps((newObj) =>
            {
                newObj.ApprovalStatus = bookingFound.ApprovalStatus;
            }, bookingFound.Id);
        }
        public List<Booking> UsersBookingsGenerator()
        {
            List<Booking> bookingsToReturn = new List<Booking>();
            List < Booking > bookings = bookingDatAccess.GetAllObjects();
            foreach (string OfferId in user.Offers)
            {
                var Booking = bookings.Find(_ => (string.Equals(_.OfferId, OfferId)));
                if (Booking != null && Booking.ApprovalStatus.Equals(BookingConfirmationType.None))
                {
                    bookingsToReturn.Add(Booking);
                }
            }
            return bookingsToReturn;
        }
        public List<Booking> ViewCompletedRides()
        {
            List<Booking> completed = new List<Booking>();
            foreach(string ID in user.BookingIDs)
            {
                var booking = bookingDatAccess.FindByProperty("id", ID);
                if(booking!=null && booking.ApprovalStatus.Equals(BookingConfirmationType.Accept))
                {
                    completed.Add(booking);
                }
            }
            return completed;
        }
        public List<Booking> ViewDebtedBookings()
        {
            List<Booking> debtedBookings = new List<Booking>();
            foreach(string bookingID in user.BookingIDs)
            {
                var Booking = bookingDatAccess.FindByProperty("id", bookingID);
                if (Booking.ApprovalStatus.Equals(BookingConfirmationType.Accept) && Booking.IsPaid.Equals(false))
                {
                    debtedBookings.Add(Booking);
                }
            }
            return debtedBookings;
        }
        public List<Booking> GetPendingBookings()
        {
            List<Booking> pendingBookings = new List<Booking>();
            List<string> ids = user.Offers;

            foreach(Booking booking in bookings)
            {
                if (booking.ApprovalStatus.Equals(BookingConfirmationType.None))
                {
                    foreach(string id in ids)
                    {
                        if (booking.OfferId.Equals(id))
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
