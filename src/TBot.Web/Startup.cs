using TBot.WebApp;

namespace TBot.Web
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddControllers()
                .AddNewtonsoftJson();
            services.AddRouting();
            services.AddHealthChecks();

            services.AddSwaggerGen(opts => { opts.SwaggerDoc("v1", new() { Title = "kraken API", Version = "v1" }); });

            AppIocConfigure.Configure(services);
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline
        /// <summary>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "TBot"); });

            app.UseRouting();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseMiddleware<AppMiddlewareException>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health");
            });
        }
    }
}