using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Dtos;
using WebApplication1.Endpoints;
using WebApplication1.Models;
// using WebApplication1.Services;

var builder = WebApplication.CreateBuilder(args);

builder
.Services.AddDbContext<AppDbContext>(
    options => options.UseSqlite("Data Source=identity-db.db"));



builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

builder.Services
    .AddIdentityApiEndpoints<User>()
    .AddEntityFrameworkStores<AppDbContext>();

// builder.Services.AddTransient<TokenService>();

var connString = builder.Configuration.GetConnectionString("ReservaStore");
builder.Services.AddSqlite<ReservaStoreContext>(connString);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.MapReservas();

app.MigrateDb();



if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapIdentityApi<User>();

// app.MapGet("/token", (TokenService service)
//     => service.Generate(new User(1, "teste@gmail.com", "123",new[]{
//     "student", "premium"
//     } )));

app.Run();
