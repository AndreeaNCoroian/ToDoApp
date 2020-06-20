using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoApp.Models;

namespace ToDoApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskItemsController : ControllerBase
    {
        private readonly TaskItemsDbContext _context;

        public TaskItemsController(TaskItemsDbContext context)
        {
            _context = context;
        }

        // GET: api/TaskItems
        //tested: api/taskitems?from=2020-06-20T01:36:55.6367476&to=2020-10-20T23:50:00

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskItem>>> GetTaskItems(DateTimeOffset? from = null, DateTimeOffset? to = null)
        {
            IQueryable<TaskItem> result = _context.TaskItems;
            if (from != null && to != null)
            {
                result = result.Where(f => from <= f.Deadline && f.Deadline <= to);
            }
            else if (from != null)
            {
                result = result.Where(f => from <= f.Deadline);
            }
            else if (to != null)
            {
                result = result.Where(f => f.Deadline <= to);
            }

            var resultList = await result.ToListAsync();
            //var resultList = await result.OrderByDescending(f => f.Deadline).ToListAsync(); ---> generates a SQL query order by Deadline descending
         
            return resultList;
        }

        // GET: api/TaskItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TaskItem>> GetTaskItem(long id)
        {
            var taskItem = await _context.TaskItems.FindAsync(id);

            if (taskItem == null)
            {
                return NotFound("Task does not exist!");
            }

            return taskItem;
        }


        // PUT: api/TaskItems/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTaskItem(long id, TaskItem taskItem)
        {
            if (id != taskItem.Id)
            {
                return BadRequest();
            }


            /*Lab2: modificarea stării unui task: dacă se schimbă în closed, se completează câmpul closedAt 
            ca fiind timpul request-ului. Dacă se schimbă din închis în altceva, se pune timpul închiderii pe null.*/
            if (taskItem.State.Equals(State.Closed))
            {
                taskItem.ClosedAt = DateTime.Now;
            }
            else
            {
                taskItem.ClosedAt = default; 
            }
            //------------------------------------------------------------------------------------------------------
            _context.Entry(taskItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaskItemExists(id))
                {
                    return NotFound("This task does not exist!");
                }
                else
                {
                    throw;
                }
            }

            return Ok(taskItem);
        }

        // POST: api/TaskItems
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<TaskItem>> PostTaskItem(TaskItem taskItem)
        {
            _context.TaskItems.Add(taskItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTaskItem", new { id = taskItem.Id }, taskItem);
        }

        // DELETE: api/TaskItems/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TaskItem>> DeleteTaskItem(long id)
        {
            var taskItem = await _context.TaskItems.FindAsync(id);
            if (taskItem == null)
            {
                return NotFound("This task does not exist!");
            }

            _context.TaskItems.Remove(taskItem);
            await _context.SaveChangesAsync();

            return taskItem;
        }

        private bool TaskItemExists(long id)
        {
            return _context.TaskItems.Any(e => e.Id == id);
        }
    }
}
