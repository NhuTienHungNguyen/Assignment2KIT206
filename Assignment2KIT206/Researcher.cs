i
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2KIT206
{
   namespace Researchers
    {
        using Controller;

        public class Researcher
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public string Type { get; set; }
            public string Title { get; set; }
            public EmploymentLevel Level { get; set; }
            public string School { get; set; }
            public string Campus { get; set; }
            public string Email { get; set; }
            public string Photo { get; set; }
            public List<Publication> Skills { get; set; }
            public List<Position> Positions { get; set; }

            public Position GetCurrentJob()
            {
                return Positions[Positions.Count() - 1];
            }

            public string CurrentJobTitle()
            {
                Position currentPos = GetCurrentJob();

                return currentPos.ToTitle(currentPos.Level);
            }

            public DateTime CurrentJobStart()
            {
                Position currentPos = GetCurrentJob();

                return currentPos.Start;
            }

            public Position GetEarliestJob()
            {
                return Positions[0];
            }

            public DateTime EarliestStart()
            {
                Position EarliesPos = GetEarliestJob();

                return EarliesPos.Start;
            }

            public string GetPreviousJob()
            {
                string previousJobs = "";

                if (Positions.Count == 1)
                {
                    previousJobs = "NULL \n";
                }
                else
                {
                    for (int i = 0; i < Positions.Count - 1; i++)
                    {
                        previousJobs += (Positions[i].ToString() + "\n");
                    }
                }

                return previousJobs;
            }

            public double Tenure()
            {
                double daysInYear = 365.0;

                return Math.Round((DateTime.Today - EarliestStart()).Days / daysInYear, 1);
            }

            public int PublicationsCount()
            {

                return Skills.Count;
            }

            public void displayCommulativePublicationCount()
            {
                int commulativeCount;

                for (int i = EarliestStart().Year; i <= (DateTime.Today.Year); i++)
                {
                    commulativeCount = 0;
                    foreach (Publication t in Skills)
                    {
                        if (t.Year == i)
                        {
                            commulativeCount++;
                        }
                    }

                    Console.WriteLine("Number of commulative publications in year {0} is: {1}", i, commulativeCount);
                }
            }

            public override string ToString()
            {
                return String.Format("Name: {0} ({1})", Name, Title);
            }
        }

        public class Staff : Researcher
        {
            public double ThreeYearAverage()
            {
                double threeYearPublicationCount = 0.0;

                foreach (Publication t in Skills)
                {
                    if ((t.Year >= (DateTime.Today.Year - 3)) && (t.Year <= (DateTime.Today.Year - 1)))
                    {
                        threeYearPublicationCount++;
                    }
                }

                return Math.Round(threeYearPublicationCount / 3, 1);
            }

            public double Performance()
            {
                double realPublications = ThreeYearAverage();
                double expectedPublications;

                switch (Level)
                {
                    case EmploymentLevel.A:
                        expectedPublications = 0.5;
                        break;
                    case EmploymentLevel.B:
                        expectedPublications = 1;
                        break;
                    case EmploymentLevel.C:
                        expectedPublications = 2;
                        break;
                    case EmploymentLevel.D:
                        expectedPublications = 3.2;
                        break;
                    default:
                        expectedPublications = 4;
                        break;
                }

                return Math.Round(100 * (realPublications / expectedPublications), 1);
            }

            public List<Student> supervision()
            {
                List<Researcher> researchers = ResearcherController.LoadResearcher();
                List<Student> supervisions = new List<Student>();

                foreach (Researcher r in researchers)
                {
                    if (r.Type == "Student")
                    {
                        Student s = ResearcherController.LoadStudent(r.ID);

                        if (s.SupervisorID == ID)
                        {
                            supervisions.Add(s);
                        }
                    }
                }

                return supervisions;
            }

            public int supervisionCount()
            {
                List<Student> supervisions = supervision();
                return supervisions.Count();
            }

            public void displaySupervision()
            {
                List<Student> supervisions = supervision();
                int count = supervisionCount();

                if (count > 0)
                {
                    foreach (Student s in supervision())
                    {
                        Console.WriteLine(s.ToString());
                    }
                }
                else
                {
                    Console.WriteLine("Researcher has no supvervision");
                }
            }

            public string ToDetailsString()
            {
                return String.Format("Name: {0} \n" +
                                          "Title: {1} \n" +
                                          "Unit: {2} \n" +
                                          "Campus: {3} \n" +
                                          "Email: {4} \n" +
                                          "Current job: {5} \n" +
                                          "Commenced with institution: {6} \n" +
                                          "Commenced with current position: {7} \n" +
                                          "Previous positions: \n{8}" +
                                          "Tenure: {9} years     Publications: {10} (Show Cumulative Count) \n" +
                                          "3-year average: {11}   Performance: {12}% \n" +
                                          "Supervision: {13} (Show Names)", Name, Title, School, Campus, Email,
                                          GetCurrentJob().ToTitle(Level), EarliestStart().ToString("dd-MM-yyyy"), CurrentJobStart().ToString("dd-MM-yyyy"), GetPreviousJob(),
                                          Tenure(), PublicationsCount(), ThreeYearAverage(),
                                          Performance(), supervisionCount());
            }
        }

        public class Student : Researcher
        {
            public string Degree { get; set; }
            public int SupervisorID { get; set; }

            public string ToDetailsString()
            {
                return String.Format("Name: {0} \n" +
                                          "Title: {1} \n" +
                                          "Unit: {2} \n" +
                                          "Campus: {3} \n" +
                                          "Email: {4} \n" +
                                          "Current job: {5} \n" +
                                          "Commenced with institution: {6} \n" +
                                          "Commenced with current position: {7} \n" +
                                          "Previous positions: \n{8}" +
                                          "Tenure: {9} years     Publications: {10} (Show Cumulative Count) \n", Name, Title, School, Campus, Email,
                                          GetCurrentJob().ToTitle(Level), EarliestStart().ToString("dd-MM-yyyy"), CurrentJobStart().ToString("dd-MM-yyyy"), GetPreviousJob(),
                                          Tenure(), PublicationsCount());
            }
        }
    }
}
