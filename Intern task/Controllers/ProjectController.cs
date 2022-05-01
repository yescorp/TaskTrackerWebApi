
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Intern_task.DTO;
using Intern_task.Enums;
using Intern_task.ExtensionMethods;
using Intern_task.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic.CompilerServices;

namespace Intern_task.Controllers
{
    
    [ApiController]
    [Route("[controller]")]
    public class ProjectController : Controller
    {
        private readonly TaskTrackerContext _context;
        
        public ProjectController(TaskTrackerContext context)
        {
            _context = context;
        }
        
        [HttpGet]
        [Route("get")]
        public ActionResult<IEnumerable<Project>> Get()
        {
            return Ok(_context.Projects.ToList());
        }
        
        [HttpGet]
        [Route("filter")]
        public ActionResult<ProjectPaginationResponseDTO> FilterProjects([FromQuery] DateTime? dateRangeStart, 
            [FromQuery] DateTime? dateRangeEnd, [FromQuery] int? startPriority, [FromQuery] int? endPriority,
            [FromQuery] ProjectStatusEnum? status, [FromQuery] string? searchText, 
            [FromQuery] int? page = 1, [FromQuery] int? limit = 10 
        )
        {
            try
            {
                //get the projects by filters
                List<Project> projects = (List<Project>) _context.Projects.ToList().Where(p =>
                {
                    List<Task> tasks = _context.Tasks.ToList().Where(t =>
                            searchText == null || t.Name.Contains(searchText) || t.Description.Contains(searchText))
                        .ToList();

                    return (dateRangeStart == null || p.StartDate > dateRangeStart!)
                           && (p.CompletionDate == null || dateRangeEnd == null || p.CompletionDate < dateRangeEnd)
                           && (startPriority == null || p.Priority > startPriority)
                           && (endPriority == null || p.Priority < endPriority)
                           && (status == null || (int) status == p.Status)
                           && (searchText == null || tasks.Count > 0 || p.Name.Contains(searchText));

                }).ToList();

                //pagination of the projects list
                int totalNumberOfPages = (int) ((projects.Count + limit - 1) / limit)!;
                projects = projects.Skip((int) ((page - 1) * limit)!).Take((int) limit).ToList();

                ProjectPaginationResponseDTO result = new ProjectPaginationResponseDTO(projects, totalNumberOfPages);

                return Ok(result);
            }
            catch (NullReferenceException ex)
            {
                return BadRequest();
            }
        }
        
        [HttpGet]
        [Route("get/tasks")]
        public ActionResult<IEnumerable<Task>> GetTasks([FromQuery] ProjectIdDto dto)
        {
            try
            {
                return Ok(_context.Tasks.ToList().Where(t => t.ProjectId == dto.ProjectId));
            }

            catch (NullReferenceException ex)
            {
                return BadRequest();
            }
        }
        
        [HttpPost]
        [Route("create")]
        public ActionResult<Project> CreateProject([FromBody] CreateProjectDTO projectDto)
        {
            Project project = new Project(projectDto);

            _context.Projects.Add(project);
            _context.SaveChanges();

            return Ok(project);
        }
        
        [HttpPatch]
        [Route("update")]
        public ActionResult UpdateProject([FromBody] UpdateProjectDTO dto)
        {
            if (dto == null)
            {
                return BadRequest();
            }
            
            Project project = _context.Projects.FirstOrDefault(p => p.Id == dto.ProjectId);

            if (project == null)
            {
                return BadRequest();
            }
            
            project.UpdateProject(dto);

            _context.Update(project);
            _context.SaveChanges();

            return Ok();
        }
        
        [HttpPost]
        [Route("start")]
        public IActionResult StartProject([FromBody] ProjectIdDto dto)
        {
            Project project = _context.Projects.FirstOrDefault(p => p.Id == dto.ProjectId);
            
            if (project == null)
            {
                return BadRequest("Project not found");
            }
            
            project.Status = (short?) ProjectStatusEnum.Active;

            _context.Update(project);
            _context.SaveChanges();
            
            return Ok();
        }
        
        [HttpPost]
        [Route("finish")]
        public IActionResult FinishProject([FromBody] ProjectIdDto dto)
        {
            Project project = _context.Projects.FirstOrDefault(p => p.Id == dto.ProjectId);

            if (project == null)
            {
                return BadRequest("Project not found");
            }
            
            project.Status = (short?) ProjectStatusEnum.Completed;
            project.CompletionDate = DateTime.Now;

            _context.Update(project);
            _context.SaveChanges();
            
            return Ok();
        }

        [HttpDelete]
        [Route("delete")]
        public IActionResult DeleteProject([FromBody] ProjectIdDto dto)
        {
            _context.RemoveRange(_context.Tasks.ToList().Where(t => t.ProjectId == dto.ProjectId));
            _context.Remove(_context.Projects.FirstOrDefault(p => p.Id == dto.ProjectId)!);
            _context.SaveChanges();
            return Ok();
        }
    }
}