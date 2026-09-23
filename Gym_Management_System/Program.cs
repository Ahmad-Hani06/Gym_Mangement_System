

using Gym_Management_System.BackgroundServices;
using Gym_Management_System.Business.Services;
using Gym_Management_System.Data;
using Gym_Management_System.DataAccess;
using Gym_Management_System.DataAccess.Members;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));
// Add services to the container.

// Already Created an objects
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

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

//builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHostedService<MembershipExpirationService>();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
