using System;
using System.Collections.Generic;
using System.Text;

namespace CarPoolingApp.Helpers
{
    public class GuidGenerator
    {
        public static string GenerateID()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
