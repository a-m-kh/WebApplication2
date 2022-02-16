using WebApplication2.Models;
using System.Threading.Tasks;
using System.Linq;
using WebApplication2.Dto;
using Microsoft.EntityFrameworkCore;
using Mapster;
using System.Collections.Generic;
namespace WebApplication2.Repository
{
    public class WalletRepository : WebApplication2.Contract.IWalletRepository
    {
        private readonly DBContext _context;
        public WalletRepository(DBContext context) 
        {
            _context = context;
        }

        public async Task<WalletDto> Create(string wname)
        {
            NewWalletDto wallet = new NewWalletDto(wname);
            await _context.wallets.AddAsync(wallet.Adapt<Wallet>());
            await _context.SaveChangesAsync();
            return wallet.Adapt<WalletDto>();
        }

        public async  Task<WalletDto> Delete(string wname)
        {
            Wallet wallet = await _context.wallets.Include(a => a.coins).Where(a => a.name == wname).FirstOrDefaultAsync();
            _context.wallets.Remove(wallet);
            await _context.SaveChangesAsync();
            return wallet.Adapt<WalletDto>();
        }

        public async Task<ALL> Get()
        {
            List<Wallet> wallets = await _context.wallets.Include("coins").ToListAsync();
            int count = await _context.wallets.CountAsync();
            ALL all = new ALL();
            all.wallets = wallets.Adapt<List<WalletDto>>();
            all.size = count;
            return all;
        }

        public async Task<bool> Search(string wname)
        {
            Wallet wallet = await _context.wallets.Where(a => a.name == wname).FirstOrDefaultAsync();
            if(wallet == null)
            {
                return false;
            }
            return true;
        }

        public async Task<WalletDto> Update(string wname, string name)
        {
            var wallets = await _context.wallets.Where(a => a.name == wname).FirstOrDefaultAsync();
            wallets.name = name;
            await _context.SaveChangesAsync();
            return wallets.Adapt<WalletDto>();
            
        }
    }
}
