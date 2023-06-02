using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolStoreProject.KioskVVM
{
    internal class BasketProduct
    {
        // Fields
        // Static
        private static int amount = -1;
        //
        private readonly int id;
        private readonly string name;
        private readonly double price;
        //

        // Properties
        internal int Id 
        {
            get => id;
        }
        internal string DisplayingName
        {
            get
            {
                return name + ", " + Convert.ToString(price) + "₽";
            }
        }
        internal double Price
        {
            get => price;
        }
        //

        // Constructors
        internal BasketProduct(string name, double price)
        {
            amount++;
            this.id = amount;
            this.name = name;
            this.price = price;
        }
        //
    }
}
