using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StudentData;
using StudentWebApi.Configuration;
using StudentWebApi.Controllers.Models;
using StudentWebApi.Services;

namespace StudentWebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.Configure<MailConfig>(builder.Configuration.GetSection("MailConfig"));

            var testVar = builder.Configuration.GetValue<string>("TESTVAR");
            Console.WriteLine(testVar);
            // SQLite
            builder.Services.AddDbContext<StudentContext>(options => options.UseSqlite("Data Source = student.db"));
            // Add services to the container.

            // AutoMapper Configuration
            var mapper = new MapperConfiguration(mc => mc.AddProfile<MapperProfile>())
                .CreateMapper();

            builder.Services.AddSingleton(mapper);
            builder.Services.AddSingleton<SingletonService>();
            builder.Services.AddSingleton<UserVisitService>();
            builder.Services.AddTransient<TransientService>();
            builder.Services.AddTransient<TransientService2>();
            builder.Services.AddScoped<ScopedService>();


            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddAuthorization();
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        // указывает, будет ли валидироваться издатель при валидации токена
                        ValidateIssuer = true,
                        // строка, представляющая издателя
                        ValidIssuer = AuthOptions.ISSUER,
                        // будет ли валидироваться потребитель токена
                        ValidateAudience = true,
                        // установка потребителя токена
                        ValidAudience = AuthOptions.AUDIENCE,
                        // будет ли валидироваться время существования
                        ValidateLifetime = true,
                        // установка ключа безопасности
                        IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                        // валидация ключа безопасности
                        ValidateIssuerSigningKey = true,
                    };
                });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthentication();
            app.UseAuthorization();



            app.MapControllers();

            app.Run();
        }
    }
}
