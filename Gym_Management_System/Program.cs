using Gym_Management_System.BackgroundServices;
using Gym_Management_System.Business.Services;
using Gym_Management_System.Data;
using Gym_Management_System.DataAccess;
using Gym_Management_System.DataAccess.Members;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;

using System.Text;


var builder = WebApplication.CreateBuilder(args);


// ===============================
// Database
// ===============================

var connectionString =
    builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));


// ===============================
// Dependency Injection
// ===============================

builder.Services.AddScoped<MemberService>();
builder.Services.AddScoped<MemberData>();

builder.Services.AddScoped<PersonService>();
builder.Services.AddScoped<PersonData>();

builder.Services.AddScoped<SubscriptionTypeService>();
builder.Services.AddScoped<SubscriptionTypeData>();

builder.Services.AddScoped<MembershipData>();
builder.Services.AddScoped<MembershipService>();

builder.Services.AddScoped<PaymentService>();
builder.Services.AddScoped<PaymentData>();

builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<UserData>();

builder.Services.AddScoped<CoachService>();
builder.Services.AddScoped<CoachData>();
builder.Services.AddScoped<RefreshTokenData>();
builder.Services.AddScoped<TokenService>();

// ===============================
// Controllers
// ===============================

builder.Services.AddControllers();


// ===============================
// JWT Authentication
// ===============================

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters =
            new TokenValidationParameters
            {
                // Check issuer
                ValidateIssuer = true,

                // Check audience
                ValidateAudience = true,

                // Check expiration
                ValidateLifetime = true,

                // Check signature
                ValidateIssuerSigningKey = true,


                // Must match JWT generation
                ValidIssuer = "GymManagementSystemAPI",

                ValidAudience = "GymManagementSystemClient",


                // Must be the SAME key used when creating JWT
                IssuerSigningKey =
                    new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(
                            "THIS_IS_A_VERY_SECRET_KEY_123456"
                        )
                    )



            };

    });


// ===============================
// Authorization
// ===============================

builder.Services.AddAuthorization();


// ===============================
// Swagger + JWT
// ===============================

builder.Services.AddSwaggerGen(options =>
{
    // Define Bearer authentication
    options.AddSecurityDefinition(
        "Bearer",
        new OpenApiSecurityScheme
        {
            Type = SecuritySchemeType.Http,

            Scheme = "bearer",

            BearerFormat = "JWT",

            Description =
                "Enter your JWT token here."
        }
    );


    // Tell Swagger to use Bearer authentication
    options.AddSecurityRequirement(document =>
        new OpenApiSecurityRequirement
        {
            [new OpenApiSecuritySchemeReference(
                "Bearer",
                document
            )] = []
        });
});


// ===============================
// Background Services
// ===============================

builder.Services.AddHostedService<MembershipExpirationService>();


var app = builder.Build();


// ===============================
// HTTP Pipeline
// ===============================

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI();
}


app.UseHttpsRedirection();


// IMPORTANT:
// Authentication must come before Authorization
app.UseAuthentication();

app.UseAuthorization();


app.MapControllers();


app.Run();