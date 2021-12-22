using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewAssignment2KIT206
{
    namespace Researchers
    {
        //In this fictitious example these are the 'modes' in which training can occur,
        //but you may recognise them as something else related to the assignment
        public enum Type { Conference, Journal, Other };

        /// <summary>
        /// A training session undertaken by an employee on a particular date.
        /// Created in task 1.2 of the Week 8 tutorial.
        /// </summary>
        public class Publication
        {
            public string DOI { get; set; }
            public string Title { get; set; }
            public string Authors { get; set; }
            public int Year { get; set; }
            public Type Mode { get; set; }
            public string Cite_as { get; set; }
            public DateTime Available { get; set; }

            public int Age
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
                //This is a straightforward way of constructing the string using DateTime's
                //ToShortDateString method to remove the time component of the complted date
                return Title + " completed by " + Mode + " on " + Available.ToShortDateString();

                //This alternative approach uses the Format method of string, with the
                //short date format requested via the :d in the format string
                //return string.Format("{0} completed by {1} on {2:d}", Title, Mode, Certified);
            }

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


