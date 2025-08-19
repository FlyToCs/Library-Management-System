using HW7.Entities;

namespace HW7.Contract;

public interface IMemberService
{
    bool BorrowBook(Member member, Book book);
    bool ReturnBook(Member member, Book book);
    List<Book> GetListOfUserBooks(Member member);
    List<Book> GetListOfLibraryBooks();
    Book SearchBook(int bookId);

}