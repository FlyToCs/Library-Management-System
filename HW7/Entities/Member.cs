using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW7.Entities
{
    public class Member : User
    {
        public List<Book> BorrowedBook { get; set; }
        public Member(string firstName, string lastName, string email, string password, RoleEnum role, List<Book> borrowedBook) : base(firstName, lastName, email, password, RoleEnum.Member)
        {
            BorrowedBook = borrowedBook ?? new List<Book>();
        }

    }
}
