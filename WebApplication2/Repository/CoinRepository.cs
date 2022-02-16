using WebApplication2.Dto;
using WebApplication2.Models;
using WebApplication2.Contract;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Mapster;
using System.Collections.Generic;
namespace WebApplication2.Repository
{
    public class CoinRepository : WebApplication2.Contract.ICoinRepository 
    {
        private readonly DBContext _content;

        public CoinRepository(DBContext dBContext)
        {
            _content = dBContext;
        }

        public async Task<CoinDto> BuildNewCoin(string name, Vorodi Body)
        {
            Wallet wallet =await _content.wallets.Include(a => a.coins).Where(a => a.name == name).FirstOrDefaultAsync();
            Coin coin = wallet.coins.Where(a => a.Symbol == Body.Symbol).FirstOrDefault();
            if( coin == null)
            {
                CoinDto NewCoin = Body.Adapt<CoinDto>();
                wallet.AddCoin(NewCoin);
                return NewCoin;
            }
            return null;
        }

        public async Task<CoinDto> DeleteCoin(string name, string symbol)
        {
            Wallet wallet = await _content.wallets.Include(a => a.coins).Where(a => a.name == name).FirstOrDefaultAsync();
            CoinDto coin = wallet.coins.Where(a => a.Symbol == symbol).FirstOrDefault().Adapt<CoinDto>();
            if( coin != null)
            {
                wallet.DeletE(coin);
                await _content.SaveChangesAsync();
                return coin;
            }
            return null;

        }

        public async Task<WalletDto> GetCoinsAsync(string name)
        {
            Wallet wallet = await _content.wallets.Where(a => a.name == name).FirstOrDefaultAsync();
            WalletDto wallet1 = wallet.Adapt<WalletDto>();
            return wallet1;
        }
        public async Task<CoinDto> UpdateCoin(string name, string symbol, Vorodi Body)
        {
            Wallet wallet = await _content.wallets.Include(a => a.coins).Where(a => a.name == name).FirstOrDefaultAsync();
           
            var coin =  wallet.coins.Where(a => a.Symbol == symbol).FirstOrDefault().Adapt<CoinDto>();
            wallet.UpDate(coin, Body);
            await _content.SaveChangesAsync();
            return Body.Adapt<CoinDto>();


        }
        public async Task<bool> Search(string wname , string Symbol)
        {
            var wallet = await _content.wallets.Where(a => a.name == wname).FirstOrDefaultAsync();
            if( wallet == null )
            {
                return false;
            }
           var coin = wallet.coins.Where(a => a.Symbol == Symbol).FirstOrDefault();
            if(coin == null )
            {
                return false;
            }
            return true;
        }
        public async Task<bool> SearchW(string name)
        {
            var wallet = await _content.wallets.Where(a => a.name == name).FirstOrDefaultAsync();
            if(wallet == null )
            {
                return false;
            }
            return true;
        }
    }
}
