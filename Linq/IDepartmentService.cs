using System;
using System.Collections.Generic;
using System.Text;
using Linq.Entity;

namespace Linq
{
    public interface IDepartmentService
    {
        IList<Department> GetDepartments();
        Department GetDepartmentById(int departmentId);
                      
    }
}
