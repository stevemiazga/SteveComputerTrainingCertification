using System.Collections.Generic;

namespace Application
{
    public class StudentCourseDTO
    {
        public int CourseId { get; set; }
        public string CourseTitle { get; set; }
    }

    public class StudentExamDTO
    {
        public int ExamId { get; set; }
        public string ExamTitle { get; set; }
    }

    public class StudentCertificationDTO
    {
        public int CertificationId { get; set; }
        public string CertificationTitle { get; set; }
    }

    public class StudentRecordDTO
    {
        public int StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public List<StudentCourseDTO> Courses { get; set; }
        public List<StudentExamDTO> Exams { get; set; }
        public List<StudentCertificationDTO> Certifications { get; set; }
    }

    public class AvailableCertificationsDTO
    {
        public List<StudentCourseDTO> Courses { get; set; }
        public List<StudentExamDTO> Exams { get; set; }
        public List<StudentCertificationDTO> Certifications { get; set; }
    }
}