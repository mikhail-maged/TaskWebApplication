using System.ComponentModel.DataAnnotations;

namespace TaskWebApplication.Models
{
    public class ToDoListItemsDto
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }

        public bool Completed { get; set; }
    }
}
