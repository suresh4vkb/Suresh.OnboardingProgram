using Common.Utilities;
using System;

namespace CSharpFeatures
{
    /// <summary>
    /// Task 2 : This is a custom class which demonstrates Expression body Members.
    /// </summary>
    public class Student
    {
        #region Local Variables        
        
        private readonly string[] _skills = { "Dot Net", "Javascript", "SQL Server", "Angular Js", "Web Designing", "MVC" };
        private string _otherSkill;

        #endregion

        #region Constructor

        /// <summary>
        /// Expression Body for Constructor
        /// </summary>
        /// <param name="name"></param>
        public Student(string name) => Name = name;

        #endregion

        #region Properties

        public string Name { get; set; }
        
        /// <summary>
        /// Expression body for Property/Member
        /// </summary>
        public string OtherSkill
        {
            get => _otherSkill;
            set => _otherSkill = value;
        }

        /// <summary>
        /// Indexer
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public string this[int index] {
            get => _skills[index];
            set => _skills[index] = value;
        }

        #endregion

        #region Methods
        public void ShowOtherSkill() => Message.Print("Other Skill is " + _otherSkill);

        public void ShowSkill(int index) => Message.Print(string.Format("Skill of index {0} is {1}", index, this[index]));

        public void ShowSkills() => Message.Print(string.Format("\n{0} has the following skills: \n{1}", Name, string.Join(",", _skills)));

        #endregion

        #region Finalizer

        /// <summary>
        /// Finalizer
        /// </summary>
        ~Student() => Message.Print("Clearing all the resources.");
        #endregion
    }
}
