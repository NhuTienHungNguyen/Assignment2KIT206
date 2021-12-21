using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows; //for generating a MessageBox upon encountering an error
using System.Diagnostics;


namespace NewAssignment2KIT206
{
    abstract class ERDAdapter
    {
        //If including error reporting within this class (as this sample does) then you'll need a way
        //to control whether the errors are actually shown or silently ignored, since once you have
        //connected the GUI to the Boss object then the GUI designer will execute its code, which
        //will try to connect to the database to load live data into the GUI at design time.
        private static bool reportingErrors = false;

        //These would not be hard coded in the source file normally, but read from the application's settings (and, ideally, with some amount of basic encryption applied)
        private const string db = "kit206";
        private const string user = "kit206";
        private const string pass = "kit206";
        private const string server = "alacritas.cis.utas.edu.au";

        private static MySqlConnection conn = null;

        //Part of step 2.3.3 in Week 8 tutorial. This method is a gift to you because .NET's approach to converting strings to enums is so horribly broken
        public static T ParseEnum<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value);
        }

        /// <summary>
        /// Creates and returns (but does not open) the connection to the database.
        /// </summary>
        private static MySqlConnection GetConnection()
        {
            if (conn == null)
            {
                //Note: This approach is not thread-safe
                string connectionString = String.Format("Database={0};Data Source={1};User Id={2};Password={3}", db, server, user, pass);
                conn = new MySqlConnection(connectionString);
            }
            return conn;
        }

        //For step 2.2 in Week 8 tutorial
        public static List<Researcher> LoadAll()
        {
            List<Researcher> staff = new List<Researcher>();

            MySqlConnection conn = GetConnection();
            MySqlDataReader rdr = null;

            string degree;
            string supervisorid;
            EmploymentLevel level;
            string databasePhoto;
            string validPhoto = "";

            try
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand("select id, type, given_name, family_name, title, unit, campus, email, photo, level, degree, supervisor_id from researcher", conn);
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    bool checkValid = false;
                    int checkPosition = 0;

                    if (rdr.GetString(1) == "Staff")
                    {
                        degree = "Not available";
                        supervisorid = "Not available";
                        level = ParseEnum<EmploymentLevel>(rdr.GetString(9));
                    }
                    else
                    {
                        degree = rdr.GetString(10);
                        supervisorid = (rdr.GetInt32(11)).ToString();
                        level = ParseEnum<EmploymentLevel>(rdr.GetString(1));
                    }

                    databasePhoto = rdr.GetString(8);

                    while (checkValid != true)
                    {
                        if (databasePhoto[checkPosition] == '?')
                        {
                            checkValid = true;
                        }
                        else
                        {
                            checkPosition++;
                        }
                    }

                    validPhoto = databasePhoto.Substring(0, checkPosition);

                    //Note that in your assignment you will need to inspect the *type* of the
                    //employee/researcher before deciding which kind of concrete class to create.
                    staff.Add(new Researcher
                    {
                        ID = rdr.GetInt32(0),
                        Type = rdr.GetString(1),
                        GivenName = rdr.GetString(2),
                        FamilyName = rdr.GetString(3),
                        FullName = rdr.GetString(2) + " " + rdr.GetString(3),
                        Title = rdr.GetString(4),
                        Unit = rdr.GetString(5),
                        Campus = rdr.GetString(6),
                        Email = rdr.GetString(7),
                        Photo = validPhoto, 
                        Level = level,
                        SupervisorID = supervisorid,
                        Degree = degree
                    });

                    /*Debug.WriteLine("@@@@@@@@@@@@@@@");
                    Debug.WriteLine(staff[0].Photo);
                    Debug.WriteLine("@@@@@@@@@@@@@@@");*/
                }
            }
            catch (MySqlException e)
            {
                ReportError("loading staff", e);
            }
            finally
            {
                if (rdr != null)
                {
                    rdr.Close();
                }
                if (conn != null)
                {
                    conn.Close();
                }
            }

            return staff;
        }

        //For step 2.3 in Week 8 tutorial
        public static ObservableCollection<Publication> LoadPublications(int id)
        {
            ObservableCollection<Publication> orderedWork = new ObservableCollection<Publication>();
            List<Publication> work = new List<Publication>();

            MySqlConnection conn = GetConnection();
            MySqlDataReader rdr = null;

            try
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand("SELECT publication.doi, publication.title, publication.authors, publication.year, publication.type, publication.cite_as, publication.available, researcher_publication.researcher_id, researcher_publication.doi FROM `publication`, `researcher`,`researcher_publication` WHERE publication.doi = researcher_publication.doi AND researcher_publication.researcher_id = id", conn);


                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    if (rdr.GetInt32(7) == id)
                    {
                        if (rdr.GetInt32(7) == id)
                        {
                            work.Add(new Publication { 
                                DOI = rdr.GetString(0),
                                Title = rdr.GetString(1), 
                                Authors = rdr.GetString(2), 
                                Year = rdr.GetInt32(3), 
                                Mode = ParseEnum<Type>(rdr.GetString(4)), 
                                Cite_as = rdr.GetString(5), 
                                Available = rdr.GetDateTime(6) });
                        }
                    }

                }
            }
            catch (MySqlException e)
            {
                Console.WriteLine("Error connecting to database: " + e);
            }
            finally
            {
                if (rdr != null)
                {
                    rdr.Close();
                }
                if (conn != null)
                {
                    conn.Close();
                }
            }


            var orderingPublicationList = from Publication p in work
                                          orderby p.Available.Year descending, p.Title ascending
                                          select p;

            work = orderingPublicationList.ToList();

            foreach (Publication p in work)
            {
                orderedWork.Add(p);
            }    
            
            return orderedWork;
        }

        public static List<Researcher> LoadSupervision(int id)
        {
            List<Researcher> allResearcher = LoadAll();
            List<Researcher> supervision = new List<Researcher>();

            foreach (Researcher e in allResearcher)
            {
                if (e.SupervisorID == id.ToString())
                {
                    supervision.Add(e);
                }
            }

            return supervision;
        }

        public static List<Position> LoadPositions(Researcher r)
        {
            List<Position> positions = new List<Position>();

            MySqlConnection conn = GetConnection();
            MySqlDataReader rdr = null;

            try
            {
                conn.Open();

                if (r.Type == "Student")
                {
                    MySqlCommand cmd = new MySqlCommand("select id, type, utas_start from researcher", conn);

                    rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        if (rdr.GetInt32(0) == r.ID)
                        {
                            positions.Add(new Position { ID = rdr.GetInt32(0), Level = ParseEnum<EmploymentLevel>(rdr.GetString(1)), Start = rdr.GetDateTime(2), End = DateTime.Today });
                        }
                    }
                }
                else
                {
                    MySqlCommand cmd = new MySqlCommand("select id, level, start, end from position", conn);

                    rdr = cmd.ExecuteReader();
                    DateTime end;

                    while (rdr.Read())
                    {
                        if (rdr.GetInt32(0) == r.ID)
                        {
                            if (rdr["end"] is DBNull)
                            {
                                end = DateTime.Today;
                            }
                            else
                            {
                                end = rdr.GetDateTime(3);
                            }
                            positions.Add(new Position { ID = rdr.GetInt32(0), Level = ParseEnum<EmploymentLevel>(rdr.GetString(1)), Start = rdr.GetDateTime(2), End = end });
                        }
                    }
                }
            }
            catch (MySqlException e)
            {
                Console.WriteLine("loading positions: " + e);
            }
            finally
            {
                if (rdr != null)
                {
                    rdr.Close();
                }
                if (conn != null)
                {
                    conn.Close();
                }
            }

            return positions;
        }

        /// <summary>
        /// In a more complete application this error would be logged to a file
        /// and the error reported back to the original caller, who is closer
        /// to the GUI and hence better able to produce the error message box
        /// (which would not show the actual error details like this does).
        /// </summary>
        private static void ReportError(string msg, Exception e)
        {
            if (reportingErrors)
            {
                MessageBox.Show("An error occurred while " + msg + ". Try again later.\n\nError Details:\n" + e,
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}

