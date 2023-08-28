using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskWebApplication.Models
{
    public class AppUser : IdentityUser
    {
        
        public List<ToDoListItems>? userListedItems { get; set; }
    }
}
