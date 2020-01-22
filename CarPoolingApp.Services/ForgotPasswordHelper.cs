using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using CarPoolingApp.Models;

namespace CarPoolingApp.Services
{
    public class ForgotPasswordHelper
    {
        // ## No UI interactions can be performed in the service layer
        public void ForgotPasswordService(ref OverallSupervisor supervisor, string userName)
        {
            // * ** @ - refer to the comments in BookingServiceProvider.cs
            var UserFound = supervisor.Accounts.FirstOrDefault(_ => (string.Equals(_.UserName, userName)));
            if(UserFound == null)
            {
                throw new Exception("User does not exist");
            }
            // ##
            Console.WriteLine("What was the name of your first school");

            // * ** - refer to the comments in BookingServiceProvider.cs
            supervisor.Accounts.Remove(UserFound);
            string Answer = Console.ReadLine();
            if (UserFound.SecurityAnswer.Equals(Answer))
            {
                // ##
                Console.WriteLine("Enter new password");

                // * ** - refer to the comments in BookingServiceProvider.cs 
                //##
                UserFound.Password = Console.ReadLine();
                // * ** - refer to the comments in BookingServiceProvider.cs
                supervisor.Accounts.Add(UserFound);
            }
            else
            {
                // ##
                Console.WriteLine("Wrong answer");
            }
        }
    }
}
