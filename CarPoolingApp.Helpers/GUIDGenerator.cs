using System;
using System.Collections.Generic;
using System.Text;

namespace CarPoolingApp.Helpers
{
    public class GUIDGenerator
    {
        public static string GenerateID()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
