using Sprout.Exam.Common.Enums;
using System;

namespace Sprout.Exam.WebApp.Services
{
    public class SalaryService
    {
        public decimal Salary(int typeId, decimal absentDays, decimal workedDays)
        {
            
            decimal salary = 0;
            var type = (EmployeeType)typeId;
            switch(type)
            {
                case EmployeeType.Regular:
                    var regular = new CalculateRegular();
                    regular.BasicSalary = 20000;
                    regular.AbsentDays = absentDays;
                    salary = regular.ComputeSalary();
                    break;

                case EmployeeType.Contractual:
                    var contractual = new CalculateContractual();
                    contractual.DailyRate = 500;
                    contractual.WorkedDays = workedDays;
                    salary = contractual.ComputeSalary();
                    break;

                default:
                    throw new InvalidOperationException("Employee Type not found");
            }

            return Math.Round(salary, 2);
        }
    }
}
