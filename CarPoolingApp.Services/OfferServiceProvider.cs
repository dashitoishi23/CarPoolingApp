using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using CarPoolingApp.Models;

namespace CarPoolingApp.Services
{
    public class OfferServiceProvider
    {
        //Use constructor to intantiate Supervisor and user model and make them properties of this class. For explaination refer to BookinsServiceProvider.cs

        // ref keyword need not be used for objects.
        public void CreateOffer(ref OverallSupervisor supervisor, string userName, string startPoint, string viaPoints, string endPoint, int cost, int maxPeople, string carModel)
        {
            string[] points = viaPoints.Split(' ');
            // @ -refer to the comments in BookingServiceProvider.cs
            List<string> ViaPoints = points.ToList();
            // @ -refer to the comments in BookingServiceProvider.cs
            Offer NewOffer = new Offer(cost, maxPeople, startPoint, ViaPoints, endPoint, carModel);
            // @ - refer to the comments in BookingServiceProvider.cs
            var UserFounds = supervisor.Accounts.Find(_ => (string.Equals(_.UserName, userName)));
            supervisor.Accounts.Remove(UserFounds);
            UserFounds.Offers.Add(NewOffer.ID);
            supervisor.Offers.Add(NewOffer);
            supervisor.Accounts.Add(UserFounds);
        }

        // ref keyword need not be used for objects.
        public List<Offer> ViewOffers(string userName, ref OverallSupervisor supervisor)
        {
            // @ - refer to the comments in BookingServiceProvider.cs
            var UserFound = supervisor.Accounts.FirstOrDefault(_ => (string.Equals(_.UserName, userName)));
            // @ - refer to the comments in BookingServiceProvider.cs
            List<Offer> Offers = new List<Offer>();
            foreach (string offerID in UserFound.Offers)
            {
                Offers.Add(supervisor.Offers.Find(_ => (string.Equals(_.ID, offerID))));
            }
            return Offers;
        }
    }
}
