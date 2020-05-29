using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class TrainingContext : DbContext
    {

        private readonly IMediator _mediator;

        public TrainingContext(DbContextOptions<TrainingContext> options, IMediator mediator) : base(options)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public TrainingContext(DbContextOptions<TrainingContext> options) : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<Certification> Certifications { get; set; }
        public DbSet<CertificationRequirement> CertificationRequirements { get; set; }
        public DbSet<StudentObtainedCertificationEvent> StudentObtainedCertificationEvents { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>(x =>
            {
                x.ToTable("Certify_Student").HasKey(k => k.Id);
                x.Property(p => p.Id).HasColumnName("StudentId");
                x.OwnsOne(p => p.Name, p =>
                {
                    p.Property(pp => pp.First).HasColumnName("FirstName");
                    p.Property(pp => pp.Last).HasColumnName("LastName");
                });
                x.Property(p => p.Email)
                    .HasConversion(p => p.Value, p => Email.Create(p));
                x.HasMany(p => p.CoursesTaken).WithOne(p => p.Student)
                    .OnDelete(DeleteBehavior.Cascade);
                x.HasMany(p => p.ExamsPassed).WithOne(p => p.Student)
                    .OnDelete(DeleteBehavior.Cascade);
                x.HasMany(p => p.CertificationsObtained).WithOne(p => p.Student)
                    .OnDelete(DeleteBehavior.Cascade);
            });
            modelBuilder.Entity<Course>(x =>
            {
                x.ToTable("Certify_Course").HasKey(k => k.Id);
                x.Property(p => p.Id).HasColumnName("CourseId");
                x.Property(p => p.Title);
            });
            modelBuilder.Entity<Exam>(x =>
            {
                x.ToTable("Certify_Exam").HasKey(k => k.Id);
                x.Property(p => p.Id).HasColumnName("ExamId");
                x.Property(p => p.ExamNumber);
                x.Property(p => p.ExamDescription);
            });
            modelBuilder.Entity<Certification>(x =>
            {
                x.ToTable("Certify_Certification").HasKey(k => k.Id);
                x.Property(p => p.Id).HasColumnName("CertificationId");
                x.Property(p => p.CertificationCredential);
                x.Property(p => p.CertificationDescription);
            });
            modelBuilder.Entity<CertificationRequirement>(x =>
            {
                x.ToTable("Certify_CertificationRequirement").HasKey(k => k.Id);
                x.Property(p => p.Id).HasColumnName("CertificationRequirementId");
                x.Property(p => p.ExamChoice).HasColumnName("ExamChoice");
                x.Property(p => p.TotalRequirements).HasColumnName("TotalRequirements");
                x.HasOne(p => p.Exam).WithMany();
                x.HasOne(p => p.Certification).WithMany(p => p.CertificationRequirements);
                x.HasOne(p => p.Course).WithMany();
            });
            modelBuilder.Entity<CourseTaken>(x =>
            {
                x.ToTable("Certify_CourseTaken").HasKey(k => k.Id);
                x.Property(p => p.Id).HasColumnName("CourseTakenId");
                x.HasOne(p => p.Student).WithMany(p => p.CoursesTaken);
                x.HasOne(p => p.Course).WithMany();
            });
            modelBuilder.Entity<ExamPassed>(x =>
            {
                x.ToTable("Certify_ExamPassed").HasKey(k => k.Id);
                x.Property(p => p.Id).HasColumnName("ExamPassId");
                x.HasOne(p => p.Student).WithMany(p => p.ExamsPassed);
                x.HasOne(p => p.Exam).WithMany();
            });
            modelBuilder.Entity<CertificationObtained>(x =>
            {
                x.ToTable("Certify_CertificationObtained").HasKey(k => k.Id);
                x.Property(p => p.Id).HasColumnName("CertificationObtainedId");
                x.HasOne(p => p.Student).WithMany(p => p.CertificationsObtained);
                x.HasOne(p => p.Certification).WithMany();
            });
            modelBuilder.Entity<StudentObtainedCertificationEvent>(x =>
           {
               x.ToTable("StudentObtainedCertificationEvent").HasKey(k => new { k.StudentId, k.CertificationId, k.ObtainCertificationDate });
           });
        }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            await _mediator.DispatchDomainEventsAsync(this);
            var result = await base.SaveChangesAsync(cancellationToken);

            return true;
        }
    }

}
