using System.Threading.Tasks;
using Web.ViewModels;

namespace Web.Services
{
    public interface ICertificationService
    {
        Task<AvailableCertificatonsViewModel> GetAvailableCertifications();
    }
}
