using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Intern_task.DTO;
using Intern_task.Enums;
using Intern_task.ExtensionMethods;
using Intern_task.Models;
using Microsoft.AspNetCore.Mvc;

namespace Intern_task.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TaskController : Controller
    {
        private readonly TaskTrackerContext _context;
        
        public TaskController(TaskTrackerContext context)
        {
            _context = context;
        }
        
        // returns the list of all tasks
        [HttpGet]
        [Route("get")]
        public ActionResult<IEnumerable<Task>> Get()
        {
            return Ok(_context.Tasks.ToList());
        }
        
        
        //creates a new task and assigns it to the existing project
        [HttpPost]
        [Route("create")]
        public ActionResult<IEnumerable<Task>> CreateTask([FromBody]CreateTaskDTO dto)
        {
            Task task = new Task(dto);
            
            _context.Tasks.Add(task);
            _context.SaveChanges();
            
            return Ok(task);
        }
        
        // Updates one or more fields of the task
        [HttpPatch]
        [Route("update")]
        public ActionResult<IEnumerable<Task>> UpdateTask([FromBody] UpdateTaskDTO dto)
        {
            if (dto == null)
            {
                return BadRequest();
            }
            
            Task task = _context.Tasks.FirstOrDefault(t => t.Id == dto.TaskId);
            
            if (task == null)
            {
                return BadRequest();
            }
            
            task.UpdateTask(dto);
            
            _context.Update(task);
            _context.SaveChanges();

            return Ok();
        }
        
        // Changes the status of the task to InProgress
        [HttpPatch]
        [Route("make/inprogress")]
        public ActionResult<IEnumerable<Task>> MakeInprogress([FromBody] TaskIdDTO dto)
        {
            if (dto == null)
            {
                return BadRequest();
            }
            
            Task task = _context.Tasks.FirstOrDefault(t => t.Id == dto.TaskId);
            
            if (task == null)
            {
                return BadRequest();
            }

            task.TaskStatus = (short?) TaskStatusEnum.InProgress;
            
            _context.Update(task);
            _context.SaveChanges();

            return Ok();
        }
        
        // Changes the status of the task to Finished
        [HttpPatch]
        [Route("make/done")]
        public ActionResult<IEnumerable<Task>> FinishTask([FromBody] TaskIdDTO dto)
        {
            if (dto == null)
            {
                return BadRequest();
            }
            
            Task task = _context.Tasks.FirstOrDefault(t => t.Id == dto.TaskId);
            
            if (task == null)
            {
                return BadRequest();
            }

            task.TaskStatus = (short?) TaskStatusEnum.Done;
            
            _context.Update(task);
            _context.SaveChanges();

            return Ok();
        }


        //deletes the task by id
        [HttpDelete]
        [Route("delete")]
        public ActionResult DeleteTask([FromBody] TaskIdDTO dto)
        {
            Task task = _context.Tasks.FirstOrDefault(t => t.Id == dto.TaskId);

            _context.Tasks.Remove(task!);
            _context.SaveChanges();

            return Ok();
        }
    }
}