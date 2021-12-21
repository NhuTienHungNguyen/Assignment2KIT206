using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace WpfApp1
{

    /// <summary>
    /// A class baring a striking resemblance to a university researcher
    /// </summary>
    public class Researcher
    {
        public int ID { get; set; }
        public string Type { get; set; }
        public string GivenName { get; set; }
        public string FamilyName { get; set; }
        public string FullName { get; set; }
        public string Title { get; set; }
        public string Unit { get; set; }
        public string Campus { get; set; }
        public string Email { get; set; }
        public string Photo { get; set; }
        public string Degree { get; set; }
        public string SupervisorID { get; set; }
        public EmploymentLevel Level { get; set; }
        public List<Position> Positions { get; set; }
        public List<Researcher> Supervision { get; set; }
        public ObservableCollection<Publication> Publications { get; set; }

        public string GetCurrentJob
        {
            get
            {
                var currentJob = from Position p in Positions
                                 orderby p.Start ascending
                                 select p;

                return currentJob.First().ToTitle(currentJob.First().Level);
            }
        }

        public DateTime CurrentJobStart
        {
            get
            {
                var currentJob = from Position p in Positions
                                 orderby p.Start descending
                                 select p;

                return currentJob.First().Start;
            }
        }

        public DateTime EarliestJobStart
        {
            get
            {
                var earliestJob = from Position p in Positions
                                  orderby p.Start ascending
                                  select p;

                return earliestJob.First().Start;
            }
        }

        public double Tenure
        {
            get
            {
                double daysInYear = 365.0;

                return Math.Round((DateTime.Today - EarliestJobStart).Days / daysInYear, 1);
            }
        }

        public int PublicationCount
        {
            get { return Publications == null ? 0 : Publications.Count(); }
        }

        public double threeYearAverage
        {
            get
            {
                double threeYearPublicationCount = 0.0;

                foreach (Publication t in Publications)
                {
                    if ((t.Year >= (DateTime.Today.Year - 3)) && (t.Year <= (DateTime.Today.Year - 1)))
                    {
                        threeYearPublicationCount++;
                    }
                }

                return Math.Round(threeYearPublicationCount / 3, 1);
            }
        }

        public double getPerformance
        {
            get
            {
                if (Type == "Staff")
                {
                    double realPublications = threeYearAverage;
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

                    return (Math.Round(100 * (realPublications / expectedPublications), 1));
                }
                else
                {
                    return 0;
                }
            }
        }

        public int SupervisionCount
        {
            get
            {
                return Supervision.Count();
            }
        }

        public List<string> displayCommulativePublicationCount
        {
            get
            {
                int commulativeCount = 0;
                List<string> Commulative = new List<string>();

                for (int i = EarliestJobStart.Year; i <= (DateTime.Today.Year); i++)
                {
                    foreach (Publication t in Publications)
                    {
                        if (t.Year == i)
                        {
                            commulativeCount++;
                        }
                    }

                    Commulative.Add("Commulative count in " + i + " is: " + commulativeCount);
                }

                return Commulative;
            }
        }

        public List<string> orderingItems
        {
            get
            {
                List<string> ordering = new List<string>();

                ordering.Add("New to Old");
                ordering.Add("Old to New");

                return ordering;
            }
        }

        public override string ToString()
        {
            //For the purposes of this week's demonstration this returns only the name
            return FamilyName + ", " + GivenName + " (" + Title + ")";
        }
    }
}
