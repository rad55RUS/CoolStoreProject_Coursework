using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CoolStoreProject.KioskVVM
{
    internal class KioskPage_Main_ViewModel : INotifyPropertyChanged
    {
        // Fields
        private string? currentPicture;
        private string? productName;
        private string? productWeight;
        private string? productPrice;
        private string? clientExtraInfo;
        private double? weighableProductPrice;
        //

        // Properties
        /// <summary>
        /// Represents collection displaying in basket list box
        /// </summary>
        public ObservableCollection<BasketProduct>? BasketContent
        { 
            get => KioskController.BasketContent;
            set => KioskController.BasketContent = value;
        }

        /// <summary>
        /// Represents displaying product picture path
        /// </summary>
        public string? CurrentPicture
        {
            get => currentPicture;
            set
            {
                currentPicture = value;
                OnPropertyChanged("CurrentPicture");
            }
        }

        /// <summary>
        /// Represents displaying product name
        /// </summary>
        public string? ProductName
        {
            get => productName;
            set
            {
                productName = value;
                OnPropertyChanged("ProductName");
            }
        }

        /// <summary>
        /// Represents displaying product weight
        /// </summary>
        public string? ProductWeight
        {
            get => productWeight;
            set
            {
                productWeight = value;
                OnPropertyChanged("ProductWeight");
            }
        }

        /// <summary>
        /// Represents displaying product price
        /// </summary>
        public string? ProductPrice
        {
            get => productPrice;
            set
            {
                productPrice = value;
                OnPropertyChanged("ProductPrice");
            }
        }

        /// <summary>
        /// Represents displaying extra info to client for next possible actions
        /// </summary>
        public string? ClientExtraInfo
        {
            get => clientExtraInfo;
            set
            {
                clientExtraInfo = value;
                OnPropertyChanged("ClientExtraInfo");
            }
        }
        //

        // Constructors
        public KioskPage_Main_ViewModel()
        {
            BasketContent = new ObservableCollection<BasketProduct>();
        }
        //

        // Methods
        /// <summary>
        /// Show weighing result info and add it to the basket
        /// </summary>
        /// <param name="weight"></param>
        public void WeighProduct(int weight)
        {
            string displayedWeightString;
            if (weighableProductPrice != null)
            {
                if (weight >= 1000)
                {
                    displayedWeightString = Convert.ToString(Math.Round(Convert.ToDouble(weight / 1000), 2)) + " кг";
                }
                else
                {
                    displayedWeightString = Convert.ToString(weight) + " г";
                }

                weighableProductPrice = Math.Round(Convert.ToDouble(weighableProductPrice / 1000 * Convert.ToDouble(weight)), 2);
                ProductPrice = Convert.ToString(weighableProductPrice) + "₽";
                ProductWeight = displayedWeightString;

                ClientExtraInfo = "Продукт был добавлен в корзину. Вы можете отсканировать следующий продукт, убрать лишние продукты из корзины или перейти к оплате.";
                BasketContent.Add(new BasketProduct(productName, Convert.ToDouble(weighableProductPrice)));

                weighableProductPrice = null;
            }
        }

        /// <summary>
        /// Show info about scanned product and add it to the basket if isn't weighable
        /// </summary>
        /// <param name="scannedProduct"></param>
        public void ScanProduct(Product scannedProduct)
        {
            if (weighableProductPrice == null)
            {
                string displayedWeightVolumeString;
                if (scannedProduct.Weight > 0)
                {
                    if (scannedProduct.Weight >= 1000)
                    {
                        displayedWeightVolumeString = Convert.ToString(Math.Round(Convert.ToDouble(scannedProduct.Weight / 1000), 2)) + " кг";
                    }
                    else
                    {
                        displayedWeightVolumeString = Convert.ToString(scannedProduct.Weight) + " г";
                    }
                }
                else
                {
                    if (scannedProduct.Volume >= 1000)
                    {
                        displayedWeightVolumeString = Convert.ToString(Math.Round(Convert.ToDouble(scannedProduct.Volume / 1000), 2)) + " л";
                    }
                    else
                    {
                        displayedWeightVolumeString = Convert.ToString(scannedProduct.Volume) + " мл";
                    }
                }

                CurrentPicture = scannedProduct.ImagePath;
                ProductName = scannedProduct.Name;
                ProductWeight = displayedWeightVolumeString;
                ProductPrice = Convert.ToString(Math.Round(scannedProduct.Price, 2)) + "₽";
                if (scannedProduct.IsWeighable)
                {
                    ClientExtraInfo = "Необходимо взвесить продукт перед добавлением в корзину!";
                    weighableProductPrice = scannedProduct.Price;
                }
                else
                {
                    ClientExtraInfo = "Продукт был добавлен в корзину. Вы можете отсканировать следующий продукт, убрать лишние продукты из корзины или перейти к оплате.";
                    weighableProductPrice = null;
                    BasketContent.Add(new BasketProduct(scannedProduct.Name, scannedProduct.Price));
                }
            }
        }
        //

        // Commands
        private RelayCommand? removeCommand;

        /// <summary>
        /// Imitate product scanning and send it's info to the kiosk via control
        /// </summary>
        public RelayCommand? RemoveCommand
        {
            get
            {
                return removeCommand ??
                    (removeCommand = new RelayCommand(obj =>
                    {
                        BasketProduct product = obj as BasketProduct;
                        if (product != null)
                        {
                            BasketContent.Remove(product);
                        }
                        if (BasketContent.Count == 0)
                        {
                            KioskController.CurrentPage = KioskController.KioskPage_ScanWaiting;
                        }
                    },
                    (obj) => BasketContent.Count > 0));
            }
        }

        private RelayCommand? paymentCommand;

        /// <summary>
        /// Start working with kiosk
        /// </summary>
        public RelayCommand? PaymentCommand
        {
            get
            {
                return paymentCommand ??
                  (paymentCommand = new RelayCommand(obj =>
                  {
                      if (weighableProductPrice == null)
                      {
                          KioskController.CurrentPage = KioskController.KioskPage_Payment;
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
