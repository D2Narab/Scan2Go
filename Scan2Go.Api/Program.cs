using Scan2Go.Api.Filters;
using Scan2Go.Api.Services;
using Scan2Go.Mapper.Managers;
using Scan2Go.Mapper.Models.UserModels;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
    
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        myBuilder =>
        {
            myBuilder
                .AllowAnyOrigin()
            .AllowAnyMethod()
                .AllowAnyHeader();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UseCors("AllowAll");
app.UseMiddleware<JwtMiddleware>();


new TranslationsManager(new UsersModel()).GetTranslations(0, 0);

app.Run();
