using WebApplication2.Dto;
using WebApplication2.Models;
using System.Threading.Tasks;

namespace WebApplication2.Contract
{
    public interface IWalletRepository
    {
        public Task<ALL> Get();
        public Task<WalletDto> Create(string wname);
        public Task<WalletDto> Update(string wname , string name);
        public Task<WalletDto> Delete(string wname);
        public Task<bool> Search(string wname);

        
    }
}
