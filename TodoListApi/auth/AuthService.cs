using Microsoft.EntityFrameworkCore;
using TodoListApi.Data;

namespace TodoListApi.auth
{
    public class AuthService
    {
        private readonly DataContext _context;

        public AuthService(DataContext context)
        {
            this._context = context;
        }

        public bool AuthenticateUser(string username, string password)
        {

            var user = _context.users.FirstOrDefault(un => un.Username == username);
            if (user == null || user.Password != password)
            {
                return false;
            }
            return true;
        }
    }
}
