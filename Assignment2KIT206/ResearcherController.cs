using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2KIT206
{
    namespace Controller
    {
        using Adapter;
        using Researchers;

        abstract class ResearcherController
        {
            public static T ParseEnum<T>(string value)
            {
                return (T)Enum.Parse(typeof(T), value);
            }
            public static List<Researcher> LoadResearcher()
            {
                List<Researcher> researchers = ERPAdapter.LoadAll();

                return researchers;
            }

            public static Researcher fetchFullResearcherDetails(int id)
            {
                Researcher newResearcher = new Researcher();
                List<Researcher> researchers = LoadResearcher();

                foreach (Researcher r in researchers)
                {
                    if (r.ID == id)
                    {
                        newResearcher = r;
                    }
                }

                return newResearcher;
            }

            public static Staff LoadStaff(int id)
            {
                Researcher r = fetchFullResearcherDetails(id);
                Staff staff = ERPAdapter.completeStaffDetails(r);

                return staff;
            }

            public static Student LoadStudent(int id)
            {
                Researcher r = fetchFullResearcherDetails(id);
                Student student = ERPAdapter.completeStudentDetails(r);

                return student;
            }

            public List<Researcher> filterByName(string name)
            {
                List<Researcher> researchersList = LoadResearcher();

                IEnumerable<Researcher> filteredList = from researcher in researchersList
                                                       where (researcher.Name == name)
                                                       select researcher;

                List<Researcher> filteredResearchers = filteredList.ToList();

                return filteredResearchers;
            }

            public List<Researcher> filterByLevel(string level)
            {
                EmploymentLevel enumLevel = ParseEnum<EmploymentLevel>(level);

                List<Researcher> researchersList = LoadResearcher();

                IEnumerable<Researcher> filteredList = from researcher in researchersList
                                                       where researcher.Level == enumLevel
                                                       select researcher;

                List<Researcher> filteredResearchers = filteredList.ToList();

                return filteredResearchers;
            }

            public List<Researcher> orderedByName()
            {
                List<Researcher> researchersList = LoadResearcher();

                IEnumerable<Researcher> filteredList = from researcher in researchersList
                                                       orderby researcher.Name
                                                       select researcher;

                List<Researcher> newList = filteredList.ToList();

                return newList;
            }
        }
    }
}
