using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewAssignment2KIT206
{
    public enum EmploymentLevel { Any, Student, A, B, C, D, E };

    public class Position
    {
        /// <summary> 
        /// A training session undertaken by an employee on a particular date.
        /// Created in task 1.2 of the Week 9 tutorial.
        /// </summary>
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

        public override string ToString()
        {
            string start = Start.ToString("dd-MM-yyyy");
            string end;
            string title = ToTitle(Level);

            if (DateTime.Compare(End, DateTime.Today) == 0)
            {
                end = "NULL";
            }
            else
            {
                end = End.ToString("dd-MM-yyyy");
            }
            return String.Format("Start: {0}, End: {1},  {2}", start, end, title);
        }

    }
}

