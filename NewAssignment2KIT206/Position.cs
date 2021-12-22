using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewAssignment2KIT206
{
    using Controllers;

    namespace Researchers
    {

        public class Position
        {
            public int ID { get; set; }                         //ID of researcher
            public EmploymentLevel Level { get; set; }          //Employment level of the following position
            public DateTime Start { get; set; }                 //Start day of the position
            public DateTime End { get; set; }                   //End day of the position (if available)

            //To title of the position
            public string ToTitle(EmploymentLevel e)
            {
                string title;                                   //The title of the researcher
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

            //ToString method for the position
            public override string ToString()
            {
                string start = Start.ToString("dd-MM-yyyy");  //The start date of the position
                string end;                                   //The end date of the position
                string title = ToTitle(Level);                //The title of the position

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
}

