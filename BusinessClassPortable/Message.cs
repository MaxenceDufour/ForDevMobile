using System;

namespace BusinessClassPortable
{
    /// <summary>
    /// Business class to build a message
    /// </summary>
    public class Message
    {
        public Person Person { get; set; } = new Person();
        public Topic Topic { get; set; } = new Topic();
        public int TypeReply { get; set; }
        public int Vote { get; set; }
        public int TopicReplyId { get; set; }
        public int AuthorId { get; set; }
        public string AuthorFirstName { get; set; }
        public string AuthorLastName { get; set; }
        public DateTime DateAdd { get; set; }
        public DateTime DateUp { get; set; }
        public string ReplyContent { get; set; }

        public Message() { }

        /// <summary>
        /// Comparison of a message by its content
        /// </summary>
        /// <param name="obj">other message</param>
        /// <returns>true if equals</returns>
        public override bool Equals(object obj)
        {
            Message m = obj as Message;
            if (m == null) return false;
            return ReplyContent == m.ReplyContent;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
