using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2KIT206
{
    public class Researcher
    {
        public int ID { get; set; }
        public string GivenName { get; set; }
        public string FamilyName { get; set; }
        public string Title { get; set; }
        public string School { get; set; }
        public string Campus { get; set; }
        public string Email { get; set; }

        public Uri Photo { get; set; }

        List<Position> positions;
        public Researcher()
        {
            positions = Agency.GeneratePositions();
        }

        public Position GetCurrentJob()
        {
            return positions[positions.Count - 1];
        }

        public string CurrentJobTitle()
        {
            return GetCurrentJob().ToTitle(GetCurrentJob().Level);
        }

        public DateTime CurrentJobStart()
        {
            return GetCurrentJob().Start;
        }

        public Position GetEarliestJob()
        {
            return positions[0];
        }

        public DateTime EarliestStart()
        {
            return GetEarliestJob().Start;
        }

        public double Tenure()
        {
            double daysInYear = 365.0;

            return ((CurrentJobStart() - EarliestStart()).Days) / daysInYear;
        }

        //Testing method
        public void DisplayPositions()
        {
            Console.WriteLine(GetCurrentJob().ToTitle(GetCurrentJob().Level));
        }
    }

    public class Staff : Researcher
    {
        public float ThreeYearAverage()
        {
            return 0;
        }

        public float Performance()
        {
            return 0;
        }
    }

    public class Student : Researcher
    {
        public string Degree { get; set; }
    }
}
