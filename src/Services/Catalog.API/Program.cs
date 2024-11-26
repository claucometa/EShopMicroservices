var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCarter();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Program>());

builder.Services.AddMarten(o =>
{
    o.Connection(builder.Configuration.GetConnectionString("Database")!);
}).UseLightweightSessions();

var app = builder.Build();

app.MapCarter();

var scope = app.Services.CreateScope();
var db = scope.ServiceProvider.GetRequiredService<IDocumentSession>();
db.Store(new Product());


app.Run();
