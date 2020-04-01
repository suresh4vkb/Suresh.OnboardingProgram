using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Utilities
{
    public class Message
    {
        #region Console Messages
        public static void Print(string message)
        {            
            Console.WriteLine(message);
        }

        public static void PrintWithOutNewLine(string message)
        {
            Console.Write(message);
        }

        public static void PrintTask(string taskName)
        {
            Console.WriteLine("\n{0}: \n---------------------------------------", taskName);
        }

        public static void PrintSubTask(string subTaskName, int subTaskNumber)
        {
            Console.WriteLine("\nTask {0}. {1}: \n", subTaskNumber, subTaskName);
        }

        //public static void PrintEmployeeDetails<T>(T employee)
        //{
        //    throw new NotImplementedException();
        //}

        //public static void PrintEmployees<T>(T employees)
        //{
        //    throw new NotImplementedException();
        //}

        //public static void PrintDepartments<T>(T employees)
        //{
        //    throw new NotImplementedException();
        //}

        #endregion
    }
}
