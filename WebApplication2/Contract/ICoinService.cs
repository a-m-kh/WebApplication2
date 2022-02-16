using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication2.Dto;
using WebApplication2.Models;

namespace WebApplication2.Contract
{
    public interface ICoinService
    {
       public Task<WalletDto> GetCoinsAsync(string name);
        // Task<long> GetCoinsCount(string name);
        public Task<CoinDto> BuildNewCoin(string name, Vorodi Body);
        public Task<CoinDto> UpdateCoin(string name ,string symbol, Vorodi Body);
        public Task<CoinDto> DeleteCoin(string name, string symbol);
    }
}
