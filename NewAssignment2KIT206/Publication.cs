using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewAssignment2KIT206
{
    namespace Researchers
    {
        //Enum type for publication type
        public enum Type { Conference, Journal, Other };

        /// <summary>
        /// A training session undertaken by an employee on a particular date.
        /// Created in task 1.2 of the Week 8 tutorial.
        /// </summary>
        public class Publication
        {
            public string DOI { get; set; }                 //Publication DOI
            public string Title { get; set; }               //Publication Title
            public string Authors { get; set; }             //Publication Author(s)
            public int Year { get; set; }                   //Publication available Year
            public Type Mode { get; set; }                  //Publication type
            public string Cite_as { get; set; }             //Publication cite as string
            public DateTime Available { get; set; }         //Publication available date

            public int Age                                  //Age of Publication until now
            {
                get { return (DateTime.Today - Available).Days; }
            }

            //ToString method to display publication
            public override string ToString()
            {
                return Title + " completed by " + Mode + " on " + Available.ToShortDateString();
            }

            //String method to display details of a publication
            public string ToDetailedString()
            {
                return String.Format("DOI: {0} \n" +
                                     "Title: {1} \n" +
                                     "Author: {2} \n" +
                                     "Year: {3} \n" +
                                     "Type: {4} \n" +
                                     "Cite as: {5} \n" +
                                     "Available date: {6} \n" +
                                     "Age: {7} day(s)", DOI, Title, Authors, Year, Mode, Cite_as, Available.ToString("dd-MM-yyyy"), Age);
            }
        }
    }    
}


