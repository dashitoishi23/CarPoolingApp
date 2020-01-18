using System;
using System.Collections.Generic;
using CarPoolingApp.Models;

namespace CarPoolingApp.Services
{
    public class OfferServiceProvider
    {
        public void CreateOffer(ref OverallSupervisor supervisor, int costPerKm, int maxPeople, string startPoint, List<string> viaPoints, string endPoint, string carModel, ref User user)
        {
            Offer NewOffer = new Offer(costPerKm, maxPeople, startPoint, viaPoints, endPoint, carModel);
            supervisor.Offers.Add(NewOffer);
            user.Offers.Add(NewOffer.ID);
        }
        public List<Offer> ViewOffers(OverallSupervisor supervisor)
        {
            return supervisor.Offers;
        }
        public List<Offer> ViewAllUserOffers(OverallSupervisor supervisor, User user)
        {
            List<Offer> Offers = new List<Offer>();
            foreach(string offerID in user.Offers)
            {
                Offer Offer = supervisor.Offers.Find(_=>(_.ID.Equals(offerID)));
                user.Offers.Add(Offer.ID);
            }
            return Offers;
        }

    }
}
