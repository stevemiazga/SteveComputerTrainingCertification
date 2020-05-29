using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Services;
using Web.ViewModels;

namespace Web.Pages.Student
{
    public class EditModel : PageModel
    {
        private readonly IStudentService _studentService;
        private readonly ICertificationService _certificationService;

        public EditModel(IStudentService studentService, ICertificationService certificationService)
        {
            _studentService = studentService;
            _certificationService = certificationService;
        }

        [BindProperty]
        public StudentRecordViewModel StudentRecord { get; set; }

        [BindProperty]
        public AvailableCertificatonsViewModel AvailableCertificatons { get; set; }

        public async Task OnGet(int id)
        {
            var availableCertifications = await _certificationService.GetAvailableCertifications();
            AvailableCertificatons = availableCertifications;

            var studentRecord = await _studentService.GetStudentRecordById(id);
            foreach (var record in studentRecord.Courses)
            {
                foreach (var available in availableCertifications.Courses)
                {
                    if (available.CourseId == record.CourseId)
                    {
                        available.CourseCheckboxAnswer = true;
                    }
                }
            }

            foreach (var record in studentRecord.Exams)
            {
                foreach (var available in availableCertifications.Exams)
                {
                    if (available.ExamId == record.ExamId)
                    {
                        available.ExamCheckboxAnswer = true;
                    }
                }
            }

            foreach (var record in studentRecord.Certifications)
            {
                foreach (var available in availableCertifications.Certifications)
                {
                    if (available.CertificationId == record.CertificationId)
                    {
                        available.CertificationCheckboxAnswer = true;
                    }
                }
            }

            StudentRecord = studentRecord;
        }

        public async Task<IActionResult> OnPost()
        {
            List<StudentCourseViewModel> studentCourses = new List<StudentCourseViewModel>();

            for (int i = 0; i < AvailableCertificatons.Courses.Count(); i++)
            {
                if (AvailableCertificatons.Courses[i].CourseCheckboxAnswer == true)
                {
                    studentCourses.Add(new StudentCourseViewModel()
                    {
                        CourseId = AvailableCertificatons.Courses[i].CourseId
                    });
                }
            }

            List<StudentExamViewModel> studentExams = new List<StudentExamViewModel>();

            for (int i = 0; i < AvailableCertificatons.Exams.Count(); i++)
            {
                if (AvailableCertificatons.Exams[i].ExamCheckboxAnswer == true)
                {
                    studentExams.Add(new StudentExamViewModel()
                    {
                        ExamId = AvailableCertificatons.Exams[i].ExamId
                    });
                }
            }

            var studentRecord = new StudentRecordViewModel()
            {
                StudentId = StudentRecord.StudentId,
                FirstName = StudentRecord.FirstName,
                LastName = StudentRecord.LastName,
                Email = StudentRecord.Email,
                Courses = studentCourses,
                Exams = studentExams

            };
            await _studentService.EditStudentRecord(studentRecord);
            return RedirectToPage("./Index");
        }
    }
}