using TodoListApi.Models;

namespace TodoListApi.Dto_s
{
    public class TaskDto
    {
        public int Id { get; set; }
        public string Task { get; set; }
        public string TaskStatus { get; set; }
    }
}
