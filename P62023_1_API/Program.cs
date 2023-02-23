using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using P62023_1_API.Models;

internal class Program
{ 
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

        var CnnStrBuilder = new SqlConnectionStringBuilder(
            builder.Configuration.GetConnectionString("CNNSTR"));
        string cnnStr = CnnStrBuilder.ConnectionString;
        builder.Services.AddDbContext<P620231_AutoAppoContext>(options => options.UseSqlServer(cnnStr));

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseRouting();
        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }
}