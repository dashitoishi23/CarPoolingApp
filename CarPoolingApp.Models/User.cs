using System;
using System.Collections.Generic;
using System.Text;
using CarPoolingApp.Helpers;

namespace CarPoolingApp.Models
{
    public class User
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string UserID { get; }
        public int Debt { get; set; }
        public string SecurityAnswer { get; set; }
        public List<string> Offers = new List<string>();
        public List<string> BookingIDs = new List<string>();
        public string WalletID { get; set; }

        public User(string userName, string password, string answer, string walletID)
        {
            this.UserName = userName;
            this.Password = password;
            this.Debt = 0;
            this.UserID = GUIDGenerator.GenerateID();
            this.SecurityAnswer = answer;
            this.WalletID = walletID;
        }
    }
}
