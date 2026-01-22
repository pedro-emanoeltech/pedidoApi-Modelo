using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace Pedidos.Api.Configuracao
{
    public static class ApiConfiguracao
    {
        public static IServiceCollection AddApiConfiguracao(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMvc();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = $"Api de Pedidos",
                    Description = "ASP.NET Core ,REST API, DDD, Princípios SOLID e Clean Architecture",
                    Version = "v1",
                    Contact = new OpenApiContact
                    {
                        Name = "Pedro Emanoel",
                        Email = "pedro.emanoeltech@hotmail.com",
                    }
                });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Cabeçalho de autorização JWT usando o esquema Bearer. \r\n\r\nDigite 'Bearer' [espaço] e depois seu token no campo abaixo.\r\n\r\nExemplo: \"Bearer 12345abcdef\"",
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
                            Array.Empty<string>()
                    }
                });

                var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath, true);
            });

            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                var secretJwt = configuration.GetSection("JwtConfig").GetValue<string>("SecretKey");
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretJwt)),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = false,
                };
            });
 
            services.AddHttpLogging(delegate (HttpLoggingOptions options)
            {
                options.LoggingFields = HttpLoggingFields.ResponseStatusCode;
            });

            services.Configure(delegate (RouteOptions routeOptions)
            {
                routeOptions.LowercaseUrls = true;
                routeOptions.LowercaseQueryStrings = true;
            });
 
            services.AddControllers().ConfigureApiBehaviorOptions(delegate (ApiBehaviorOptions options)
            {
                options.SuppressMapClientErrors = true;
                options.SuppressModelStateInvalidFilter = true;
            });

            return services;
        }

        public static void UseApiConfiguracao(this IApplicationBuilder app)
        {
            app.UseHttpLogging();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
            app.UseAuthorization();
            app.UseAuthentication();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", $"Pedidos v1");
            });
        }


    }
}
