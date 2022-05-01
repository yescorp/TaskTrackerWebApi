using Intern_task.DTO;
using Intern_task.Models;

namespace Intern_task.ExtensionMethods
{
    public static class TaskExtensions
    {
        public static void UpdateTask(this Task task, UpdateTaskDTO dto)
        {
            task.Name = dto.Name ?? task.Name;
            task.Priority = dto.Priority ?? task.Priority;
            task.Description = dto.Description ?? task.Description;
        }
    }
}