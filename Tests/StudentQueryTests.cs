using Application;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Tests
{
    public class StudentQueryTests
    {
        private readonly Mock<IQueryRepository> _mockQueryRepository;

        public StudentQueryTests()
        {
            _mockQueryRepository = new Mock<IQueryRepository>();
        }

        [Fact]
        public async Task GetStudentRecords_Should()
        {
            var studentRecords = new List<StudentRecordDTO>();
            var studentCourses = new List<StudentCourseDTO>();
            var studentCourse = new StudentCourseDTO()
            {
                CourseId = 5,
                CourseTitle = "Microsoft SQL Server Database Development"
            };
            studentCourses.Add(studentCourse);
            var studentExams = new List<StudentExamDTO>();
            var studentExam = new StudentExamDTO()
            {
                ExamId = 6,
                ExamTitle = "Microsoft SQL Server 2008, Database Development"
            };
            studentExams.Add(studentExam);
            var studentCertifications = new List<StudentCertificationDTO>();
            var studentCertification = new StudentCertificationDTO()
            {
                CertificationId = 5,
                CertificationTitle = "Microsoft Certified Technical Specialist in SQL Server 2008, Database Development"
            };
            studentCertifications.Add(studentCertification);
            var studentRecord = new StudentRecordDTO()
            {
                StudentId = 1,
                FirstName = "Steve",
                LastName = "Miazga",
                Email = "smiazga@sbcglobal.net",
                Courses = studentCourses,
                Exams = studentExams,
                Certifications = studentCertifications
            };
            studentRecords.Add(studentRecord);

            _mockQueryRepository.Setup(x => x.GetStudentRecords(0)).Returns(studentRecords);

            var query = new GetStudentRecordsQuery();
            var handler = new GetStudentRecordsQueryHandler(_mockQueryRepository.Object);
            var result = await handler.Handle(query, CancellationToken.None);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetObtainedCertifications_Should()
        {
            var obtainedCertificatonNotices = new List<ObtainCertificationNoticeDTO>();
            var obtainedCertificationNotice = new ObtainCertificationNoticeDTO()
            {
                FirstName = "Steve",
                LastName = "Miazga",
                Email = "smiazga@sbcglobal.net",
                CertificationCredential = "MCSD",
                CertificationDescription = "Microsoft Certified Solutions Developer in Web Applications",
                ObtainCertificationDate = Convert.ToDateTime("08/01/2017")
            };
            obtainedCertificatonNotices.Add(obtainedCertificationNotice);
            var obtainedCertificatons = new ObtainCertificationsDTO()
            {
                Count = 1,
                ObtainCertificationNotices = obtainedCertificatonNotices
            };


            _mockQueryRepository.Setup(x => x.GetObtainCertifications(Convert.ToDateTime("08/01/2017"))).Returns(obtainedCertificatons);

            var query = new GetObtainCertificationNoticesQuery(Convert.ToDateTime("08/01/2017"));
            var handler = new GetObtainCertificationNoticesQueryHandler(_mockQueryRepository.Object);
            var result = await handler.Handle(query, CancellationToken.None);

            Assert.NotNull(result);
        }

    }
}
