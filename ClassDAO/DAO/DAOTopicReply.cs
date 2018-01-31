using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using BusinessClassPortable;

namespace DAOClass
{
    /// <summary>
    /// Data Acces Obect topicReply
    /// </summary>
    public sealed class DAOTopicReply : DAO
    {
        // Singleton
        private static DAOTopicReply _instance;
        static readonly object instanceLock = new object();
        private DAOTopicReply() { }
        public static DAOTopicReply getInstance()
        {
            lock (instanceLock)
            {
                if (_instance == null)
                    _instance = new DAOTopicReply();

                return _instance;
            }
        }

        /// <summary>
        /// Get the message list of a topic in db
        /// </summary>
        /// <param name="topicId">topic id</param>
        /// <returns>list message</returns>
        public List<Message> GetTopicReplyTop10(int topicId)
        {
            List<Message> myList = new List<Message>();
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand();
                command.CommandText = "GetLastReplysOfTopic";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@topicId", SqlDbType.Int).Value = topicId;
                command.Connection = connection;

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Message message = new Message();
                        message.Topic.TopicId = reader.GetInt32(0);
                        message.Topic.Title = reader.GetString(1);
                        message.Topic.Description = reader.GetString(2);
                        message.Topic.TypeTopic = reader.GetInt16(3);
                        message.DateAdd = reader.GetDateTime(4);
                        if (reader.IsDBNull(5))
                            message.DateUp = message.DateAdd;
                        else
                            message.DateUp = reader.GetDateTime(5);
                        message.Vote = reader.GetInt32(6);
                        message.AuthorId = reader.GetInt32(7);
                        message.AuthorFirstName = reader.GetString(8);
                        message.AuthorLastName = reader.GetString(9);
                        message.TopicReplyId = reader.GetInt32(10);
                        message.ReplyContent = reader.GetString(11);
                        message.Topic.DateAdd = reader.GetDateTime(12);
                        if (reader.IsDBNull(13))
                            message.Topic.DateUp = message.Topic.DateAdd;
                        else
                            message.Topic.DateUp = reader.GetDateTime(13);
                        myList.Add(message);

                        //message.Topic.DateAdd = new DateTime(2017, 1, 3, 5,5,5, DateTimeKind.Utc);
                        //message.Topic.DateUp = new DateTime(2017, 1, 3, 5, 5, 5, DateTimeKind.Utc);
                    }
                }
            }
            return myList;
        }

        /// <summary>
        /// Add a message to a topic in db
        /// </summary>
        /// <param name="message">object message</param>
        public void AddNewTopicReply(Message message)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand();
                command.CommandText = "AddNewTopicReply";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@replyContent", SqlDbType.Text).Value = message.ReplyContent;
                command.Parameters.Add("@dateAdd", SqlDbType.DateTime).Value = DateTime.Now;
                command.Parameters.Add("@dateUp", SqlDbType.DateTime).Value = DateTime.Now;
                command.Parameters.Add("@topicId", SqlDbType.Int).Value = message.Topic.TopicId;
                command.Parameters.Add("@personId", SqlDbType.Int).Value = message.AuthorId;
                command.Connection = connection;

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                reader.Close();
                connection.Close();
            }
        }

        /// <summary>
        /// Delete a message from a topic in db
        /// </summary>
        /// <param name="personId">person id</param>
        /// <param name="topicReplyId">topicReply id</param>
        public void DeleteTopicReplyById(int personId, int topicReplyId)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand();
                command.CommandText = "DeleteTopicReplyById";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@topicReplyId", SqlDbType.Int).Value = topicReplyId;
                //command.Parameters.Add("@personId", SqlDbType.Int).Value = personId;
                command.Connection = connection;

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                connection.Close();
            }
        }

        /// <summary>
        /// Update a message from a topic in db
        /// </summary>
        /// <param name="topicReplyId">topicReply id</param>
        /// <param name="personId">person id</param>
        public void UpdateVoteTopicReply(int topicReplyId, int personId)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand();
                command.CommandText = "UpdateVoteTopicReply";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@topicReplyId", SqlDbType.Int).Value = topicReplyId;
                command.Parameters.Add("@personId", SqlDbType.Int).Value = personId;
                command.Parameters.Add("@one", SqlDbType.Int).Value = 1;
                command.Connection = connection;

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                connection.Close();
            }
        }

        /// <summary>
        /// Check if a user has already voted for the answer
        /// </summary>
        /// <param name="topicReplyId">message id</param>
        /// <param name="personId">person id</param>
        /// <returns>true if the person has already voted</returns>
        public bool CheckVoted(int topicReplyId, int personId)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand();
                command.CommandText = "CheckVoted";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@topicReplyId", SqlDbType.Int).Value = topicReplyId;
                command.Parameters.Add("@personId", SqlDbType.Int).Value = personId;
                command.Connection = connection;

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    connection.Close();
                    return true;
                }
                connection.Close();
            }
            return false;
        }
    }
}
