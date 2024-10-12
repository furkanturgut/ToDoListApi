using Microsoft.EntityFrameworkCore;
using TodoListApi.Models;

namespace TodoListApi.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options ) :base(options)
        {

        }
        public DbSet<TodoStatus> todoStatuses { get; set; }
        public DbSet<User> users { get; set; }
        public DbSet<TodoTask> todoTasks{ get; set; }


    }
}
