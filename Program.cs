using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using webAPi.Models;

var builder = WebApplication.CreateBuilder(args);
var origin = "AllOrigins";
builder.Services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var contextOptions = new DbContextOptionsBuilder<MyDbContext>().
                    UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=db_club;Trusted_Connection=true").Options;
using (var context = new MyDbContext(contextOptions))
{
    context.Database.EnsureCreated();
};
builder.Services.AddDbContext<MyDbContext>(option => option.UseSqlServer(
   "Server=(localdb)\\MSSQLLocalDB;Database=db_club;Trusted_Connection=true"
    ));
builder.Services.AddCors(options =>
    {
        options.AddPolicy(name: origin,
            policy =>
            {
                policy.WithOrigins("*");
            });
    }
);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(origin);
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
