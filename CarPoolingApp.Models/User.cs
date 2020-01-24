using System;
using System.Collections.Generic;
using CarPoolingApp.Helpers;
using System.Text;
using CarPoolingApp.DataRepositories;

namespace CarPoolingApp.Models { 
    public partial class User:Entity
    {
        public string Password { get; set; }
        public int Debt { get { return 0; } set { } }
        public string SecurityAnswer { get; set; }
        public List<string> Offers = new List<string>();
        public List<string> BookingIDs = new List<string>();
        public string WalletID { get; set; }
    }
}
