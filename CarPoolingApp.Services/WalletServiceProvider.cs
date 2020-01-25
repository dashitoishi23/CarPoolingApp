using System;
using System.Collections.Generic;
using System.Text;
using CarPoolingApp.DataRepositories;
using CarPoolingApp.Helpers;
using CarPoolingApp.Models;

namespace CarPoolingApp.Services
{
    public class WalletServiceProvider
    {
        User userFound;
        Wallet wallet;
        Repository<User> userDataAccess = new Repository<User>();
        Repository<Wallet> walletDataAccess = new Repository<Wallet>();
        public WalletServiceProvider(string userName)
        {
            this.userFound = userDataAccess.FindByProperty("userName", userName);
            this.wallet = walletDataAccess.FindByProperty("id", userFound.WalletID);
        }
        public decimal TopUpWallet(decimal money)
        {
            this.wallet.Funds += money;
            walletDataAccess.UpdateByProps((exitingObj) => {
                exitingObj.Funds = wallet.Funds;
            }, this.wallet.Id);
            return this.wallet.Funds;
        }
        public bool IsFundSufficient(decimal toPay)
        {
            return (toPay >= this.wallet.Funds);
        }
        public static Wallet GetNewWallet()
        {
            return new Wallet(GuidGenerator.GenerateID());
        }
        public decimal DeductWalletFund(decimal toPay)
        {
            this.wallet.Funds -= toPay;
            walletDataAccess.UpdateByProps((newObj) =>
            {
                newObj.Funds = this.wallet.Funds;
            }, this.wallet.Id);
            return this.wallet.Funds;
        }
    }
}
