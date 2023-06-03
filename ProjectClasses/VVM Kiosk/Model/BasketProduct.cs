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
        private readonly string name;
        private readonly double price;
        //

        // Properties
        public string DisplayingName
        {
            get
            {
                return name + ", " + Convert.ToString(price) + "₽";
            }
        }
        public double Price
        {
            get => price;
        }
        //

        // Constructors
        public BasketProduct(string name, double price)
        {
            this.name = name;
            this.price = price;
        }
        //
    }
}