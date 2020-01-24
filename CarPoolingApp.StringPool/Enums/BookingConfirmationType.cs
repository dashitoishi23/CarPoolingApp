using System;
using System.Collections.Generic;
using System.Text;

namespace CarPoolingApp.Models
{
    // Enums Style - Avoid plurals in Enum it looks odd while using them as properties in classes. Also Seperate the enums from regular models by putting then in a separate directory.
    public enum BookingConfirmationType
    {
        Accept,
        Reject,
        None
    }
}
