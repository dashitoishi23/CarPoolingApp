using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using CarPoolingApp.Models;

namespace CarPoolingApp.Services
{
    public class OfferServiceProvider
    {
        public void CreateOffer(ref OverallSupervisor supervisor, string userName, string startPoint, string viaPoints, string endPoint, int cost, int maxPeople, string carModel)
        {
            List<string> ViaPoints = (List<string>)viaPoints.Split(' ').Take(3);
            Offer NewOffer = new Offer(cost, maxPeople, startPoint, ViaPoints, endPoint, carModel);
            var UserFounds = supervisor.Accounts.Find(_ => (string.Equals(_.UserName, userName)));
            supervisor.Accounts.Remove(UserFounds);
            UserFounds.Offers.Add(NewOffer.ID);
            supervisor.Offers.Add(NewOffer);
            supervisor.Accounts.Add(UserFounds);
        }
        public List<Offer> ViewOffers(string userName, ref OverallSupervisor supervisor)
        {
            var UserFound = supervisor.Accounts.FirstOrDefault(_ => (string.Equals(_.UserName, userName)));
            List<Offer> Offers = new List<Offer>();
            foreach(string offerID in UserFound.Offers)
            {
                Offers.Add(supervisor.Offers.Find(_ => (string.Equals(_.ID, offerID))));
            }
            return Offers;
        }
    }
}
