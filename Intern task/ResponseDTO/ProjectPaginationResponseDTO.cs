using System.Collections;
using System.Collections.Generic;
using Intern_task.Models;

namespace Intern_task.DTO
{
    public class ProjectPaginationResponseDTO
    {
        public ProjectPaginationResponseDTO(IEnumerable<Project> projects, int totalNumberOfPages)
        {
            this.projects = projects;
            this.TotalNumberOfPages = totalNumberOfPages;
        }
        public IEnumerable<Project> projects { get; set; }
        public int TotalNumberOfPages { get; set; }
    }
}