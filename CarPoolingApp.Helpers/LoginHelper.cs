﻿using System;
using System.Linq;
using CarPoolingApp.Models;

namespace CarPoolingApp.Helpers
{
    public class LoginHelper
    {
        User AccountToBeValidated;
        OverallSupervisor Supervisor;

        public LoginHelper(User user, OverallSupervisor supervisor)
        {
            this.AccountToBeValidated = user;
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