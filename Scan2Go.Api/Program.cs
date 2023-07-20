using Newtonsoft.Json.Serialization;
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


builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    // Use the default property (Pascal) casing
    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

    //Ignore will ignore type restricting if sent values are empty or null.For example if RoleId in model is integer and it was sent by frontend as null
    //The service will handle it as 0, if this property is set to include, the service will fire an exception.
    //However, if this property is sent to Ignore, null values will also not be returned to the frontend, if its set to include, it will be sent back
    //to frontend even if its null. Naser 13.08.2021
    //options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;

    options.SerializerSettings.MissingMemberHandling = Newtonsoft.Json.MissingMemberHandling.Ignore;
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    // options.SerializerSettings.re = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
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
