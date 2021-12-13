using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2KIT206
{
   namespace Researchers
    {
        //In this fictitious example these are the 'modes' in which training can occur,
        //but you may recognise them as something else related to the assignment
        public enum OutputType { Conference, Journal, Other };

        /// <summary> 
        /// A training session undertaken by an employee on a particular date.
        /// Created in task 1.2 of the Week 9 tutorial.
        /// </summary>
        public class Publication
        {
            public string DOI { get; set; }
            public string Title { get; set; }
            public string Authors { get; set; }
            public int Year { get; set; }
            public OutputType Type { get; set; }
            public string CiteAs { get; set; }
            public DateTime Available { get; set; }

            public int Freshness
            {
                //DateTime.Today returns today's date. As DateTime objects overload the
                //addition and subtraction operators we can use them to determine the
                //elapsed time between today's date and the Completed date. However, 
                //the result is not a number but a TimeSpan object, whose Days
                //property gives the number of whole days represented by the TimeSpan.
                get { return (DateTime.Today - Available).Days; }
            }

            public override string ToString()
            {
                return String.Format("DOI: {0}\t, Authors: {1}\t", DOI, Authors);
            }

            public string ToDetailsString()
            {
                return String.Format("DOI: {0} \n" +
                                     "Title: {1} \n" +
                                     "Author: {2} \n" +
                                     "Year: {3} \n" +
                                     "Type: {4} \n" +
                                     "Cite as: {5} \n" +
                                     "Available date: {6} \n", DOI, Title, Authors, Year, Type, CiteAs, Available.ToString("dd-MM-yyyy"));
            }
        }
    }
}
