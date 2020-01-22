using System;
using System.Collections.Generic;
using System.Text;
using CarPoolingApp.Models;

namespace CarPoolingApp.Services
{
    public class WalletServiceProvider
    {
        public decimal TopUpWallet(ref OverallSupervisor supervisor, string userName, decimal money)
        {

            // * ** @ - refer to the comments in BookingServiceProvider.cs
            var UserFound = supervisor.Accounts.Find(_ => (string.Equals(_.UserName, userName)));
            // ** @ - refer to the comments in BookingServiceProvider.cs
            var Wallet = supervisor.Wallets.Find(_ => (string.Equals(_.UserID, UserFound.UserID)));
            supervisor.Wallets.Remove(Wallet);
            Wallet.Funds += money;
            supervisor.Wallets.Add(Wallet);
            return Wallet.Funds;
        }
        public bool IsFundSufficient(ref OverallSupervisor supervisor, string userName, decimal toPay)
        {
            var UserFound = supervisor.Accounts.Find(_ => (string.Equals(_.UserName, userName)));
            var Wallet = supervisor.Wallets.Find(_ => (string.Equals(_.UserID, UserFound.UserID)));
            return (toPay >= Wallet.Funds);
        }
        public decimal DeductWalletFund(ref OverallSupervisor supervisor, string userName, decimal toPay)
        {
            var UserFound = supervisor.Accounts.Find(_ => (string.Equals(_.UserName, userName)));
            var Wallet = supervisor.Wallets.Find(_ => (string.Equals(_.UserID, UserFound.UserID)));
            supervisor.Wallets.Remove(Wallet);
            Wallet.Funds -= toPay;
            supervisor.Wallets.Add(Wallet);
            return Wallet.Funds;
        }
    }
}
