using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace CoolStoreProject
{
    /// <summary>
    /// Represents product data
    /// </summary>
    public class Product
    {
        // Fields
        private string name;
        private string imagePath;
        private double price;
        private bool isWeighable;
        private int weight;
        private int volume;
        //

        // Properties
        // Data
        public string Name
        {
            get => name;
            set => name = value;
        }
        public string ImagePath
        {
            get => imagePath;
            set => imagePath = value;
        }
        public double Price
        {
            get => price;
            set => price = value;
        }
        public bool IsWeighable
        {
            get => isWeighable;
            set => isWeighable = value;
        }
        public int Weight
        {
            get => weight;
            set => weight = value;
        }
        public int Volume
        {
            get => volume;
            set => volume = value;
        }
        //
        // Representation
        public string Name_Representation
        {
            get => name;
        }
        public string Price_Representation
        {
            get => "Price: " + price.ToString() + " RUB";
        }
        public string VolumeWeight_Representation
        {
            get
            {
                if (volume != 0)
                {
                    return "Volume: " + volume.ToString() + " milliliters";
                }
                else
                {
                    return "Weight: " + weight.ToString() + " grams";
                }
            }
        }
        //
        //

        // Constructors
        public Product(string name, string imagePath, double price, bool isWeighable, int weight)
        {
            this.name = name;
            this.imagePath = imagePath;
            this.price = price;
            this.isWeighable = isWeighable;
            this.weight = weight;
        }

        public Product(string name, string imagePath, double price, int volume)
        {
            this.name = name;
            this.imagePath = imagePath;
            this.price = price;
            isWeighable = false;
            this.volume = volume;
        }
        //
    }
}
