using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models;
using System.Linq;
using System.Collections.Generic;
using System;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WebApplication2.Contract;
using WebApplication2.Service;
using WebApplication2.Dto;
using Mapster;
namespace WebApplication2.Controllers
{
    [ApiController]
    [Route("Wallets")]

    public class WalletController : ControllerBase
    {
        private readonly IWalletService _walletService;
        public WalletController(IWalletService walletService)
        {
            _walletService = walletService;
        }
        [HttpGet]
        public async Task<ALL> Get()
        {
            ALL aLL = await _walletService.GetWallet();            
            return aLL;
        }
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] vorodi enter)
        {
            WalletDto dt = await _walletService.BuldNewWallet(enter.name);
           if(dt != null)
            {
                ShowWallet output = dt.Adapt<ShowWallet>();
                output.message = "Created seccesfully";
                return Ok(output);
            }
            return NotFound();
        }
        [HttpPut("{wname}")]
        public async Task<ActionResult> PUT(string wname, [FromBody]vorodi input)
        {
            WalletDto wd = await _walletService.UpdateWallet(wname, input);
            if(wd != null)
            {
                ShowWallet output = wd.Adapt<ShowWallet>();
                output.message = "Wallet name changed successfully!";
                return Ok(output);
            }
            return NotFound();           
        }

        [HttpDelete("{wname}")]
        public async Task<ActionResult> Delete(string wname)
        {
            WalletDto wd = await _walletService.DeleteWallet(wname);
            if( wd != null)
            {
                ShowWallet output = wd.Adapt<ShowWallet>();
                output.message = "Wallet deleted (logged out) successfully!";
                return Ok(output);
            }
            return NotFound();                      
        }
    }
}
