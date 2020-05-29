using Domain;
using System.Threading.Tasks;

namespace Application
{
    public interface IStudentRepository
    {
        Student GetStudentById(int studentId);
        void Delete(Student student);
        void Save(Student student);
        void SaveObtainedCertificationEvent(StudentObtainedCertificationEvent studentObtainedCertificationEvent);
        void SaveChanges();
        Task<bool> SaveEntitiesAsync();
    }
}