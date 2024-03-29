using AidanTwomey.PaymentsGateway.API.Command;
using AidanTwomey.PaymentsGateway.API.Validation;
using AidanTwomey.PaymentsGateway.API.Payments;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Runtime.Caching;
using AidanTwomey.PaymentsGateway.API.Query;
using System;
using Refit;

namespace AidanTwomey.PaymentsGateway.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            // services.AddAuthentication(options =>
            // {
            //     options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //     options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            //     options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            // }).AddScheme()
            // .AddJwtBearer(jwt =>
            // {
            //     jwt.Audience = "identity.aidant-payment-gateway";
            //     jwt.Authority = "https://identity.aidant-payment-gateway"; 
            // });

            services.AddSingleton<ObjectCache>(_ => new MemoryCache("payment_store"));
            services.AddSingleton<IPaymentValidator, PaymentValidator>();
            services.AddSingleton<IStorePaymentCommand, InMemoryStorePaymentCommand>();
            services.AddSingleton<IPaymentTransactionQuery, InMemoryPaymentTransactionQuery>();
            services.AddSingleton<IPaymentService, PaymentService>();
            services.AddTransient<ApiKeyHeaderHandler>();

            services.AddSwaggerGen();

            services
                .AddRefitClient<IBank>(p => new RefitSettings())
                .ConfigureHttpClient(c => c.BaseAddress = new Uri(Configuration["BankUri"]))
                .AddHttpMessageHandler<ApiKeyHeaderHandler>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            // app.UseAuthentication();
            // app.UseAuthorization();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Payment Gateway API V1");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
