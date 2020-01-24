using System;
using System.Collections.Generic;
using System.Text;
using CarPoolingApp.Helpers;

namespace CarPoolingApp.DataRepositories
{
    public class Offer:IEntity
    {
        // Wrong case
        public int costPerKm { get; set; }
        public int maxPeople { get; set; }
        public string startPoint { get; set; }
        public List<string> viaPoints { get; set; } = new List<string>();
        public string endPoint { get; set; }
        public string carModel { get; set; }
        public string userID { get; set; }

        public Offer(int costPerKm, int maximumPeople, string startPoint, List<string> viaPoints, string endPoint, string carModel, string userID)
        {
            this.costPerKm = costPerKm;
            this.maxPeople = maximumPeople;
            this.startPoint = startPoint;
            this.viaPoints = viaPoints;
            this.endPoint = endPoint;
            this.carModel = carModel;
            this.userID = userID;
        }

    }
}
