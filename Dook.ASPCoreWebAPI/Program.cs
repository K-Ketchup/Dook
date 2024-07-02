using Dook.ASPCoreWebAPI;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Azure.Identity;
using Microsoft.Data.SqlClient;
using System;

var builder = WebApplication.CreateBuilder(args);

//Update Builder config to access values stored in app config. Maybe should be changed for azure sql db.

//builder.Configuration.AddAzureAppConfiguration(options =>
//    options.Connect(
//        new Uri(builder.Configuration["AppConfig:Endpoint"]),
//        new ManagedIdentityCredential()));

// Add services to the container. Maybe this part should be changed for azure sql db?

builder.Services.AddControllers();
builder.Services.AddDbContext<DookWebAPIContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING")));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Dook.ASPCoreWebAPI", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseDeveloperExceptionPage();
//    app.UseSwagger();
//    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Dook.ASPCoreWebAPI v1"));
//}
//else
//{
//    app.UseSwagger();
//    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Dook.ASPCoreWebAPI v1"));
//    app.UseHttpsRedirection();
//}

app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Dook.ASPCoreWebAPI v1"));

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.UseHttpsRedirection();

string connectionString = app.Configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING")!;

app.UseAuthorization();

app.MapControllers();

app.Run();



//try
//{
//    // Table would be created ahead of time in production
//    using var conn = new SqlConnection(connectionString);
//    conn.Open();

//    var command = new SqlCommand(
//        "CREATE TABLE Persons (ID int NOT NULL PRIMARY KEY IDENTITY, FirstName varchar(255), LastName varchar(255));",
//        conn);
//    using SqlDataReader reader = command.ExecuteReader();
//}
//catch (Exception e)
//{
//    // Table may already exist
//    Console.WriteLine(e.Message);
//}

//app.MapGet("/Person", () => {
//    var rows = new List<string>();

//    using var conn = new SqlConnection(connectionString);
//    conn.Open();

//    var command = new SqlCommand("SELECT * FROM Persons", conn);
//    using SqlDataReader reader = command.ExecuteReader();

//    if (reader.HasRows)
//    {
//        while (reader.Read())
//        {
//            rows.Add($"{reader.GetInt32(0)}, {reader.GetString(1)}, {reader.GetString(2)}");
//        }
//    }

//    return rows;
//})
//.WithName("GetPersons")
//.WithOpenApi();

//app.MapPost("/Person", (Person person) => {
//    using var conn = new SqlConnection(connectionString);
//    conn.Open();

//    var command = new SqlCommand(
//        "INSERT INTO Persons (firstName, lastName) VALUES (@firstName, @lastName)",
//        conn);

//    command.Parameters.Clear();
//    command.Parameters.AddWithValue("@firstName", person.FirstName);
//    command.Parameters.AddWithValue("@lastName", person.LastName);

//    using SqlDataReader reader = command.ExecuteReader();
//})
//.WithName("CreatePerson")
//.WithOpenApi();

//app.Run();