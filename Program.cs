using WebApplication1.Data;
using WebApplication1.Dtos;
using WebApplication1.Endpoints;

var builder = WebApplication.CreateBuilder(args);

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


app.Run();
