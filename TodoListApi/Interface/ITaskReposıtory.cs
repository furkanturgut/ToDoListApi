using TodoListApi.Models;

namespace TodoListApi.Interface
{
    public interface ITaskReposıtory
    {
        TodoTask GetTask(int TaskId);
        List<TodoTask> GetAllTasks();
        List<TodoTask> GetCompletedTasks();
        List<TodoTask> GetIncompleteTasks();

        bool CreateTask(TodoTask task);
        bool UpdateTask(TodoTask task);
        TodoStatus GetStatus (int StatusId);
        bool DeleteTask(TodoTask task);
        bool TaskExist(int TaskId);
        bool Save();

    }
}
