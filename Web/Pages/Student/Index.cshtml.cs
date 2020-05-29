using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;
using Web.Services;
using Web.ViewModels;

namespace Web.Pages.Student
{
    public class IndexModel : PageModel
    {
        private readonly IStudentService _studentService;

        public IndexModel(IStudentService studentService)
        {
            _studentService = studentService;
        }

        public List<StudentRecordViewModel> StudentRecords { get; set; }

        public async Task OnGet()
        {
            StudentRecords = await _studentService.GetStudentRecords();
        }
    }
}