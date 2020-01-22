using System;
using System.Collections.Generic;
using System.Text;

namespace CarPoolingApp.Models
{
    public class Wallet
    {
        public decimal Funds { get; private set; }
        public string UserID { get; set; }
        public string ID { get; set; }

        public Wallet(string ID)
        {
            this.ID = ID;
        }
        public void setWallet(decimal funds)
        {
            this.Funds += funds;
        }
    }
}
