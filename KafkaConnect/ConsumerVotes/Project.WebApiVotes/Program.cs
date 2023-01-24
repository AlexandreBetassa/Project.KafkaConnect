using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Project.Infra.src.Context;
using Project.Infra.src.Database;
using Project.Infra.src.PubSub.Subscribe;
using Project.Models.src.Contracts;
using Project.Models.src.Entities;
using Project.Repository.src;
using Project.Services.src;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped(typeof(IServices<>), typeof(Services<>));
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped(typeof(IDatabase<>), typeof(Data<>));
builder.Services.AddScoped<ISubscribeService, SubscribeService>();
builder.Services.AddScoped<SubscribeHostService>();

builder.Services.Configure<SubscribeOptions>(builder.Configuration.GetSection(nameof(SubscribeOptions)));
builder.Services.AddScoped<ISubscribeOptions, SubscribeOptions>(opt => opt.GetRequiredService<IOptions<SubscribeOptions>>().Value);

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration["Database"]));

builder.Services
    .AddScoped<SubscribeHostService>()
    .AddHostedService(scoped => builder.Services.BuildServiceProvider()
    .GetRequiredService<SubscribeHostService>());

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
