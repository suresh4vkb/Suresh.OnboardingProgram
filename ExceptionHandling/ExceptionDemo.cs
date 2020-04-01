using Common.Utilities;
using System.IO;

namespace ExceptionHandling
{
    public class ExceptionDemo
    {
        public static void ReadWeekDays()
        {
            try
            {
                ReadData();
            }
            catch (MyException exception)
            {
                // Custom Index Out Of Range Exception
                exception.IndexNotFoundException();
            }
        }

        public static void ReadFileContent()
        {
            StreamReader file = null;
            const string filePath = @"C:\ExceptionHandling\MyFile.txt";
            try
            {
                if (!File.Exists(filePath))
                    throw new MyException();
                file = new StreamReader(filePath);
                char[] buffer = new char[10];
                file.ReadBlock(buffer, 0, buffer.Length);
            }
            catch (MyException exception)
            {
                exception.FileNotFoundException();
            }
            finally
            {
                file?.Close();
            }
        }

        #region Helper Methods
                
        private static void ReadData()
        {
            var list = new string[5];
            list[0] = "Sunday";
            list[1] = "Monday";
            list[2] = "Tuesday";
            list[3] = "Wednesday";
            list[4] = "Thursday";
            for (var i = 0; i <= list.Length; i++)
            {
                if (!IsIndexInRange(i, list.Length))
                    throw new MyException();
                Message.Print(!string.IsNullOrEmpty(list[i])?list[i]: "Value is not assigned.");
            }
        }

        private static bool IsIndexInRange(int currentIndex, int arrayLength)
        {
            return (currentIndex >= 0) && (currentIndex < arrayLength);
        }
        #endregion
    }
}
