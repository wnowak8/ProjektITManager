using System.ComponentModel.DataAnnotations;

namespace Frontend.Models
{
    public class TaskModel
    {
        public string TaskId { get; set; }

        public required string Title { get; set; }

        public required string Description { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public required DateTime Deadline { get; set; }

        public required string Status { get; set; }

        public required string ProjectName { get; set; }

        public required string AssignedTo { get; set; }
    }
}
