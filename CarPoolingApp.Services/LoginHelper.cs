using System;
using System.Linq;
using CarPoolingApp.DataRepositories;
using CarPoolingApp.Helpers;
using Newtonsoft.Json;
using System.Collections.Generic;
using CarPoolingApp.StringPool;
using CarPoolingApp.Models;

namespace CarPoolingApp.Services
{
    public class LoginHelper
    {
        public User LoginValidator(string username, string password)
        {
            Repository<User> userDataAccess = new Repository<User>();
            var userFound = userDataAccess.FindByProperty("userName", username);
            if (userFound == null)
            {
                throw new Exception(ExceptionMessages.VoidExistance);
            }
            return (userFound.Password == password) ? userFound : null;
        }
    }
}