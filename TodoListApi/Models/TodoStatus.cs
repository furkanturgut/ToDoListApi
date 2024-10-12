namespace TodoListApi.Models
{
    public class TodoStatus
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public List<TodoTask> Tasks { get; set; }
    }
}
