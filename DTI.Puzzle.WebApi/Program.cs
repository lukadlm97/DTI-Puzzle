using DTI.Puzzle.Application;
using DTI.Puzzle.Persistence;
using DTI.Puzzle.WebApi.Untilities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.ConfigurePersistenceServices(builder.Configuration);
builder.Services.ConfigureGlossaryApplicationServices(builder.Configuration);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.ConfigureSwaggerServices(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI(c => 
       c.SwaggerEndpoint("/swagger/v1/swagger.json",
    $"{builder.Configuration.GetSection("ApplicationSettings:Title").Value}" +
    $" {builder.Configuration.GetSection("ApplicationSettings:Version").Value}"));

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

app.MapControllers();

app.Run();
