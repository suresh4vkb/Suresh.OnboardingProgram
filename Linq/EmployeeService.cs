using System;
using System.Collections.Generic;
using System.Linq;
using Linq.Entity;

namespace Linq
{
    public class EmployeeService : IEmployeeService
    {
        private IList<Employee> _employees;
        private IDepartmentService _departmentService;
        private const decimal HikeAmount1 = 1000;
        private const decimal HikeAmount2 = 1500;
        private const decimal HikePercent = 10;
        private const int ReporteeCount = 2;

        public EmployeeService()
        {
            _departmentService = new DepartmentService();
            GetEmployees();
        }
        public IList<Employee> GetEmployees()
        {
            _employees = new List<Employee>();
            _employees.Add(new Employee() { EmployeeId = 1, Name = "Suresh Adimulam", DepartmentId = 102, Age = 30, Salary = 25000, Gender = "Male", ManagerId = null });
            _employees.Add(new Employee() { EmployeeId = 2, Name = "Sahana Kuntikana", DepartmentId = 101, Age = 30, Salary = 15000, Gender = "Female", ManagerId = null });
            _employees.Add(new Employee() { EmployeeId = 3, Name = "Sumanth Komaravelli", DepartmentId = 102, Age = 32, Salary = 35000, Gender = "Male", ManagerId = 1 });
            _employees.Add(new Employee() { EmployeeId = 4, Name = "Aneesh Bharatam", DepartmentId = 102, Age = 32, Salary = 25001, Gender = "Male", ManagerId = 1 });
            _employees.Add(new Employee() { EmployeeId = 5, Name = "Saurabh Mishra  ", DepartmentId = 101, Age = 31, Salary = 25999, Gender = "Male", ManagerId = 1 });
            _employees.Add(new Employee() { EmployeeId = 6, Name = "Suchethana Mukarji", DepartmentId = 103, Age = 30, Salary = 43455, Gender = "Female", ManagerId = 2 });
            _employees.Add(new Employee() { EmployeeId = 7, Name = "Ratna Joshi Yadala", DepartmentId = 103, Age = 30, Salary = 43400, Gender = "Male", ManagerId = 2 });
            _employees.Add(new Employee() { EmployeeId = 8, Name = "Swamy Vekanuru  ", DepartmentId = 103, Age = 30, Salary = 10503, Gender = "Male", ManagerId = 2 });
            _employees.Add(new Employee() { EmployeeId = 9, Name = "Sai Kiran Tirunagr", DepartmentId = 103, Age = 30, Salary = 50015, Gender = "Male", ManagerId = 10 });
            _employees.Add(new Employee() { EmployeeId = 10, Name = "Venkat Karimekala", DepartmentId = 101, Age = 30, Salary = 60000, Gender = "Male", ManagerId = null });

            return _employees;
        }

        public Employee GetEmployeeById(int employeeId)
        {
            return _employees.Where(x => x.EmployeeId == employeeId).Select(x=>x).SingleOrDefault();            
        }

        /// <summary>
        /// Gets the collection of employees with odd Salaries.
        /// </summary>
        /// <returns></returns>
        public IList<Employee> GetOddSalaryEmployees()
        {
            return _employees.AsParallel().Where(x => x.Salary % 2 != 0).Select(x => x).ToList();
        }

        /// <summary>
        /// Task 1: Gets all the employees under a department using LINQ grouping
        /// </summary>
        /// <returns></returns>
        public Department GetEmployeesByDepartment(int departmentId)
        {
            return _employees.GroupBy(x => x.DepartmentId)
                                      .Select(x => new Department
                                      {
                                          DepartmentId = x.Key,
                                          Name = _departmentService.GetDepartmentById(x.Key).Name,
                                          Employees = _employees.Where(p => p.DepartmentId == x.Key).ToList()
                                      })
                                      .Where(p => p.DepartmentId == departmentId)
                                      .SingleOrDefault();
        }

        /// <summary>
        /// Task 2: Gets the Highest Salary Employees details by Department
        /// </summary>
        /// <returns></returns>
        public IList<Employee> GetHighestSalaryByDepartment()
        {
            var highestSalaryByDepartment = (from emp in _employees
                                             group emp by emp.DepartmentId into empGroup
                                             let max = empGroup.Max(sal => sal.Salary)
                                             select new Employee
                                             {
                                                 Department = _departmentService.GetDepartmentById(empGroup.Key),
                                                 Name = empGroup.First(val => val.Salary == max).Name,
                                                 Salary = empGroup.First(val => val.Salary == max).Salary
                                             }).ToList();

            return highestSalaryByDepartment;
        }

        /// <summary>
        /// Returns the Manager details whose Reportees are more than 2
        /// </summary>
        /// <returns></returns>
        public IList<Employee> GetManagersByReporteeCount()
        {
            return _employees.Join(_employees, emp => emp.ManagerId, manager => manager.EmployeeId, (emp, manager) => new { emp, manager })  // Self Join
                             .Select(p => new
                             {
                                 ManagerId = p.manager.EmployeeId,
                                 ManagerName = p.manager.Name
                             })
                             .GroupBy(x => x.ManagerId)  // Grouping with Manager Id, we will get reportee count.
                             .Where(x => x.Count() > ReporteeCount)  // Busines Case, Reportee Count should be more than 2
                             .Select(x => new Employee
                             {
                                 EmployeeId = x.Key,
                                 Name = x.Where(p => p.ManagerId == x.Key)
                                                .Select(y => y.ManagerName)
                                                .FirstOrDefault()
                             }).ToList();
        }

        /// <summary>
        /// Hike the Employee Salary Based on Business Rule
        /// Male Employee Salary should incremented by 1000 & Female Employee Salary should incremented by 1500
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        public Employee HikeSalary(int employeeId)
        {
            var employee = _employees.Where(x => x.EmployeeId == employeeId).Select(x => x).SingleOrDefault();
            if (employee != null)
            {
                employee.Salary += GetHike(employee.Gender);
                return employee;
            }
            return employee;
        }


        public IList<Employee> GetEmployeesByManager(int managerId)
        {
            return _employees.Where(x => x.ManagerId == managerId).Select(x=>x).ToList();
        }

        public Employee HikeSalaryInPercent(int employeeId)
        {
            var employee = _employees.Where(x => x.EmployeeId == employeeId).Select(x => x).SingleOrDefault();
            if (employee != null)
            {
                employee.Salary = GetTenPercentHike(employee.Salary);
                return employee;
            }
            return employee;
        }

        /// <summary>
        /// Cloning the object. 
        /// </summary>
        /// <returns></returns>
        public EmployeeService DeepCopy() {
            return new EmployeeService();            
        }

        #region Helper Methods

        private decimal GetHike(string gender)
        {
            return gender == Gender.Male.ToString() ? HikeAmount1 : gender == Gender.Female.ToString() ? HikeAmount2 : 0;
        }
        private decimal GetTenPercentHike(decimal currentSalary)
        {
            return currentSalary + (currentSalary * HikePercent / 100);
        }

        #endregion
    }
}
