using System;

namespace Domain
{
    public class EmailException : Exception
    {
        public EmailException(string message) : base(message)
        {

        }
    }

    public class NameException : Exception
    {
        public NameException(string message) : base(message)
        {

        }
    }

    public class StudentException : Exception
    {
        public StudentException(string message) : base(message)
        {

        }
    }

    public class CourseException : Exception
    {
        public CourseException(string message) : base(message)
        {

        }
    }

    public class ExamException : Exception
    {
        public ExamException(string message) : base(message)
        {

        }
    }

    public class CertificationException : Exception
    {
        public CertificationException(string message) : base(message)
        {

        }
    }
}
