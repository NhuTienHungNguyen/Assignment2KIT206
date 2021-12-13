using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Assignment2KIT206
{
    namespace Adapter
    {
        using Researchers;

        abstract class ERPAdapter
        {
            private const string db = "kit206";
            private const string user = "kit206";
            private const string pass = "kit206";
            private const string server = "alacritas.cis.utas.edu.au";

            private static MySqlConnection conn = null;

            public static T ParseEnum<T>(string value)
            {
                return (T)Enum.Parse(typeof(T), value);
            }

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


            public static List<Researcher> LoadAll()
            {
                List<Researcher> staff = new List<Researcher>();

                MySqlConnection conn = GetConnection();
                MySqlDataReader rdr = null;

                EmploymentLevel level;

                try
                {
                    conn.Open();

                    MySqlCommand cmd = new MySqlCommand("select id, type, given_name, family_name, title, unit, campus, email, photo, level from researcher", conn);
                    rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        if (rdr.GetString(1) == "Staff")
                        {
                            level = ParseEnum<EmploymentLevel>(rdr.GetString(9));
                        }
                        else
                        {
                            level = ParseEnum<EmploymentLevel>(rdr.GetString(1));
                        }

                        //Note that in your assignment you will need to inspect the *type* of the
                        //employee/researcher before deciding which kind of concrete class to create.
                        staff.Add(new Researcher { ID = rdr.GetInt32(0), Name = rdr.GetString(2) + " " + rdr.GetString(3), Title = rdr.GetString(4), School = rdr.GetString(5), Campus = rdr.GetString(6), Email = rdr.GetString(7), Photo = rdr.GetString(8), Type = rdr.GetString(1), Level = level });

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

                return staff;
            }

            public static Staff completeStaffDetails(Researcher e)
            {
                Staff newStaff = new Staff();

                newStaff.ID = e.ID;
                newStaff.Name = e.Name;
                newStaff.Type = e.Type;
                newStaff.Title = e.Title;
                newStaff.Level = e.Level;
                newStaff.School = e.School;
                newStaff.Campus = e.Campus;
                newStaff.Email = e.Email;
                newStaff.Photo = e.Photo;
                newStaff.Positions = e.Positions;
                newStaff.Skills = e.Skills;

                return newStaff;
            }

            public static Student completeStudentDetails(Researcher e)
            {
                MySqlDataReader rdr = null;
                Student newStudent = new Student();
                string degree = "";
                int supervisor_id = 0;

                newStudent.ID = e.ID;
                newStudent.Name = e.Name;
                newStudent.Type = e.Type;
                newStudent.Title = e.Title;
                newStudent.Level = e.Level;
                newStudent.School = e.School;
                newStudent.Campus = e.Campus;
                newStudent.Email = e.Email;
                newStudent.Photo = e.Photo;

                try
                {
                    conn.Open();

                    MySqlCommand cmd = new MySqlCommand("select id, degree, supervisor_id from researcher", conn);

                    rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        if (rdr.GetInt32(0) == newStudent.ID)
                        {
                            degree = rdr.GetString(1);
                            supervisor_id = rdr.GetInt32(2);
                        }
                    }
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Error connecting to database: " + ex);
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

                newStudent.Degree = degree;
                newStudent.SupervisorID = supervisor_id;

                return newStudent;

            }

            public static List<Publication> LoadTrainingSessions(int id)
            {
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
                                work.Add(new Publication { DOI = rdr.GetString(0), Title = rdr.GetString(1), Authors = rdr.GetString(2), Year = rdr.GetInt32(3), Type = ParseEnum<OutputType>(rdr.GetString(4)), CiteAs = rdr.GetString(5), Available = Convert.ToDateTime(rdr.GetString(6)) });
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

                return work;
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

                return positions;
            }
        }
    }
}


