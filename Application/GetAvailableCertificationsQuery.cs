using MediatR;

namespace Application
{
    public class GetAvailableCertificationsQuery : IRequest<AvailableCertificationsDTO>
    { }
}
