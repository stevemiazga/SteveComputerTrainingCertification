using MediatR;

namespace Application
{
    public class DeleteStudentRecordCommand : IRequest<bool>
    {
        public int Id { get; private set; }

        public DeleteStudentRecordCommand(int studentId)
        {
            Id = studentId;
        }
    }
}
