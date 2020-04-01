using System;
using System.Collections.Generic;
using System.Text;

namespace Linq.Entity
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public decimal Salary { get; set; }
        public Department Department { get; set; }
        public int? ManagerId { get; set; }
        public int DepartmentId { get; set; }
    }

    public enum Gender
    {
        Male,
        Female
    }
}
