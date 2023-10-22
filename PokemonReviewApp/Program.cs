using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using PokemonReviewApp.Data;
using PokemonReviewApp.Data.Interface;
using PokemonReviewApp.Helpers.AuthJWT;
using PokemonReviewApp.Migrations;
using PokemonReviewApp.Repository;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

namespace PokemonReviewApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            //registering the DB connectiviety using EF core.
            builder.Services.AddDbContext<PokemonDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));

            });


            // Add services to the container.

            /* this service will tell the web if consumer api for ex: requested application/xml and this isn't supported in our 
            app it will return to him 406 NOT ACCEPTABLE and not returning application/json (which is the default).?*/
            builder.Services.AddControllers(configure =>
            {
                configure.RespectBrowserAcceptHeader = true;
                configure.ReturnHttpNotAcceptable = true;

                //Adding support for application/xml
            }).AddXmlDataContractSerializerFormatters()
            .ConfigureApiBehaviorOptions(setupAction =>
            {
                setupAction.InvalidModelStateResponseFactory = context =>
                {
                    // create a validation problem details object
                    var problemDetailsFactory = context.HttpContext.RequestServices
                        .GetRequiredService<ProblemDetailsFactory>();

                    var validationProblemDetails = problemDetailsFactory
                        .CreateValidationProblemDetails(
                            context.HttpContext,
                            context.ModelState);
                    // add additional info not added by default
                    validationProblemDetails.Detail =
                        "See the errors field for details.";
                    validationProblemDetails.Instance =
                        context.HttpContext.Request.Path;

                    // report invalid model state responses as validation issues
                    validationProblemDetails.Type =
                        "https://PokemonReviewApp.com/modelvalidationproblem";
                    validationProblemDetails.Status =
                        StatusCodes.Status422UnprocessableEntity;
                    validationProblemDetails.Title =
                        "One or more validation errors occurred.";
                    return new UnprocessableEntityObjectResult(
                    validationProblemDetails)
                    {
                        ContentTypes = { "application/problem+json" }
                    };
                };
            });







            builder.Services.AddTransient<Seeding>();
          
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());




            // for JWT Mapping for class
            builder.Services.Configure<JWT>(builder.Configuration.GetSection("JWT"));
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<PokemonDbContext>()
                            .AddDefaultTokenProviders();

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(o =>
                {
                    o.RequireHttpsMetadata = false;
                    o.SaveToken = false;
                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidIssuer = builder.Configuration["JWT:Issuer"],
                        ValidAudience = builder.Configuration["JWT:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]))

                    };
                });





            //registering repos 
            builder.Services.AddScoped<IPokemonRepository, PokemonRepository>();
            builder.Services.AddScoped<ICategoryRepository , CategoryRepository>();
            builder.Services.AddScoped<ICountryRepository , CountyRepository>();
            builder.Services.AddScoped<IOwnerRepository , OwnerRepository>();
            builder.Services.AddScoped<IReviewRepository , ReviewRepository>();
            builder.Services.AddScoped<IReviewerRepsitory , ReviewerRepository>();

            builder.Services.AddScoped<IAuthRepository, AuthRepsitory>();



            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(setupAction =>
            {
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                setupAction.IncludeXmlComments(xmlPath);
            });

            builder.Services.AddApiVersioning(setupAction =>
            {
                setupAction.AssumeDefaultVersionWhenUnspecified = true;
                setupAction.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
                setupAction.ReportApiVersions = true;


            });            


            var app = builder.Build();


            if (args.Length == 1 && args[0].ToLower() == "seeddata")
                SeedData(app);

            void SeedData(IHost app)
            {
                var scopedFactory = app.Services.GetService<IServiceScopeFactory>();

                using (var scope = scopedFactory.CreateScope())
                {
                    var service = scope.ServiceProvider.GetService<Seeding>();
                    service.SeedDataContext();
                }
            }
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.Run();
        }
    }
}