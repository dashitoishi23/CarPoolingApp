using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using CarPoolingApp.Models;

namespace CarPoolingApp.Services
{
    public class ForgotPasswordHelper
    {
        public void ForgotPasswordService(ref OverallSupervisor supervisor, string userName)
        {
            var UserFound = supervisor.Accounts.FirstOrDefault(_ => (string.Equals(_.UserName, userName)));
            if(UserFound == null)
            {
                throw new Exception("User does not exist");
            }
            Console.WriteLine("What was the name of your first school");
            supervisor.Accounts.Remove(UserFound);
            string Answer = Console.ReadLine();
            if (UserFound.SecurityAnswer.Equals(Answer))
            {
                Console.WriteLine("Enter new password");
                UserFound.Password = Console.ReadLine();
                supervisor.Accounts.Add(UserFound);
            }
            else
            {
                Console.WriteLine("Wrong answer");
            }
        }
    }
}
