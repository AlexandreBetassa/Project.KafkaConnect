using Microsoft.Extensions.Options;
using Project.Contracts.src;
using Project.Kafka.src;
using Project.Models.src.Entities;
using Project.Services.src;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped(typeof(IServices<>), typeof(Services<>));
builder.Services.AddScoped<IKafkaService, KafkaService>();
builder.Services.Configure<KafkaOptions>(builder.Configuration.GetSection(nameof(KafkaOptions)));
builder.Services.AddScoped<IKafkaOptions>(opt => opt.GetRequiredService<IOptions<KafkaOptions>>().Value);

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
