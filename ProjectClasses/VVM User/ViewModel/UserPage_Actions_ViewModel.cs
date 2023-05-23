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
        private string? warningText;
        private string inputWeight = "0";
        //

        // Properties
        /// <summary>
        /// Represents collection displaying in products list box
        /// </summary>
        public ObservableCollection<Product>? Products { get; set; }

        /// <summary>
        /// Represents current selected product in products list box
        /// </summary>
        public Product? CurrentProduct
        {
            get => currentProduct;
            set
            {
                currentProduct = value;
                OnPropertyChanged("CurrentProduct");
            }
        }

        /// <summary>
        /// Represents displaying warning text
        /// </summary>
        public string? WarningText
        {
            get => warningText;
            set
            {
                warningText = value;
                OnPropertyChanged("WarningText");
            }
        }

        /// <summary>
        /// Represents weight inputting by user
        /// </summary>
        public string InputWeight
        {
            get => inputWeight;
            set
            {
                inputWeight = value;
                OnPropertyChanged("InputWeight");
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
        private RelayCommand? weighCommand;

        /// <summary>
        /// Imitate product weighing and send it's result to the kiosk via control
        /// </summary>
        public RelayCommand? WeighCommand
        {
            get
            {
                return weighCommand ??
                  (weighCommand = new RelayCommand(obj =>
                  {
                      if (InputWeight != "0" || InputWeight != "")
                      {
                          UserController.WeighProduct();
                          WarningText = "";
                      }
                      else
                      {
                          WarningText = "*You've inputted incorrect weight!";
                      }
                  }));
            }
        }

        private RelayCommand? scanCommand;

        /// <summary>
        /// Imitate product scanning and send it's info to the kiosk via control
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
