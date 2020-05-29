using Application;
using Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure
{
    public class CertificationRepository : ICertificationRepository
    {
        private readonly TrainingContext _context;

        public CertificationRepository(TrainingContext context)
        {
            _context = context;
        }

        public List<CertificationRequirement> GetCertificationRequirementsAll()
        {
            return _context.CertificationRequirements.Include(e => e.Exam).Include(c => c.Certification).ToList();
        }

        public Certification GetCertificationById(int certificationId)
        {
            return _context.Certifications.Find(certificationId);
        }

        public Course GetCourseById(int courseId)
        {
            return _context.Courses.Find(courseId);
        }

        public Exam GetExamById(int examId)
        {
            return _context.Exams.Find(examId);
        }

        public List<Course> GetCourses()
        {
            return _context.Courses.ToList();
        }

        public List<Exam> GetExams()
        {
            return _context.Exams.ToList();
        }

        public List<Certification> GetCertifications()
        {
            return _context.Certifications.ToList();
        }

    }

}
