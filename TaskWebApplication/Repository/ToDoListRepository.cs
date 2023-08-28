using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using TaskWebApplication.Data;
using TaskWebApplication.Models;

namespace TaskWebApplication.Repository
{
    public class ToDoListRepository : IToDoListRepository
    {
        private readonly DbContextRef dbContextRef;
        private readonly UserManager<AppUser> userManager;

        public ToDoListRepository(DbContextRef _dbContextRef,UserManager<AppUser> _userManager)
        {
            dbContextRef = _dbContextRef;
            userManager = _userManager;
        }
        
        public List<ToDoListItems> GetAllItems(string email)
        {
            AppUser? user = userManager.Users.Include(x => x.userListedItems).FirstOrDefault(x => x.Email == email);
            List<ToDoListItems> result = new List<ToDoListItems>();
            if (user != null)
            {
                return user.userListedItems?? result;
            }
            return result;
        }

        

        public ToDoListItems GetItemById(int id,string email)
        {
            ToDoListItems? selectedItem = dbContextRef.ToDoListItems.Include(x=>x.AppUser).FirstOrDefault(x=>x.Id==id);

            ToDoListItems result = new ToDoListItems();
            if (selectedItem != null)
            {
                if (selectedItem.AppUser.Email == email)
                {
                    return selectedItem;
                }
            }

            return result;
        }

        public ToDoListItems Updateitem(int id, string email, ToDoListItemCreateDto newData)
        {
            ToDoListItems selectedItem = GetItemById(id,email);

            ToDoListItems result = new ToDoListItems();

            if (selectedItem.Id != 0)
            {
                selectedItem.Title= newData.Title;
                selectedItem.Completed = newData.Completed;
                dbContextRef.ToDoListItems.Update(selectedItem);
                dbContextRef.SaveChanges();

                return selectedItem;
            }

            return result;
        }

        public ToDoListItems DeleteItem(int id,string email)
        {
            ToDoListItems selectedItem = GetItemById(id, email);

            ToDoListItems result = new ToDoListItems();

            if (selectedItem.Id != 0)
            {
                dbContextRef.ToDoListItems.Remove(selectedItem);
                dbContextRef.SaveChanges();

                return selectedItem;
            }

            return result;
        }


        public async Task<ToDoListItems> Create(ToDoListItemCreateDto toDoListItemCreateDto,string email)
        {
            AppUser user =await userManager.FindByEmailAsync(email);
            
            ToDoListItems newItem = new ToDoListItems() { Title=toDoListItemCreateDto.Title,Completed=toDoListItemCreateDto.Completed,AppUserId=user.Id};
            
            dbContextRef.ToDoListItems.Add(newItem);
            dbContextRef.SaveChanges();

            
            return newItem;

        }
        

        
     
    }
}

