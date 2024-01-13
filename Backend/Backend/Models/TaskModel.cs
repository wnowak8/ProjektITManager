using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    public class TaskModel
    {
        [Key]
        public Guid TaskId { get; set; }

        public required string Title { get; set; }

        public required string Description { get; set; }

        public required DateTime Deadline { get; set; }

        public required string Status { get; set; }

        public required string ProjectName { get; set; }
    }
}
