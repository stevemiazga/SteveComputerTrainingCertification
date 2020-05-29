namespace Domain
{
    public class Course : Entity
    {
        public string Title { get; }

        protected Course()
        {
        }

        public Course(int id, string title)
            : base(id)
        {
            Title = title;
        }

    }
}
