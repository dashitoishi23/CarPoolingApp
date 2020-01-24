using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Net.Http.Headers;
using ServiceStack.Text;
using RestSharp;
using Flurl.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using CarPoolingApp.Helpers;
using CarPoolingApp.Models;

namespace CarPoolingApp.DataRepositories
{
    public class Booking:IEntity
    {

        public BookingConfirmationType approvalStatus { get { return BookingConfirmationType.None; }set { } }
        public DateTime dateCreated { get; } = DateTime.Now;
        public string offerID { get; set; }
        public string startPoint { get; set; }
        public string endPoint { get; set;}

        //Why does is this still a constant value.
        public int distance { get { return 20; } }
        public int price { get; set; }
        public bool isPaid { get { return false; } set { } }

        public Booking(string offerID, string startPoint, string endPoint, int costPerKm)
        {
            this.offerID = offerID;
            this.startPoint = startPoint;
            this.endPoint = endPoint;
            this.price = this.distance * costPerKm;
        }


        }
    }
