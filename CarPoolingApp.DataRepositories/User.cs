using System;
using System.Collections.Generic;
using CarPoolingApp.Helpers;
using System.Text;

namespace CarPoolingApp.DataRepositories
{
    public partial class User:IEntity
    {
        public string password { get; set; }
        public int debt { get { return 0; } set { } }
        public string securityAnswer { get; set; }
        public List<string> offers = new List<string>();
        public List<string> bookingIDs = new List<string>();
        public string walletID { get; set; }
    }
}
