using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessClass;

namespace DAOClass
{
    public class DAOPerson : DAO
    {
        private static DAOPerson _instance;
        static readonly object instanceLock = new object();

        private DAOPerson()
        {

        }

        public static DAOPerson getInstance()
        {
            lock (instanceLock)
            {
                if (_instance == null)
                    _instance = new DAOPerson();

                return _instance;
            }
        }

        public List<Person> GetLastTopicAuthorTop10()
        {
            List<Person> myList = new List<Person>();
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand();
                command.CommandText = "GetLastTopicAuthorTop10";
                command.CommandType = CommandType.StoredProcedure;
                command.Connection = connection;

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Person newPerson = new Person();
                        newPerson.PersonId = reader.GetInt32(0);
                        newPerson.PersonFirstName = reader.GetString(1);
                        newPerson.PersonLastName = reader.GetString(2);
                        myList.Add(newPerson);
                    }
                }
                reader.Close();
                connection.Close();
            }
            return myList;
        }
    }
}
