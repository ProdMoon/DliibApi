using DliibApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using DliibApi.AutoMapperProfiles;
using DliibApi.Services;
using DliibApi.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Load App Secrets For Development Environment
builder.Configuration.AddJsonFile("appsecrets.json", optional: true, reloadOnChange: true);

/* CONFIGURATION */
// Database
var mysqlConnectionString = builder.Configuration.GetConnectionString("MysqlConnection");
builder.Services.AddDbContext<AppDbContext>(options => options.UseMySql(mysqlConnectionString, ServerVersion.AutoDetect(mysqlConnectionString)));

// Identity
builder.Services.AddAuthorization();
builder.Services.AddIdentityApiEndpoints<DliibUser>()
    .AddEntityFrameworkStores<AppDbContext>();
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;
});

// Controllers
builder.Services.AddControllers();

// Services
builder.Services.AddScoped<DliibLikeService>();
builder.Services.AddScoped<DliibService>();
builder.Services.AddScoped<UserService>();

// Repositories
builder.Services.AddScoped<DliibLikeRepository>();
builder.Services.AddScoped<DliibRepository>();
builder.Services.AddScoped<UserRepository>();

// CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register AutoMapper
builder.Services.AddAutoMapper(typeof(DliibProfile));

/* BUILD */
var app = builder.Build();

/* POST-BUILD */
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapGroup("/api/auth").MapIdentityApi<DliibUser>();
app.UseHttpsRedirection();
app.UseCors();
app.UseAuthorization();
app.MapControllers();

app.Run();