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
        private string? currentBasketProduct;
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
        public ObservableCollection<string>? BasketContent { get; set; }

        /// <summary>
        /// Represents current selected product from basket list box
        /// </summary>
        public string? CurrentBasketProduct
        {
            get => currentBasketProduct;
            set
            {
                currentBasketProduct = value;
                OnPropertyChanged("CurrentBasketProduct");
            }
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
            BasketContent = new ObservableCollection<string>();
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

                ProductPrice = Convert.ToString(Math.Round(Convert.ToDouble(weighableProductPrice / 1000 * Convert.ToDouble(weight)), 2)) + "₽";
                ProductWeight = displayedWeightString;
                ClientExtraInfo = "Продукт был добавлен в корзину. Вы можете отсканировать следующий продукт, убрать лишние продукты из корзины или перейти к оплате.";

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
                    BasketContent.Add(ProductName);
                }
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
