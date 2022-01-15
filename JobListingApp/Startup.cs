using AutoMapper;
using JobListingApp.AppCommons;
using JobListingApp.AppCores.Implementations;
using JobListingApp.AppCores.Interfaces;
using JobListingApp.AppDataAccess.DataContext;
using JobListingApp.AppDataAccess.Repository.Implementations;
using JobListingApp.AppDataAccess.Repository.Interfaces;
using JobListingApp.AppModels.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace JobListingApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Job Listing Api",
                    Description = "Job Listing Api Project",
                    Version = "v1",
                });
                option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    In = ParameterLocation.Header,
                    BearerFormat = "JWT",
                    Scheme = "Bearer",
                    Description = "JWT Authorisation header using bearer scheme",

                });
                option.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Beaerer"
                        }
                    },
                    new string[] { }
                    }
                });
            });
            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["[JWT:SecurityKey]"]))
                };
            });
            services.AddDbContextPool<JobDbContext>(option => option.UseSqlite(Configuration.GetConnectionString("Default")));
            services.AddIdentity<AppUser, IdentityRole>(option =>
            {
                option.Password.RequireUppercase = false;
                option.Password.RequireLowercase = false;
                option.Password.RequiredUniqueChars = 0;
                option.Password.RequireDigit = true;
                option.SignIn.RequireConfirmedEmail = true;
                option.Password.RequiredLength = 5;
                option.Password.RequireNonAlphanumeric = false;
                //option.Lockout.AllowedForNewUsers = true;
            }).AddEntityFrameworkStores<JobDbContext>().AddDefaultTokenProviders();
            services.AddAutoMapper();
            services.AddTransient<SeedClass>();
            services.Configure<CloudinarySettings>(Configuration.GetSection("CloudinarySettings"));
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<ICVUpload, CvUpLoadRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IIndustryRepository, IndustryRepository>();
            services.AddScoped<IJobRepository, JobRepository>();
            services.AddScoped<IJobApplicationRepo, JobApplicationRepository>();
            services.AddScoped<IUploadService, UploadService>();
            services.AddScoped<IJobServices, JobServices>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IIndustryService, IndustryServices>();
            services.AddScoped<IApplication, ApplicationServices>();
            services.AddTransient<IMailService, MailService>();
            services.Configure<MailSettings>(Configuration.GetSection("MailSettings"));




        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, SeedClass seed)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
           seed.SeedMe().Wait();
            app.UseSwagger();
            app.UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v1/swagger.json", "Job Listing API V1"));
        }
    }
}
