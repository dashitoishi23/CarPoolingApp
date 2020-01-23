using System;
using CarPoolingApp.Helpers;
using System.Collections.Generic;
using CarPoolingApp.DataRepositories;
using Newtonsoft.Json;
using CarPoolingApp.StringPool;

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
            userFound = userDataAccess.FindByName(userName);
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
                    userName = userName,
                    password = password,
                    securityAnswer = answer
                };
                newUser.walletID = newWallet.id;
                userDataAccess.Add(newUser);
                newWallet.userID = newUser.id;
                walletDataAccess.Add(newWallet);
                break;
            }
            return newUser;   
        }
    }
}
