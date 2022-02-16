using System;
using System.Collections.Generic;
using WebApplication2.Models;

namespace WebApplication2.Dto
{
    public class NewWalletDto
    {
        public string name { get; set; }
        public float balance { get; set; }
        public List<Coin> coins { get; set; } = new List<Coin>();
        public DateTime last_update { get; set; }

        public NewWalletDto(string name)
        {
            this.name = name;
            balance = 0;
            coins = new List<Coin>();
            DateTime last_update_time = DateTime.Now;
        }
    }
}
