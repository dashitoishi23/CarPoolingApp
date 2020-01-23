using System;
using CarPoolingApp.DataRepositories;
using CarPoolingApp.StringPool;

namespace CarPoolingApp.Services
{
    public class ForgotPasswordHelper
    {
        public void ForgotPasswordService(string userName, string answer)
        {
            Repository<User> userDataAccess = new Repository<User>();
            var userFound = userDataAccess.FindByName(userName);
            if(userFound == null)
            {
                throw new Exception(ExceptionMessages.VoidExistance);
            }
            if (userFound.securityAnswer.Equals(answer))
            {
                string password = Console.ReadLine();
                userFound.password = password;
                userDataAccess.UpdateByName(userFound);
            }
        }
    }
}
