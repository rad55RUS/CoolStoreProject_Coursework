using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CoolStoreProject
{
    /// <summary>
    /// Represents user for imitation of user interaction with kiosk
    /// </summary>
    public class User
    {
        // Fields
        private double cash;
        private double cardMoney;
        private string bonusCard;
        //

        // Properties
        public double Cash {
            get => cash;
            set => cash = value;
        }
        public double CardMoney {
            get => cardMoney;
            set => cardMoney = value;
        }
        public string BonusCard { 
            get => bonusCard;
            set => bonusCard = value;
        }
        //

        // Constructors
        public User(double cash, double cardMoney, string bonusCard)
        {
            this.cash = cash;
            this.cardMoney = cardMoney;
            this.bonusCard = bonusCard;
        }
        //
    }
}
