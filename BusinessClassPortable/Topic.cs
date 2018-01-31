using System;
using System.Collections.Generic;

namespace BusinessClassPortable
{
    /// <summary>
    /// Business class to build a topic
    /// </summary>
    public class Topic
    {
        public List<Message> listTopicReply = new List<Message>();
        public Person Person { get; set; } = new Person();
        public Rubric Rubric { get; set; } = new Rubric();
        public int TypeTopic { get; set; }
        public int TopicId { get; set; }
        public string Title { get; set; }
        public DateTime DateAdd { get; set; }
        public DateTime DateUp { get; set; }
        public string Description { get; set; }
        public string Message { get; set; }
        //public im
        //public PictureBox RubricIcon { get; set; }

        #region Builders

        public Topic() { }
        public Topic(Topic topic)
        {
            Person = topic.Person;
            Rubric = topic.Rubric;
            TypeTopic = topic.TypeTopic;
            TopicId = topic.TopicId;
            Title = topic.Title;
            DateAdd = topic.DateAdd;
            DateUp = topic.DateUp;
            Description = topic.Description;
            Message = topic.Message;
            //RubricIcon = topic.RubricIcon;
        }
        public Topic(Person person, int typeTopic, Rubric rubric, DateTime dateAdd, string description, string message, string title)
        {
            Person = person;
            Rubric = rubric;
            TypeTopic = typeTopic;
            DateAdd = dateAdd;
            Description = description;
            Message = message;
            Title = title;
        }
        public Topic(int topicId, string title, string description, DateTime dateAdd, DateTime dateUpp, int typeTopic)
        {
            TopicId = topicId;
            Title = title;
            Description = description;
            DateAdd = dateAdd;
            DateUp = dateUpp;
            TypeTopic = typeTopic;
        }
        public Topic(int topicId, string title, string description, DateTime dateAdd, DateTime dateUpp, int typeTopic,
            int personId, string personFirstName, string personLastName, int rubricId, string rubricTitle) : this(topicId, title, description, dateAdd, dateUpp, typeTopic)
        {
            Person.PersonId = personId;
            Person.PersonFirstName = personFirstName;
            Person.PersonLastName = personLastName;
            Rubric.RubricId = rubricId;
            Rubric.Title = rubricTitle;
        }

        #endregion

        public override bool Equals(object obj)
        {
            Topic t = obj as Topic;
            if (t == null) return false;
            return TopicId == t.TopicId;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

    
    }
}
