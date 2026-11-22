using EmployeeLeaveManagementSystem.Helpers;
using EmployeeLeaveSystemData;
using EmployeeLeaveSystemData.Entites;
using Microsoft.EntityFrameworkCore;

namespace EmployeeLeaveManagementSystem.Services
{
    public class EmployeeService
    {
        public List<Employee> GetAllEmployees()
        {
            using var context = new EmployeeLeaveContext();

            return context.employees.ToList();
        }

        public Employee? GetEmployee(int id)
        {

            using var context = new EmployeeLeaveContext();
            return context.employees
              .FirstOrDefault(e => e.Id == id);
        }


        public Employee AddEmployee(Employee employee)
        {
            using var context = new EmployeeLeaveContext();

            // Validation
            EmployeeValidationHelper.ValidateEmployee(employee);
            if (context.employees.Any(e => e.Email == employee.Email))
                throw new Exception("Email already exists");

            // Database operation
            context.employees.Add(employee);
            context.SaveChanges();

            return employee;
        }

        public Employee UpdateEmployee(int id, Employee updatedEmployee)
        {
            using var context = new EmployeeLeaveContext();

            var employee = context.employees
                .FirstOrDefault(e => e.Id == id);

            if (employee == null)
                throw new Exception("Employee not found");

            // Validation
            EmployeeValidationHelper.ValidateEmployee(updatedEmployee);

            bool emailExists = context.employees.Any(e =>
                e.Email == updatedEmployee.Email && e.Id != id);

            if (emailExists)
                throw new Exception("Email already exists");


            // Update employee information
            employee.Name = updatedEmployee.Name;
            employee.Email = updatedEmployee.Email;
            employee.AnnualLeaveBalance = updatedEmployee.AnnualLeaveBalance;

            context.SaveChanges();

            return employee;
        }
        public void DeleteEmployee(int id)
        {
            using var context = new EmployeeLeaveContext();

            var employee = context.employees
                .Include(e => e.LeaveRequests)
                .FirstOrDefault(e => e.Id == id);

            if (employee == null)
                throw new Exception("Employee not found");

            if (employee.LeaveRequests.Any())
                throw new Exception(
                    "Cannot delete an employee who has leave requests");

            context.employees.Remove(employee);

            context.SaveChanges();
        }

    }
}
