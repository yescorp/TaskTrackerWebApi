#nullable enable
using System;

namespace Intern_task.DTO
{
    public class UpdateProjectDTO
    {
        public int ProjectId { get; set; }
        public string? NewName { get; set; }
        public int? NewPriority { get; set; }
    }
}