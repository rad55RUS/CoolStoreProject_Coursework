using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolStoreProject
{
    /// <summary>
    /// Represents bonus card data
    /// </summary>
    public class BonusCard
    {
        // Fields
        private string number;
        private int bonuses;
        //

        // Properties
        public string Number { 
            get => number; 
            set => number = value;
        }
        public int Bonuses { 
            get => bonuses;
            set => bonuses = value; 
        }
        //

        // Constructors
        BonusCard(string number, int bonuses)
        {
            this.number = number;
            this.bonuses = bonuses;
        }
        //
    }
}
