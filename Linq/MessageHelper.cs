using Common.Utilities;
using Linq.Entity;
using System.Collections.Generic;
using System.Linq;

namespace Linq
{
    public class MessageHelper
    {
        #region Helper Methods to Print Output

        /// <summary>
        /// Prints the employees information
        /// </summary>
        /// <param name="employees">employees</param>
        public static void PrintEmployeeDetails(IList<Employee> employees)
        {
            foreach (var emp in employees)
                Message.Print(string.Format("Name: {0}, Salary: {1}", emp.Name, emp.Salary));
        }

        /// <summary>
        /// Prints the manager information
        /// </summary>
        /// <param name="employees"></param>
        public static void PrintManagerDetails(IList<Employee> employees)
        {
            foreach (var emp in employees)
                Message.Print(string.Format("Manager Name: {0}", emp.Name));
        }

        /// <summary>
        /// Prints the employee revised salary details
        /// </summary>
        /// <param name="employee"></param>
        public static void PrintRevisedSalary(Employee employee)
        {
            Message.Print(string.Format("Name: {0}, Revised Salary: {1}, Gender: {2}", employee.Name, employee.Salary, employee.Gender));
        }

        /// <summary>
        /// Prints the department details
        /// </summary>
        /// <param name="department"></param>
        public static void PrintDepartmentEmployees(Department department)
        {
            Message.Print(string.Format("Department Name: {0}, Department Id: {1}", department.Name, department.DepartmentId));
            if (department.Employees.Any())
                foreach (var emp in department.Employees)
                    Message.Print(string.Format("Name: {0}, Salary: {1}", emp.Name, emp.Salary));
            else
                Message.Print(string.Format("No Employees found in {0} Department", department.Name));
        }

        /// <summary>
        /// Prints the list of employee details
        /// </summary>
        /// <param name="employees"></param>
        public static void PrintEmployees(IList<Employee> employees)
        {
            Message.Print("Employee Id \tName \t\t\tAge \tGender \tSalary \tDepartment Id \tManager Id");
            Message.Print("---------------------------------------------------------------------------------------------------------");
            if (employees.Any())
                foreach (var emp in employees)
                    Message.Print(string.Format("{0} \t\t{1} \t{2} \t{3} \t{4} \t{5} \t\t{6}", emp.EmployeeId, emp.Name, emp.Age, emp.Gender, emp.Salary, emp.DepartmentId, emp.ManagerId != null ? emp.ManagerId : 0));
            else
                Message.Print("No Employees found");
        }

        /// <summary>
        /// Prints the list of department details
        /// </summary>
        /// <param name="departments"></param>
        public static void PrintDepartments(IList<Department> departments)
        {
            Message.Print(string.Format("\nDepartment Id \tDepartment Name"));
            Message.Print("--------------------------------------------");

            if (departments.Any())
                foreach (var dept in departments)
                    Message.Print(string.Format("{0} \t\t{1}", dept.DepartmentId, dept.Name));
            else
                Message.Print("No Departments found");
        }

        #endregion
    }
}
