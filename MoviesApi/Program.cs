using CruidApplication.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MoviesApi.Services;

var builder = WebApplication.CreateBuilder(args);

//add database
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

//used to add interface services to api
builder.Services.AddTransient<IGenresServices, GenresServices>();
builder.Services.AddTransient<IMoviesServices, MoviesServices>();

builder.Services.AddAutoMapper(typeof(Program));

//used to manage any application frontend to connect to this api 
builder.Services.AddCors();

//adding authenthing on api and definations of api
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
                           {
                               Version = "v1",
                               Title = "MoviesApi",
                               Description = "My first api",
                               TermsOfService = new Uri("https://www.google.com"),
                               Contact = new OpenApiContact
                               {
                                   Name = "DevCreed",
                                   Email = "test@domain.com",
                                   Url = new Uri("https://www.google.com")
                               },
                               License = new OpenApiLicense
                               {
                                   Name = "My license",
                                   Url = new Uri("https://www.google.com")
                               }
                           });

options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
{
    Name = "Authorization",
    Type = SecuritySchemeType.ApiKey,
    Scheme = "Bearer",
    BearerFormat = "JWT",
    In = ParameterLocation.Header,
    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\""
});

options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Name = "Bearer",
                In = ParameterLocation.Header
            },
            new List<string>()
        }
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

//middleware for addcors .net core
app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

app.UseAuthorization();

app.MapControllers();

app.Run();
