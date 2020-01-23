using System;
using System.Collections.Generic;
using System.Text;

namespace CarPoolingApp.DataRepositories
{
    public class Wallet : IEntity
    {
        public decimal funds { get;  set; }
        public string userID{ get; set; }

        public Wallet(string id)
        {
            this.id = id;
        }
        public void setWallet(decimal funds)
        {
            this.funds += funds;
        }
    }
}
