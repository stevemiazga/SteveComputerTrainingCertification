using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application
{
    public class GetStudentRecordsQueryHandler : IRequestHandler<GetStudentRecordsQuery, List<StudentRecordDTO>>
    {
        private readonly IQueryRepository _queryRepository;

        public GetStudentRecordsQueryHandler(IQueryRepository queryRepository)
        {
            _queryRepository = queryRepository;
        }

        public Task<List<StudentRecordDTO>> Handle(GetStudentRecordsQuery request, CancellationToken cancellationToken)
        {
            var studentRecords = _queryRepository.GetStudentRecords();

            return Task.FromResult(studentRecords);
        }
    }

}
