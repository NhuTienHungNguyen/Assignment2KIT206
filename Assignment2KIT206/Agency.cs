using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2KIT206
{
    abstract class Agency
    {
        public static List<Position> GeneratePositions()
        {
            return new List<Position>()
                {
                    new Position { ID = 123460, Level = EmploymentLevel.B, Start = new DateTime(2010, 6, 17) },
                    new Position { ID = 123461, Level = EmploymentLevel.A, Start = new DateTime(2006, 1, 19), End = new DateTime(2010, 2, 16) },
                    new Position { ID = 123461, Level = EmploymentLevel.B, Start = new DateTime(2010, 2, 17), End = new DateTime(2013, 12, 31) },
                    new Position { ID = 123461, Level = EmploymentLevel.C, Start = new DateTime(2014, 1, 1) },
                    new Position { ID = 123462, Level = EmploymentLevel.A, Start = new DateTime(2011, 1, 10), End = new DateTime(2012, 4, 15) },
                    new Position { ID = 123462, Level = EmploymentLevel.B, Start = new DateTime(2012, 4, 16) },
                    new Position { ID = 123463, Level = EmploymentLevel.B, Start = new DateTime(2012, 1, 28) },
                    new Position { ID = 123464, Level = EmploymentLevel.B, Start = new DateTime(2007, 1, 19), End = new DateTime(2011, 3, 13) },
                    new Position { ID = 123464, Level = EmploymentLevel.C, Start = new DateTime(2011, 3, 14), End = new DateTime(2014, 4, 11) },
                    new Position { ID = 123464, Level = EmploymentLevel.D, Start = new DateTime(2014, 4, 12) },
                    new Position { ID = 123465, Level = EmploymentLevel.A, Start = new DateTime(2014, 5, 23) },
                };
        }

        public static List<Publication> GeneratePublications()
        {
            return new List<Publication>()
                {
                    new Publication { DOI = "10.1007/11504894_31", Title = "Structural advantages for ant colony optimisation inherent in permutation scheduling problems", Authors = "Alexandra Halloran, Gemma Stanford", Year = 2017, Type = OutputType.Conference, CiteAs = " Halloran, A and Stanford, G, Structural advantages for ant colony optimisation inherent in permutation scheduling problems, Proceedings 18th International ", Available = new DateTime(2017, 7, 13) },

                };
        }
    }
}
