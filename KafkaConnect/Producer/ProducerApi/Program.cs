using Microsoft.Extensions.Options;
using Project.Contracts.src;
using Project.Kafka.src;
using Project.Models.src.Entities;
using Project.Services.src;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton(typeof(IServices<>), typeof(Services<>));
builder.Services.AddSingleton<IKafkaService, KafkaService>();
builder.Services.Configure<KafkaOptions>(builder.Configuration.GetSection(nameof(KafkaOptions)));
builder.Services.AddSingleton<IKafkaOptions>(opt => opt.GetRequiredService<IOptions<KafkaOptions>>().Value);

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
