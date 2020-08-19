using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TodoAPI.Models;

namespace TodoAPI.Repositories
{
    public class TodoItemRepo : ITodoItemRepo
    {
        private TodoContext _context;
        public TodoItemRepo(TodoContext context)
        {
             _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<TodoItem> add(TodoItem entity)
        {
            _context.TodoItems.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<TodoItem> delete(long id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);
            if (todoItem == null)
            {
                return null;
            }

            _context.TodoItems.Remove(todoItem);
            await _context.SaveChangesAsync();

            return todoItem;
        }

        public async Task<IEnumerable<TodoItem>> get(int pageNumber = 1, int pageSize = 10)
        {
            return await _context.TodoItems.ToListAsync();
        }

        public async Task<TodoItem> getById(long id)
        {
            return await _context.TodoItems.FindAsync(id);
        }

        public async Task<TodoItem> update(long id, TodoItem entity)
        {
            _context.Entry(entity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await TodoItemExistsAsync(id))
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }

            return entity;
        }

        private async Task<bool> TodoItemExistsAsync(long id)
        {

            return await _context.TodoItems.AnyAsync(e => e.Id == id);
        }
    }
}