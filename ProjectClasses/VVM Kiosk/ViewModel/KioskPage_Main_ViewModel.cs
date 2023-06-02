using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;
using System.Windows;

namespace CoolStoreProject.KioskVVM
{
    internal class KioskPage_Main_ViewModel : INotifyPropertyChanged
    {
        // Fields
        // Strings
        private string? currentPicture;
        private string? productName;
        private string? productWeight;
        private string? productPrice;
        private string? clientExtraInfo;
        private string? waitingText;
        //
        // Doubles
        private double weighableProductPrice = 0;
        private double paymentAmount_Double = 0;
        // Integers
        private int useBonuses = 0;
        //
        // Booleans
        private bool isPaymentFinished = false;
        private bool isPaymentWaiting = false;
        private bool isBonusesPayment = false;
        //
        // Threads
        private Thread? thread1;
        //
        //

        // Properties
        /// <summary>
        /// Represents collection displaying in basket list box
        /// </summary>
        internal ObservableCollection<BasketProduct>? BasketContent { get; set; }

        /// <summary>
        /// Represents displaying product picture path
        /// </summary>
        internal string? CurrentPicture
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
        internal string? ProductName
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
        internal string? ProductWeight
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
        internal string? ProductPrice
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
        internal string? ClientExtraInfo
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
        internal double PaymentAmount_Double
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
        private bool IsPaymentWaiting
        {
            get => isPaymentWaiting;
            set
            {
                isPaymentWaiting = value;
                OnPropertyChanged("PaymentVisibility");
            }
        }

        /// <summary>
        /// </summary>
        private bool IsPaymentFinished
        {
            get => isPaymentFinished;
            set
            {
                isPaymentFinished = value;
                OnPropertyChanged("PaymentButtonVisibility");
            }
        }

        /// <summary>
        /// </summary>
        private bool IsBonusesPayment
        {
            get => isBonusesPayment;
            set
            {
                isBonusesPayment = value;
                OnPropertyChanged("BonusesPaymentVisibility");
                OnPropertyChanged("PaymentButtonVisibility");
            }
        }

        // Readonly
        /// <summary>
        /// Represents displaying payment amount
        /// </summary>
        internal string? PaymentAmount
        {
            get
            {
                return paymentAmount_Double.ToString() + "₽";
            }
        }

        /// <summary>
        /// Represents displaying payment waiting grid
        /// </summary>
        internal Visibility PaymentVisibility
        {
            get
            {
                if (isPaymentWaiting)
                {
                    return Visibility.Visible;
                }
                else
                {
                    return Visibility.Collapsed;
                }
            } 
        }

        /// <summary>
        /// Represents displaying bonus payment buttons visibility
        /// </summary>
        internal Visibility BonusesPaymentVisibility
        {
            get
            {
                if (isBonusesPayment)
                {
                    return Visibility.Visible;
                }
                else
                {
                    return Visibility.Collapsed;
                }
            }
        }

        /// <summary>
        /// Represents displaying payment and return buttons visibility
        /// </summary>
        internal Visibility PaymentButtonVisibility
        {
            get
            {
                if (!isBonusesPayment && !isPaymentFinished)
                {
                    return Visibility.Visible;
                }
                else
                {
                    return Visibility.Collapsed;
                }
            }
        }

        /// <summary>
        /// Represents displaying text on visibility grid
        /// </summary>
        internal string? WaitingText
        {
            get => waitingText;
            set
            {
                waitingText = value;
                OnPropertyChanged("WaitingText");
            }
        }
        //
        //

        // Constructors
        internal KioskPage_Main_ViewModel()
        {
            BasketContent = new ObservableCollection<BasketProduct>();
        }
        //

        // Methods
        /// <summary>
        /// Show weighing result info and add it to the basket
        /// </summary>
        /// <param name="weight"></param>
        internal void WeighProduct(int weight)
        {
            string displayedWeightString;
            if (weighableProductPrice != 0)
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

                weighableProductPrice = 0;
            }
        }

