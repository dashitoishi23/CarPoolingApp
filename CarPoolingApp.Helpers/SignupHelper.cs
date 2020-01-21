using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using CarPoolingApp.Models;

namespace CarPoolingApp.Helpers
{
    public class SignupHelper
    {
        public void SignupService(ref OverallSupervisor supervisor, string userName, string password, string answer)
        {
            User NewUser;
            while (true)
            {
                var UserDuplicate = supervisor.Accounts.FirstOrDefault(_ => (string.Equals(_.UserName, userName)));
                if (UserDuplicate != null)
                {
                    throw new Exception("User exists");
                }
                NewUser = new User(userName, password, answer);
                break;
            }
            supervisor.Accounts.Add(NewUser);
            
        }
    }
}
