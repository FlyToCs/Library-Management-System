using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HW7.Entities;

namespace HW7.Database
{
    public static class Storage
    {
        public static List<User> userList = new List<User>();
        public static User? CurrentUser;

        public static List<Book> bookList = new List<Book>();
        public static Book? CurrentBook;



        static Storage()
        {
            //add some users
            userList.Add(new Librarian("mohammad", "Dehghani", "m@gmail.com", "123456", RoleEnum.Librarian));
            userList.Add(new Member("Ali", "Akbari", "a@gmail.com", "123", RoleEnum.Member, null!));
            userList.Add(new Member("nima", "Kazemi", "n@gmail.com", "123", RoleEnum.Member, null!));


            //add some books
            bookList.Add(new Book("Winners", "Max", "How to win"));
            bookList.Add(new Book("Better Version", "Karen", "Motivation"));
            bookList.Add(new Book("Life", "Rita", "What is life"));
            bookList.Add(new Book("New Race", "max", "Competition"));
            bookList.Add(new Book("Angular", "max", "Good front framework"));
            bookList.Add(new Book("C#", "max", "Learn c# language"));
            bookList.Add(new Book("Html", "max", "Basic of html"));
            bookList.Add(new Book("Python 101", "alex", "Beginner guide py"));
            bookList.Add(new Book("React Mastery", "sara", "Build web apps"));
            bookList.Add(new Book("Java Fundamentals", "john", "Core of Java"));
            bookList.Add(new Book("Data Science Basics", "lisa", "data analysis"));
            bookList.Add(new Book("Design Patterns", "eric", "solutions software"));
            bookList.Add(new Book("Docker Deep Dive", "mark", "Containerization"));
            bookList.Add(new Book("Machine Learning", "nina", "ML applications"));
            bookList.Add(new Book("Kotlin for Android", "paul", "Android development"));
            bookList.Add(new Book("CSS Secrets", "lea", "Tips for styling"));
            bookList.Add(new Book("Algorithms Unlocked", "thomas", "algorithms"));


        }
    }
}
