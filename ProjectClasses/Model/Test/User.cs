using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation;

namespace CoolStoreProject
{
    /// <summary>
    /// Represents user for imitation of user interaction with kiosk
    /// </summary>
    public class User
    {
        // Fields
        // Static
        private static int amount = 0;
        //
        // Data
        private int id;
        private double cash;
        private double cardMoney;
        private string bonusCard;
        //
        //

        // Properties
        // Data
        public int ID {
            get => id;
        }
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
        // Representation
        public string ID_Representation {
            get => "User example " + id;
        }
        public string Cash_Representation {
            get => "Cash: " + cash.ToString();
        }
        public string CardMoney_Representation {
            get => "Card money: " + cardMoney.ToString();
        }
        public string BonusCard_Representation { 
            get => "Bonus card number: " + bonusCard;
        }
        //
        //

        // Constructors
        public User(double cash, double cardMoney, string bonusCard)
        {
            amount++;

            this.id = amount - 1;
            this.cash = cash;
            this.cardMoney = cardMoney;
            this.bonusCard = bonusCard;
        }
        //
    }
}
