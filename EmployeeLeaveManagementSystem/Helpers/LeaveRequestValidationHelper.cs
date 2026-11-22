using EmployeeLeaveSystemData.Entites;

namespace EmployeeLeaveManagementSystem.Helpers
{
    public static class LeaveRequestValidationHelper
    {
        public static void ValidateLeaveRequest(LeaveRequest leaveRequest)
        {
            if (leaveRequest.StartDate.Date < DateTime.Today)
                throw new Exception("Start date cannot be in the past");

            if (leaveRequest.EndDate.Date < leaveRequest.StartDate.Date)
                throw new Exception("End date cannot be before start date");
        }
    }
}
