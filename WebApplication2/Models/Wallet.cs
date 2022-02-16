using System.Collections.Generic;
using System;
using System.Linq;
using WebApplication2.Dto;
using WebApplication2.Controllers;
using Mapster;
//sing WebApplication2.Controllers;

namespace WebApplication2.Models
{
    public class Wallet
    {
        public int id { get; set; }
        public string name { get; set; }
        public float balance { get; set; }
        public List<Coin> coins { get; set; } = new List<Coin>();
        public DateTime last_update { get; set; }
        public void AddCoin(CoinDto coin)
        {
            balance += coin.Amount * coin.Rate;
            last_update = DateTime.Now;
            coins.Add(coin.Adapt<Coin>());

        }
        public void UpDate(CoinDto coin, Vorodi body)
        {
            balance -= coin.Amount * coin.Rate;
            var COIN = coins.Where(a => a.Symbol == body.Symbol).FirstOrDefault();
            COIN.Amount = body.Amount;
            COIN.Rate = body.Rate;
            COIN.Symbol = body.Symbol;
            COIN.Name = body.Name;
            balance += COIN.Rate * COIN.Amount;
        }
        public void DeletE(CoinDto coin)
        {
            balance -= coin.Amount * coin.Rate;
            Coin x = coin.Adapt<Coin>();
            Coin y = coins.Where(a => a.Name == coin.Name).FirstOrDefault();
            coins.Remove(y);
        }
    }
}
