using System;
using System.ComponentModel;

namespace UnitTestReference
{
    public class MyService:IService
    {
        #region Declaration

        public const string ConstantA = "A";
        public const string ConstantB = "B";
        public const string ArgumentNull = "Argument is null";
        public const string InvalidArgument = "Invalid argument";
        public const int LowerLimit = 1;
        public const int UpperLimit = 51;

        #endregion

        /// <summary>
        /// Returns A or B based on the input provided
        /// throws exception if number is null or 0
        /// </summary>
        /// <param name="number"></param>
        /// <returns>A or B</returns>
        public string ReturnAorB(int? number)
        {
            if (number == null)
                throw new ArgumentNullException();
            if (number < LowerLimit)
                throw new InvalidEnumArgumentException(InvalidArgument);
            
            return (number < UpperLimit) ? ConstantA: ConstantB;
        }
    }
}
