using System;

namespace ExceptionHandling
{
    public class MyException : ApplicationException
    {
        public MyException() : base("This is my custom exception message")
        {

        }

        public void IndexNotFoundException()
        {
            Common.Utilities.Message.Print("Index was out of range exception.");
        }

        public void FileNotFoundException()
        {
            Common.Utilities.Message.Print("File not found exception.");
        }

        public void InvalidArgumentException() { }
    }
}
