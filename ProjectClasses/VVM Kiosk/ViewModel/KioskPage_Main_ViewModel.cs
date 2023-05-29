using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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
        private double? paymentAmount_Double = 0;
        private double? useBonuses = -1;

        private bool isPaymentProcess = false;
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

        /// <summary>
        /// Represents payment amount
        /// </summary>
        public double? PaymentAmount_Double
        {
            get => paymentAmount_Double;
            set
            {
                paymentAmount_Double = value;
                OnPropertyChanged("PaymentAmount");
            }
        }

        /// <summary>
        /// </summary>
        private bool IsPaymentProcess
        {
            get => isPaymentProcess;
            set
            {
                isPaymentProcess = value;
                OnPropertyChanged("PaymentVisibility");
            }
        }

        // Readonly
        /// <summary>
        /// Represents displaying payment amount
        /// </summary>
        public string? PaymentAmount
        {
            get
            {
                return paymentAmount_Double.ToString() + "₽";
            }
        }

        /// <summary>
        /// Represents displaying payment waiting grid
        /// </summary>
        public Visibility PaymentVisibility
        {
            get
            {
                if (isPaymentProcess)
                {
                    return Visibility.Visible;
                }
                else
                {
                    return Visibility.Collapsed;
                }
            } 
        }
        //
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

                PaymentAmount_Double += weighableProductPrice;

                weighableProductPrice = null;
            }
        }

        /// <summary>
        /// Show info about scanned product and add it to the basket if isn't weighable
        /// </summary>
        /// <param name="scannedProduct"></param>
        public void ScanProduct(Product scannedProduct)
        {
            if (!IsPaymentProcess)
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

                        PaymentAmount_Double += scannedProduct.Price;
                    }
                }
            }
        }

        /// <summary>
        /// Pay by cash method
        /// </summary>
        /// <param name="cash"></param>
        public void CashPayment(double cash)
        {
            if (cash > 0)
            {

            }
            else
            {

            }
        }

        /// <summary>
        /// Pay by card method
        /// </summary>
        /// <param name="cash"></param>
        public void CardPayment(double cardMoney)
        {
            if (cardMoney > 0)
            {

            }
            else
            {

            }
        }

        /// <summary>
        /// Pay by bonuses method
        /// </summary>
        /// <param name="cash"></param>
        public void BonusesPayment(int bonuses)
        {
            if (bonuses > 0)
            {

            }
            else
            {

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

                            PaymentAmount_Double -= product.Price;
                        }
                        if (BasketContent.Count == 0)
                        {
                            if (weighableProductPrice == null)
                            {
                                KioskController.CurrentPage = KioskController.KioskPage_ScanWaiting;
                            }
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
                          IsPaymentProcess = true;
                      }
                  }));
            }
        }

        private RelayCommand? returnCommand;

        /// <summary>
        /// Start working with kiosk
        /// </summary>
        public RelayCommand? ReturnCommand
        {
            get
            {
                return returnCommand ??
                  (returnCommand = new RelayCommand(obj =>
                  {
                      IsPaymentProcess = false;
                      useBonuses = 0;
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
