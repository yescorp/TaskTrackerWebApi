using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Intern_task.DTO;
using Intern_task.Enums;

#nullable disable

namespace Intern_task.Models
{
    public partial class Project
    {
        public Project()
        {
            Tasks = new HashSet<Task>();
        }
        
        public Project(CreateProjectDTO dto)
        {
            Name = dto.Name;
            Priority = dto.Priority;
            Status = (short?) ProjectStatusEnum.NotStarted;
            CompletionDate = null;
            StartDate = DateTime.Now;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? CompletionDate { get; set; }
        public short? Status { get; set; }
        public int? Priority { get; set; }

        [JsonIgnore]
        public virtual ICollection<Task> Tasks { get; set; }
    }
}
