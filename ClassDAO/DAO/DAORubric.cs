using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using BusinessClassPortable;


namespace DAOClass
{
    /// <summary>
    /// Data Acces Object Rubric
    /// </summary>
    public sealed class DAORubric : DAO
    {
        // Singleton
        private static DAORubric _instance;
        static readonly object instanceLock = new object();
        private DAORubric() { }
        public static DAORubric getInstance()
        {
            lock (instanceLock)
            {
                if (_instance == null)
                    _instance = new DAORubric();

                return _instance;
            }
        }

        /// <summary>
        /// Get the rubric list in db
        /// </summary>
        /// <returns>list of rubric</returns>
        public List<Rubric> GetListRubric()
        {
            List<Rubric> myList = new List<Rubric>();

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand();
                command.CommandText = "GetRubric";
                command.CommandType = CommandType.StoredProcedure;
                command.Connection = connection;

                //try
                //{
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Rubric myRubric = new Rubric(
                                reader.GetInt32(0),
                                reader.GetString(1));
                            myList.Add(myRubric);
                        }
                    }
                    reader.Close();
                    connection.Close();
                //}
                //catch (SqlException e)
                //{
                //    MessageBox.Show(e.ToString(), "Connection lost", MessageBoxButtons.RetryCancel);
                //    GetListRubric();
                //}
                
                
            }
            return myList;
        }     
    }
}
