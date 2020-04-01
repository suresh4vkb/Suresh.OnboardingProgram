using System;
using System.Collections.Generic;
using System.Text;

namespace Linq.Entity
{
    public class Manager
    {
        public int ManagerId { get; set; }
        public string ManagerName { get; set; }
        public IList<Employee> Employees { get; set; }
    }
}
