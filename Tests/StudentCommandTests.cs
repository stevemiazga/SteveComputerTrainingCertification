using Application;
using Domain;
using Moq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Tests
{
    public class StudentCommandTests
    {
        private readonly Mock<IStudentRepository> _mockStudentRepository;
        private readonly Mock<ICertificationRepository> _mockCertificationRepository;

        public StudentCommandTests()
        {
            _mockCertificationRepository = new Mock<ICertificationRepository>();
            _mockStudentRepository = new Mock<IStudentRepository>();
        }

        [Fact]
        public async Task CreateStudentRecord_Should()
        {
            var name = Name.Create("Steve", "Miazga");
            var email = Email.Create("smiazga@sbcglobal.net");
            var student = new Student(name, email);
            var course = new Course(5, "Microsoft SQL Server Database Development");
            var exam = new Exam(6, "70-433", "Microsoft SQL Server 2008, Database Development");
            var certification = new Certification(5, "MCTS", "Microsoft Certified Technical Specialist in SQL Server 2008, Database Development");
            var certificationRequirement = new CertificationRequirement(certification, exam, course);
            _mockCertificationRepository.Setup(x => x.GetCourseById(5)).Returns(course);
            _mockCertificationRepository.Setup(x => x.GetExamById(6)).Returns(exam);
            _mockCertificationRepository.Setup(x => x.GetCertificationById(5)).Returns(certification);
            _mockCertificationRepository.Setup(x => x.GetCertificationRequirementsAll()).Returns(new List<CertificationRequirement>() { certificationRequirement });

            var command = new CreateStudentRecordCommand("Steve", "Miazga", "smiazga@sbcglobal.net", new List<int>() { 5 }, new List<int>() { 6 });
            var handler = new CreateStudentRecordCommandHandler(_mockStudentRepository.Object, _mockCertificationRepository.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            _mockStudentRepository.Verify(x => x.Save(It.IsAny<Student>()), Times.Once);
            _mockStudentRepository.Verify(x => x.SaveEntitiesAsync(), Times.Once);
        }

        [Fact]
        public async Task EditStudentRecord_Should()
        {
            var name = Name.Create("Steve", "Miazga");
            var email = Email.Create("smiazga@sbcglobal.net");
            var student = new Student(name, email);
            var exam = new Exam(5, "70-448", "Microsoft SQL Server 2008, Business Intelligence Development and Maintenance");
            var certification = new Certification(4, "MCTS", "Microsoft Certified Technical Specialist in SQL Server 2008, Business Intelligence Development and Maintenance");
            var certificationRequirement = new CertificationRequirement(certification, exam, null);
            _mockStudentRepository.Setup(x => x.GetStudentById(1)).Returns(student);
            _mockCertificationRepository.Setup(x => x.GetExamById(5)).Returns(exam);
            _mockCertificationRepository.Setup(x => x.GetCertificationById(4)).Returns(certification);
            _mockCertificationRepository.Setup(x => x.GetCertificationRequirementsAll()).Returns(new List<CertificationRequirement>() { certificationRequirement });

            var command = new EditStudentRecordCommand(1, "Steve", "Miazga", "smiazga@sbcglobal.net", null, new List<int>() { 5 });
            var handler = new EditStudentRecordCommandHandler(_mockStudentRepository.Object, _mockCertificationRepository.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            _mockStudentRepository.Verify(x => x.Save(It.IsAny<Student>()), Times.Once);
            _mockStudentRepository.Verify(x => x.SaveEntitiesAsync(), Times.Once);
        }

        [Fact]
        public async Task DeleteStudentRecord_Should()
        {
            var name = Name.Create("Steve", "Miazga");
            var email = Email.Create("smiazga@sbcglobal.net");
            var student = new Student(name, email);

            _mockStudentRepository.Setup(x => x.GetStudentById(1)).Returns(student);

            var command = new DeleteStudentRecordCommand(1);
            var handler = new DeleteStudentRecordCommandHandler(_mockStudentRepository.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            _mockStudentRepository.Verify(x => x.Delete(It.IsAny<Student>()), Times.Once);
            _mockStudentRepository.Verify(x => x.SaveEntitiesAsync(), Times.Once);
        }
    }

}
