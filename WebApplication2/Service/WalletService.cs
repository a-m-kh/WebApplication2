using Mapster;
using System.Collections.Generic;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WebApplication2.Contract;
using WebApplication2.Models;
using WebApplication2.Dto;
using NLog;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WebApplication2.Repository;
namespace WebApplication2.Service
{
    public class WalletService : IWalletService
    {
        private string id()
        {
            Guid id = Guid.NewGuid();
            return id.ToString();
        }
        //private readonly DBContext _context;
        private readonly IWalletRepository _repository;
        private readonly ILogger<WalletService> _logger;
        
        public WalletService(IWalletRepository repository, ILogger<WalletService> logger)
        {
            _repository = repository;
            _logger = logger;
            
        }


        public async Task<ALL> GetWallet()
        {
            ALL all = new ALL();           
            string ID = id();
            _logger.LogInformation("Id : " +ID + ";" + " Request : Get All of Wallets;" );
            try
            {
               all =  await _repository.Get();
            }catch (Exception ex)
            {
                _logger.LogError("Id : " + ID + "; " + ex.Message);
                return null; 
            }
            _logger.LogInformation("Id : " + ID + ";" + " Response is Send");                       
            return all;
        }
        public async Task<WalletDto> BuldNewWallet(string name)
        {
            string ID = id();
            _logger.LogInformation("Id : " + ID + ";" + " Request : Build a new Wallet ");
            try {

                bool status =await _repository.Search(name);
                if (!status)
                {
                    WalletDto wallet =  await _repository.Create(name);

                    _logger.LogInformation("Id : " + ID + "; " + " Build");
                    return wallet;
                }
                }catch(Exception ex)
                {
                    _logger.LogError("Id : "+ ID + ";" + ex.Message);
                }            
            _logger.LogInformation( "Id : "+ ID + ";" + "Name : " + name + " is dublicated");
            return null;
        }

        public async Task<WalletDto> UpdateWallet(string name, vorodi Body)
        {
            string ID = id();
            _logger.LogInformation("Id : " + ID + ";" + " Requset: Update Wallet ");
            try
            {
                bool status = await _repository.Search(name);
                if(status)
                {
                    WalletDto wd = await _repository.Update(name, Body.name);
                    _logger.LogInformation("Id : " + ID + ";" + "wallet is fined");
                    return wd;
                }               
            }catch(Exception e)
            {
                _logger.LogError("Id : " + ID + "; "+ e.Message);
            }
            return null;
        }
        public async Task<WalletDto> DeleteWallet(string name)
        {
            string ID = id();
            _logger.LogInformation("Id : " + ID + ";" + " Request : Delete a wallet");
            try
            {
                bool status = await _repository.Search(name);
                if (status)
                {
                    WalletDto wd = await _repository.Delete(name);
                    _logger.LogInformation("Id : " + ID + ";" + " Wallet is removed");
                    return wd;
                }
            }catch(Exception e)
            {
                _logger.LogError("Id : " + ID + ";" + e.Message);
            }
            return null;                      
        }
    }
}
