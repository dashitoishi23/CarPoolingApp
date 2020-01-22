using System;
using System.Linq;
using CarPoolingApp.Models;

namespace CarPoolingApp.Services
{
    public class LoginHelper
    {
        OverallSupervisor Supervisor;

        public LoginHelper(OverallSupervisor supervisor)
        {
            this.Supervisor = supervisor;
        }

        public User LoginValidator(string username, string password)
        {
            var UserFound = this.Supervisor.Accounts.FirstOrDefault(_ => (string.Equals(_.UserName, username)));
            if (UserFound == null)
            {
                return UserFound;
            }
            return (UserFound.UserName == username && UserFound.Password == password) ? UserFound : null;
        }
    }
}