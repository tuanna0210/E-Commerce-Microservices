namespace Ordering.Application.Orders.Commands.CreateOrder
{
    public class CreateOrderCommandHandler : ICommandHandler<CreateOrderCommand, CreateOrderResult>
    {
        public Task<CreateOrderResult> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
