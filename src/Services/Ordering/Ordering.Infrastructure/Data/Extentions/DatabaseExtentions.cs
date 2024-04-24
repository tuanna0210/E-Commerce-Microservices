﻿using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;


namespace Ordering.Infrastructure.Data.Extentions
{
    public static class DatabaseExtentions
    {
        public static async Task InitializeDatabaseAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            context.Database.MigrateAsync().GetAwaiter().GetResult();
        }
    }
}
