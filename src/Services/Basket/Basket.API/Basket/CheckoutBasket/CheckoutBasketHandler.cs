
using BuildingBlocks.Messaging.Events;
using MassTransit;

namespace Basket.API.Basket.CheckoutBasket
{
    public record CheckoutBasketCommand(BasketCheckoutDto BasketCheckoutDto) : ICommand<CheckoutBasketResult>;

    public record CheckoutBasketResult(bool IsSuccess);

    public class CheckoutBasketCommandValidator: AbstractValidator<CheckoutBasketCommand>
    {
        public CheckoutBasketCommandValidator()
        {
            RuleFor(x => x.BasketCheckoutDto).NotNull().WithMessage("BasketCheckoutDto can't be null");
            RuleFor(x => x.BasketCheckoutDto.UserName).NotEmpty().WithMessage("UserName is required");
        }
    }
    public class CheckoutBasketHandler 
        (IBasketRepository repository, IPublishEndpoint publishEndpoint)
        : ICommandHandler<CheckoutBasketCommand, CheckoutBasketResult>
    {
        public async Task<CheckoutBasketResult> Handle(CheckoutBasketCommand command, CancellationToken cancellationToken)
        {
            //Get existing basket with totalPrice
            var basket = await repository.GetBasket(command.BasketCheckoutDto.UserName, cancellationToken);
            if(basket is null)
            {
                return new CheckoutBasketResult(false);
            }
            //Set totalPrive on BasketCheckedOutEvent
            var eventMessage = command.BasketCheckoutDto.Adapt<BasketCheckedOutEvent>();
            eventMessage.TotalPrice = basket.TotalPrice;

            //Send BasketCheckedOutEvent to rabbitMQ using MassTransit
            await publishEndpoint.Publish(eventMessage, cancellationToken);
            //Delete the basket
            await repository.DeleteBasket(command.BasketCheckoutDto.UserName, cancellationToken);

            return new CheckoutBasketResult(true);
        }
    }
}
