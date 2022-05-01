using Intern_task.DTO;
using Intern_task.Models;

namespace Intern_task.ExtensionMethods
{
    public static class ProjectExtensions
    {
        public static void UpdateProject(this Project project, UpdateProjectDTO dto)
        {
            project.Name = dto.NewName ?? project.Name;
            project.Priority = dto.NewPriority ?? project.Priority;
        }
    }
}