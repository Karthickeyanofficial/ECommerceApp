using ECommerceApp.PaymentService.Data;
using MassTransit;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<PaymentDbContext>(opt =>
{
    opt.UseNpgsql(builder.Configuration.GetConnectionString("InventoryDb"));
});
builder.Services.AddMassTransit(x =>
{
    // Register consumer    
    x.UsingRabbitMq((context, cfg) =>
    {
        Console.WriteLine("RabbitMQ configured in PaymentService");
        cfg.Host("localhost", "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });

        // IMPORTANT: auto creates queue + binding
        cfg.ConfigureEndpoints(context);
    });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
