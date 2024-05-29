using back.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<CourseContext>(context => context.UseMySQL("Server=localhost;Database=golearning;User=root;Password=meorin"));
builder.Services.AddDbContext<UserContext>(context => context.UseMySQL("Server=localhost;Database=golearning;User=root;Password=meorin"));
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
//Program.cs

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      builder =>
                      {
                          //����������ĵ�ַ
                          builder.WithOrigins("http://localhost:1420", "http://localhost:8081")
                          .AllowAnyHeader()
                          .AllowAnyMethod()
                          .AllowCredentials();
                      });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseCors(MyAllowSpecificOrigins);//���ÿ�������

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
