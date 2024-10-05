using Microsoft.EntityFrameworkCore;

namespace HammerFallInteractive.EcoGame.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddDbContext<ApplicationDatabaseContext>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            using (ApplicationDatabaseContext dbContext = new ApplicationDatabaseContext())
                dbContext.Database.EnsureCreated();

            app.Run();
        }
    }
}
