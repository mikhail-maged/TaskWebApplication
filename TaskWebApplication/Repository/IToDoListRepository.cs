using TaskWebApplication.Models;

namespace TaskWebApplication.Repository
{
    public interface IToDoListRepository
    {
        List<ToDoListItems> GetAllItems(string email);
        ToDoListItems GetItemById(int id, string email);
        ToDoListItems Updateitem(int id, string email, ToDoListItemCreateDto newData);
        ToDoListItems DeleteItem(int id, string email);
        Task<ToDoListItems> Create(ToDoListItemCreateDto toDoListItemCreateDto, string email);
    }
}
