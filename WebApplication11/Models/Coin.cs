using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace WebApplication11.Models
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

        //public static bool operator ==(Coin left, Coin right) =>
        //    (left.Weight, left.Radius) == (right.Weight, right.Radius);

        //public static bool operator !=(Coin left, Coin right) =>
        //    (left.Weight, left.Radius) != (right.Weight, right.Radius);

        //public override int GetHashCode() => Weight.GetHashCode() ^ Radius.GetHashCode();

        //public override bool Equals(object? obj) =>
        //    (obj is Coin otherCoin)
        //    ? this == otherCoin
        //    : false;

        public (string, decimal) Type
        {
            get
            {
                return (Weight, Radius) switch
                {
                    (5, 21) => ("Nickel", 5),
                    (2, 18) => ("Dime", 10),
                    (6, 24) => ("Quarter", 25),
                    (3, 19) => ("Pennie", 1),
                    (_, _) => ("Invalid", 0)
                };
            }
        }
    }
}
