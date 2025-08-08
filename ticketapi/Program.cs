using Microsoft.EntityFrameworkCore;
using Ticketapi;
using Ticketapi.Data;

var builder = WebApplication.CreateBuilder(args);

// Connect SQLite database
builder.Services.AddDbContext<TicketDbContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

//Extract all the modules
var moduleType = typeof(IModule);
var modules = AppDomain.CurrentDomain.GetAssemblies()
    .SelectMany(s => s.GetTypes())
    .Where(p => moduleType.IsAssignableFrom(p) && !p.IsInterface)
    .Select(Activator.CreateInstance)
    .Cast<IModule>()
    .ToList();

// Register all the services
foreach (var module in modules)
{
    module.RegisterServices(builder.Services);
}

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseCors();

foreach (var module in modules) 
{
    module.MapEndpoints(app);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.Use(async (context, next) => 
    {
        if(context.Request.Path == "/")
        {
            context.Response.Redirect("/swagger");
            return;
        }

        await next();
    });

    app.UseSwagger();
    app.UseSwaggerUI();
}


app.Run();

