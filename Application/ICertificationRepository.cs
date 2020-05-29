using Domain;
using System.Collections.Generic;

namespace Application
{
    public interface ICertificationRepository
    {
        Exam GetExamById(int examId);
        Course GetCourseById(int courseId);
        List<CertificationRequirement> GetCertificationRequirementsAll();
        Certification GetCertificationById(int certificationId);
        List<Course> GetCourses();
        List<Exam> GetExams();
        List<Certification> GetCertifications();
    }
}