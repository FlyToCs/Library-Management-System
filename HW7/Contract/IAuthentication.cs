using HW7.Entities;

namespace HW7.Contract;

public interface IAuthentication
{
    User? Login(string email, string password);
    User? Register(string email, string password, RoleEnum role);
}