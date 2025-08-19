using HW6.Extentions;
using HW7.Contract;
using HW7.Database;
using HW7.Entities;

namespace HW7.Services;

public class LibrarianService : ILibrarianService
{
    public List<User> GetListOfUsers()
    {
        return Storage.userList;
    }

    public List<Book> GetListOfLibraryBooks()
    {

        for (int i = 0; i < Storage.bookList.Count; i++)
        {
            if (Storage.bookList[i].IsBorrow)
            {
                Storage.bookList.Remove(Storage.bookList[i]);
            }
        }
        return Storage.bookList;
    }

    public void DetailsOfMember(Member member)
    {
        ConsolePainter.WriteTable(new List<Member>(){member});
    }
}