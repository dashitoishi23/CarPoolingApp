using System;
using CarPoolingApp.Helpers;
using System.Collections.Generic;
using CarPoolingApp.DataRepositories;
using Newtonsoft.Json;
using CarPoolingApp.StringPool;
using CarPoolingApp.Models;

namespace CarPoolingApp.Services
{
    public class SignupHelper
    {
        User userFound;
        Repository<User> userDataAccess;
        Repository<Wallet> walletDataAccess;
        public SignupHelper(string userName)
        {
            userDataAccess = new Repository<User>();
            walletDataAccess = new Repository<Wallet>();
            userFound = userDataAccess.FindByProperty("UserName", userName);
        }
        public User SignupService(string userName, string password, string answer)
        {
            User newUser;
            while (true)
            {
                if (userFound != null)
                {
                    throw new Exception(ExceptionMessages.AlreadyExists);
                }
                Wallet newWallet = WalletServiceProvider.GetNewWallet();
                newUser = new User
                {
                    UserName = userName,
                    Password = password,
                    SecurityAnswer = answer
                };
                newUser.WalletID = newWallet.Id;
                userDataAccess.Add(newUser);
                newWallet.UserID = newUser.Id;
                walletDataAccess.Add(newWallet);
                break;
            }
            return newUser;   
        }
    }
}
