using Application;
using Domain;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class StudentRepository : IStudentRepository
    {
        private readonly TrainingContext _context;

        public StudentRepository(TrainingContext context)
        {
            _context = context;
        }

        public Student GetStudentById(int studentId)
        {
            Student student = _context.Students.Find(studentId);

            if (student == null)
                return null;

            _context.Entry(student).Collection(x => x.CoursesTaken).Load();
            _context.Entry(student).Collection(x => x.ExamsPassed).Load();
            _context.Entry(student).Collection(x => x.CertificationsObtained).Load();

            return student;
        }


        public void Save(Student student)
        {
            _context.Students.Attach(student);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task<bool> SaveEntitiesAsync()
        {
            return await _context.SaveEntitiesAsync();
        }


        public void Delete(Student student)
        {
            _context.Students.Remove(student);
        }

        public void SaveObtainedCertificationEvent(StudentObtainedCertificationEvent studentObtainedCertificationEvent)
        {
            _context.StudentObtainedCertificationEvents.Add(studentObtainedCertificationEvent);
        }

    }

}
