using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation;

namespace CoolStoreProject.UserVVM
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
        private readonly int id;
        private double cash;
        private double cardMoney;
        private string bonusCard;
        private int bonuses;
        //
        //

        // Properties
        // Data
        internal int ID {
            get => id;
        }
        internal double Cash {
            get => cash;
            set => cash = value;
        }
        internal double CardMoney {
            get => cardMoney;
            set => cardMoney = value;
        }
        internal string BonusCard { 
            get => bonusCard;
            set => bonusCard = value;
        }
        internal int Bonuses
        {
            get => bonuses;
            set => bonuses = value;
        }
        //
        // Representation
        internal string ID_Representation {
            get => "User example " + id;
        }
        internal string Cash_Representation {
            get => "Cash: " + cash.ToString();
        }
        internal string CardMoney_Representation {
            get => "Card money: " + cardMoney.ToString();
        }
        internal string BonusCard_Representation { 
            get => "Bonus card number: " + bonusCard;
        }
        internal string Bonuses_Representation
        {
            get => "Bonuses: " + bonuses.ToString();
        }
        //
        //

        // Constructors
        internal User(double cash, double cardMoney, string bonusCard, int bonuses)
        {
            amount++;

            this.id = amount - 1;
            this.cash = cash;
            this.cardMoney = cardMoney;
            this.bonusCard = bonusCard;
            this.bonuses = bonuses;
        }
        //
    }
}
