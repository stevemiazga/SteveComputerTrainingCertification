using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Services;
using Web.ViewModels;

namespace Web.Pages.Student
{
    public class CreateModel : PageModel
    {
        private readonly IStudentService _studentService;
        private readonly ICertificationService _certificationService;

        public CreateModel(IStudentService studentService, ICertificationService certificationService)
        {
            _studentService = studentService;
            _certificationService = certificationService;
        }

        [BindProperty]
        public StudentRecordViewModel StudentRecord { get; set; }

        [BindProperty]
        public AvailableCertificatonsViewModel AvailableCertificatons { get; set; }

        public async Task<IActionResult> OnGet()
        {
            var availableCertifications = await _certificationService.GetAvailableCertifications();
            AvailableCertificatons = availableCertifications;
            return Page();
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

            await _studentService.CreateStudentRecord(studentRecord);
            return RedirectToPage("./Index");
        }

    }
}