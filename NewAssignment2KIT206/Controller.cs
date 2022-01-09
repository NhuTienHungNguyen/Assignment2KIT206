using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewAssignment2KIT206
{
    using Researchers;
    using Adapter;

    namespace Controllers
    {
        public enum EmploymentLevel { Any, Student, A, B, C, D, E };

        public class Controller
        {
            private List<Researcher> researchers;
            public List<Researcher> allResearchers { get { return researchers; } set { } }

            private ObservableCollection<Researcher> viewableResearchers;
            public ObservableCollection<Researcher> VisibleResearchersList { get { return viewableResearchers; } set { } }

            public Controller()
            {
                researchers = ERDAdapter.LoadAll();
                viewableResearchers = new ObservableCollection<Researcher>(researchers);

                foreach (Researcher e in researchers)
                {
                    e.Publications = ERDAdapter.LoadPublications(e.ID);
                    e.Positions = ERDAdapter.LoadPositions(e);
                    e.Supervision = ERDAdapter.LoadSupervision(e.ID);
                }
            }

            public ObservableCollection<Researcher> GetViewableList()
            {
                return VisibleResearchersList;
            }

            public ObservableCollection<Researcher> GetStarPerformanceResearcher()
            {
                ObservableCollection<Researcher> newList = new ObservableCollection<Researcher>();

                var OrderedList = from Researcher r in VisibleResearchersList
                                  where r.getPerformance >= 200.0
                                  orderby r.getPerformance descending
                                  select r;

                foreach (Researcher r in OrderedList)
                {
                    newList.Add(r);
                }

                return newList;
            }

            public ObservableCollection<Researcher> GetMeetMinimumResearcher()
            {
                ObservableCollection<Researcher> newList = new ObservableCollection<Researcher>();

                var OrderedList = from Researcher r in VisibleResearchersList
                                  where r.getPerformance >= 110.0 && r.getPerformance < 200.0
                                  orderby r.getPerformance descending
                                  select r;

                foreach (Researcher r in OrderedList)
                {
                    newList.Add(r);
                }

                return newList;
            }

            public ObservableCollection<Researcher> GetBelowExpResearcher()
            {
                ObservableCollection<Researcher> newList = new ObservableCollection<Researcher>();

                var OrderedList = from Researcher r in VisibleResearchersList
                                  where r.getPerformance >= 70.0 && r.getPerformance < 110.0
                                  orderby r.getPerformance ascending
                                  select r;

                foreach (Researcher r in OrderedList)
                {
                    newList.Add(r);
                }

                return newList;
            }

            public ObservableCollection<Researcher> GetPoorResearcher()
            {
                ObservableCollection<Researcher> newList = new ObservableCollection<Researcher>();

                var OrderedList = from Researcher r in VisibleResearchersList
                                  where r.Type == "Staff" && r.getPerformance < 70.0
                                  orderby r.getPerformance ascending
                                  select r;

                foreach (Researcher r in OrderedList)
                {
                    newList.Add(r);
                }

                return newList;
            }

            public void Filter(EmploymentLevel level)
            {
                var selected = from Researcher e in researchers
                               where level == EmploymentLevel.Any || e.Level == level
                               select e;
                viewableResearchers.Clear();

                selected.ToList().ForEach(viewableResearchers.Add);
            }

        }
    }
}

