using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace MediaRIntegration
{
    public class PaymentCommandHandler : IRequestHandler<PaymentCommand>
    {
        public Task Handle(PaymentCommand message, CancellationToken cancellationToken)
        {
            throw new InvalidOperationException();
            return Task.CompletedTask;
        }
    }
}