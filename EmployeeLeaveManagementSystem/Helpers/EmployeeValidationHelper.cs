using EmployeeLeaveSystemData.Entites;

namespace EmployeeLeaveManagementSystem.Helpers
{
    public static class EmployeeValidationHelper
    {
        public static void ValidateEmployee(Employee employee)
        {
            if (string.IsNullOrWhiteSpace(employee.Name))
                throw new Exception("Employee name is required");

            if (string.IsNullOrWhiteSpace(employee.Email))
                throw new Exception("Employee email is required");

            if (employee.AnnualLeaveBalance < 0)
                throw new Exception("Leave balance cannot be negative");
        }
    }
}
