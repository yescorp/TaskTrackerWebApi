using System;

namespace Intern_task.DTO
{
    public class CreateTaskDTO
    {
        public String Name { get; set; }
        public String Description { get; set; }
        public short Priority { get; set; }
        public int ProjectId { get; set; }
        
    }
}