using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace MediaRIntegration
{
    public class AvailabilityRequestHandler : IRequestHandler<AvailabilityRequest, AvailabilityResponse>
    {
        public Task<AvailabilityResponse> Handle(AvailabilityRequest message, CancellationToken cancellationToken)
        {
            return Task.FromResult(new AvailabilityResponse());
        }
    }
}