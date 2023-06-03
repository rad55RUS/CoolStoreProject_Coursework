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
        public int Bonuses
        {
            get => bonuses;
            set => bonuses = value;
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
        public string Bonuses_Representation
        {
            get => "Bonuses: " + bonuses.ToString();
        }
        //
        //

        // Constructors
        public User(int id, double cash, double cardMoney, string bonusCard, int bonuses)
        {
            this.id = id;
            this.cash = cash;
            this.cardMoney = cardMoney;
            this.bonusCard = bonusCard;
            this.bonuses = bonuses;
        }
        //
    }
}
