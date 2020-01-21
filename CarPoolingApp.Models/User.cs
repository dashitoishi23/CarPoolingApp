using System;
using System.Collections.Generic;
using System.Text;

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

        public User(string userName, string password, string answer)
        {
            this.UserName = userName;
            this.Password = password;
            this.Debt = 0;
            this.UserID = "USR" + DateTime.Now.ToString();
            this.SecurityAnswer = answer;
        }
    }
}
