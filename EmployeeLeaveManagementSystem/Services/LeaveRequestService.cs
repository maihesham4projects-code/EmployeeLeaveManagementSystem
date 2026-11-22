using Azure.Core;
using EmployeeLeaveManagementSystem.Helpers;
using EmployeeLeaveSystemData;
using EmployeeLeaveSystemData.Entites;
using EmployeeLeaveSystemData.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeLeaveManagementSystem.Services
{
    public class LeaveRequestService
    {

        public List<LeaveRequest> GetAllLeaveRequests()
        {

            using var context = new EmployeeLeaveContext();

            return context.leaveRequests
                .Include(l => l.Employee)
                .ToList();
        }
        public LeaveRequest? GetLeaveRequestById(int id)
        {

            using var context = new EmployeeLeaveContext();

            return context.leaveRequests
                .Include(l => l.Employee)
                .FirstOrDefault(l => l.Id == id);
                
        }
        public LeaveRequest CreateLeaveRequest( LeaveRequest leaveRequest)
        {
            using var _context = new EmployeeLeaveContext();
            var employee = _context.employees
           .FirstOrDefault(e => e.Id == leaveRequest.EmployeeId);

            if (employee == null)
                throw new Exception("Employee not found");

            LeaveRequestValidationHelper.ValidateLeaveRequest(leaveRequest);

            int requestedDays =LeaveRequestCalculationHelper.CalculateRequestedDays(leaveRequest.StartDate, leaveRequest.EndDate);

            if (requestedDays > employee.AnnualLeaveBalance)
                throw new Exception("Insufficient annual leave balance");

            leaveRequest.Status = LeaveRequestStatus.Pending;

            _context.leaveRequests.Add(leaveRequest);
            _context.SaveChanges();

            return leaveRequest;
        }


        public bool ApproveLeaveRequest(int id)
        {
            using var _context = new EmployeeLeaveContext();
            var request = _context.leaveRequests
                .Include(l => l.Employee)
                .FirstOrDefault(l => l.Id == id);

            if (request == null)
                throw new Exception("Leave request not found");

            if (request.Status != LeaveRequestStatus.Pending)
                throw new Exception("Only pending requests can be approved");

            int requestedDays = LeaveRequestCalculationHelper.CalculateRequestedDays(request.StartDate, request.EndDate);

            if (requestedDays > request.Employee.AnnualLeaveBalance)
                throw new Exception("Insufficient annual leave balance");

            request.Employee.AnnualLeaveBalance -= requestedDays;

            request.Status = LeaveRequestStatus.Approved;

            _context.SaveChanges();

            return true;
        }

        public bool RejectLeaveRequest(int id)
        {
            using var _context = new EmployeeLeaveContext();
            var request = _context.leaveRequests
                .FirstOrDefault(l => l.Id == id);

            if (request == null)
                throw new Exception("Leave request not found");

            if (request.Status != LeaveRequestStatus.Pending)
                throw new Exception("Only pending requests can be rejected");

            request.Status = LeaveRequestStatus.Rejected;

            _context.SaveChanges();

            return true;
        }


    }
}
