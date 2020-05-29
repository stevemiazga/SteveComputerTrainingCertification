using System.Collections.Generic;

namespace Domain
{
    public class Name : ValueObject
    {
        public string First { get; }
        public string Last { get; }

        protected Name()
        {
        }

        private Name(string first, string last)
            : this()
        {
            First = first;
            Last = last;
        }

        public static Name Create(string firstName, string lastName)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                throw new NameException("First name should not be empty");
            if (string.IsNullOrWhiteSpace(lastName))
                throw new NameException("Last name should not be empty");

            firstName = firstName.Trim();
            lastName = lastName.Trim();

            if (firstName.Length > 200)
                throw new NameException("First name is too long");
            if (lastName.Length > 200)
                throw new NameException("Last name is too long");

            return new Name(firstName, lastName);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return First;
            yield return Last;
        }
    }
}
