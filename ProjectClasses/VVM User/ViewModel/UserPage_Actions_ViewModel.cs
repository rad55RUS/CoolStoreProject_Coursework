using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace CoolStoreProject.UserVVM
{
    internal class UserPage_Actions_ViewModel : INotifyPropertyChanged
    {
        // Fields
        private Product? currentProduct;
        //

        // Properties
        public ObservableCollection<Product>? Products { get; set; }
        public Product? CurrentProduct
        {
            get => currentProduct;
            set
            {
                currentProduct = value;
                OnPropertyChanged("CurrentProduct");
            }
        }
        //

        // Constructors
        public UserPage_Actions_ViewModel()
        {
            LoadProductData(Environment.CurrentDirectory + @"\Data\Products.xml");
        }
        //

        // Methods
        /// <summary>
        /// Load product data from Products.xml
        /// </summary>
        /// <param name="path"></param>
        /// <exception cref="Exception"></exception>
        public void LoadProductData(string path)
        {
            try
            {
                Products = new ObservableCollection<Product>();
                XmlDocument products_XML = new();
                products_XML.Load(path);
                XmlElement? products_Root = products_XML.DocumentElement;
                if (products_XML != null)
                {
                    foreach (XmlElement products_Node in products_Root)
                    {
                        string name = "";
                        string imagePath = "";
                        double price = 0;
                        bool isWeighable = false;
                        int weight = 0;
                        int volume = 0;


                        foreach (XmlNode product_Node in products_Node.ChildNodes)
                        {
                            if (product_Node.Name == "Name")
                            {
                                name = product_Node.InnerText;
                            }
                            if (product_Node.Name == "ImagePath")
                            {
                                imagePath = product_Node.InnerText;
                            }
                            if (product_Node.Name == "Price")
                            {
                                string tempString = "";
                                for (int i = 0; i < product_Node.InnerText.Length; i++)
                                {
                                    if (product_Node.InnerText[i] == '.')
                                    {
                                        tempString += ",";
                                    }
                                    else
                                    {
                                        tempString += product_Node.InnerText[i];
                                    }
                                }
                                price = Convert.ToDouble(tempString);
                            }
                            if (product_Node.Name == "IsWeighable")
                            {
                                isWeighable = Convert.ToBoolean(product_Node.InnerText);
                            }
                            if (product_Node.Name == "Weight")
                            {
                                weight = Convert.ToInt32(product_Node.InnerText);
                            }
                            if (product_Node.Name == "Volume")
                            {
                                volume = Convert.ToInt32(product_Node.InnerText);
                            }
                        }

                        if (weight > 0)
                        {
                            Products.Add(new(name, imagePath, price, isWeighable, weight));
                        }
                        else
                        {
                            Products.Add(new(name, imagePath, price, volume));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }
        //

        // INotifyPropertyChanged realization
        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
        //
    }
}
