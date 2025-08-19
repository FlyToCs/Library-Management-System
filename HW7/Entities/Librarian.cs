using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW7.Entities
{
    public class Librarian : User
    {



        public Librarian(string firstName, string lastName, string email, string password, RoleEnum role) : base(firstName, lastName, email, password, RoleEnum.Librarian)
        {
            
        }
    }
}
