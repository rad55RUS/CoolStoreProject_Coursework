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
        private string warningText;
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
        public string? WarningText
        {
            get => warningText;
            set
            {
                warningText = value;
                OnPropertyChanged("WarningText");
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
                                imagePath = Environment.CurrentDirectory + @product_Node.InnerText;
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

        // Commands
        private RelayCommand? scanCommand;

        /// <summary>
        /// Start working with kiosk
        /// </summary>
        public RelayCommand? ScanCommand
        {
            get
            {
                return scanCommand ??
                  (scanCommand = new RelayCommand(obj =>
                  {
                      if (currentProduct != null)
                      {
                          UserController.ScanProduct();
                          currentProduct = null;
                          WarningText = "";
                      }
                      else
                      {
                          WarningText = "*You haven't selected a product!";
                      }
                  }));
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
