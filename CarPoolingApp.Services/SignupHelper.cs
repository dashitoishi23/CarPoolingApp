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
        public void SignupService(ref OverallSupervisor supervisor, string userName, string password, string answer)
        {
            User NewUser;
            while (true)
            {
                var UserDuplicate = supervisor.Accounts.FirstOrDefault(_ => (string.Equals(_.UserName, userName)));
                if (UserDuplicate != null)
                {
                    throw new Exception("User exists");
                }
                Wallet NewWallet = new Wallet(GUIDGenerator.GenerateID());
                NewUser = new User(userName, password, answer, NewWallet.ID);
                NewWallet.UserID = NewUser.UserID;
                supervisor.Wallets.Add(NewWallet);
                break;
            }
            supervisor.Accounts.Add(NewUser);
            
        }
    }
}
