using System;
using CarPoolingApp.DataRepositories;
using CarPoolingApp.Models;
using CarPoolingApp.StringPool;

namespace CarPoolingApp.Services
{
    public class ForgotPasswordHelper
    {
        public void ForgotPasswordService(string userName, string answer)
        {
            Repository<User> userDataAccess = new Repository<User>();
            var userFound = userDataAccess.FindByProperty("userName", userName);
            if(userFound == null)
            {
                throw new Exception(ExceptionMessages.VoidExistance);
            }
            if (userFound.SecurityAnswer.Equals(answer))
            {
                string password = Console.ReadLine();
                userFound.Password = password;
                userDataAccess.UpdateByProps((newObj) =>
                {
                    newObj.Password = password;
                }, userFound.Id);
            }
        }
    }
}
