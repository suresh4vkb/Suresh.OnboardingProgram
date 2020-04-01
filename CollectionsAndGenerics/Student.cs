using Common.Utilities;
using System;
using System.Linq;

namespace CollectionsAndGenerics
{
    public class Student
    {
        #region Properties
        public int RegistrationNumber { get; set; }
        public string Name { get; set; }
        public int PhysicsScore { get; set; }
        public int ChemistryScore { get; set; }
        public int MathScore { get; set; }
        public int BiologyScore { get; set; }

        #endregion

        #region Constructor
        public Student(string name, int registrationNumber, int physicsScore, int chemistryScore, int mathScore,
            int biologyScore)
        {
            Name = name;
            RegistrationNumber = registrationNumber;
            PhysicsScore = physicsScore;
            ChemistryScore = chemistryScore;
            MathScore = mathScore;
            BiologyScore = biologyScore;
        }

        #endregion

        #region Business Methods

        /// <summary>
        /// Method used to retrieve list of students
        /// </summary>
        /// <returns></returns>
        public static Student[] GetSampleData()
        {
            Student[] arr = new Student[]
            {
                new Student("zz", 1025, 65, 58, 93, 76),
                new Student("yy", 1024, 66, 60, 91, 84),
                new Student("xx", 1023, 67, 62, 90, 77),
                new Student("ww", 1022, 68, 64, 89, 83),
                new Student("vv", 1021, 69, 66, 87, 82),
                new Student("uu", 1020, 70, 68, 87, 78),
                new Student("tt", 1019, 71, 70, 85, 81),
                new Student("ss", 1018, 72, 72, 84, 76),
                new Student("rr", 1017, 73, 74, 83, 80),
                new Student("qq", 1016, 74, 76, 81, 79),
                new Student("pp", 1015, 75, 78, 81, 78),
                new Student("oo", 1014, 76, 80, 79, 78),
                new Student("nn", 1013, 77, 82, 78, 80),
                new Student("mm", 1012, 78, 84, 77, 69),
                new Student("ll", 1011, 79, 86, 75, 70),
                new Student("kk", 1010, 80, 88, 75, 82),
                new Student("JJ", 1009, 81, 90, 73, 71),
                new Student("II", 1008, 82, 92, 72, 84),
                new Student("HH", 1007, 83, 94, 71, 72),
                new Student("GG", 1006, 84, 96, 69, 73),
                new Student("FF", 1005, 85, 98, 69, 86),
                new Student("EE", 1004, 86, 70, 67, 74),
                new Student("DD", 1003, 87, 71, 66, 88),
                new Student("CC", 1002, 88, 72, 65, 75),
                new Student("BB", 1001, 89, 73, 63, 90),
                new Student("AA", 1000, 90, 74, 60, 92)
            };
            return arr;
        }

        /// <summary>
        /// Search the student from students list
        /// </summary>
        /// <param name="registrationNumber"></param>
        public static void SearchStudent(int registrationNumber)
        {
            PrintMessage(registrationNumber);

            Student student = GetSampleData().Where(x => x.RegistrationNumber == registrationNumber)
                .Select(x => x).SingleOrDefault();

            if (student != null)
                student.Print();
            else
                PrintNoData();
        }

        /// <summary>
        /// Sorts the Students collection by total score
        /// </summary>
        public static void SortStudentsByScore()
        {
            var sortedList = GetSampleData().Select(x => new
            {
                x.Name,
                x.RegistrationNumber,
                TotalScore = x.BiologyScore + x.PhysicsScore + x.MathScore + x.ChemistryScore
            }).OrderBy(x => x.TotalScore).ToList();

            foreach (var student in sortedList)
                Print(student.Name, student.RegistrationNumber, student.TotalScore);
        }

        #endregion

        #region Helper Methods
        private void Print()
        {
            int totalScore = MathScore + PhysicsScore + ChemistryScore + BiologyScore;
            Message.Print(string.Format("Name:{0};Reg Number:{1};Maths:{2};Phy:{3};Chem:{4};Bio:{5};Total:{6}",
               Name,
               RegistrationNumber,
               MathScore,
               PhysicsScore,
               ChemistryScore,
               BiologyScore,
               totalScore
            ));
        }

        private static void PrintNoData()
        {
            Message.Print("No student details found");
        }

        private static void PrintMessage(int registrationNumber)
        {
            Message.Print("\nSearching for student with Reg. Number: " + registrationNumber);
        }

        private static void Print(string name, int registrationNumber, int totalScore)
        {
            Message.Print(string.Format("Name: {0}, Registration Number: {1}, Total Score: {2}", name, registrationNumber, totalScore));
        }

        #endregion
    }
}
