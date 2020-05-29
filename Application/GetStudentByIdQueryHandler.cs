using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application
{
    public class GetStudentByIdQueryHandler : IRequestHandler<GetStudentByIdQuery, StudentRecordDTO>
    {
        private readonly IQueryRepository _queryRepository;

        public GetStudentByIdQueryHandler(IQueryRepository queryRepository)
        {
            _queryRepository = queryRepository;
        }

        public Task<StudentRecordDTO> Handle(GetStudentByIdQuery request, CancellationToken cancellationToken)
        {
            var student = _queryRepository.GetStudentById(request.StudentId);
            return Task.FromResult(student);
        }
    }

}
