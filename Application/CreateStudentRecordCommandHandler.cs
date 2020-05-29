using Domain;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application
{
    public class CreateStudentRecordCommandHandler : IRequestHandler<CreateStudentRecordCommand, bool>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly ICertificationRepository _certificationRepository;

        public CreateStudentRecordCommandHandler(IStudentRepository studentRepository, ICertificationRepository certificationRepository)
        {
            _studentRepository = studentRepository;
            _certificationRepository = certificationRepository;
        }

        public async Task<bool> Handle(CreateStudentRecordCommand command, CancellationToken cancellationToken)
        {
            Email email = Email.Create(command.Email);
            Name name = Name.Create(command.FirstName, command.LastName);
            var student = new Student(name, email);

            if (command.CoursesTaken != null)
            {
                foreach (var courseId in command.CoursesTaken)
                {
                    Course course = _certificationRepository.GetCourseById(courseId);
                    if (course == null)
                        throw new CourseException("Course not found");

                    string result = student.TakeCourse(course);
                    if (result != "OK")
                        throw new CourseException(result);
                }
            }

            if (command.ExamsPassed != null)
            {
                foreach (var examId in command.ExamsPassed)
                {
                    Exam exam = _certificationRepository.GetExamById(examId);
                    if (exam == null)
                        throw new ExamException("Exam not found");

                    string result = student.PassExam(exam);
                    if (result != "OK")
                        throw new ExamException(result);
                }
            }

            List<CertificationRequirement> certificationRequirements = _certificationRepository.GetCertificationRequirementsAll();
            if (certificationRequirements == null)
                throw new CertificationException("Certification requirements not found");

            List<int> qualifyCertifications = student.GetStudentCertificationIds(certificationRequirements);

            if (qualifyCertifications != null)
            {
                foreach (var certificationId in qualifyCertifications)
                {
                    Certification certification = _certificationRepository.GetCertificationById(certificationId);
                    if (certification == null)
                        throw new CertificationException("Certification not found");

                    string result = student.ObtainCertification(certification);
                    if (result != "OK")
                        throw new CertificationException(result);
                }
            }

            _studentRepository.Save(student);
            return await _studentRepository.SaveEntitiesAsync();

        }
    }
}
