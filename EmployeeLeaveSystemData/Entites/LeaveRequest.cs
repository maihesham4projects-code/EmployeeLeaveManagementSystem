using EmployeeLeaveSystemData.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeLeaveSystemData.Entites
{
    public class LeaveRequest:BaseEntity
    {
        public int Id { get; set; }

        public int EmployeeId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
        [Required]
        public string Reason { get; set; } = string.Empty;

        public LeaveRequestStatus Status { get; set; }
        public Employee? Employee { get; set; } 
    }
}
