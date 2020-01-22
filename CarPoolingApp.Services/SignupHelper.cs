using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using CarPoolingApp.Models;
using CarPoolingApp.Helpers;

namespace CarPoolingApp.Services
{
    public class SignupHelper
    {
        // *# Ids must be generated only once within the respective class and cannot be updated by any other class at any given point.
        // You can pass complete User Model instead of doing it individually.
        // *** When you want to get a default object of a particular class try using the factory approach. Example below.

        public void SignupService(ref OverallSupervisor supervisor, string userName, string password, string answer)
        {
            // @ - refer to the comments in BookingServiceProvider.cs
            User NewUser;
            while (true)
            {
                // * ** @- refer to the comments in BookingServiceProvider.cs 
                var UserDuplicate = supervisor.Accounts.FirstOrDefault(_ => (string.Equals(_.UserName, userName)));
                if (UserDuplicate != null)
                {
                    throw new Exception("User exists");
                }

                // * @- refer to the comments in BookingServiceProvider.cs 
                // *#
                // *** - This can be chaged to 
                // newWallet = WalletServiceProvider.GetNewWallet();
                // GetNewWallet() can be a static method that initializes this object.
                // This approach can be followed when you have no properties needed to create the object or the properties are automatically generated.
                Wallet NewWallet = new Wallet(IDGenerator.GenerateID());

                NewUser = new User(userName, password, answer, NewWallet.ID);
                NewWallet.UserID = NewUser.UserID;

                // * ** - refer to the comments in BookingServiceProvider.cs 
                supervisor.Wallets.Add(NewWallet);
                break;
            }
            // * ** - refer to the comments in BookingServiceProvider.cs 
            supervisor.Accounts.Add(NewUser);
            
        }
    }
}
