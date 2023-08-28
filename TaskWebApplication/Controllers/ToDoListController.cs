using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskWebApplication.Models;
using TaskWebApplication.Repository;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace TaskWebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ToDoListController : ControllerBase
    {
        private readonly IToDoListRepository toDoListRepository;

        public ToDoListController(IToDoListRepository _toDoListRepository)
        {
            toDoListRepository = _toDoListRepository;
        }


        [HttpGet("FetchItem/{id:int}")]
        public IActionResult FetchItem(int id)
        {
            ToDoListItems selectedItem = toDoListRepository.GetItemById(id, User?.Identity?.Name ?? "");
            if (selectedItem.Id != 0)
            {
                var selectedItemDto =new ToDoListItemsDto() {Id=selectedItem.Id,Completed=selectedItem.Completed,Title=selectedItem.Title };
                return Ok(selectedItemDto);
            }
            return BadRequest();
        }

        [HttpGet("FetchAllItems")]
        public IActionResult FetchAllItems()
        {
            List<ToDoListItems> selectedItem = toDoListRepository.GetAllItems(User?.Identity?.Name ?? "");
            List<ToDoListItemsDto> selectedItemDto = new List<ToDoListItemsDto>();
            
            foreach(var item in selectedItem) 
            {
                selectedItemDto.Add(new ToDoListItemsDto() { Id = item.Id, Completed = item.Completed, Title = item.Title });
            }

            return Ok(selectedItemDto);
        }

        [HttpPost("AddNewItem")]
        public async Task<IActionResult> AddNewItem(ToDoListItemCreateDto toDoListItemCreateDto)
        {
            ToDoListItems newItem =await toDoListRepository.Create(toDoListItemCreateDto, User?.Identity?.Name ?? "");
            ToDoListItemsDto newItemDto =new ToDoListItemsDto() { Title = newItem.Title, Completed = newItem.Completed, Id = newItem.Id };
            return Ok(newItemDto);
        }

        [HttpPost("UpdateItem/{id:int}")]
        public ActionResult UpdateItem(int id,ToDoListItemCreateDto toDoListItemCreateDto)
        {
            ToDoListItems newItem =toDoListRepository.Updateitem(id, User?.Identity?.Name ?? "", toDoListItemCreateDto);
            if(newItem.Id!=0)
            {
                ToDoListItemsDto updated = new ToDoListItemsDto() { Title = newItem.Title, Completed = newItem.Completed, Id = newItem.Id };

                return Ok(updated);
            }
            return BadRequest();
        }

        [HttpGet("RemoveItem/{id:int}")]
        public ActionResult RemoveItem(int id)
        {
            ToDoListItems deletedItem = toDoListRepository.DeleteItem(id, User?.Identity?.Name ?? "");
            if (deletedItem.Id != 0)
            {
                ToDoListItemsDto deleted = new ToDoListItemsDto() { Title = deletedItem.Title,Completed=deletedItem.Completed,Id=deletedItem.Id  };
                return Ok(deleted);
            }
            return BadRequest();
        }
    }
}
