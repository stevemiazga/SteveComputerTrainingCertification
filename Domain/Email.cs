using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Domain
{
    public class Email : ValueObject
    {
        public string Value { get; }

        private Email(string value)
        {
            Value = value;
        }

        public static Email Create(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new EmailException("Email should not be empty");

            email = email.Trim();

            if (email.Length > 200)
                throw new EmailException("Email is too long");

            if (!Regex.IsMatch(email, @"^(.+)@(.+)$"))
                throw new EmailException("Email is invalid");

            return new Email(email);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public static implicit operator string(Email email)
        {
            return email.Value;
        }
    }
}
