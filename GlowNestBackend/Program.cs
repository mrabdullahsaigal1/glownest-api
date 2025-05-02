using System.Data.SQLite;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();

// Add CORS services
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()   // Allow all origins
              .AllowAnyHeader()   // Allow any headers
              .AllowAnyMethod();  // Allow any HTTP method (GET, POST, etc.)
    });
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Apply migrations programmatically at startup
using (var connection = new SQLiteConnection("sqlitecloud://cwaq4vfxnk.g4.sqlite.cloud:8860/chinook.sqlite?apikey=PbCsf8u3mWXEzRpLmz2Lb5O9zO7M4sKIlkFZh8d71C8"))
{
    try
    {
        connection.Open();
        Console.WriteLine("Connected to SQLite Cloud successfully!");
        // Execute any initialization or schema creation here if required
    }
    catch (Exception ex)
    {
        Console.WriteLine($"An error occurred while connecting to SQLite Cloud: {ex.Message}");
    }
}

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Use the CORS policy
app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
