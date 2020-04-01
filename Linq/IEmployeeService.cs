using System;
using System.Collections.Generic;
using System.Text;
using Linq.Entity;

namespace Linq
{
    public interface IEmployeeService
    {
        IList<Employee> GetEmployees();

        Employee GetEmployeeById(int employeeId);

        IList<Employee> GetOddSalaryEmployees();

        Department GetEmployeesByDepartment(int departmentId);

        IList<Employee> GetHighestSalaryByDepartment();

        IList<Employee> GetManagersByReporteeCount();

        Employee HikeSalary(int employeeId);

        Employee HikeSalaryInPercent(int employeeId);
        IList<Employee> GetEmployeesByManager(int managerId);

        EmployeeService DeepCopy();
    }
}
