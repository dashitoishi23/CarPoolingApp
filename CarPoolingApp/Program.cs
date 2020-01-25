using System;
using System.Collections.Generic;
using CarPoolingApp.DataRepositories;
using CarPoolingApp.Services;
using System.Linq;
using System.Reflection;
using CarPoolingApp.Models;
using CarPoolingApp.Helpers;

namespace CarPoolingApp
{
    public class Program
    {
        
        static string loggedInUser = string.Empty;
        static User sessionUser;
        static void Main(string[] args)
        {

            while(true)
            {

                Console.WriteLine("Welcome to the app");
                if (loggedInUser.Equals(""))
                {
                    Console.WriteLine("1. Login");
                }
                else
                {
                    Console.WriteLine("1. Continue as " + loggedInUser);
                }
                Console.WriteLine("2. Signup");
                Console.WriteLine("3. Forgot Password");
                 
                int userInput = Convert.ToInt32(Console.ReadLine());
                switch (userInput)
                {
                    case 1:
                        string userName;
                        string password;
                        if (sessionUser == null) {
                            Console.WriteLine("Enter your username");

                            userName = Console.ReadLine();
                            Console.WriteLine("Enter your password");

                            password = Console.ReadLine();

                            LoginHelper authenticator = new LoginHelper();
                            try
                            {
                                if (authenticator.LoginValidator(userName, password) == null)
                                {
                                    Console.WriteLine("Invalid Login or the user does not exist");
                                }
                                else
                                {
                                    Console.WriteLine("Succesful Login");
                                    loggedInUser = userName;
                                    sessionUser = authenticator.LoginValidator(userName, password);
                                    Menu();
                                }
                            }
                            catch(Exception e)
                            {
                                Console.WriteLine(e.Message);
                            }
                        }
                        else
                        {
                            Menu();
                        }
                        break;
                    case 2:
                        Console.WriteLine("Enter a username");
                        userName = Console.ReadLine();
                        Console.WriteLine("Enter a password");
                        password = Console.ReadLine();
                        Console.WriteLine("What was the name of your first school?");
                        string Answer = Console.ReadLine();
                        SignupHelper signupService = new SignupHelper(userName);
                        try
                        {
                            sessionUser = signupService.SignupService(userName, password, Answer);
                            loggedInUser = userName;
                            Menu();
                        }
                        catch(Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        break;
                    case 3:

                        ForgotPasswordHelper resetPasswordService = new ForgotPasswordHelper();
                        Console.WriteLine("Enter the user name of the account");
                        userName = Console.ReadLine();
                        try
                        {
                            Console.WriteLine("What is the name of your first school?");
                            string answer = Console.ReadLine();
                            resetPasswordService.ForgotPasswordService(userName, answer);
                        }
                        catch(Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        break;
                }
            }
        }
        static void Menu()
        {
            Console.Clear();
            Console.WriteLine("Logged in as " + loggedInUser);
            Console.WriteLine("Please choose an option");
            Console.WriteLine("1. Create an offer");
            Console.WriteLine("2. Make a booking");
            Console.WriteLine("3. View all offers");
            if(sessionUser.BookingIDs.ToArray().Length == 0)
            {
                Console.WriteLine("4. View all bookings (No bookings made)");
            }
            else
            {
                int numberBookings = sessionUser.BookingIDs.ToArray().Length;
                Console.WriteLine("4. View all "+numberBookings+" bookings ");
            }
            BookingServiceProvider bookingService = new BookingServiceProvider(loggedInUser);
            List<Booking> pendingBookings = bookingService.GetPendingBookings();
            if(pendingBookings.ToArray().Length == 0)
            {

                Console.WriteLine("5. Confirm a booking (No bookings to confirm)");
            }
            else
            {
                Console.WriteLine("5. You have " + pendingBookings.ToArray().Length + "bookings to confirm");
            }

            Console.WriteLine("6. Ride History");
            Console.WriteLine("7. Top Up Wallet");
            Console.WriteLine("8. Payment");
            Console.WriteLine("9. Show Debts");
            Console.WriteLine("10. Logout");
            int userInput = Convert.ToInt32(Console.ReadLine());
            switch (userInput)
            {
                case 1:
                    CreateOfferFlow();
                    break;
                case 2:
                    MakeBookingFlow();
                    break;
                case 3:
                    ViewOffersFlow();
                    break;
                case 4:
                    ViewBookingsFlow();
                    break;
                case 5:
                    ConfirmBookingFlow();
                    break;
                case 6:
                    RideHistoryFlow();
                    break;
                case 7:
                    TopUpWalletFlow();
                    break;
                case 8:
                    PaymentFlow();
                    break;
                case 9:
                    ViewDebtsFlow();
                    break;
                case 10:
                    Console.WriteLine("Logging out..");
                    loggedInUser = "";
                    sessionUser = null;
                    break;
            }
        }
        static void CreateOfferFlow()
        {
            OfferServiceProvider offerService = new OfferServiceProvider(loggedInUser);
            BookingServiceProvider bookingService = new BookingServiceProvider(loggedInUser);
            if(bookingService.IsBookingPending(loggedInUser))
            {
                Console.WriteLine("Please clear pending booking");
                ConfirmBookingFlow();
            }
            else
            {
                Console.WriteLine("Determine a cost per km");
                 
                int costPerKm = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Decide a starting point for the ride");
                 
                string startPoint = Console.ReadLine();
                Console.WriteLine("Decide upto 3 via points (Seperate the points with a space)");
                 
                string viaPoints = Console.ReadLine();
                if(viaPoints.Split(' ').Length > 3)
                {
                    CreateOfferFlow();
                }
                Console.WriteLine("Decide an ending point");
                 
                string EndPoint = Console.ReadLine();
                Console.WriteLine("Enter max number of people");
                 
                int maxPeople = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Enter the car you are using");
                string carModel = Console.ReadLine();
                offerService.CreateOffer(startPoint, viaPoints, EndPoint, costPerKm, maxPeople, carModel);
            }

        }
        static void MakeBookingFlow()
        {
            BookingServiceProvider bookingService = new BookingServiceProvider(loggedInUser);
            OfferServiceProvider offerService = new OfferServiceProvider(loggedInUser);
            List<Offer> availableOffers = offerService.ViewAllOffersOtherThanUser();
            if (availableOffers.ToArray().Length != 0)
            {

                foreach(var offer in availableOffers)
                {
                    AttributeDisplayHelper.DisplayAttributes<Offer>(offer);
                }
                Console.WriteLine("Enter offer ID");
                string OfferID = Console.ReadLine();
                try
                {
                    bookingService.MakeBooking(OfferID);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            else
            {
                Console.WriteLine("No offers made by any user");
            }

        }
        static void ViewOffersFlow()
        {
            OfferServiceProvider offerService = new OfferServiceProvider(loggedInUser);
            List<Offer> OffersToDisplay = offerService.ViewOffers();
            foreach (Offer display in OffersToDisplay)
            {
                AttributeDisplayHelper.DisplayAttributes<Offer>(display);
            }

        }
        static void ViewBookingsFlow()
        {
            BookingServiceProvider bookingService = new BookingServiceProvider(loggedInUser);
            List<Booking> bookingToDisplay = bookingService.ViewBookings();
            foreach(Booking display in bookingToDisplay)
            {
                AttributeDisplayHelper.DisplayAttributes<Booking>(display);
            }

        }
        static void ConfirmBookingFlow()
        {
            BookingServiceProvider bookingService = new BookingServiceProvider(loggedInUser);
            List<Booking> bookingsMade = bookingService.UsersBookingsGenerator();
            foreach (Booking display in bookingsMade)
            {
                AttributeDisplayHelper.DisplayAttributes<Booking>(display);
            }
            Console.WriteLine("Enter Booking ID");
            string bookingID = Console.ReadLine();
            Console.WriteLine("Enter a response (1 to accept and 2 to reject)");
            int response = Convert.ToInt32(Console.ReadLine());
            response = response % 2;
            response = response == 0 ? 2 : response;
            try
            {
                bookingService.ConfirmBooking(response,bookingID);
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }

        }
        static void RideHistoryFlow()
        {
            BookingServiceProvider bookingService = new BookingServiceProvider(loggedInUser);
            List<Booking> completed = bookingService.ViewCompletedRides();
            foreach (Booking display in completed)
            {
                AttributeDisplayHelper.DisplayAttributes<Booking>(display);
            }

        }
        static void TopUpWalletFlow()
        {
            Console.WriteLine("Amount to be topped up");
            WalletServiceProvider walletService = new WalletServiceProvider(loggedInUser);
            decimal money = Convert.ToDecimal(Console.ReadLine());
            Console.WriteLine("Funds in the wallet now are Rs. " + walletService.TopUpWallet( money));

        }
        static void PaymentFlow()
        {
            BookingServiceProvider bookingService = new BookingServiceProvider(loggedInUser);
            WalletServiceProvider walletService = new WalletServiceProvider(loggedInUser);
            List<Booking> Completed = bookingService.ViewCompletedRides();
            if(Completed.ToArray().Length == 0)
            {
                Console.WriteLine("No trips made so far!");
                Menu();
            }
            else
            {
                List<Booking> SortedBookings = Completed.OrderByDescending(o => o.DateCreated).ToList();
                Repository<Booking> bookingDataAccess = new Repository<Booking>();
                Console.WriteLine("Your last ride from " + SortedBookings.ElementAt(0).StartPoint + " to " + SortedBookings.ElementAt(0).EndPoint + " amounts to Rs. " + SortedBookings.ElementAt(0).Price);
                if(walletService.IsFundSufficient(SortedBookings.ElementAt(0).Price)){
                    decimal LeftMoney = walletService.DeductWalletFund(SortedBookings.ElementAt(0).Price);
                    bookingDataAccess.Remove(SortedBookings.ElementAt(0));
                    SortedBookings.ElementAt(0).IsPaid = true;
                    bookingDataAccess.Add(SortedBookings.ElementAt(0));
                    Console.WriteLine("Money left is Rs. " + LeftMoney);
                }
                else
                {
                    Console.WriteLine("Wallet money not sufficient");
                    Console.WriteLine("Redirecting to Top Up!");
                    TopUpWalletFlow();
                }
            }
        }
        static void ViewDebtsFlow()
        {
            BookingServiceProvider bookingService = new BookingServiceProvider(loggedInUser);
            List<Booking> DebtedBookings = bookingService.ViewDebtedBookings();
            foreach(Booking booking in DebtedBookings)
            {
                AttributeDisplayHelper.DisplayAttributes<Booking>(booking);
            }
        }


        //Missing method for dynamically reading input for properties
    }
}
