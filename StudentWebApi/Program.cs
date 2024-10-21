using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StudentData;
using StudentWebApi.Configuration;
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

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
