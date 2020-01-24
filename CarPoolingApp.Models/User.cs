using System;
using System.Collections.Generic;
using CarPoolingApp.Helpers;
using System.Text;
using CarPoolingApp.DataRepositories;

namespace CarPoolingApp.Models { 
    public partial class User:Entity
    {
        public string password { get; set; }
        public int debt { get { return 0; } set { } }
        public string securityAnswer { get; set; }
        public List<string> offers = new List<string>();
        public List<string> bookingIDs = new List<string>();
        public string walletID { get; set; }
    }
}
