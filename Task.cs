using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Task_Management.Models
{
        
        public class Tasks
        {
            public int TaskId { get; set; }
            [Required]
            public string TaskTitle { get; set; }
            public string TaskDescription { get; set; }
            public DateTime? TaskDueDate { get; set; }
            public string TaskStatus { get; set; }
            public string TaskRemarks { get; set; }
            public DateTime? CreatedOn { get; set; }
            public DateTime? UpdatedOn { get; set; }
            public string CreatedBy { get; set; }
            public string UpdatedBy { get; set; }
        }

   }
