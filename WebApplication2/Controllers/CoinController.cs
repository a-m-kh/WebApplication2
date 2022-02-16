using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebApplication2.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WebApplication2.Contract;
using WebApplication2.Service;
using WebApplication2.Dto;
using Mapster;
using System;
using Microsoft.Extensions.DependencyInjection;

namespace WebApplication2.Controllers
{

    [ApiController]
    [Route("Coins")]
    public class CoinController : ControllerBase
    {
        //private readonly IServiceProvider _serviceProvider;
        private readonly ICoinService _coinService;
        private readonly NLog.ILogger log = NLog.LogManager.GetCurrentClassLogger(); 
        public CoinController(ICoinService coinService)
        {
            _coinService = coinService;
        }

        [HttpPost("{wname}")]
        public async Task<IActionResult> Post(string wname, [FromBody] Vorodi body )
        {                            
            CoinDto dt = await _coinService.BuildNewCoin(wname, body);
            if (dt != null)
            {
                ShowCoin output = dt.Adapt<ShowCoin>();
                output.message = "Coin added successfully!";
                return Ok(output);
            }
            return NotFound();                      
        }
        [HttpGet("{wname}")]
        public async Task<IActionResult> Get(string wname)
        {           
            WalletDto wd = await _coinService.GetCoinsAsync(wname);
            if (wd != null)
            {
                ShowWallet output = wd.Adapt<ShowWallet>();
                output.message = "All coins received sucessfuly!";
                return Ok(output);
            }
            return NotFound();
        }
        [HttpPut("{wname}/{symbol}")]
        public async Task<IActionResult> put(string wname, string symbol, [FromBody] Vorodi body)
        {           
            CoinDto cd = await _coinService.UpdateCoin(wname, symbol, body);
            if (cd != null)
            {
                ShowCoin output = cd.Adapt<ShowCoin>();
                output.message = "Coin updated successfully!";
                return Ok(output);
            }
            return NotFound();
        }
        [HttpDelete("{wname}/{symbol}")]
        public async Task<IActionResult> Delete(string wname, string symbol)
        {
            
            CoinDto cd = await _coinService.DeleteCoin(wname, symbol);
            if (cd != null)
            {
                ShowCoin output = cd.Adapt<ShowCoin>();
                output.message = "Coin deleted successfully!";
                return Ok(output);
            }
            return NotFound();
        }
    }
}
