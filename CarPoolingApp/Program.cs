using System;
using System.Collections.Generic;
using CarPoolingApp.Models;
using CarPoolingApp.Services;
using System.Linq;

namespace CarPoolingApp
{
    public class Program
    {
        // If you think it is useful and can avoid fetching user object everytime you could have complete User object instead of just user name.
        // @ only constants are all upper.
        static string USERNAME = "";
        static OverallSupervisor Supervisor = new OverallSupervisor();
        static void Main(string[] args)
        {
            while(true)
            {

                Console.WriteLine("Welcome to the app");
                if (USERNAME.Equals(""))
                {
                    Console.WriteLine("1. Login");
                }
                else
                {
                    Console.WriteLine("2. Continue as " + USERNAME);
                }
                Console.WriteLine("2. Signup");
                Console.WriteLine("3. Forgot Password");
                //@
                int UserInput = Convert.ToInt32(Console.ReadLine());
                switch (UserInput)
                {
                    case 1:
                        Console.WriteLine("Enter your username");
                        //@
                        string UserName = Console.ReadLine();
                        Console.WriteLine("Enter your password");
                        //@
                        string Password = Console.ReadLine();
                        //@
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
                        //@ aslo Signer is an improper variable name.
                        SignupHelper Signer = new SignupHelper();
                        try
                        {
                            Signer.SignupService(ref Supervisor, UserName, Password, Answer);
                            USERNAME = UserName;
                            Menu();
                        }
                        catch(Exception e)
                        {
                            Console.WriteLine(e);
                        }
                        break;
                    case 3:
                        //@
                        ForgotPasswordHelper ResetPasswordService = new ForgotPasswordHelper();
                        Console.WriteLine("Enter the user name of the account");
                        UserName = Console.ReadLine();
                        try
                        {
                            ResetPasswordService.ForgotPasswordService(ref Supervisor, UserName);
                        }
                        catch(Exception e)
                        {
                            Console.WriteLine(e);
                        }
                        break;
                }
            }
        }
        static void Menu()
        {
            Console.Clear();
            Console.WriteLine("Logged in as " + USERNAME);
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
                // Use suffix(as flows for example) to these methods to know that these are flows not just actions.
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
                    Payment();
                    break;
                case 9:
                    ViewDebts();
                    break;
                case 10:
                    Console.WriteLine("Logging out..");
                    USERNAME = "";
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
                //@
                int CostPerKm = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Decide a starting point for the ride");
                //@
                string StartPoint = Console.ReadLine();
                Console.WriteLine("Decide upto 3 via points (Seperate the points with a space)");
                //@
                string ViaPoints = Console.ReadLine();
                if(ViaPoints.Split(' ').Length > 3)
                {
                    CreateOffer();
                }
                Console.WriteLine("Decide an ending point");
                //@
                string EndPoint = Console.ReadLine();
                Console.WriteLine("Enter max number of people");
                //@
                int MaxPeople = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Enter the car you are using");
                //@
                string CarModel = Console.ReadLine();
                offerService.CreateOffer(ref Supervisor, USERNAME, StartPoint, ViaPoints, EndPoint, CostPerKm, MaxPeople, CarModel);
            }

        }
        static void MakeBooking()
        {
            BookingServiceProvider bookingService = new BookingServiceProvider();
            //@
            List<Offer> AvailableOffers = Supervisor.Offers.FindAll(_=>(!string.Equals(_.UserID,Supervisor.Accounts.Find(u=>(string.Equals(u.UserName, USERNAME))).UserID)));
            if (AvailableOffers.ToArray().Length != 0)
            {
                foreach (Offer display in AvailableOffers)
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
                Console.WriteLine("Enter offer ID");
                // @
                string OfferID = Console.ReadLine();
                try
                {
                    bookingService.MakeBooking(OfferID, USERNAME, ref Supervisor);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
            else
            {
                Console.WriteLine("No offers made by any user");
            }

        }
        static void ViewOffers()
        {
            OfferServiceProvider offerService = new OfferServiceProvider();
            //@
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
            //@
            string BookingID = Console.ReadLine();
            Console.WriteLine("Enter a response (1 to accept and 2 to reject");
            //@
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
            //@
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
            Console.WriteLine("Amount to be topped up");
            WalletServiceProvider walletService = new WalletServiceProvider();
            decimal money = Convert.ToDecimal(Console.ReadLine());
            Console.WriteLine("Funds in the wallet now are Rs. " + walletService.TopUpWallet(ref Supervisor, USERNAME, money));

        }
        static void Payment()
        {
            BookingServiceProvider bookingService = new BookingServiceProvider();
            WalletServiceProvider walletService = new WalletServiceProvider();
            List<Booking> Completed = bookingService.ViewCompletedRides(USERNAME, Supervisor);
            if(Completed.ToArray().Length == 0)
            {
                Console.WriteLine("No trips made so far!");
                Menu();
            }
            else
            {
                List<Booking> SortedBookings = Completed.OrderByDescending(o => o.DateCreated).ToList();
                Console.WriteLine("Your last ride from " + SortedBookings.ElementAt(0).StartPoint + " to " + SortedBookings.ElementAt(0).EndPoint + " amounts to Rs. " + SortedBookings.ElementAt(0).Price);
                if(walletService.IsFundSufficient(ref Supervisor, USERNAME, SortedBookings.ElementAt(0).Price)){
                    decimal LeftMoney = walletService.DeductWalletFund(ref Supervisor, USERNAME, SortedBookings.ElementAt(0).Price);
                    Supervisor.Bookings.Remove(SortedBookings.ElementAt(0));
                    SortedBookings.ElementAt(0).IsPaid = true;
                    Supervisor.Bookings.Add(SortedBookings.ElementAt(0));
                    Console.WriteLine("Money left is Rs. " + LeftMoney);
                }
                else
                {
                    Console.WriteLine("Wallet money not sufficient");
                    Console.WriteLine("Redirecting to Top Up!");
                    TopUpWallet();
                }
            }
        }
        static void ViewDebts()
        {
            BookingServiceProvider bookingService = new BookingServiceProvider();
            List<Booking> DebtedBookings = bookingService.ViewDebtedBookings(ref Supervisor, USERNAME);
            foreach(Booking booking in DebtedBookings)
            {
                Console.WriteLine("BookingID" + booking.BookingID);
                Console.WriteLine("From" + booking.StartPoint);
                Console.WriteLine("To" + booking.EndPoint);
                Console.WriteLine("To Pay: Rs. " + booking.Price);
            }
        }
    }
}
