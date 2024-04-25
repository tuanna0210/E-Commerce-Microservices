
using Microsoft.EntityFrameworkCore;
using Ordering.Domain.Models;

namespace Ordering.Application.Data
{
    public interface IApplicationDbContext
    {
        //Thay vì sử dụng repository, ta sử dụng luôn DbContext từ EFCore
        DbSet<Customer> Customers { get; }
        DbSet<Product> Products { get; }
        DbSet<Order> Orders { get; }
        DbSet<OrderItem> OrderItems { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
