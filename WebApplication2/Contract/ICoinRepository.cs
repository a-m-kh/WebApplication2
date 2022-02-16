using WebApplication2.Models;
using WebApplication2.Dto;
using System.Threading.Tasks;
namespace WebApplication2.Contract
{
    public interface ICoinRepository
    {
        public Task<WalletDto> GetCoinsAsync(string wname);
        public Task<CoinDto> UpdateCoin( string wname ,string symbol ,Vorodi Body);
        public Task<CoinDto> DeleteCoin(string wname  ,string symbol);
        public Task<CoinDto> BuildNewCoin(string wname , Vorodi Body);
        public Task<bool> Search(string wname , string Cname);
        public Task<bool> SearchW(string name);
    }
}
