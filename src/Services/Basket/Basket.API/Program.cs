var builder = WebApplication.CreateBuilder(args);
//Add services to the container
var assembly = typeof(Program).Assembly;

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblies(assembly);

    cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
    cfg.AddOpenBehavior(typeof(LoggingBehavior<,>));
});

builder.Services.AddCarter();

builder.Services.AddMarten(opts =>
{
    opts.Connection(builder.Configuration.GetConnectionString("Database")!);
    opts.Schema.For<ShoppingCart>().Identity(x => x.Username);
})
.UseLightweightSessions();

builder.Services.AddScoped<IBasketRepository, BasketRepository>();

var app = builder.Build();

//COnfigure the HTTP pipeline
app.MapCarter();
app.Run();
