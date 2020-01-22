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
            Wallet.setWallet(money);
            return Wallet.Funds;
        }
    }
}