        /// <summary>
        /// Show info about scanned product and add it to the basket if isn't weighable
        /// </summary>
        /// <param name="scannedProduct"></param>
        internal void ScanProduct(Product scannedProduct)
        {
            if (!IsPaymentWaiting)
            {
                if (weighableProductPrice == 0)
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
                        weighableProductPrice = 0;
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
        internal void CashPayment(double cash)
        {
            if (IsPaymentWaiting && !IsPaymentFinished && !IsBonusesPayment)
            {
                if (cash > 0)
                {
                    if (cash > paymentAmount_Double - Convert.ToDouble(useBonuses))
                    {
                        IsPaymentFinished = true;
                        WaitingText = "Оплата пройдена. Не забудьте забрать сдачу.";
                        thread1 = new Thread(ResetKioskData);
                        thread1.Start();
                    }
                    else
                    {
                        WaitingText = "Внесено недостаточно купюр. Доложите купюры или попробуйте другой способ оплаты.";
                    }
                }
            }
        }

        /// <summary>
        /// Pay by card method
        /// </summary>
        /// <param name="cash"></param>
        internal void CardPayment(double cardMoney)
        {
            if (IsPaymentWaiting && !IsPaymentFinished && !IsBonusesPayment)
            {
                if (cardMoney >= paymentAmount_Double - Convert.ToDouble(useBonuses))
                {
                    IsPaymentFinished = true;
                    WaitingText = "Оплата пройдена. Возврат к начальному окну через несколько секунд.";
                    thread1 = new Thread(ResetKioskData);
                    thread1.Start();
                }
                else
                {
                    WaitingText = "Недостаточно средств на карте. Попробуйте другой способ оплаты.";
                }
            }
        }

        /// <summary>
        /// Pay by bonuses method
        /// </summary>
        /// <param name="cash"></param>
        internal void BonusesPayment(int bonuses)
        {
            if (IsPaymentWaiting && !IsPaymentFinished && !IsBonusesPayment)
            {
                if (bonuses > 0)
                {
                    if (bonuses >= paymentAmount_Double / Convert.ToDouble(100) * Convert.ToDouble(3))
                    {
                        useBonuses = Convert.ToInt32(Math.Round(paymentAmount_Double / Convert.ToDouble(100) * Convert.ToDouble(3)));
                        if (useBonuses > paymentAmount_Double / Convert.ToDouble(100) * Convert.ToDouble(3))
                        {
                            useBonuses -= 1;
                        }
                        if (useBonuses < 0)
                        {
                            useBonuses = 0;
                        }
                    }
                    else
                    {
                        useBonuses = bonuses;
                    }
                    IsBonusesPayment = true;
                    WaitingText = "У вас " + Convert.ToString(bonuses) + " бонусов. Хотите ли вы списать бонусы, чтобы оплатить до 30% суммы товаров?";
                }
                else
                {
                    WaitingText = "У вас " + Convert.ToString(bonuses) + " бонусов. Совершите покупку, чтобы получить 3% бонусов от стоимости товаров.";
                }
            }
        }

        internal void ResetKioskData()
        {
            Thread.Sleep(5000);
            App.Current.Dispatcher.Invoke(delegate
            {
                KioskController.ResetData();
            });
        }
        //

        // Commands
        private RelayCommand? removeCommand;

        /// <summary>
        /// Imitate product scanning and send it's info to the kiosk via control
        /// </summary>
        internal RelayCommand? RemoveCommand
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
                            if (weighableProductPrice == 0)
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
        internal RelayCommand? PaymentCommand
        {
            get
            {
                return paymentCommand ??
                  (paymentCommand = new RelayCommand(obj =>
                  {
                      if (weighableProductPrice == 0)
                      {
                          IsPaymentWaiting = true;
                          WaitingText = "Ожидание оплаты товаров. Не забудьте сперва приложить бонусную карту.";
                      }
                  }));
            }
        }

        private RelayCommand? returnCommand;

        /// <summary>
        /// Start working with kiosk
        /// </summary>
        internal RelayCommand? ReturnCommand
        {
            get
            {
                return returnCommand ??
                  (returnCommand = new RelayCommand(obj =>
                  {
                      IsPaymentWaiting = false;
                      IsPaymentFinished = false;
                      IsBonusesPayment = false;
                      PaymentAmount_Double += useBonuses;
                      useBonuses = 0;
                  }));
            }
        }

        private RelayCommand? bonusesPayment_Yes;

        /// <summary>
        /// Use bonuses on payment
        /// </summary>
        internal RelayCommand? BonusesPayment_Yes
        {
            get
            {
                return bonusesPayment_Yes ??
                  (bonusesPayment_Yes = new RelayCommand(obj =>
                  {
                      IsBonusesPayment = false;
                      WaitingText = "Ожидание оплаты товаров.";
                      PaymentAmount_Double -= useBonuses;
                  }));
            }
        }

        private RelayCommand? bonusesPayment_No;

        /// <summary>
        /// Don't use bonuses on payment
        /// </summary>
        internal RelayCommand? BonusesPayment_No
        {
            get
            {
                return bonusesPayment_No ??
                  (bonusesPayment_No = new RelayCommand(obj =>
                  {
                      IsBonusesPayment = false;
                      useBonuses = 0;
                      WaitingText = "Ожидание оплаты товаров.";
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
