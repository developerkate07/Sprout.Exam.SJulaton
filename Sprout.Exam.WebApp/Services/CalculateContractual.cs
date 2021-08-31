using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sprout.Exam.WebApp.Services
{
    public class CalculateContractual : Calculator
    {
        public decimal DailyRate { get; set; }
        public decimal WorkedDays { get; set; }
        public override decimal ComputeSalary()
        {
            return DailyRate * WorkedDays;
        }
    }
}
