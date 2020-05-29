using MediatR;
using System.Collections.Generic;

namespace Application
{
    public class GetStudentRecordsQuery : IRequest<List<StudentRecordDTO>>
    { }
}
