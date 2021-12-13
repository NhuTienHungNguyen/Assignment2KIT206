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

        abstract class PublicationController
        {

            public List<Publication> LoadPublications(int id)
            {
                List<Publication> publications = ERPAdapter.LoadTrainingSessions(id);

                return publications;
            }

            public List<Publication> filterByYears(int id, int Start, int End)
            {
                List<Publication> publications = LoadPublications(id);
                List<Publication> filterredList = new List<Publication>();

                foreach (Publication p in publications)
                {
                    if (Start <= p.Year && p.Year <= End)
                    {
                        filterredList.Add(p);
                    }
                }

                return filterredList;
            }

            public List<Publication> orderredByYear(List<Publication> publications)
            {
                IEnumerable<Publication> filteredList = from publication in publications
                                                        orderby publication.Available
                                                        select publication;

                List<Publication> newList = filteredList.ToList();

                return newList;
            }
        }
    }
}
