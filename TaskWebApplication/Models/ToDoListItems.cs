using System.ComponentModel.DataAnnotations.Schema;

namespace TaskWebApplication.Models
{
    public class ToDoListItems
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public bool Completed { get; set; }

        [ForeignKey("AppUser.Id")]
        public string AppUserId { get; set; }

        public AppUser AppUser { get; set; }

    }
}
