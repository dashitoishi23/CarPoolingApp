using System;
using CarPoolingApp.Helpers;

namespace CarPoolingApp.DataRepositories
{
    public class Entity
    {
        // Wrong case.
        public string Id { get; set; } = GuidGenerator.GenerateID();
        public string UserName { get; set; }
    }
}
