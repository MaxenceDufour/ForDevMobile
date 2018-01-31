using System;
using System.Data;
using System.Data.SqlClient;
using BusinessClassPortable;

namespace DAOClass
{
    /// <summary>
    /// Data Acces Object Login
    /// </summary>
    public sealed class DAOLogin : DAO
    {
        // Singleton
        private static DAOLogin _instance;
        static readonly object instanceLock = new object();

        private DAOLogin() { }

        public static DAOLogin getInstance()
        {
            lock (instanceLock)
            {
                if (_instance == null)
                    _instance = new DAOLogin();

                return _instance;
            }
        }

        /// <summary>
        /// Check if the username and password are correct in db
        /// </summary>
        /// <param name="login">user first name</param>
        /// <param name="password">user password</param>
        /// <returns>object person</returns>
        public Person CheckAccess(string login, string password)
        {
            Person person;

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand();
                command.CommandText = "CheckAccess";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@login", SqlDbType.NChar).Value = login;
                command.Parameters.Add("@password", SqlDbType.NChar).Value = password;
                command.Connection = connection;

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        person = new Person(
                            reader.GetInt32(0),
                            reader.GetString(1),
                            reader.GetString(2),
                            reader.GetString(3),
                            reader.GetString(4),
                            reader.GetInt16(5));
                    }
                    else
                        person = null;
                    reader.Close();
                    connection.Close();

                    if (person != null && person.PersonFirstName == login)
                        return person;

                    return person = null;
                }
                catch (Exception)
                {
                    return person = null;
                }
            }
        }

        /// <summary>
        /// Allows you to change the password in db
        /// </summary>
        /// <param name="personId">person id</param>
        /// <param name="password">person password</param>
        /// <returns>true if success</returns>
        public bool UpdatePassword(int personId, string password)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand();
                command.CommandText = "UpdatePassword";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@personId", SqlDbType.Int).Value = personId;
                command.Parameters.Add("@pass", SqlDbType.NChar).Value = password;
                command.Connection = connection;

                connection.Open();
                int result = command.ExecuteNonQuery();
                connection.Close();
                if(result > 0)
                    return true;
                return false;
            }
        }
    }
}
