using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application
{
    public class GetObtainCertificationNoticesQueryHandler : IRequestHandler<GetObtainCertificationNoticesQuery, ObtainCertificationsDTO>
    {
        private readonly IQueryRepository _queryRepository;

        public GetObtainCertificationNoticesQueryHandler(IQueryRepository queryRepository)
        {
            _queryRepository = queryRepository;
        }

        public Task<ObtainCertificationsDTO> Handle(GetObtainCertificationNoticesQuery request, CancellationToken cancellationToken)
        {
            var obtainCertifications = _queryRepository.GetObtainCertifications(request.ObtainCertificationDate);
            return Task.FromResult(obtainCertifications);
        }
    }
}
