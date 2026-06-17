using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System_uznawania_przychodow.Data;
using System_uznawania_przychodow.Entities;
using System_uznawania_przychodow.Exceptions;
using System_uznawania_przychodow.Services;
using System_uznawania_przychodow.Services.AuthService;
using System_uznawania_przychodow.Services.ClientServices;
using System_uznawania_przychodow.Services.ContractPaymentServices;
using System_uznawania_przychodow.Services.RevenueServices;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(); 
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Type your JWT Token",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            []
        }
    });
});
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IContractService, ContractService>();
builder.Services.AddScoped<IContractPaymentService, ContractPaymentService>();
builder.Services.AddScoped<IRevenueService, RevenueService>();
builder.Services.AddHttpClient<IExchangeService, ExchangeService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
        };
    });
builder.Services.AddAuthorization();

var app = builder.Build();

//Console.WriteLine(BCrypt.Net.BCrypt.HashPassword("admin123", workFactor: 12));
//Console.WriteLine(BCrypt.Net.BCrypt.HashPassword("pracownik123", workFactor: 12));
app.UseExceptionHandler(errorApp => 
{
    errorApp.Run(async context =>
    {
        var exceptionHandlerFeature = context.Features.Get<IExceptionHandlerFeature>();
        var exception = exceptionHandlerFeature?.Error;
        var statusCode = StatusCodes.Status500InternalServerError;
        var errorMessage = "Internal Server Error.";
        
        if (exception != null)
        {
            (statusCode, errorMessage) = exception switch
            {
                // 401
                InvalidCredentialsException => (StatusCodes.Status401Unauthorized, "Invalid login or password."),
                // 404
                NotFoundException => (StatusCodes.Status404NotFound, "Resource not found."),
                ClientNotFoundException => (StatusCodes.Status404NotFound, "Client not found."),
                SoftwareNotFoundException => (StatusCodes.Status404NotFound, "Software not found."),
                ContractNotFoundException => (StatusCodes.Status404NotFound, "Contract not found."),
                // 409
                EmailAlreadyInUseException => (StatusCodes.Status409Conflict, "Email already in use."),
                // 400
                CompanyClientCantBeDeletedException => (StatusCodes.Status400BadRequest, "Cannot delete company client."),
                InvalidContractDurationException => (StatusCodes.Status400BadRequest, "Invalid contract duration."),
                InvalidDateRangeException => (StatusCodes.Status400BadRequest, "Payment window must be 3-30 days."),
                ActiveContractAlreadyExistsException => (StatusCodes.Status400BadRequest, "Active contract already exists."),
                CannotDeleteSignedContractException => (StatusCodes.Status400BadRequest, "Cannot delete signed contract."),
                ContractAlreadySignedException => (StatusCodes.Status400BadRequest, "Contract already signed."),
                ContractExpiredException => (StatusCodes.Status400BadRequest, "Contract expired. Payments refunded."),
                PaymentAmountExceededException => (StatusCodes.Status400BadRequest, "Payment exceeds remaining balance."),
                UnsupportedCurrencyException => (StatusCodes.Status400BadRequest, "Unsupported currency."),
                WrongNumberFormatException => (StatusCodes.Status400BadRequest, "Invalid number format."),
                // 500
                _ => (StatusCodes.Status500InternalServerError, "Internal server error.")
            };
        }

        context.Response.StatusCode = statusCode;
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsJsonAsync(new
        {
            message = errorMessage
        });
    });
});
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();