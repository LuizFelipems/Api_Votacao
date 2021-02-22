using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Votacao.Dominio;
using Votacao.Dominio.Autenticacao;
using Votacao.Dominio.Handlers;
using Votacao.Dominio.Interfaces.Repositories;

namespace Votacao.Api
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
            #region [+] AppSettings
            services.Configure<SettingsDomain>(options => Configuration.GetSection("SettingsDomain").Bind(options));
            #endregion

            #region [+] Handlers
            services.AddTransient<UsuarioHandler>();
            services.AddTransient<FilmeHandler>();
            services.AddTransient<VotoHandler>();
            #endregion

            #region [+] Swagger
            services.AddSwaggerGen(c =>
            {
                c.DescribeAllParametersInCamelCase();
                c.IncludeXmlComments($@"{AppDomain.CurrentDomain.BaseDirectory}\Swagger.xml");
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Votação API",
                    Description = "Projeto de votação de filmes",
                    Contact = new OpenApiContact
                    {
                        Name = "Luiz Felipe",
                        Email = "luizfelipems12@gmail.com",
                        Url = new Uri("https://bitbucket.org/luizfelipems/empresas-dotnet/src/master/##--o-que-desenvolver")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Licença MIT",
                        Url = new Uri("https://bitbucket.org/luizfelipems/empresas-dotnet/src/master/##--o-que-desenvolver")
                    }
                });
            });
            #endregion

            #region [+] AutenticacaoJWT
            var key = Encoding.ASCII.GetBytes(Configuration.GetValue<string>("SettingsDomain:SecretKey"));
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.AddTransient<TokenService>();
            #endregion

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Votação API");
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
