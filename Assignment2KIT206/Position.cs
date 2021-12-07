using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2KIT206
{
    public enum EmploymentLevel
    {
        Student,
        A,
        B,
        C,
        D,
        E
    };
    public class Position
    {
        public int ID { get; set; }
        public EmploymentLevel Level { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public string ToTitle(EmploymentLevel e)
        {
            string title;
            switch (e)
            {
                case EmploymentLevel.Student:
                    title = "Student";
                    break;
                case EmploymentLevel.A:
                    title = "Post-doc";
                    break;
                case EmploymentLevel.B:
                    title = "Lecturer";
                    break;
                case EmploymentLevel.C:
                    title = "Senior Lecturer";
                    break;
                case EmploymentLevel.D:
                    title = "Associate Professor";
                    break;
                default:
                    title = "Professor";
                    break;
            }

            return title;
        }
    }
}

