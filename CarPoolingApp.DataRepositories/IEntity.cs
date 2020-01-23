using System;
using CarPoolingApp.Helpers;

namespace CarPoolingApp.DataRepositories
{
    public class IEntity
    {
        public string id { get; set; } = GuidGenerator.GenerateID();
        public string userName { get; set; }
    }
}
