using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Intern_task.DTO;
using Intern_task.Enums;

#nullable disable

namespace Intern_task.Models
{
    public partial class Task
    {
        public Task()
        {
            
        }

        public Task(CreateTaskDTO dto)
        {
            Name = dto.Name;
            Description = dto.Description;
            Priority = dto.Priority;
            ProjectId = dto.ProjectId;
            TaskStatus = (short?) TaskStatusEnum.ToDo;
        }
        
        public int Id { get; set; }
        public string Name { get; set; }
        public short? TaskStatus { get; set; }
        public string Description { get; set; }
        public short? Priority { get; set; }
        public int? ProjectId { get; set; }
        public DateTime? CompletionDate { get; set; }

        [JsonIgnore]
        public virtual Project Project { get; set; }
    }
}
