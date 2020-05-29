using Domain;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application
{
    public class DeleteStudentRecordCommandHandler : IRequestHandler<DeleteStudentRecordCommand, bool>
    {
        private readonly IStudentRepository _studentRepository;

        public DeleteStudentRecordCommandHandler(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<bool> Handle(DeleteStudentRecordCommand command, CancellationToken cancellationToken)
        {
            Student student = _studentRepository.GetStudentById(command.Id);
            if (student == null)
                throw new StudentException("Student not found");

            _studentRepository.Delete(student);
            return await _studentRepository.SaveEntitiesAsync();
        }
    }
}
