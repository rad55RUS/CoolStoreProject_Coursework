using System;
using System.Collections.Generic;
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
        private readonly int? weight;
        private readonly int? volume;
        //

        // Properties
        public int Id 
        {
            get => id;
        }
        public string DisplayingName
        {
            get
            {
                string displayedWeightVolumeString;
                if (weight != null)
                {
                    if (weight >= 1000)
                    {
                        displayedWeightVolumeString = Convert.ToString(Math.Round(Convert.ToDouble(weight / 1000), 2)) + " кг";
                    }
                    else
                    {
                        displayedWeightVolumeString = Convert.ToString(weight) + " г";
                    }
                }
                else
                {
                    if (volume >= 1000)
                    {
                        displayedWeightVolumeString = Convert.ToString(Math.Round(Convert.ToDouble(volume / 1000), 2)) + " л";
                    }
                    else
                    {
                        displayedWeightVolumeString = Convert.ToString(volume) + " мл";
                    }
                }
                return name + ", " + displayedWeightVolumeString + ", " + Convert.ToString(price) + "₽";
            }
        }
        public double Price
        {
            get => price;
        }
        //

        // Constructors
        /// <summary>
        /// Create product object with weight info
        /// </summary>
        /// <param name="name"></param>
        /// <param name="price"></param>
        /// <param name="weight"></param>
        public BasketProduct(string name, double price, int weight)
        {
            this.name = name;
            this.price = price;
            this.weight = weight;
        }

        /// <summary>
        /// Create product object with volume info
        /// </summary>
        /// <param name="name"></param>
        /// <param name="weight"></param>
        /// <param name="price"></param>
        public BasketProduct(string name, int volume, double price)
        {
            amount++;
            this.id = amount;
            this.name = name;
            this.price = price;
            this.volume = volume;
        }
        //
    }
}
