using EmployeeLeaveSystemData;
using EmployeeLeaveSystemData.Entites;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace EmployeeLeaveManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
  
    public class employeesController : ControllerBase
    {
        // GET: api/employees
        [HttpGet]
        public IActionResult GetAll()
        {
            using var context = new EmployeeLeaveContext();

            var employees = context.employees.ToList();

            return Ok(employees);
        }

        // GET: api/employees/1
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            using var context = new EmployeeLeaveContext();

            var employee = context.employees
                .FirstOrDefault(e => e.Id == id);

            if (employee == null)
                return NotFound("Employee not found");

            return Ok(employee);
        }

        // POST: api/employees
        [HttpPost]
        public IActionResult Add(Employee employee)
        {
            using var context = new EmployeeLeaveContext();

            // Validation
            if (string.IsNullOrWhiteSpace(employee.Name))
                return BadRequest("Employee name is required");

            if (string.IsNullOrWhiteSpace(employee.Email))
                return BadRequest("Employee email is required");

            if (context.employees.Any(e => e.Email == employee.Email))
                return BadRequest("Email already exists");

            if (employee.AnnualLeaveBalance < 0)
                return BadRequest("Leave balance cannot be negative");

            // Object initialization
            employee.CreatedAt = DateTime.Now;

            // Database operation
            context.employees.Add(employee);
            context.SaveChanges();

            return Ok(employee);
        }

        // PUT: api/employees/1
        [HttpPut("{id}")]
        public IActionResult Update(int id, Employee updatedEmployee)
        {
            using var context = new EmployeeLeaveContext();
            var employee = context.employees
                .FirstOrDefault(e => e.Id == id);

            if (employee == null)
                return NotFound("Employee not found");

            if (string.IsNullOrWhiteSpace(updatedEmployee.Name))
                return BadRequest("Employee name is required");

            if (string.IsNullOrWhiteSpace(updatedEmployee.Email))
                return BadRequest("Employee email is required");

            bool emailExists = context.employees.Any(e =>
                e.Email == updatedEmployee.Email && e.Id != id);

            if (emailExists)
                return BadRequest("Email already exists");

            if (updatedEmployee.AnnualLeaveBalance < 0)
                return BadRequest("Leave balance cannot be negative");

            employee.Name = updatedEmployee.Name;
            employee.Email = updatedEmployee.Email;
            employee.AnnualLeaveBalance = updatedEmployee.AnnualLeaveBalance;

            context.SaveChanges();

            return Ok(employee);
        }

        // DELETE: api/employees/1
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            using var context = new EmployeeLeaveContext();

            var employee = context.employees
                .Include(e => e.LeaveRequests)
                .FirstOrDefault(e => e.Id == id);

            if (employee == null)
                return NotFound("Employee not found");

            if (employee.LeaveRequests.Any())
                return BadRequest(
                    "Cannot delete an employee who has leave requests");

            context.employees.Remove(employee);
            context.SaveChanges();

            return Ok("Employee deleted successfully");
        }
    }
}