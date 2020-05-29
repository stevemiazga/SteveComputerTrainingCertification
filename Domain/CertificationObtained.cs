namespace Domain
{
    public class CertificationObtained : Entity
    {
        public virtual Student Student { get; }
        public virtual Certification Certification { get; }

        //protected enables lazy loading
        protected CertificationObtained()
        {
        }

        public CertificationObtained(Student student, Certification certification)
            : this()
        {
            Student = student;
            Certification = certification;
        }
    }
}
