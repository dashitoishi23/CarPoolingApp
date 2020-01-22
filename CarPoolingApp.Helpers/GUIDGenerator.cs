using System;
using System.Collections.Generic;
using System.Text;

namespace CarPoolingApp.Helpers
{
    // @
    public class IDGenerator
    {
        // Rather this can be called GuidGenerator. You may be using this as an Id or for something else. But esenstially this is generating Guid
        public static string GenerateID()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
