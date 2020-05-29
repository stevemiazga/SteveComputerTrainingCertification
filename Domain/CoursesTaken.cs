namespace Domain
{
    public class CourseTaken : Entity
    {
        public virtual Student Student { get; }
        public virtual Course Course { get; }

        //protected enables lazy loading
        protected CourseTaken()
        {
        }

        public CourseTaken(Student student, Course course)
            : this()
        {
            Student = student;
            Course = course;
        }
    }
}
