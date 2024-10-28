using Microsoft.EntityFrameworkCore;
using TodoListApi.Data;
using TodoListApi.Interface;
using TodoListApi.Models;

namespace TodoListApi.Repository
{
    public class TaskReposıtory : ITaskReposıtory
    {
        private readonly DataContext _context;

        public TaskReposıtory(DataContext dataContext)
        {
            this._context = dataContext;
        }

        public bool CreateTask(TodoTask task)
        {
         
            _context.Add(task);
            return Save();
        }

        public bool DeleteTask(TodoTask task)
        {
            _context.Remove(task);
            return Save();
        }

        public List<TodoTask> GetAllTasks()
        {
            return _context.todoTasks.Include(t => t.TodoStatus).ToList();       
        }

        public List<TodoTask> GetCompletedTasks()
        {
            return _context.todoTasks.Include(t=> t.TodoStatus).Where(ts => ts.TodoStatus.Id == 1 ).ToList();
        }

        public List<TodoTask> GetIncompleteTasks()
        {
            return _context.todoTasks.Include(t=> t.TodoStatus).Where(ts => ts.TodoStatus.Id == 2).ToList();    
        }

        public TodoStatus GetStatus(int StatusId)
        {
            return _context.todoStatuses.Find(StatusId);
          }

        public TodoTask GetTask(int TaskId)
        {
            return _context.todoTasks.Find(TaskId);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool TaskExist(int TaskId)
        {
            return _context.todoTasks.Any(t => t.Id == TaskId);
        }

        public bool UpdateTask(TodoTask task)
        {
            _context.Update(task);
            return Save();
        }
    }
}
