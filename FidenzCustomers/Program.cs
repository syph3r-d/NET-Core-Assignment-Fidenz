using FidenzCustomers.Common.Interfaces;
using FidenzCustomers.Common.Uitility;
using FidenzCustomers.Data;
using FidenzCustomers.Models;
using FidenzCustomers.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "_Fidenz",
                      policy =>
                      {
                          policy.WithOrigins("*").AllowAnyHeader();
                      });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey
        (Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = false,
        ValidateIssuerSigningKey = true
    };
});
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(SD.Role_Admin, policy => policy.RequireClaim(SD.Role_Admin,"true"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("_Fidenz");


app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    var roles = new[] { SD.Role_Admin, SD.Role_Customer };

    foreach (var role in roles)
    {
        if (!roleManager.RoleExistsAsync(role).GetAwaiter().GetResult())
        {
            roleManager.CreateAsync(new IdentityRole(role)).Wait();
        }
    }
}

using (var scope = app.Services.CreateScope())
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

    var adminUser = new ApplicationUser
    {
        UserName = "Dante@gmail.com",
        Email = "Dante@gmail.com",
        Name = "Dante",
        CreatedAt = DateTime.Now,
    };

    if (await userManager.FindByEmailAsync("Dante@gmail.com") == null)
    {
        await userManager.CreateAsync(adminUser, "Test@123");
        await userManager.AddToRoleAsync(adminUser, SD.Role_Admin);

    }

    var normalUser = new ApplicationUser
    {
        UserName = "JohnDoe@gmail.com",
        Email = "JohnDoe@gmail.com",
        Name = "John Doe",
        CreatedAt = DateTime.Now,
    };

    if (await userManager.FindByEmailAsync("JohnDoe@gmail.com") == null)
    {
        await userManager.CreateAsync(normalUser, "Test@123");
        await userManager.AddToRoleAsync(normalUser, SD.Role_Customer);

    }

}

app.Run();
