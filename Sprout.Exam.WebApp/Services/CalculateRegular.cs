namespace Sprout.Exam.WebApp.Services
{
    public class CalculateRegular : Calculator
    {
        public decimal BasicSalary { get; set; }
        public decimal AbsentDays { get; set; }
        private const decimal WorkedDays = 22m;
        private const decimal Tax = 0.12m;
        public override decimal ComputeSalary()
        {
            var absentDeduction = (BasicSalary / WorkedDays) * AbsentDays;
            var taxDeduction = BasicSalary * Tax;
            return BasicSalary - absentDeduction - taxDeduction;
        }
    }
}
