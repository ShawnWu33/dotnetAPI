using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TodoAPI.Repositories;

namespace TodoAPI.Tests
{
    public class FakeTodoItemRepo : ITodoItemRepo
    {
        public Task<TodoItem> add(TodoItem entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<TodoItem> delete(long id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<TodoItem>> get(int pageNumber = 1, int pageSize = 10)
        {
            TodoItem[] result = new TodoItem[]{
                new TodoItem {
                    Id = 1,
                    name = "item 1",
                    IsComplete = false
                },
                new TodoItem {
                    Id = 2,
                    name = "item 2",
                    IsComplete = false
                }
            };
            return result;

        }

        public async Task<TodoItem> getById(long id)
        {
            return new TodoItem{
                Id = id,
                name = "fake item",
                IsComplete = false
            };
        }

        public Task<TodoItem> update(long id, TodoItem entity)
        {
            throw new System.NotImplementedException();
        }
    }
}