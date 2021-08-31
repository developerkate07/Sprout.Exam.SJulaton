using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sprout.Exam.Business.DataTransferObjects;
using Sprout.Exam.WebApp.Models;
using Sprout.Exam.WebApp.Services;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Sprout.Exam.WebApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private IEmployeeService _employeeService;
        private SalaryService _salaryService;

        public EmployeesController(IEmployeeService employeeService, SalaryService salaryService)
        {
           _employeeService = employeeService;
           _salaryService = salaryService;
        }
        /// <summary>
        /// Refactor this method to go through proper layers and fetch from the DB.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _employeeService.GetEmployees();
            return Ok(result.Select(e => e.EmployeeToDto()).ToList());
        }

        /// <summary>
        /// Refactor this method to go through proper layers and fetch from the DB.
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _employeeService.GetEmployeeById(id);
            return Ok(result.EmployeeToDto());
        }

        /// <summary>
        /// Refactor this method to go through proper layers and update changes to the DB.
        /// </summary>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(EditEmployeeDto input)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                var editEmployee = await _employeeService.UpdateEmployee(input);
                return Ok(editEmployee);
            }
            catch(ArgumentNullException)
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Refactor this method to go through proper layers and insert employees to the DB.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post(CreateEmployeeDto input)
        {

            var newEmployee = await _employeeService.AddEmployee(input);

            return Created($"/api/employees/{newEmployee.Id}", newEmployee.Id);
        }


        /// <summary>
        /// Refactor this method to go through proper layers and perform soft deletion of an employee to the DB.
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _employeeService.DeleteEmployee(id);
                return Ok(id);
            }
            catch (ArgumentNullException)
            {
                return NotFound();
            }
            
        }



        /// <summary>
        /// Refactor this method to go through proper layers and use Factory pattern
        /// </summary>
        /// <param name="id"></param>
        /// <param name="absentDays"></param>
        /// <param name="workedDays"></param>
        /// <returns></returns>
        [HttpPost("{id}/calculate")]
        public async Task<IActionResult> Calculate(Salary input)
        {
            var result = await _employeeService.GetEmployeeById(input.Id);
            if (result == null) return NotFound();

            try
            {
                return Ok(_salaryService.Salary(result.EmployeeTypeId, input.AbsentDays, input.WorkedDays));
            }
            catch (InvalidOperationException)
            {
                return NotFound();
            }

        }

    }
}
