using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using CarPoolingApp.DataRepositories;

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
            userFound = userDataAccess.FindByName(userName);
            offers = offerDataAccess.GetAllObjects();
        }
        public void CreateOffer(string startPoint, string viaPointsList, string endPoint, int cost, int maxPeople, string carModel)
        {
            string[] points = viaPointsList.Split(' ');
            List<string> viaPoints = points.ToList();
            Offer newOffer = new Offer(cost, maxPeople, startPoint, viaPoints, endPoint, carModel, userFound.id);
            userFound.offers.Add(newOffer.id);
            userDataAccess.UpdateByName(userFound);
            offerDataAccess.Add(newOffer);
        }
        public List<Offer> ViewOffers()
        {
            List<Offer> Offers = new List<Offer>();
            foreach (string offerID in userFound.offers)
            {
                Offers.Add(offerDataAccess.FindById(offerID));
            }
            return Offers;
        }

        public List<Offer> ViewAllOffersOtherThanUser()
        {
            List<string> ids = userFound.offers;
            List<Offer> offersToBook = new List<Offer>();
            if (ids.ToArray().Length == 0)
                return offers;
            foreach(Offer offer in offers)
            {
                foreach(string id in ids)
                {
                    if (!offer.id.Equals(id))
                    {
                        offersToBook.Add(offer);
                    }
                }
                
            }
            return offersToBook;
        }

    }
}
