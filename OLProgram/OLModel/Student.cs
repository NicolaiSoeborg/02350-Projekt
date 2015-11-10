using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLModel
{
    public class Student
    {
        private static int StudentIdCounter = 0;

        public int StudentId { get; }

        public string Name { get; set; }

        public Student() : this("", "") { }

        public Student(string FirstName, string Name)
        {
            this.StudentId = StudentIdCounter++;
            this.Name = Name;
        }
    }
    /*
    public class StudyProgramme
    {
        private static int ProgrammeIdCounter = 0;

        public int ProgrammeId { get; }

        public string ProgrammeName { get; set; }
        public Boolean IsBEngProgramme { get; set; } //diplom?

        public StudyProgramme(String ProgrammeName, Boolean IsBEngProgramme)
        {
            this.ProgrammeId = ProgrammeIdCounter++;
            this.ProgrammeName = ProgrammeName;
            this.IsBEngProgramme = IsBEngProgramme;
        }
    }*/
}
