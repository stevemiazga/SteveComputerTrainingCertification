using MediatR;
using System.Collections.Generic;

namespace Application
{
    public class CreateStudentRecordCommand : IRequest<bool>
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public List<int> CoursesTaken { get; private set; }
        public List<int> ExamsPassed { get; private set; }

        public CreateStudentRecordCommand(string firstName, string lastName, string email, List<int> coursesTaken, List<int> examsPassed)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            CoursesTaken = coursesTaken;
            ExamsPassed = examsPassed;
        }
    }
}
