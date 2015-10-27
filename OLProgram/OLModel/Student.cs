using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLModel
{
    class Student
    {
        private static int StudentIdCounter = 0;

        public int StudentId { get; }

        public string LastName { get; set; }
        public string FirstName { get; set; }

        public StudyProgramme StudyProgramme { get; set; }

        public Student() : this("", "", null) { }

        public Student(string FirstName, string LastName, StudyProgramme StudyProgramme)
        {
            this.StudentId = StudentIdCounter++;
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.StudyProgramme = StudyProgramme;
        }
    }

    class StudyProgramme
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
    }
}
