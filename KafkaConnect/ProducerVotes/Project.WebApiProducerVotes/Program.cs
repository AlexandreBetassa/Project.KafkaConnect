using Microsoft.Extensions.Options;
using Project.Domain.src;
using Project.Domain.src.Contracts;
using Project.Domain.src.Entities;
using Project.Infra.src.PubSub.Publisher;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton(typeof(IServices<>), typeof(Services<>));

builder.Services.AddSingleton<IPublisherService, KafkaService>();
builder.Services.Configure<KafkaOptions>(builder.Configuration.GetSection(nameof(KafkaOptions)));
builder.Services.AddSingleton<IPublisherOptions>(opt => opt.GetRequiredService<IOptions<KafkaOptions>>().Value);

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
