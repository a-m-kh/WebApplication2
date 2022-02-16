using System.Threading.Tasks;
using System.Collections.Generic;
using WebApplication2.Dto;
using WebApplication2.Models;

namespace WebApplication2.Contract
{
    public interface IWalletService
    {
        public Task<ALL> GetWallet();
        public Task<WalletDto> BuldNewWallet(string name);
        public Task<WalletDto> UpdateWallet(string name, vorodi Body);
        public Task<WalletDto> DeleteWallet(string name);
    }
}
