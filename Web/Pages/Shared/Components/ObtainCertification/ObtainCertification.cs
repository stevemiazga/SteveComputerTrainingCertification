using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Web.Services;

namespace Web.Pages.Shared.Components.ObtainCertificationComponent
{
    public class ObtainCertification : ViewComponent
    {
        private readonly IStudentService _studentService;

        public ObtainCertification(IStudentService studentService)
        {
            _studentService = studentService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var obtainCertificationNotices = await _studentService.GetObtainCertificationNotices();
            return View(obtainCertificationNotices);
        }
    }
}
