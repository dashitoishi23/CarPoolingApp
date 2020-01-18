using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using CarPoolingApp.Models;

namespace CarPoolingApp.Helpers
{
    public class SignupHelper
    {
        public User SignupService(OverallSupervisor supervisor)
        {
            User NewUser;
            while (true)
            {
                Console.WriteLine("Enter a username");
                string UserName = Console.ReadLine();
                var UserDuplicate = supervisor.Accounts.FirstOrDefault(_ => (string.Equals(_.UserName, UserName)));
                Console.WriteLine("Enter a password");
                string Password = Console.ReadLine();
                NewUser = new User(UserName, Password);
                if (UserDuplicate != null)
                {
                    Console.WriteLine("Username exists");
                    continue;
                }
                break;
            }
                return NewUser;
            
        }
    }
}
