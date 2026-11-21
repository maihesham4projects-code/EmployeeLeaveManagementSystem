using EmployeeLeaveSystemData;
using EmployeeLeaveSystemData.Entites;
using EmployeeLeaveSystemData.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace EmployeeLeaveManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LeaveRequestsController : ControllerBase
    {
        // GET: api/leaverequests
        [HttpGet]
        public IActionResult GetAll()
        {
            using var context = new EmployeeLeaveContext();

            var requests = context.leaveRequests
                .Include(l => l.Employee)
                .ToList();

            return Ok(requests);
        }

        // GET: api/leaverequests/1
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            using var context = new EmployeeLeaveContext();

            var request = context.leaveRequests
                .Include(l => l.Employee)
                .FirstOrDefault(l => l.Id == id);

            if (request == null)
                return NotFound("Leave request not found");

            return Ok(request);
        }

        POST: api/leaverequests
       [HttpPost]
        public IActionResult Create(LeaveRequest leaveRequest)
        {
            using var context = new EmployeeLeaveContext();

            Check employee existence
           var employee = context.employees
               .FirstOrDefault(e => e.Id == leaveRequest.EmployeeId);
            if (employee == null)
                return NotFound("Employee not found");

            Validate dates
            if (leaveRequest.StartDate.Date < DateTime.Today)
                return BadRequest("Start date cannot be in the past");

            if (leaveRequest.EndDate.Date < leaveRequest.StartDate.Date)
                return BadRequest("End date cannot be before start date");

            Calculate requested leave days
            int requestedDays =
                (leaveRequest.EndDate.Date - leaveRequest.StartDate.Date).Days + 1;

            Check available balance
            if (requestedDays > employee.AnnualLeaveBalance)
                return BadRequest("Insufficient annual leave balance");
            leaveRequest.Status = LeaveRequestStatus.Pending;
            context.leaveRequests.Add(leaveRequest);
            context.SaveChanges();

            return Ok(leaveRequest);
        }

        // PUT: api/leaverequests/1/approve
        [HttpPut("{id}/approve")]
        public IActionResult Approve(int id)
        {
            using var context = new EmployeeLeaveContext();

            var request = context.leaveRequests
                .Include(l => l.Employee)
                .FirstOrDefault(l => l.Id == id);

            if (request == null)
                return NotFound("Leave request not found");

            if (request.Status != LeaveRequestStatus.Pending)
                return BadRequest("Only pending requests can be approved");

            // Calculate leave days
            int requestedDays =
                (request.EndDate.Date - request.StartDate.Date).Days + 1;

            // Check balance again before approval
            if (requestedDays > request.Employee.AnnualLeaveBalance)
                return BadRequest("Insufficient annual leave balance");

            // Update employee balance
            request.Employee.AnnualLeaveBalance -= requestedDays;

            // Update request status
            request.Status = LeaveRequestStatus.Approved;

            context.SaveChanges();

            return Ok("Leave request approved successfully");
        }

        // PUT: api/leaverequests/1/reject
        [HttpPut("{id}/reject")]
        public IActionResult Reject(int id)
        {
            using var context = new EmployeeLeaveContext();

            var request = context.leaveRequests
                .FirstOrDefault(l => l.Id == id);

            if (request == null)
                return NotFound("Leave request not found");

            if (request.Status != LeaveRequestStatus.Pending)
                return BadRequest("Only pending requests can be rejected");

            request.Status = LeaveRequestStatus.Rejected;

            context.SaveChanges();

            return Ok("Leave request rejected successfully");
        }
    }
}