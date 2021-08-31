using Microsoft.EntityFrameworkCore;
using Sprout.Exam.Business.DataTransferObjects;
using Sprout.Exam.WebApp.Data;
using Sprout.Exam.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sprout.Exam.WebApp.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly ApplicationDbContext _context;

        public EmployeeService(ApplicationDbContext context)
        {
            _context = context;
        }



        public async Task<Employee> AddEmployee(CreateEmployeeDto employee)
        {
            var newEmployee = new Employee
            {
                FullName = employee.FullName,
                Birthdate = employee.Birthdate,
                Tin = employee.Tin,
                EmployeeTypeId = employee.TypeId
            };
            _context.Set<Employee>().Add(newEmployee);

            await _context.SaveChangesAsync();

            return newEmployee;
        }

        public async Task DeleteEmployee(int id)
        {
            if (!EmployeeExists(id)) throw new ArgumentNullException("Employee does not exist");

            var currentEmployee = _context.Employee.FirstOrDefault(e => e.Id == id);

            currentEmployee.IsDeleted = true;

            await _context.SaveChangesAsync();
        }

        public async Task<Employee> GetEmployeeById(int id)
        {
            return await _context.Set<Employee>().FirstOrDefaultAsync(e => e.Id == id && e.IsDeleted == false);
        }

        public async Task<Employee> UpdateEmployee(EditEmployeeDto employee)
        {
            if (!EmployeeExists(employee.Id)) throw new ArgumentNullException("Employee does not exist");

            var currentEmployee = _context.Employee.FirstOrDefault(e => e.Id == employee.Id);

            currentEmployee.FullName = employee.FullName;
            currentEmployee.Birthdate = employee.Birthdate;
            currentEmployee.Tin = employee.Tin;
            currentEmployee.EmployeeTypeId = employee.TypeId;

            await _context.SaveChangesAsync();

            return currentEmployee;
        }

        public async Task<List<Employee>> GetEmployees()
        {
            return await _context.Set<Employee>().Where(e => e.IsDeleted == false).ToListAsync(); 
        }

        public bool EmployeeExists(int id)
        {
            return _context.Set<Employee>().Any(e => e.Id == id);
        }
    }
}
