
using Microsoft.EntityFrameworkCore;


namespace Ordering.Application.Orders.Queries.GetOrdersByName
{
    public class GetOrdersByNameHandler(IApplicationDbContext dbContext)
        : IQueryHandler<GetOrdersByNameQuery, GetOrderByNameResult>
    {
        public async Task<GetOrderByNameResult> Handle(GetOrdersByNameQuery query, CancellationToken cancellationToken)
        {
            var orders = await dbContext.Orders
                .Include(o => o.OrderItems)
                .AsNoTracking()
                .Where(o => o.OrderName.Value.Contains(query.Name))
                .OrderBy(o => o.OrderName.Value)
                .ToListAsync(cancellationToken);

            return new GetOrderByNameResult(orders.ToOrderDtoList());
        }
        private List<OrderDto> ProjectToOrderDto(List<Order> orders)
        {
            List<OrderDto> result = new();
            foreach (var item in orders)
            {
                var orderDto = new OrderDto(
                    Id: item.Id.Value,
                    CustomerId: item.CustomerId.Value,
                    OrderName: item.OrderName.Value,
                    ShippingAddress: new AddressDto(
                        item.ShippingAddress.FirstName,
                        item.ShippingAddress.LastName,
                        item.ShippingAddress.EmailAddress,
                        item.ShippingAddress.AddressLine,
                        item.ShippingAddress.Country,
                        item.ShippingAddress.State,
                        item.ShippingAddress.ZipCode
                        ),
                    BillingAddress: new AddressDto(
                        item.BillingAddress.FirstName,
                        item.ShippingAddress.LastName,
                        item.BillingAddress.EmailAddress,
                        item.BillingAddress.AddressLine,
                        item.BillingAddress.Country,
                        item.BillingAddress.State,
                        item.BillingAddress.ZipCode
                        ),
                    Payment: new PaymentDto(
                        item.Payment.CardName,
                        item.Payment.CardNumber,
                        item.Payment.Expiration,
                        item.Payment.CVV,
                        item.Payment.PaymentMethod
                        ),
                    Status: item.Status,
                    OrderItems: item.OrderItems.Select(oi => new OrderItemDto(oi.OrderId.Value, oi.ProductId.Value, oi.Quantity, oi.Price)).ToList()
                );
                result.Add(orderDto);
            }

            return result;
        }
    }
}
