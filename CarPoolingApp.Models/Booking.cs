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
using CarPoolingApp.DataRepositories;
using CarPoolingApp.StringPool;

namespace CarPoolingApp.Models { 
    public class Booking:Entity
    {

        public BookingConfirmationType ApprovalStatus { get { return BookingConfirmationType.None; }set { } }
        public DateTime DateCreated { get; } = DateTime.Now;
        public string OfferId { get; set; }
        public string StartPoint { get; set; }
        public string EndPoint { get; set;}
        public int Distance { get {
                Random random = new Random();
                return random.Next(5, 100);
            } }
        public int Price { get; set; }
        public bool IsPaid { get { return false; } set {  } }

        public Booking(string offerId, string startPoint, string endPoint, int costPerKm)
        {
            this.OfferId = offerId;
            this.StartPoint = startPoint;
            this.EndPoint = endPoint;
            this.Price = this.Distance * costPerKm;
        }
        public void SetApprovalStatus(BookingConfirmationType type)
        {
            this.ApprovalStatus = type;
        }


        }
    }
