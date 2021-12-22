using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace NewAssignment2KIT206
{
    using Controllers;

    namespace Researchers
    {
        /// <summary>
        /// A class baring a striking resemblance to a university researcher
        /// </summary>
        public class Researcher
        {
            public int ID { get; set; }                                             //Researcher ID
            public string Type { get; set; }                                        //Researcher Type (Staff or Student)
            public string GivenName { get; set; }                                   //Researcher Given name
            public string FamilyName { get; set; }                                  //Researcher Family name
            public string FullName { get; set; }                                    //Researcher Full name
            public string Title { get; set; }                                       //Researcher Title
            public string Unit { get; set; }                                        //Researcher working unit
            public string Campus { get; set; }                                      //Researcher working campus base
            public string Email { get; set; }                                       //Researcher email
            public string Photo { get; set; }                                       //Researcher Photo (URL Type)
            public string Degree { get; set; }                                      //User degree (If available)
            public string SupervisorID { get; set; }                                //Supervisor ID of researcher (If available)
            public EmploymentLevel Level { get; set; }                              //Level of researcher
            public List<Position> Positions { get; set; }                           //Past and current positions of researcher
            public List<Researcher> Supervision { get; set; }                       //Supervision list of researcher (If available)     
            public ObservableCollection<Publication> Publications { get; set; }     //Publications list of researcher

            public string GetCurrentJob                                             //Current job of researcher
            {
                get
                {
                    var currentJob = from Position p in Positions
                                     orderby p.Start ascending
                                     select p;

                    return currentJob.First().ToTitle(currentJob.First().Level);
                }
            }

            public DateTime CurrentJobStart                                         //Date of current position of researcher
            {
                get
                {
                    var currentJob = from Position p in Positions
                                     orderby p.Start descending
                                     select p;

                    return currentJob.First().Start;
                }
            }

            public DateTime EarliestJobStart                                        //Commence date of researcher with the Institution
            {
                get
                {
                    var earliestJob = from Position p in Positions
                                      orderby p.Start ascending
                                      select p;

                    return earliestJob.First().Start;
                }
            }

            public List<Position> EarlierJobs                                       //List of earlier jobs of researcher (If available)
            {
                get
                {
                    List<Position> pastJob = new List<Position>();

                    foreach (Position p in Positions)
                    {
                        pastJob.Add(p);
                    }

                    pastJob.RemoveAt(pastJob.Count - 1);

                    return pastJob;
                }
            }

            public double Tenure                                                    //Tenure of researcher
            {
                get
                {
                    double daysInYear = 365.0;

                    return Math.Round((DateTime.Today - EarliestJobStart).Days / daysInYear, 1);
                }
            }

            public int PublicationCount                                              //Researcher publication count
            {
                get { return Publications == null ? 0 : Publications.Count(); }
            }

            public double threeYearAverage                                          //Three year average of publications for reseacher
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

            public double getPerformance                                              //Performance of researcher (staff)
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

            public int SupervisionCount                                                 //Researcher supervisions count (staff)
            {
                get
                {
                    return Supervision.Count();
                }
            }

            public List<string> displayCommulativePublicationCount                      //Cummulative publication count for researcher
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

            public List<string> orderingItems                                           //Ordering items for publication list
            {
                get
                {
                    List<string> ordering = new List<string>();

                    ordering.Add("New to Old");
                    ordering.Add("Old to New");

                    return ordering;
                }
            }

            public override string ToString()                                            //To string method for researcher
            {
                return FamilyName + ", " + GivenName + " (" + Title + ")";
            }
        }
    }
}

