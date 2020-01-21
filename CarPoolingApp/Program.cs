using System;
using System.Collections.Generic;
using CarPoolingApp.Helpers;
using CarPoolingApp.Models;
using CarPoolingApp.Services;

namespace CarPoolingApp
{
    public class Program
    {
        static string USERNAME = "";
        static OverallSupervisor Supervisor = new OverallSupervisor();
        static void Main(string[] args)
        {
            while(true)
            {

                Console.WriteLine("Welcome to the app");
                Console.WriteLine("1. Login");
                Console.WriteLine("2. Signup");
                Console.WriteLine("3. Forgot Password");
                int UserInput = Convert.ToInt32(Console.ReadLine());
                switch (UserInput)
                {
                    case 1:
                        Console.WriteLine("Enter your username");
                        string UserName = Console.ReadLine();
                        Console.WriteLine("Enter your password");
                        string Password = Console.ReadLine();
                        LoginHelper Authenticator = new LoginHelper(Supervisor);
                        if(Authenticator.LoginValidator(UserName, Password) == null)
                        {
                            Console.WriteLine("Invalid Login or the user does not exist");
                        }
                        else
                        {
                            Console.WriteLine("Succesful Login");
                            USERNAME = UserName;
                            Menu();
                        }
                        break;
                    case 2:
                        Console.WriteLine("Enter a username");
                        UserName = Console.ReadLine();
                        Console.WriteLine("Enter a password");
                        Password = Console.ReadLine();
                        Console.WriteLine("What was the name of your first school?");
                        string Answer = Console.ReadLine();
                        SignupHelper Signer = new SignupHelper();
                        try
                        {
                            Signer.SignupService(ref Supervisor, UserName, Password, Answer);
                        }
                        catch(Exception e)
                        {
                            Console.WriteLine(e);
                        }
                        break;
                    case 3:
                        ForgotPasswordHelper ResetPasswordService = new ForgotPasswordHelper();
                        Console.WriteLine("Enter the user name of the account");
                        UserName = Console.ReadLine();
                        ResetPasswordService.ForgotPasswordService(ref Supervisor, UserName);
                        break;
                }
            }
        }
        static void Menu()
        {
            Console.Clear();
            Console.WriteLine("Please choose an option");
            Console.WriteLine("1. Create an offer");
            Console.WriteLine("2. Make a booking");
            Console.WriteLine("3. View all offers");
            Console.WriteLine("4. View all bookings");
            Console.WriteLine("5. Confirm a booking");
            Console.WriteLine("6. Ride History");
            Console.WriteLine("7. Top Up Wallet");
            Console.WriteLine("8. Payment");
            Console.WriteLine("9. Show Debts");
            Console.WriteLine("10. Logout");
            int UserInput = Convert.ToInt32(Console.ReadLine());
            switch (UserInput)
            {
                case 1:
                    CreateOffer();
                    break;
                case 2:
                    MakeBooking();
                    break;
                case 3:
                    ViewOffers();
                    break;
                case 4:
                    ViewBookings();
                    break;
                case 5:
                    ConfirmBooking();
                    break;
                case 6:
                    RideHistory();
                    break;
                case 7:
                    TopUpWallet();
                    break;
                case 8:
                    break;
                case 9:
                    break;
                case 10:
                    System.Environment.Exit(0);
                    break;
            }
        }
        static void CreateOffer()
        {
            OfferServiceProvider offerService = new OfferServiceProvider();
            BookingServiceProvider bookingService = new BookingServiceProvider();
            if(bookingService.IsBookingPending(Supervisor, USERNAME))
            {
                Console.WriteLine("Please clear pending booking");
                ConfirmBooking();
            }
            else
            {
                Console.WriteLine("Determine a cost per km");
                int CostPerKm = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Decide a starting point for the ride");
                string StartPoint = Console.ReadLine();
                Console.WriteLine("Decide upto 3 via points (Seperate the points with a space)");
                string ViaPoints = Console.ReadLine();
                Console.WriteLine("Decide an ending point");
                string EndPoint = Console.ReadLine();
                Console.WriteLine("Decide on the cost!");
                int Cost = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Enter max number of people");
                int MaxPeople = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Enter the car you are using");
                string CarModel = Console.ReadLine();
            }
        }
        static void MakeBooking()
        {
            BookingServiceProvider bookingService = new BookingServiceProvider();
            List<Offer> AvailableOffers = Supervisor.Offers;
            foreach(Offer display in AvailableOffers)
            {
                Console.WriteLine("ID:" + display.ID);
                Console.WriteLine("Start point " + display.StartPoint);
                foreach(string point in display.ViaPoints)
                {
                    Console.Write(point + ", ");
                }
                Console.WriteLine("End point " + display.EndPoint);
                Console.WriteLine("Cost Per KM " + display.CostPerKm);
                Console.WriteLine("Maximum People" + display.MaximumPeople);
                Console.WriteLine("Car Model" + display.CarModel);
            }
            Console.WriteLine("Enter offer ID");
            string OfferID = Console.ReadLine();
            try
            {
                bookingService.MakeBooking(OfferID, USERNAME, ref Supervisor);
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
           }
        }
        static void ViewOffers()
        {
            OfferServiceProvider offerService = new OfferServiceProvider();
            List<Offer> OffersToDisplay = offerService.ViewOffers(USERNAME, ref Supervisor);
            foreach (Offer display in OffersToDisplay)
            {
                Console.WriteLine("ID:" + display.ID);
                Console.WriteLine("Start point " + display.StartPoint);
                foreach (string point in display.ViaPoints)
                {
                    Console.Write(point + ", ");
                }
                Console.WriteLine("End point " + display.EndPoint);
                Console.WriteLine("Cost Per KM " + display.CostPerKm);
                Console.WriteLine("Maximum People" + display.MaximumPeople);
                Console.WriteLine("Car Model" + display.CarModel);
            }
        }
        static void ViewBookings()
        {
            BookingServiceProvider bookingService = new BookingServiceProvider();
            List<Booking> bookingToDisplay = bookingService.ViewBookings(USERNAME, ref Supervisor);
            foreach(Booking display in bookingToDisplay)
            {
                Console.WriteLine("ID: " + display.BookingID);
                Console.WriteLine("Start Point" + display.StartPoint);
                Console.WriteLine("End Point" + display.EndPoint);
                Console.WriteLine("Date Created" + display.DateCreated);
                Console.WriteLine("Approval Status " + display.ApprovalStatus);
                Console.WriteLine("Distance " + display.Distance);
                Console.WriteLine("Price: Rs. " + display.Price);
            }
        }
        static void ConfirmBooking()
        {
            BookingServiceProvider bookingService = new BookingServiceProvider();
            List<Booking> bookingsMade = BookingServiceProvider.UsersBookingsGenerator(USERNAME, Supervisor);
            foreach (Booking display in bookingsMade)
            {
                Console.WriteLine("ID: " + display.BookingID);
                Console.WriteLine("Start Point" + display.StartPoint);
                Console.WriteLine("End Point" + display.EndPoint);
                Console.WriteLine("Date Created" + display.DateCreated);
                Console.WriteLine("Approval Status " + display.ApprovalStatus);
                Console.WriteLine("Distance " + display.Distance);
                Console.WriteLine("Price: Rs. " + display.Price);
            }
            Console.WriteLine("Enter Booking ID");
            string BookingID = Console.ReadLine();
            Console.WriteLine("Enter a response (1 to accept and 2 to reject");
            int Response = Convert.ToInt32(Console.ReadLine());
            if (Response != 1 || Response != 2)
            {
                Console.WriteLine("Invalid response");
            }
            else
            {
                try
                {
                    bookingService.ConfirmBooking(Response, BookingID, Supervisor);
                }
                catch(Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }
        static void RideHistory()
        {
            BookingServiceProvider bookingService = new BookingServiceProvider();
            List<Booking> Completed = bookingService.ViewCompletedRides(USERNAME, Supervisor);
            foreach (Booking display in Completed)
            {
                Console.WriteLine("ID: " + display.BookingID);
                Console.WriteLine("Start Point" + display.StartPoint);
                Console.WriteLine("End Point" + display.EndPoint);
                Console.WriteLine("Date Created" + display.DateCreated);
                Console.WriteLine("Approval Status " + display.ApprovalStatus);
                Console.WriteLine("Distance " + display.Distance);
                Console.WriteLine("Price: Rs. " + display.Price);
            }
        }
        static void TopUpWallet()
        {

        }
        static void Payment()
        {

        }
        static void ViewDebts()
        {

        }
    }
}
