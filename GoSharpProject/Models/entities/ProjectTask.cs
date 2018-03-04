using System;
using GoSharpProject.Models.constants;

namespace GoSharpProject.Models.entities
{
    public class ProjectTask
    {
        public int Id  { get; set; }
        public String Name  { get; set; }
        public String Description { get; set; }
        public DateTime DueDate { get; set; }
        public TaskStatus Status { get; set; }
        public virtual ApplicationUser AssignedWorker { get; set; }
        public virtual Project assignedProject { get; set; }
        public decimal Price { get; set; }

    }
}