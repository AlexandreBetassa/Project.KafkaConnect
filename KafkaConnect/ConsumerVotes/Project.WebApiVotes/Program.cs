using Kafka.src;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Project.Contracts.src;
using Project.Infra.src.Context;
using Project.Infra.src.Database;
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
builder.Services.AddScoped<IKafkaService, KafkaService>();
builder.Services.AddScoped<KafkaHostService>();

builder.Services.Configure<KafkaOptions>(builder.Configuration.GetSection(nameof(KafkaOptions)));
builder.Services.AddScoped<IKafkaOptions, KafkaOptions>(opt => opt.GetRequiredService<IOptions<KafkaOptions>>().Value);

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration["Database"]));
builder.Services
    .AddScoped<KafkaHostService>()
    .AddHostedService(scoped => builder.Services.BuildServiceProvider()
    .GetRequiredService<KafkaHostService>());

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
