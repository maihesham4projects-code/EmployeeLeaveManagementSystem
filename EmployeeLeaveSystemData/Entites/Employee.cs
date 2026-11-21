using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeLeaveSystemData.Entites
{
    public class Employee: BaseEntity
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Email { get; set; }=string.Empty;

        public int AnnualLeaveBalance { get; set; }
        public ICollection<LeaveRequest> LeaveRequests { get; set; }=new List<LeaveRequest>();
    }
}
