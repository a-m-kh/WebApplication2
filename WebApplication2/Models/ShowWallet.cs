using System.Collections.Generic;
using System;
using WebApplication2.Dto;

namespace WebApplication2.Models
{
    public class ShowWallet
    {
        public string name { get; set; }
        public float balance { get; set; }
        public List<CoinDto> coins { get; set; }
        public DateTime last_update { get; set; }
        public string message { get; set; } = "Wallet added successfully!";
        public ShowWallet()
        {
            name = "";
            balance = 0;
            coins = null;
        }
    }

}
