using System;
using System.Collections.Generic;
using System.Text;

namespace Linq.Entity
{
    public class Department
    {
        public int DepartmentId { get; set; }
        public string Name { get; set; }
        public IList<Employee> Employees { get; set; }
    }
}
