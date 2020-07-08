using AutoMapper;
using Boticario.Domain.Interfaces;
using Boticario.Domain.Interfaces.Repositories;
using Boticario.Domain.Interfaces.Services;
using Boticario.Infraestructure;
using Boticario.Infraestructure.Repositories;
using Boticario.Service.Services;
using Boticario.WebApi.Mappings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Boticario.Web
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

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
           .AddJwtBearer(options =>
           {
               options.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateIssuer = true,
                   ValidateAudience = true,
                   ValidateLifetime = true,
                   ValidateIssuerSigningKey = true,
                   ValidIssuer = Configuration["Jwt:Issuer"],
                   ValidAudience = Configuration["Jwt:Issuer"],
                   IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
               };
           });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // Configurando o serviço de documentação do Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                        new OpenApiInfo
                        {
                            Title = "JWT",
                            Version = "v1",
                            Description = "REST API created with ASP.NET Core",
                        });
                var security = new Dictionary<string, IEnumerable<string>> { { "Bearer", new string[] { } } };

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description =
                    "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        },
                        new List<string>()
                    }
                });
            });
            
            services.AddDbContext<BoticarioContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Database")));

            // Service
            services.AddScoped<IRevendedorService, RevendedorService>();
            services.AddScoped<ICompraService, CompraService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();

            //Repository
            services.AddScoped<IRevendedorRepository, RevendedorRepository>();
            services.AddScoped<ICompraRepository, CompraRepository>();

            services.AddScoped<IUnitOfWorkFactory, UnitOfWorkFactory>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            //Register automapper
            services.AddAutoMapper(Assembly.GetAssembly(typeof(DomainProfile)));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                //app.UseHsts();
            }

            app.UseAuthentication();

            //app.UseHttpsRedirection();
            app.UseMvc();

            //Ativa o Swagger
            app.UseSwagger();

            // Ativa o Swagger UI
            app.UseSwaggerUI(opt =>
            {
                opt.SwaggerEndpoint("/swagger/v1/swagger.json", "TodoAPI V1");
            });

            UpdateDatabase(app);
        }

        private void UpdateDatabase(IApplicationBuilder app)
        {
            var optionsBuilder = new DbContextOptionsBuilder<BoticarioContext>();
            var options = optionsBuilder.UseSqlServer(Configuration.GetConnectionString("Database")).Options;

            using (var context = new BoticarioContext(options))
            {
                context.Database.Migrate();
            }
        }
    }
}
