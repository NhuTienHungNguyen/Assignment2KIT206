using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2KIT206
{
    using Researchers;
    using Adapter;
    using Controller;

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("This is some sample code Assignment 2");

            List<Researcher> researchers = ResearcherController.LoadResearcher();

            Console.WriteLine("testing has begun");

            foreach (Researcher r in researchers)
            {
                Console.WriteLine(r.ToString());
                Console.WriteLine();
            }

            int choice;

            do
            {
                showMainMenu();

                Console.WriteLine("Enter your choice: ");
                choice = Convert.ToInt32(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        showResearcherDetails();
                        break;
                    case 2:
                        Console.WriteLine("2");
                        break;
                    default:
                        break;
                }

            } while (choice < 3);

            Console.ReadLine();
        }

        public static void showMainMenu()
        {
            Console.WriteLine("Please enter the number for query: \n" +
                              "1. Show researcher details \n" +
                              "2. Show achievement report \n" +
                              "3. Quit");
            Console.WriteLine();
        }

        public static void showStaffMenu()
        {
            Console.WriteLine("Please enter the number for query: \n" +
                              "1. Show cumulative count \n" +
                              "2. Show supervision \n" +
                              "3. Show publication details \n" +
                              "4. Quit");
            Console.WriteLine();
        }

        public static void showStudentMenu()
        {
            Console.WriteLine("Please enter the number for query: \n" +
                              "1. Show cumulative count \n" +
                              "2. Show publication details \n" +
                              "3. Quit");
            Console.WriteLine();
        }

        public static void showResearcherDetails()
        {
            int researcherid;
            bool checkStaff = false;

            Console.WriteLine("Please enter researcher ID: ");

            researcherid = Convert.ToInt32(Console.ReadLine());

            List<Researcher> researchers = ResearcherController.LoadResearcher();

            foreach (Researcher r in researchers)
            {
                if (r.ID == researcherid && r.Type == "Staff")
                {
                    checkStaff = true;
                }
            }

            if (checkStaff == true)
            {
                Staff newStaff = new Staff();

                newStaff = ResearcherController.LoadStaff(researcherid);

                newStaff.Skills = ERPAdapter.LoadTrainingSessions(newStaff.ID);
                newStaff.Positions = ERPAdapter.LoadPositions((Researcher)newStaff);

                Console.WriteLine(newStaff.ToDetailsString());

                foreach (Publication p in newStaff.Skills)
                {
                    Console.WriteLine(p.ToString());
                }

                int choice;

                do
                {
                    showStaffMenu();

                    Console.WriteLine("Enter your choice: ");
                    choice = Convert.ToInt32(Console.ReadLine());

                    switch (choice)
                    {
                        case 1:
                            newStaff.displayCommulativePublicationCount();
                            break;
                        case 2:
                            newStaff.displaySupervision();
                            break;
                        case 3:
                            string doi;
                            Console.WriteLine("Please enter publication DOI: ");
                            doi = Console.ReadLine();

                            Publication p = new Publication();

                            foreach (Publication pub in newStaff.Skills)
                            {
                                if (pub.DOI == doi)
                                {
                                    p = pub;
                                }
                            }

                            Console.WriteLine(p.ToDetailsString());
                            break;
                        default:
                            break;
                    }

                } while (choice < 4);

                Console.WriteLine();


            }
            else
            {
                Student newStudent = new Student();

                newStudent = ResearcherController.LoadStudent(researcherid);

                newStudent.Skills = ERPAdapter.LoadTrainingSessions(newStudent.ID);
                newStudent.Positions = ERPAdapter.LoadPositions((Researcher)newStudent);

                Console.WriteLine(newStudent.ToDetailsString());

                Console.WriteLine();

                foreach (Publication p in newStudent.Skills)
                {
                    Console.WriteLine(p.ToString());
                }


                int choice;

                do
                {
                    showStudentMenu();

                    Console.WriteLine("Enter your choice: ");
                    choice = Convert.ToInt32(Console.ReadLine());

                    switch (choice)
                    {
                        case 1:
                            newStudent.displayCommulativePublicationCount();
                            break;
                        case 2:
                            string doi;
                            Console.WriteLine("Please enter publication DOI: ");
                            doi = Console.ReadLine();

                            Publication p = new Publication();

                            foreach (Publication pub in newStudent.Skills)
                            {
                                if (pub.DOI == doi)
                                {
                                    p = pub;
                                }
                            }

                            Console.WriteLine(p.ToDetailsString());
                            break;
                        default:
                            break;
                    }

                } while (choice < 3);

                Console.WriteLine();
            }
        }
    }
}
