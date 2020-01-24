using System;
using CarPoolingApp.Helpers;

namespace CarPoolingApp.DataRepositories
{
    public class IEntity
    {
        // Wrong case.
        public string id { get; set; } = GuidGenerator.GenerateID();
        public string userName { get; set; }
    }
}
