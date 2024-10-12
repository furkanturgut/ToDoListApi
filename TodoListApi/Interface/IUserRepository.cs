using TodoListApi.Models;

namespace TodoListApi.Interface
{
    public interface IUserRepository
    {
        User GetUser (int id);
        List<User> GetAllUsers ();

        bool CreateUser (User user);
        bool UpdateUser (User user);
        bool DeleteUser (User user);
        bool Save();
        
    }
}
