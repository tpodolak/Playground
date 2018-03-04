using MediatR;

namespace MediaRIntegration
{
    public class AvailabilityRequest : IRequest<AvailabilityResponse>, INotification
    {
    }

    public class AvailabilityResponse
    {
    }
}