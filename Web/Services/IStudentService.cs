using System.Collections.Generic;
using System.Threading.Tasks;
using Web.ViewModels;

namespace Web.Services
{
    public interface IStudentService
    {
        Task<List<StudentRecordViewModel>> GetStudentRecords();
        Task<StudentRecordViewModel> GetStudentRecordById(int studentId);
        Task<ObtainCertificationComponentViewModel> GetObtainCertificationNotices();
        Task<bool> CreateStudentRecord(StudentRecordViewModel studentRecordViewModel);
        Task<bool> EditStudentRecord(StudentRecordViewModel studentRecordViewModel);
        Task<bool> DeleteStudentRecord(int studentId);
    }
}
