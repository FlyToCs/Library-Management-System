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
    public class MemberService : IMemberService
    {
        public bool BorrowBook(Member member, Book book)
        {
            if (book is null || member is null)
            {
                return false;
            }
            member.BorrowedBook.Add(book);
            book.IsBorrow = true;
            return true;
        }

        // public bool ReturnBook(Member member, Book book)
        // {
        //     book.IsBorrow = false;
        //     member.BorrowedBook.Remove(book);
        //     return true;
        // }


        public bool ReturnBook(Member member, Book book)
        {
            if (!member.BorrowedBook.Contains(book))
                return false;
            
            member.BorrowedBook.Remove(book);
            book.IsBorrow = false;

            return true;
        }


        public List<Book> GetListOfUserBooks(Member member)
        {
            return member.BorrowedBook;
        }

        public List<Book> GetListOfLibraryBooks()
        {
            var result = new List<Book>();

            foreach (var book in Storage.bookList)
            {
                if (!book.IsBorrow)
                    result.Add(book);
            }

            return result;
        }

        // public List<Book> GetListOfLibraryBooks()
        // {
        //
        //     for (int i = 0; i < Storage.bookList.Count; i++)
        //     {
        //         if (Storage.bookList[i].IsBorrow)
        //         {
        //             
        //             Storage.bookList.Remove(Storage.bookList[i]);
        //         }
        //     }
        //
        //     foreach (var book in Storage.bookList)
        //     {
        //         if (book.IsBorrow)
        //         {
        //
        //             Storage.bookList.Remove(Storage.bookList[i]);
        //         }
        //     }
        //     return Storage.bookList;
        // }

        public Book SearchBook(int bookId)
        {
            foreach (var book in Storage.bookList)
            {
                if (book.Id == bookId)
                {
                    return book;
                }
            }
            return null!;
        }
    }
}
