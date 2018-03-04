using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using GoSharpProject.Models.constants;
using GoSharpProject.Models.entities;

namespace GoSharpProject.Models.ViewModels
{
    public class WorkItemViewModel
    {
        public int Id { get; set; }
        [Required]
        public String Name { get; set; }
         [Required]
        public String Description { get; set; }
         [Required]
        public DateTime DueDate { get; set; }
         [Required]
        public TaskStatus Status { get; set; }
         [Required]
        public virtual ApplicationUser AssignedWorker { get; set; }
        public virtual Project AssignedProject { get; set; }

        public WorkItemViewModel() { }
        public WorkItemViewModel(ProjectTask projectTask)
        {
            this.Id = projectTask.Id;
            this.Name = projectTask.Name;
            this.Description = projectTask.Description;
            this.DueDate = projectTask.DueDate;
            this.Status = projectTask.Status;
            this.AssignedWorker = projectTask.AssignedWorker;
            this.AssignedProject = projectTask.assignedProject;
        }
    }
}