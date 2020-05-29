namespace Domain
{
    public class Exam : Entity
    {
        public string ExamNumber { get; }
        public string ExamDescription { get; }

        protected Exam()
        {
        }

        public Exam(int id, string examNumber, string examDescription)
            : base(id)
        {
            ExamNumber = examNumber;
            ExamDescription = examDescription;
        }

    }

}
