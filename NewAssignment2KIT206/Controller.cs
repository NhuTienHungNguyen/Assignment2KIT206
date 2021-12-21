using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewAssignment2KIT206
{
    public class Controller
    {
        //The example shown here follows the pattern discussed in the Module 6 summary slides,
        //maintaining two collections, a master and a 'viewable' one (which is the 'view model'
        //in Microsoft's Model-View-ViewModel approach to Model-View-Controller)
        private List<Researcher> researchers;
        public List<Researcher> allResearchers { get { return researchers; } set { } }

        private ObservableCollection<Researcher> viewableResearchers;
        public ObservableCollection<Researcher> VisibleResearchersList { get { return viewableResearchers; } set { } }

        public Controller()
        {
            researchers = ERDAdapter.LoadAll();
            viewableResearchers = new ObservableCollection<Researcher>(researchers); //this list we will modify later

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
            //Converts the result of the LINQ expression to a List and then calls viewableStaff.Add with each element of that list in turn
            selected.ToList().ForEach(viewableResearchers.Add);
        }

    }
}

