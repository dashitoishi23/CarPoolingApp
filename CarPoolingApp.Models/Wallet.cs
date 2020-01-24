using System;
using System.Collections.Generic;
using System.Text;
using CarPoolingApp.DataRepositories;

namespace CarPoolingApp.Models { 
    public class Wallet : Entity
    {
        public decimal Funds { get;  set; }
        public string UserID{ get; set; }

        public Wallet(string id)
        {
            this.Id = id;
        }
    }
}
