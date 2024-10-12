using Microsoft.EntityFrameworkCore;
using TodoListApi.Data;
using TodoListApi.Interface;
using TodoListApi.Models;

namespace TodoListApi.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            this._context = context;
        }

        public bool CreateUser(User user)
        {
            _context.Add(user);
            return Save();
        }

        public bool DeleteUser(User user)
        {
            _context.Remove(user);
            return Save();
        }

        public List<User> GetAllUsers()
        {
            return _context.users.ToList();
        }

        public User GetUser(int id)
        {
            return _context.users.FirstOrDefault(u => u.Id == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateUser(User user)
        {
            _context.Update(user);
            return Save();
        }
    }
}
