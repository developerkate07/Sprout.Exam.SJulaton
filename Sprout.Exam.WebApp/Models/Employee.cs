using Sprout.Exam.Business.DataTransferObjects;
using Sprout.Exam.Common.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace Sprout.Exam.WebApp.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Birthdate { get; set; }

        [Required]
        public string Tin { get; set; }

        [Required]
        [EnumDataType(typeof(EmployeeType))]
        public int EmployeeTypeId { get; set; }

        public bool IsDeleted { get; set; }

        public EmployeeDto EmployeeToDto() => new EmployeeDto
        {
            Id = this.Id,
            FullName = this.FullName,
            Birthdate = this.Birthdate.ToString("yyyy-MM-dd"),
            Tin = this.Tin,
            TypeId = this.EmployeeTypeId
        };
    }
}
