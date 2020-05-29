using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using Web.Services;
using Web.ViewModels;

namespace Web.Pages.Student
{
    public class DeleteModel : PageModel
    {
        private readonly IStudentService _studentService;

        public DeleteModel(IStudentService studentService)
        {
            _studentService = studentService;
        }

        public StudentRecordViewModel StudentRecord { get; set; }

        public async Task OnGetAsync(int id)
        {
            StudentRecord = await _studentService.GetStudentRecordById(id);
        }

        public async Task<IActionResult> OnPost(int id)
        {
            await _studentService.DeleteStudentRecord(id);
            return RedirectToPage("./Index");
        }

    }
}