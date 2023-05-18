using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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
        // Static
        private static int amount = -1;
        //
        private int id;
        private string name;
        private BitmapImage image;
        private double price;
        private bool isWeighable;
        private int weight;
        private int volume;
        //

        // Properties
        public int Id {
            get => id;
            set => id = value;
        }
        public string Name { 
            get => name;
            set => name = value; 
        }
        public BitmapImage Image {
            get => image; 
            set => image = value; 
        }
        public double Price { 
            get => price; 
            set => price = value;
        }
        public bool IsWeighable {
            get => isWeighable; 
            set => isWeighable = value; }
        public int Weight {
            get => weight; 
            set => weight = value; 
        }
        public int Volume {
            get => volume; 
            set => volume = value;
        }
        //

        // Constructors
        public Product(string name, string imagePath, double price, bool isWeighable, int weight)
        {
            amount++;

            this.id = amount;
            this.name = name;
            this.image = new BitmapImage(new(imagePath));
            this.price = price;
            this.isWeighable = isWeighable;
            this.weight = weight;
        }

        public Product(string name, string imagePath, double price, int volume)
        {
            amount++;

            this.id = amount;
            this.name = name;
            this.image = new BitmapImage(new(imagePath));
            this.price = price;
            this.isWeighable = false;
            this.volume = volume;
        }
        //
    }
}
