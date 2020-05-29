namespace Domain
{
    public class ExamPassed : Entity
    {
        public virtual Student Student { get; }
        public virtual Exam Exam { get; }

        //protected enables lazy loading
        protected ExamPassed()
        {
        }

        public ExamPassed(Student student, Exam exam)
            : this()
        {
            Student = student;
            Exam = exam;
        }
    }
}
