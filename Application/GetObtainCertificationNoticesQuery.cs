using MediatR;
using System;

namespace Application
{
    public class GetObtainCertificationNoticesQuery : IRequest<ObtainCertificationsDTO>
    {

        public DateTime ObtainCertificationDate { get; }

        public GetObtainCertificationNoticesQuery(DateTime obtainCertificationDate)
        {
            ObtainCertificationDate = obtainCertificationDate;
        }

    }
}
