using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using BusinessClassPortable;

namespace DAOClass
{
    /// <summary>
    /// Data Acces Object Topic
    /// </summary>
    public sealed class DAOTopic : DAO
    {
        // Sigleton
        private static DAOTopic _instance;
        static readonly object instanceLock = new object();
        private DAOTopic() { }
        public static DAOTopic getInstance()
        {
            lock (instanceLock)
            {
                if (_instance == null)
                    _instance = new DAOTopic();

                return _instance;
            }
        }

        /// <summary>
        /// Retrieves the last ten topic, all rubric confused
        /// </summary>
        /// <returns>list topic</returns>
        public List<Topic> GetLastTopicTop10()
        {
            List<Topic> myList = new List<Topic>();
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand();
                command.CommandText = "GetLastTopic";
                command.CommandType = CommandType.StoredProcedure;
                command.Connection = connection;

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Topic myTopic = new Topic(
                            reader.GetInt32(0),
                            reader.GetString(1),
                            reader.GetString(2),
                            reader.GetDateTime(3),
                            reader.GetDateTime(4),
                            reader.GetInt16(5),
                            reader.GetInt32(6),
                            reader.GetString(7),
                            reader.GetString(8),
                            reader.GetInt32(9),
                            reader.GetString(10));
                        myList.Add(myTopic);
                    }
                    reader.Close();
                    connection.Close();
                }
                connection.Close();
            }
            return myList;
        }

        /// <summary>
        /// Retrieves the last ten topics from a rubric
        /// </summary>
        /// <param name="rubricId">rubric id</param>
        /// <returns>list topic</returns>
        public List<Topic> GetLastTopicTop10ById(int rubricId)
        {
            List<Topic> myList = new List<Topic>();
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand();
                command.CommandText = "GetLastTopicsOfRubric";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@rubricId", SqlDbType.Int).Value = rubricId;
                command.Connection = connection;

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Topic myTopic = new Topic();
                        myTopic.TopicId = reader.GetInt32(0);
                        myTopic.TypeTopic = reader.GetInt16(1);
                        myTopic.Rubric.RubricId = reader.GetInt32(2);
                        myTopic.Title = reader.GetString(3);
                        myTopic.Person.PersonFirstName = reader.GetString(4);
                        myTopic.Person.PersonLastName = reader.GetString(5);
                        myTopic.Description = reader.GetString(6);
                        myTopic.DateAdd = reader.GetDateTime(7);
                        myTopic.DateUp = reader.GetDateTime(8);
                        myList.Add(myTopic);
                    }
                }
                reader.Close();
                connection.Close();
            }
            return myList;
        }

        /// <summary>
        /// Add a topic in db
        /// </summary>
        /// <param name="topic">object topic</param>
        /// <param name="personId">person id</param>
        /// <returns>true if success</returns>
        public bool CreateTopic(Topic topic, int personId)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand();
                command.CommandText = "CreateTopic";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@personId", SqlDbType.Int).Value = personId;
                command.Parameters.Add("@typeTopic", SqlDbType.SmallInt).Value = topic.TypeTopic;
                command.Parameters.Add("@rubricId", SqlDbType.Int).Value = topic.Rubric.RubricId;
                command.Parameters.Add("@title", SqlDbType.VarChar).Value = topic.Title;
                command.Parameters.Add("@description", SqlDbType.VarChar).Value = topic.Description;
                command.Parameters.Add("@dateAdd", SqlDbType.DateTime).Value = topic.DateAdd;
                command.Parameters.Add("@dateUp", SqlDbType.DateTime).Value = topic.DateAdd;
                command.Parameters.Add("@replyContent", SqlDbType.VarChar).Value = topic.Message;
                command.Connection = connection;
 
                connection.Open();
                int resultAdd = command.ExecuteNonQuery();
                connection.Close();

                if (resultAdd > 0)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Delete a topic in db
        /// </summary>
        /// <param name="topicId">topic id</param>
        /// <returns>true if success</returns>
        public bool DeleteTopicInDb(int topicId)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand();
                command.CommandText = "DeleteTopic";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@topicId", SqlDbType.Int).Value = topicId;
                command.Connection = connection;

                connection.Open();
                int resultDelete = command.ExecuteNonQuery();
                connection.Close();

                if (resultDelete > 0)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Edits a topic in db
        /// </summary>
        /// <param name="topic">object topic</param>
        /// <returns>true if success</returns>
        public bool EditTopicInDb(Topic topic)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand();
                command.CommandText = "EditTopic";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@topicId", SqlDbType.Int).Value = topic.TopicId;
                command.Parameters.Add("@title", SqlDbType.NVarChar).Value = topic.Title;
                command.Parameters.Add("@description", SqlDbType.NVarChar).Value = topic.Description;
                command.Parameters.Add("@dateUpdate", SqlDbType.DateTime).Value = topic.DateUp;
                command.Connection = connection;

                connection.Open();
                int resultUpdate = command.ExecuteNonQuery();
                connection.Close();

                if (resultUpdate > 0)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Get a topic by his id in db
        /// </summary>
        /// <param name="topicId">topic id</param>
        /// <returns>object topic</returns>
        public Topic GetTopicById(int topicId)
        {
            Topic myTopic = null;
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand();
                command.CommandText = "GetTopicById";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@topicId", SqlDbType.Int).Value = topicId;
                command.Connection = connection;

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    myTopic = new Topic(
                        topicId,
                        reader.GetString(0),
                        reader.GetString(1),
                        reader.GetDateTime(2),
                        reader.GetDateTime(3),
                        reader.GetInt16(4));
                    reader.Close();
                    connection.Close();
                    return myTopic;
                }
                return null;
            }
        }
    }
}
