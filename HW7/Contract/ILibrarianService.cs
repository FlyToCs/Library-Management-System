using HW7.Entities;

namespace HW7.Contract;

public interface ILibrarianService
{
    List<User> GetListOfUsers();
    List<Book> GetListOfLibraryBooks();
    void DetailsOfMember(Member member);
}