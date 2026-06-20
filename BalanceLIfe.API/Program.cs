using System.Text;
using BalanceLife.Domain.Contracts;
using BalanceLife.Domain.Entities.IdentityModule;
using BalanceLife.Persistence.Data.DataSed;
using BalanceLife.Persistence.Data.DbContexts;
using BalanceLife.Persistence.IdentityData.DataSeed;
using BalanceLife.Persistence.IdentityData.DbContexts;
using BalanceLife.Persistence.Repositories;
using BalanceLife.Services;
using BalanceLife.Services.Abstraction;
using BalanceLife.Services.MappingProfiles;
using BalanceLIfe.API.CustomMiddlewares;
using BalanceLIfe.API.Extensions;
using BalanceLIfe.API.Factories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;

namespace BalanceLIfe.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Register DI Container
            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddDbContext<BalanceDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddKeyedScoped<IDataIntializer, DataIntializer>("Default");
            builder.Services.AddKeyedScoped<IDataIntializer, IdentityDataInitializer>("Identity");
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddAutoMapper(typeof(ServiceAssemblyReference).Assembly);

            builder.Services.AddScoped<IMealService, MealService>();
            builder.Services.AddScoped<IWaterService, WaterService>();
            builder.Services.AddScoped<IWorkoutService, WorkoutService>();
            builder.Services.AddScoped<IActivityService, ActivityService>();
            builder.Services.AddScoped<IStepsService, StepsService>();
            builder.Services.AddScoped<ICaffeineService, CaffeineService>();
            builder.Services.AddScoped<IWorkoutVideoService, WorkoutVideoService>();
            builder.Services.AddScoped<IBreathingExerciseService, BreathingExerciseService>();
            //builder.Services.AddTransient<MealPictureUrlResolver>();
            //builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
            //{
            //    return ConnectionMultiplexer.Connect(
            //        builder.Configuration.GetConnectionString("RedisConnection")!
            //        );
            //});

            //builder.Services.AddScoped<ICacheRepository, CacheRepository>();
            //builder.Services.AddScoped<ICacheService, CacheService>();

            builder.Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = ApiResponseFactory.GenerateApiValidationResponse;
            });

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("DevelopmentPolicy", builder =>
                {
                    builder.AllowAnyHeader()
                    .AllowAnyOrigin()
                    .AllowAnyMethod();
                });
            });


            builder.Services.AddDbContext<BalanceIdentityDbContext> (Options =>
            {
                Options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));
            });

            //builder.Services.AddIdentity<ApplicatioUser, IdentityRole>()
            //    .AddEntityFrameworkStores<BalanceIdentityDbContext>();


            builder.Services.AddIdentityCore<ApplicationUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<BalanceIdentityDbContext>();

            builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidIssuer = builder.Configuration["JWTOptions:Issuer"],
                    ValidAudience = builder.Configuration["JWTOptions:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(builder.Configuration["JWTOptions:SecretKey"]!)),

                };
            });
            
            builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Enter your JWT token here."
    });

    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});


            #endregion

            var app = builder.Build();

            await app.MigrateDataBaseAsync();
            await app.MigrateIdentityDataBaseAsync();

            await app.SeedDataAsync();
            await app.SeedIdentityDataAsync();


            #region Configure PipeLine [Middlewares]
            // Configure the HTTP request pipeline.

            app.UseMiddleware<ExceptionHandlerMiddleware>();
            //if (app.Environment.IsDevelopment())
            //{
                app.UseSwagger();
                app.UseSwaggerUI();
            //}

            app.UseStaticFiles();
            //app.UseHttpsRedirection();
            app.UseCors("DevelopmentPolicy");
            //app.UseHsts();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers(); 
            #endregion

            await app.RunAsync();
        }
    }
}
