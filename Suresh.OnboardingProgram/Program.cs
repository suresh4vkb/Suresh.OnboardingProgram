using Common.Utilities;
using CSharpFeatures;
using ExceptionHandling;
using Linq;
using MemoryManagement;
using System;
using System.Dynamic;
using StudentCollection = CollectionsAndGenerics.Student;

namespace Suresh.OnboardingProgram
{
    /// <summary>
    /// This console application demonstrates on all the .Net onboarding program tasks.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            #region C# 7 New Features
            Message.PrintTask(Resources.CSharpFeature);

            // Task 1: Invoking and Deconstruct Tuple
            TupleTask task = new TupleTask();
            var (firstName, middleName, lastName) = task.GetData(Constants.FirstName, Constants.MiddleName, Constants.LastName);
            Message.Print(string.Format(Resources.GetFullName, firstName, middleName, lastName));

            // Task 2: Expression Bodied Members
            Student student = new Student(Constants.FirstName);
            student.ShowSkills();
            student.OtherSkill = Skills.AWS.ToString();
            student.ShowOtherSkill();
            student.ShowSkill(2);

            // Updating the skill by index
            student[2] = Skills.Java.ToString();
            student.ShowSkill(2);

            // Task 3: Expando Object
            dynamic dynamicEmployee = new ExpandoObject();

            // Properties of Employee
            Employee employee = new Employee()
            {
                Email = Constants.Email,
                Name = Constants.FirstName,
                Salary = Constants.Salary
            };

            dynamicEmployee.Email = employee.Email;
            dynamicEmployee.Name = employee.Name;
            dynamicEmployee.Salary = employee.Salary;

            // New Expando Properties
            dynamicEmployee.Address = Constants.Address;
            dynamicEmployee.Age = Constants.Age;

            // New Expando Method
            dynamicEmployee.GetDetails = new Action(() => { Message.Print(string.Format("\nName: {0}, Age: {1}, Address: {2}", dynamicEmployee.Name, dynamicEmployee.Age, dynamicEmployee.Address)); });

            // Invoking the Expando Method
            dynamicEmployee.GetDetails();

            #endregion

            #region Memory Management   

            Message.PrintTask(Resources.MemoryManagement);

            // Task 1: Displays the property value using "using" block
            using (MyClass myClass = new MyClass())
            {
                Message.Print(string.Format(Resources.IntegerResult, myClass.MyInteger));
            }

            // Task 2: Disposing the object using finally block
            MyClass myClassObject = new MyClass();
            try
            {
                Message.Print(string.Format(Resources.IntegerResult, myClassObject.MyInteger));
            }
            catch (Exception exception)
            {
                Message.Print(exception.Message);
            }
            finally
            {
                myClassObject.Dispose();
            }

            #endregion

            #region Linq

            Message.PrintTask(Resources.Linq);

            IEmployeeService employService = new EmployeeService();
            IDepartmentService departmentService = new DepartmentService();

            MessageHelper.PrintEmployees(employService.GetEmployees());
            MessageHelper.PrintDepartments(departmentService.GetDepartments());

            Message.PrintSubTask("Odd Salary Employee Details", 1);
            var employees = employService.GetOddSalaryEmployees();
            foreach (var employ in employees)
                Message.Print(string.Format("'{0}' has salary Rs.{1}/-", employ.Name, employ.Salary));

            Message.PrintSubTask("Get Employee Details by Department ", 2);
            var departmentEmployees = employService.GetEmployeesByDepartment(102);
            MessageHelper.PrintDepartmentEmployees(departmentEmployees);

            var highestSalaryByDepartment = employService.GetHighestSalaryByDepartment();
            Message.PrintSubTask("Highest Salary By Department", 3);

            foreach (var highSalaryEmployee in highestSalaryByDepartment)
                Message.Print(string.Format("In Department '{0}', '{1}' has highest salary Rs.{2}/-", highSalaryEmployee.Department.Name, highSalaryEmployee.Name, highSalaryEmployee.Salary));

            Message.PrintSubTask("Get Manager Details whose reportees are more than 2", 4);
            var managerList = employService.GetManagersByReporteeCount();
            MessageHelper.PrintManagerDetails(managerList);

            Message.PrintSubTask("Hike Salary with Business Rules", 5);
            int maleEmployeeId = 1, femaleEmployeeId = 2;
            var employeeRevisedSalary = employService.HikeSalary(maleEmployeeId);
            MessageHelper.PrintRevisedSalary(employeeRevisedSalary);
            employeeRevisedSalary = employService.HikeSalary(femaleEmployeeId);
            MessageHelper.PrintRevisedSalary(employeeRevisedSalary);

            Message.PrintSubTask("Get the salary details of employees under specific manager and increment the salary", 6);
            int managerId = 10;
            IEmployeeService cloneEmployeeService = employService.DeepCopy();
            var employList = employService.GetEmployeesByManager(managerId);
            var employListCopy = cloneEmployeeService.GetEmployeesByManager(managerId);

            Message.Print("Before Deep Copy Salary Change");
            MessageHelper.PrintEmployeeDetails(employList);
            MessageHelper.PrintEmployeeDetails(employListCopy);

            foreach (var emp in employListCopy)
                emp.Salary = cloneEmployeeService.HikeSalaryInPercent(emp.EmployeeId).Salary;

            Message.Print("After Deep Copy Salary Change");
            MessageHelper.PrintEmployeeDetails(employList);
            MessageHelper.PrintEmployeeDetails(employListCopy);

            #endregion

            #region Collections and Generics

            Message.PrintTask(Resources.CollectionsAndGenerics);

            int registrationNumber = 1021;
            StudentCollection.SearchStudent(registrationNumber);

            // Sort the students by Total Score
            StudentCollection.SortStudentsByScore();
            #endregion

            #region Delegates And Events

            Message.PrintTask(Resources.DelegatesAndEvents);
            DelegatesAndEvents.ShoppingCart.PerformBilling();

            #endregion
            
            #region Exception Handling

            Message.PrintTask(Resources.ExceptionHandling);
            ExceptionDemo.ReadWeekDays();
            ExceptionDemo.ReadFileContent();

            #endregion
            
            Console.ReadLine();
        }
    }
}
