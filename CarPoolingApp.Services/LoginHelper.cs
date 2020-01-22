using System;
using System.Linq;
using CarPoolingApp.Models;

namespace CarPoolingApp.Services
{
    public class LoginHelper
    {
        // Sign up, Login and Forgot password fall into the same 'context' they all can be in one class.
        // 'Context', in reference to all these actions doesnt need a logged in user context.

        OverallSupervisor Supervisor;

        public LoginHelper(OverallSupervisor supervisor)
        {
            this.Supervisor = supervisor;
        }

        public User LoginValidator(string username, string password)
        {
            // * ** @ - refer to the comments in BookingServiceProvider.cs
            var UserFound = this.Supervisor.Accounts.FirstOrDefault(_ => (string.Equals(_.UserName, username)));
            if (UserFound == null)
            {
                return UserFound;
            }
            return (UserFound.UserName == username && UserFound.Password == password) ? UserFound : null;
        }
    }
}