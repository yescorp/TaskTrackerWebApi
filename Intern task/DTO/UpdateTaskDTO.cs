using Intern_task.Enums;

#nullable enable
namespace Intern_task.DTO
{
    public class UpdateTaskDTO
    {
        public int TaskId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public short? Priority { get; set; }
    }
}