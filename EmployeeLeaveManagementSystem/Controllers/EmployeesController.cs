using EmployeeLeaveManagementSystem.Services;
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

            try
            {
                EmployeeService employeeService = new EmployeeService();

                return Ok(employeeService.GetAllEmployees());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/employees/1
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            EmployeeService employeeService = new EmployeeService();

            var employee = employeeService.GetEmployee(id);

            if (employee == null)
                return NotFound("Employee not found");

            return Ok(employee);
        }

        // POST: api/employees
        [HttpPost]
        public IActionResult Add(Employee employee)
        {
            try
            {
                EmployeeService employeeService = new EmployeeService();
                var result = employeeService.AddEmployee(employee);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/employees/1
        [HttpPut("{id}")]
        public IActionResult Update(int id, Employee updatedEmployee)
        {
            try
            {
                EmployeeService employeeService = new EmployeeService();
                var result = employeeService.UpdateEmployee(id, updatedEmployee);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/employees/1
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                EmployeeService employeeService = new EmployeeService();
                employeeService.DeleteEmployee(id);

                return Ok("Employee deleted successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}