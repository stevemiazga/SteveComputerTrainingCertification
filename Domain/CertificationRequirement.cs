namespace Domain
{
    public class CertificationRequirement : Entity
    {
        public virtual Certification Certification { get; }
        public virtual Exam Exam { get; }
        public virtual Course Course { get; }

        public string ExamChoice { get; }
        public int TotalRequirements { get; }

        //protected enables lazy loading
        protected CertificationRequirement()
        {
        }

        public CertificationRequirement(Certification certification, Exam exam, Course course)
            : this()
        {
            Certification = certification;
            Exam = exam;
            Course = course;
        }

    }
}
