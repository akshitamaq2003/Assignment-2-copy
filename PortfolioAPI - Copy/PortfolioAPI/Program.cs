using PortfolioAPI.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<ProjectsService>();
builder.Services.AddSingleton<SkillsService>();
builder.Services.AddSingleton<ContactInfoService>();
builder.Services.AddSingleton<DatabaseContext>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontendOrigin",
        builder => builder.WithOrigins("http://localhost:5173")
                          .AllowAnyHeader()
                          .AllowAnyMethod());
});
var app = builder.Build();




// Use the CORS policy
app.UseCors("AllowFrontendOrigin");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
