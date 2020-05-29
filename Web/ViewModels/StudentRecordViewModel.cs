using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels
{
    public class StudentCourseViewModel
    {
        public int CourseId { get; set; }
        public string CourseTitle { get; set; }
        public bool CourseCheckboxAnswer { get; set; }
    }

    public class StudentExamViewModel
    {
        public int ExamId { get; set; }
        public string ExamTitle { get; set; }
        public bool ExamCheckboxAnswer { get; set; }
    }

    public class StudentCertificationViewModel
    {
        public int CertificationId { get; set; }
        public string CertificationTitle { get; set; }
        public bool CertificationCheckboxAnswer { get; set; }
    }

    public class StudentRecordViewModel
    {
        public int StudentId { get; set; }

        [Required, StringLength(50)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required, StringLength(50)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required, StringLength(50)]
        [EmailAddress]
        public string Email { get; set; }

        public List<StudentCourseViewModel> Courses { get; set; }
        public List<StudentExamViewModel> Exams { get; set; }
        public List<StudentCertificationViewModel> Certifications { get; set; }
    }

    public class AvailableCertificatonsViewModel
    {
        public List<StudentCourseViewModel> Courses { get; set; }
        public List<StudentExamViewModel> Exams { get; set; }
        public List<StudentCertificationViewModel> Certifications { get; set; }
    }

}
