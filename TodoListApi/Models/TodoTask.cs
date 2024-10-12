namespace TodoListApi.Models
{
    public class TodoTask
    {
        public int Id { get; set; }
        public string Task { get; set; }
        public User User { get; set; }
        public TodoStatus TodoStatus { get; set; }
    }
}
