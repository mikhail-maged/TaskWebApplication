using System.ComponentModel.DataAnnotations;

namespace TaskWebApplication.Models
{
    public class ToDoListItemCreateDto
    {
        [Required]
        public string Title { get; set; }

        public bool Completed { get; set; }
    }
}
