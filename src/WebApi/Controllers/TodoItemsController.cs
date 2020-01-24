using MyCleanApp.Application.TodoItems.Commands.CreateTodoItem;
using MyCleanApp.Application.TodoItems.Commands.DeleteTodoItem;
using MyCleanApp.Application.TodoItems.Commands.UpdateTodoItem;
using MyCleanApp.Application.TodoItems.Commands.UpdateTodoItemDetail;
using MyCleanApp.Application;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MyCleanApp.WebApi.Controllers
{
    //[Authorize]
    public class TodoItemsController : ApiController
    {
        [HttpPost]
        public async Task<ActionResult<long>> Create(CreateTodoItemCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(long id, UpdateTodoItemCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            await Mediator.Send(command);

            return NoContent();
        }

        [HttpPut("[action]")]
        public async Task<ActionResult> UpdateItemDetails(long id, UpdateTodoItemDetailCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            await Mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(long id)
        {
            await Mediator.Send(new DeleteTodoItemCommand { Id = id });

            return NoContent();
        }
    }
}
