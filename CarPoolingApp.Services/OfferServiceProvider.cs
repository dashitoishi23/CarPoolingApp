using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using CarPoolingApp.DataRepositories;
using CarPoolingApp.Models;

namespace CarPoolingApp.Services
{
    public class OfferServiceProvider
    {
        User userFound;
        Repository<User> userDataAccess = new Repository<User>();
        Repository<Offer> offerDataAccess = new Repository<Offer>();
        List<Offer> offers;
        public OfferServiceProvider(string userName)
        {
            userFound = userDataAccess.FindByProperty("userName", userName);
            offers = offerDataAccess.GetAllObjects();
        }
        public void CreateOffer(string startPoint, string viaPointsList, string EndPoint, int cost, int maxPeople, string carModel)
        {
            string[] points = viaPointsList.Split(' ');
            List<string> viaPoints = points.ToList();
            Offer newOffer = new Offer(cost, maxPeople, startPoint, viaPoints, EndPoint, carModel, userFound.Id);
            userFound.Offers.Add(newOffer.Id);
            userDataAccess.UpdateByProps((exitingObj) => {
                exitingObj.Offers = userFound.Offers;
            }, userFound.Id);
            offerDataAccess.Add(newOffer);
        }
        public List<Offer> ViewOffers()
        {
            List<Offer> Offers = new List<Offer>();
            foreach (string OfferId in userFound.Offers)
            {
                Offers.Add(offerDataAccess.FindByProperty("id", OfferId));
            }
            return Offers;
        }

        public List<Offer> ViewAllOffersOtherThanUser()
        {
            List<string> ids = userFound.Offers;
            List<Offer> offersToBook = new List<Offer>();
            if (ids.ToArray().Length == 0)
                return offers;
            foreach(Offer offer in offers)
            {
                foreach(string id in ids)
                {
                    if (!offer.Id.Equals(id))
                    {
                        offersToBook.Add(offer);
                    }
                }
                
            }
            return offersToBook;
        }

    }
}
