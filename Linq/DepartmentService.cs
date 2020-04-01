using System;
using System.Collections.Generic;
using System.Linq;
using Linq.Entity;

namespace Linq
{
    public class DepartmentService : IDepartmentService
    {
        private IList<Department> _departments;

        public DepartmentService()
        {
            GetDepartments();
        }
       
        public IList<Department> GetDepartments()
        {
            _departments = new List<Department>();
            _departments.Add(new Department() { DepartmentId = 101, Name = "HR"});
            _departments.Add(new Department() { DepartmentId = 102, Name = "IT" });
            _departments.Add(new Department() { DepartmentId = 103, Name = "Administration" });
            _departments.Add(new Department() { DepartmentId = 104, Name = "Operations" });

            return _departments;
        }

        public Department GetDepartmentById(int departmentId)
        {
            return _departments.Where(x => x.DepartmentId == departmentId).Select(x => x).SingleOrDefault();
        }
    }
}
