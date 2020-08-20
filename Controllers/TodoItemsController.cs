using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TodoAPI.Repositories;

namespace TodoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly ITodoItemRepo _todoItemRepo;
        private readonly ILogger _logger;

        public TodoItemsController(ITodoItemRepo todoItemRepo, ILogger<TodoItemsController> logger)
        {
            _todoItemRepo = todoItemRepo;
            _logger = logger;
        }

        // GET: api/TodoItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodoItems()
        {
            var result = await _todoItemRepo.get();

            return Ok(result);
            
        }

        // GET: api/TodoItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItem>> GetTodoItem(long id)
        {
            var todoItem = await _todoItemRepo.getById(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            return Ok(todoItem);
        }

        // PUT: api/TodoItems/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoItem(long id, TodoItemDTO todoItemDTO)
        {
            if (id != todoItemDTO.Id)
            {
                return BadRequest();
            }
            try
            {
                var todoItem = await _todoItemRepo.getById(todoItemDTO.Id);
                if (todoItem == null)
                {
                    return NotFound();
                }
                todoItem.name = todoItemDTO.name;
                todoItem.IsComplete = todoItemDTO.IsComplete;
                await _todoItemRepo.update(todoItem.Id, todoItem);
                return NoContent();

            }
            catch (Exception)
            {
                return BadRequest();
            }



        }

        // POST: api/TodoItems
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<TodoItem>> PostTodoItem(TodoItem todoItem)
        {
            await _todoItemRepo.add(todoItem);
            return CreatedAtAction(nameof(GetTodoItem), new { id = todoItem.Id }, todoItem);
        }

        // DELETE: api/TodoItems/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TodoItem>> DeleteTodoItem(long id)
        {
            var result = await _todoItemRepo.delete(id);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

    }
}
