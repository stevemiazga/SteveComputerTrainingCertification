using Domain;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application
{
    public class StudentObtainedCertificationDomainEventHandler : INotificationHandler<StudentObtainedCertificationEvent>
    {
        private readonly IStudentRepository _studentRepository;

        public StudentObtainedCertificationDomainEventHandler(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public Task Handle(StudentObtainedCertificationEvent notification, CancellationToken cancellationToken)
        {
            _studentRepository.SaveObtainedCertificationEvent(notification);
            _studentRepository.SaveChanges();

            return Task.FromResult(true);
        }
    }
}
