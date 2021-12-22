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

