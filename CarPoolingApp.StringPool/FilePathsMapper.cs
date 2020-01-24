using System;
using System.Collections.Generic;
using System.Text;

namespace CarPoolingApp.StringPool
{
    public class FilePathsMapper
    {
        public static Dictionary<string, string> fileMapper = new Dictionary<string, string>()
        {
            { "User", "User.json"},
            { "Booking", "Booking.json"},
            { "Wallet", "Wallet.json"},
            { "Offer", "Offer.json"}
        };
    }
}
