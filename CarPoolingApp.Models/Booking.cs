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

namespace CarPoolingApp.Models
{
    public class Booking
    {
        [MyAttr]
        public string BookingID { get; set; }
        public BookingConfirmationTypes ApprovalStatus { get; set; }
        public DateTime DateCreated { get; }
        public string OfferID { get; set; }
        public string StartPoint { get; set; }
        public string EndPoint { get; set;}
        public int Distance { get; set; }
        public int Price { get; set; }

        public Booking(string offerID, string startPoint, string endPoint, int costPerKm)
        {
            this.OfferID = offerID;
            this.StartPoint = startPoint;
            this.EndPoint = endPoint;
            this.Distance = 20;
            this.DateCreated = DateTime.Now;
            this.ApprovalStatus = BookingConfirmationTypes.None;
            this.BookingID = IDGenerator.GenerateID();
            this.Price = this.Distance * costPerKm;
            this.GetType().GetProperties();
        }


        }
    }
