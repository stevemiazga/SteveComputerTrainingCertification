using Domain;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Tests
{
    public class StudentTests
    {
        private string _courseTitle = "Microsoft SQL Server Database Development";
        private string _examDescription = "Microsoft SQL Server 2008, Database Development";
        private string _certificationDescription = "Microsoft Certified Technical Specialist in SQL Server 2008, Database Development";
        private Student _student;


        public StudentTests()
        {
            var name = Name.Create("Steve", "Miazga");
            var email = Email.Create("smiazga@sbcglobal.net");

            _student = new Student(name, email);
        }

        [Fact]
        public void AddCourse()
        {
            var course = new Course(5, "Microsoft SQL Server Database Development");

            _student.TakeCourse(course);
            var firstCourseTaken = _student.CoursesTaken.Single();

            Assert.Equal(_courseTitle, firstCourseTaken.Course.Title);
        }

        [Fact]
        public void AddExam()
        {
            var exam = new Exam(6, "70-433", "Microsoft SQL Server 2008, Database Development");

            _student.PassExam(exam);
            var firstExamPassed = _student.ExamsPassed.Single();

            Assert.Equal(_examDescription, firstExamPassed.Exam.ExamDescription);
        }

        [Fact]
        public void ObtainCertification()
        {
            var certification = new Certification(5, "MCTS", "Microsoft Certified Technical Specialist in SQL Server 2008, Database Development");

            _student.ObtainCertification(certification);
            var firstObtainCertification = _student.CertificationsObtained.Single();

            Assert.Equal(_certificationDescription, firstObtainCertification.Certification.CertificationDescription);
        }

        [Fact]
        public void QualifiedCertification()
        {
            var course = new Course(5, "Microsoft SQL Server Database Development");
            var exam = new Exam(6, "70-433", "Microsoft SQL Server 2008, Database Development");
            var certification = new Certification(5, "MCTS", "Microsoft Certified Technical Specialist in SQL Server 2008, Database Development");
            var certificationRequirement = new CertificationRequirement(certification, exam, course);
            _student.PassExam(exam);

            List<int> qualifyCertifications = _student.GetStudentCertificationIds(new List<CertificationRequirement>() { certificationRequirement });
            Assert.Equal(5, qualifyCertifications.Single());

        }
    }
}
