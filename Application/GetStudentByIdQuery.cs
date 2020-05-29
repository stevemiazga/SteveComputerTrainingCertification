using MediatR;

namespace Application
{
    public class GetStudentByIdQuery : IRequest<StudentRecordDTO>
    {
        public int StudentId { get; }

        public GetStudentByIdQuery(int studentId)
        {
            StudentId = studentId;
        }

    }
}
