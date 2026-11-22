namespace EmployeeLeaveManagementSystem.Helpers
{
    public static class LeaveRequestCalculationHelper
    {
        public static int CalculateRequestedDays(
            DateTime startDate,
            DateTime endDate)
        {
            return (endDate.Date - startDate.Date).Days + 1;
        }
    }
}
