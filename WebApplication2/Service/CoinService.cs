using Mapster;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Contract;
using WebApplication2.Dto;
using WebApplication2.Models;
using System;
using WebApplication2.Repository;
using Microsoft.Extensions.Logging;

namespace WebApplication2.Service
{
    public class CoinService : ICoinService
    {
        private string id()
        {
            Guid uniqe = Guid.NewGuid();
            return uniqe.ToString();
        }
        private readonly ILogger<WalletService> _logger ;
        private readonly  ICoinRepository _repository;
       
        public CoinService(ICoinRepository repository , ILogger<WalletService> log)
        {
            _repository = repository;
            _logger = log;
        }
        public async Task<CoinDto> BuildNewCoin(string name , Vorodi Body)
        {
            string Id = id();
            _logger.LogInformation("Id : " + Id + ";" + " Request : Build a new Coin");
            var x = await _repository.Search(name, Body.Symbol);
            if(!x)
            {
                _logger.LogInformation("Id : " + Id + ";" + " coin was builded");
                return null;
            }
            try
            {
                Coin coin = Body.Adapt<Coin>();
                CoinDto Coin =  await _repository.BuildNewCoin(name, Body);
                _logger.LogInformation("Id : " + Id + ";" + " coin build");
                return Coin;
            }
            catch (Exception ex)
            {
                _logger.LogError("Id : " + Id + "; " + ex.Message);
            }
            return null;            
        }

        public async Task<WalletDto> GetCoinsAsync(string name)
        {
            string Id = id();
            _logger.LogInformation("Id : " + Id + "; " + "Request : Get Coins");
            try
            {
                var wallet = await _repository.SearchW(name);
                if(wallet)
                {
                    WalletDto wd = await _repository.GetCoinsAsync(name);
                    _logger.LogInformation("Id : " + Id + "; " + "Succses");
                    return wd;
                }
                return null;
                
            }catch(Exception ex)
            {
                _logger.LogInformation("Id : " + Id + "; " + ex.Message);
                return null;
            }

        }

        public async Task<CoinDto> UpdateCoin(string name, string symbol , Vorodi Body)
        {
            string Id = id();
            _logger.LogInformation("Id : " + Id + "; " + "Request is Update Coin");
            var wallet = await _repository.Search(name , symbol);
            try
            {
                if(wallet)
                {
                    CoinDto wd = await _repository.UpdateCoin(name, symbol, Body);
                    return wd;
                }
                else
                {
                    return null;
                }
            }
            catch(Exception ex)
            {
                _logger.LogError("Id : " + Id + "; " + ex.Message);
                return null;
            }            
        }
        public async Task<CoinDto> DeleteCoin(string name , string symbol)
        {
            string Id = id();

            _logger.LogInformation("Id : " + Id + "; " + "Request : Delete Coin");
            var wallet = await _repository.Search(name , symbol);
            try
            {
                if (wallet)
                {
                    CoinDto cd = await _repository.DeleteCoin(name , symbol);
                    return cd;
                }
                return null;
            }catch(Exception ex)
            {
                _logger.LogError("Id : " + Id + "; " + ex.Message);
            }
            return null;           
        }
    }
}
