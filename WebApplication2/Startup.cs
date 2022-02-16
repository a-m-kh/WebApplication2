using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Contract;
using WebApplication2.Models;
using WebApplication2.Service;
using Mapster;
using WebApplication2.Dto;
using NLog;
using Microsoft.Extensions.DependencyInjection.ServiceLookup;
using WebApplication2.Repository;
//using Microsoft.Extensions.DependencyInjection;


namespace WebApplication2
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
            services.AddScoped<IWalletService, WalletService>();
            services.AddScoped<IRepository<Wallet>, Repository.Repository<Wallet>>();
            services.AddScoped<ICoinService , CoinService>();
            services.AddScoped<IWalletRepository, WalletRepository>();
            services.AddScoped<ICoinRepository , CoinRepository>();
            services.AddControllers();
            services.AddDbContext<DBContext>(
        options => options.UseSqlServer(@"Server=localhost;Database=WalletDB;Trusted_Connection=True;"));            
        }

        
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });



            TypeAdapterConfig<Wallet, WalletDto>.NewConfig()
                            .Map(dest => dest.name, src => src.name)                            
                            .Map(dest => dest.balance, src => src.balance)
                            .Map(dest => dest.last_update, src => src.last_update)
                            .Map(dest => dest.coins, src => src.coins);
            TypeAdapterConfig<ShowWallet, WalletDto>.NewConfig()
                            .Map(dest => dest.name, src => src.name)
                            .Map(dest => dest.balance, src => src.balance)
                            .Map(dest => dest.last_update, src => src.last_update)
                            .Map(dest => dest.coins, src => src.coins);
            TypeAdapterConfig<Wallet, WalletDto>.NewConfig()
                            .Map(dest => dest.name, src => src.name)
                            .Map(dest => dest.balance, src => src.balance)
                            .Map(dest => dest.last_update, src => src.last_update)
                            .Map(dest => dest.coins, src => src.coins);
            TypeAdapterConfig<Wallet, NewWalletDto>.NewConfig()
                            .Map(dest => dest.name, src => src.name)
                            .Map(dest => dest.balance, src => src.balance)
                            .Map(dest => dest.last_update, src => src.last_update)
                            .Map(dest => dest.coins, src => src.coins);
            TypeAdapterConfig<ShowCoin, CoinDto>.NewConfig()
                            .Map(dest => dest.Name, src => src.Name)
                            .Map(dest => dest.Amount, src => src.Amount)
                            .Map(dest => dest.Rate, src => src.Rate)
                            .Map(dest => dest.Symbol, src => src.Symbol);
            TypeAdapterConfig<Coin, CoinDto>.NewConfig()
                            .Map(dest => dest.Name, src => src.Name)
                            .Map(dest => dest.Amount, src => src.Amount)
                            .Map(dest => dest.Rate, src => src.Rate)
                            .Map(dest => dest.Symbol, src => src.Symbol);
            TypeAdapterConfig<ShowCoin, Coin>.NewConfig()
                            .Map(dest => dest.Name, src => src.Name)
                            .Map(dest => dest.Amount, src => src.Amount)
                            .Map(dest => dest.Rate, src => src.Rate)
                            .Map(dest => dest.Symbol, src => src.Symbol);
        }
    }
}
