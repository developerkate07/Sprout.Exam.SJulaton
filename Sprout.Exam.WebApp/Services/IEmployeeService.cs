using Sprout.Exam.Business.DataTransferObjects;
using Sprout.Exam.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sprout.Exam.WebApp.Services
{
    public interface IEmployeeService
    {
        Task<List<Employee>> GetEmployees();

        Task<Employee> GetEmployeeById(int id);

        Task<Employee> AddEmployee(CreateEmployeeDto employee);

        Task<Employee> UpdateEmployee(EditEmployeeDto employee);

        Task DeleteEmployee(int id);

        bool EmployeeExists(int id);
    }
}
