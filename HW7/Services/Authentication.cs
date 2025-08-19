using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HW7.Contract;
using HW7.Database;
using HW7.Entities;

namespace HW7.Services
{
    public class Authentication : IAuthentication
    {
        public User? Login(string email, string password)
        {
            foreach (var user in Storage.userList)
            {
                if (user.Email == email && user.Password == password)
                {
                    return user;
                }
            }
            return null;
        }

        public User? Register(string email, string password, RoleEnum role)
        {
            return new User(email, password, role);
        }
    }
}
