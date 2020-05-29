using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain
{
    public class Student : Entity
    {
        public virtual Name Name { get; private set; }
        public Email Email { get; private set; }

        private readonly List<CourseTaken> _coursesTaken = new List<CourseTaken>();
        public virtual IReadOnlyList<CourseTaken> CoursesTaken => _coursesTaken.ToList();

        private readonly List<ExamPassed> _examsPassed = new List<ExamPassed>();
        public virtual IReadOnlyList<ExamPassed> ExamsPassed => _examsPassed.ToList();

        private readonly List<CertificationObtained> _certificationsObtained = new List<CertificationObtained>();
        public virtual IReadOnlyList<CertificationObtained> CertificationsObtained => _certificationsObtained.ToList();

        protected Student()
        {
        }

        public Student(Name name, Email email)
            : this()
        {
            Name = name;
            Email = email;
        }

        public void EditPersonalInfo(Name name, Email email)
        {
            if (name == null)
                throw new ArgumentNullException();
            if (email == null)
                throw new ArgumentNullException();

            Name = name;
            Email = email;
        }

        public void RemoveCoursesTaken()
        {
            _coursesTaken.Clear();
        }

        public void RemoveExamsPassed()
        {
            _examsPassed.Clear();
        }

        public void RemoveCertificationsObtained()
        {
            _certificationsObtained.Clear();
        }

        public string TakeCourse(Course course)
        {
            if (_coursesTaken.Any(x => x.Course == course))
                return $"Already took course '{course.Title}'";

            var courseTaken = new CourseTaken(this, course);
            _coursesTaken.Add(courseTaken);

            return "OK";
        }

        public string PassExam(Exam exam)
        {
            if (_examsPassed.Any(x => x.Exam == exam))
                return $"Already pass exam '{exam.ExamDescription}'";

            var examPassed = new ExamPassed(this, exam);
            _examsPassed.Add(examPassed);

            return "OK";
        }

        public string ObtainCertification(Certification certification)
        {
            if (_certificationsObtained.Any(x => x.Certification == certification))
                return $"Already obtain certification '{certification.CertificationDescription}'";

            var certificationObtained = new CertificationObtained(this, certification);
            _certificationsObtained.Add(certificationObtained);

            ObtainCerfiticationDomainEvent(this.Id, certification.Id, System.DateTime.Now);

            return "OK";
        }

        public List<int> GetStudentCertificationIds(List<CertificationRequirement> certificationRequirements)
        {
            return (from c in certificationRequirements
                    join ep in _examsPassed on c.Exam.Id equals ep.Exam.Id
                    group c by new { CertificationId = c.Certification.Id, TotalRequirements = c.TotalRequirements } into crGroup
                    where crGroup.Count() >= crGroup.Key.TotalRequirements
                    select crGroup.Key.CertificationId).ToList();
        }

        private void ObtainCerfiticationDomainEvent(int studentId, int certificationId, DateTime obtainCertificationDate)
        {
            var createdEvent = new StudentObtainedCertificationEvent(studentId, certificationId, obtainCertificationDate);
            this.AddDomainEvent(createdEvent);
        }

    }
}
