using EmployeeLeaveManagementSystem.Services;
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
           

            try
            {
                LeaveRequestService leaveRequestService = new LeaveRequestService();
                return Ok(leaveRequestService.GetAllLeaveRequests());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/leaverequests/1
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try { 
                LeaveRequestService leaveRequestService = new LeaveRequestService();
                var request=leaveRequestService.GetLeaveRequestById(id);
                if (request == null)
                    return NotFound("Leave request not found");

                return Ok(request);
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
             }
        }

       [HttpPost]
        public IActionResult Create(LeaveRequest leaveRequest)
        {
            try { 
            LeaveRequestService leaveRequestService = new LeaveRequestService();

            return Ok(leaveRequestService.CreateLeaveRequest(leaveRequest));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/leaverequests/1/approve
        [HttpPut("{id}/approve")]
        public IActionResult Approve(int id)
        {
            try { 
            LeaveRequestService leaveRequestService = new LeaveRequestService();
            leaveRequestService.ApproveLeaveRequest(id);
            return Ok("Leave request approved successfully");
        }
    catch (Exception ex)
    {
       

        return BadRequest(ex.Message);
    }
}

        // PUT: api/leaverequests/1/reject
        [HttpPut("{id}/reject")]
        public IActionResult Reject(int id)
        {
            try { 
            LeaveRequestService leaveRequestService = new LeaveRequestService();
            leaveRequestService.RejectLeaveRequest(id);

            return Ok("Leave request rejected successfully");
        }
    catch (Exception ex)
    {
       

        return BadRequest(ex.Message);
    }
}
    }
}