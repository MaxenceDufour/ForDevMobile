namespace BusinessClassPortable
{
    /// <summary>
    /// Business class to build a person
    /// </summary>
    public class Person
    {
        public int PersonId { get; set; }
        public string PersonFirstName { get; set; }
        public string PersonLastName { get; set; }
        public string Email { get; set; }
        public short Privilege { get; set; }

        #region Buiders
        
        public Person() { }

        //public Person()
        //{
        //    PersonId = 0;
        //    PersonFirstName = "PersonFirstName";
        //    PersonLastName = "PersonLastName";
        //    Email = "Email";
        //    Privilege = 0;
        //}

        public Person(int personId, string personFirstName, string personLastName, string email, string pass,
            short privilege)
        {
            PersonId = personId;
            PersonFirstName = personFirstName;
            PersonLastName = personLastName;
            Email = email;
            Privilege = privilege;
        }
        #endregion

        /// <summary>
        /// Comparison of a person by its personId
        /// </summary>
        /// <param name="obj">other person</param>
        /// <returns>true if equals</returns>
        public override bool Equals(object obj)
        {
            Person p = obj as Person;
            if (p == null) return false;
            return PersonId == p.PersonId;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
