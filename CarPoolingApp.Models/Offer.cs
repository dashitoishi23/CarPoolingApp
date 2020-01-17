using System;
using System.Collections.Generic;
using System.Text;

namespace CarPoolingApp.Models
{
    class Offer
    {
        public int CostPerKm { get; set; };
        public int MaximumPeople { get; set; };
        public string StartPoint { get; set; };
        public List<string> ViaPoints = new List<string>();
        public string EndPoint { get; set; }
        public string ID { get; set; }
        public string CarModel { get; set; }

        public Offer(int costPerKm, int maximumPeople, string startPoint, List<string> viaPoints, string endPoint, string carModel)
        {
            this.CostPerKm = costPerKm;
            this.MaximumPeople = maximumPeople;
            this.StartPoint = startPoint;
            this.ViaPoints = viaPoints;
            this.EndPoint = endPoint;
            this.CarModel = carModel;
        }

    }
}
