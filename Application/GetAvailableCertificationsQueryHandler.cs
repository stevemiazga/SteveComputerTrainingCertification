using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application
{
    public class GetAvailableCertificationsQueryHandler : IRequestHandler<GetAvailableCertificationsQuery, AvailableCertificationsDTO>
    {
        private readonly ICertificationRepository _certificationRepository;

        public GetAvailableCertificationsQueryHandler(ICertificationRepository certificationRepository)
        {
            _certificationRepository = certificationRepository;
        }

        public Task<AvailableCertificationsDTO> Handle(GetAvailableCertificationsQuery request, CancellationToken cancellationToken)
        {
            var courses = _certificationRepository.GetCourses();
            var exams = _certificationRepository.GetExams();
            var certifications = _certificationRepository.GetCertifications();

            var coursesDTO = new List<StudentCourseDTO>();
            coursesDTO = courses.Select(c => new StudentCourseDTO() { CourseId = c.Id, CourseTitle = c.Title }).ToList();
            var examsDTO = new List<StudentExamDTO>();
            examsDTO = exams.Select(e => new StudentExamDTO() { ExamId = e.Id, ExamTitle = e.ExamNumber + " - " + e.ExamDescription }).ToList();
            var certificationsDTO = new List<StudentCertificationDTO>();
            certificationsDTO = certifications.Select(c => new StudentCertificationDTO() { CertificationId = c.Id, CertificationTitle = c.CertificationCredential + " - " + c.CertificationDescription }).ToList();

            AvailableCertificationsDTO availableCertificationDTO = new AvailableCertificationsDTO()
            {
                Courses = coursesDTO,
                Exams = examsDTO,
                Certifications = certificationsDTO
            };

            return Task.FromResult(availableCertificationDTO);
        }
    }
}
