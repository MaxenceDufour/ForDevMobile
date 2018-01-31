namespace BusinessClassPortable
{
    public class Rubric
    {
        private int _rubricId;
        public int RubricId
        {
            get { return _rubricId;}
            set { _rubricId = value; }

        }

        private string _title;

        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        #region Builders

        public Rubric() { }
        public Rubric(int rubricId, string title)
        {
            _rubricId = rubricId;
            _title = title;
        }

        #endregion

        /// <summary>
        /// Rubric comparison by id and title
        /// </summary>
        /// <param name="obj">other rubric</param>
        /// <returns>true if equals</returns>
        public override bool Equals(object obj)
        {
            Rubric r = obj as Rubric;
            if (r == null) return false;
            return RubricId == r.RubricId && Title == r.Title;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
