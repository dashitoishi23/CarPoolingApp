using System;
using System.Collections.Generic;
using System.Text;
using CarPoolingApp.DataRepositories;
using CarPoolingApp.Helpers;

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
            this.userFound = userDataAccess.FindByName(userName);
            this.wallet = walletDataAccess.FindById(userFound.walletID);
        }
        public decimal TopUpWallet(decimal money)
        {
            this.wallet.funds += money;
            walletDataAccess.UpdateById(this.wallet);
            return this.wallet.funds;
        }
        public bool IsFundSufficient(decimal toPay)
        {
            return (toPay >= this.wallet.funds);
        }
        public static Wallet GetNewWallet()
        {
            return new Wallet(GuidGenerator.GenerateID());
        }
        public decimal DeductWalletFund(decimal toPay)
        {
            this.wallet.funds -= toPay;
            walletDataAccess.UpdateById(this.wallet);
            return this.wallet.funds;
        }
    }
}
