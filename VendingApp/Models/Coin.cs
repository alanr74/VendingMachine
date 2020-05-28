using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace VendingApp.Models
{
    public struct Coin
    {
        public int Weight;
        public int Radius;
        public string CoinName
        {
            get
            {
                return Type.Item1;
            }
        }

        public decimal CoinWorth
        {
            get
            {
                return Type.Item2;
            }
        }

        public bool IsValid
        {
            get
            {
                return !Type.Item1.Equals("Invalid");
            }
        }

        public Coin(int Weight, int Radius) =>
            (this.Weight, this.Radius) = (Weight, Radius);

        public (string, decimal) Type
        {
            get
            {
                return (Weight, Radius) switch
                {
                    (5, 21) => ("Nickel", 0.5m),
                    (2, 18) => ("Dime", 0.10m),
                    (6, 24) => ("Quarter", 0.25m),
                    (_, _) => ("Invalid", 0.0m)
                };
            }
        }
    }
}
