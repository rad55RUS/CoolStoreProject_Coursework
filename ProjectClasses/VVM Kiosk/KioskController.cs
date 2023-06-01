using CoolStoreProject.UserVVM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace CoolStoreProject.KioskVVM
{
    /// <summary>
    /// Represents kiosk controller
    /// </summary>
    internal class KioskController
    {
        // Fields
        private static readonly Window_ViewModel window_ViewModel = new();
        private static KioskPage_Main_ViewModel kioskPage_Main_ViewModel = new();
        private static KioskPage_ScanWaiting kioskScanWaiting_Page = new();
        private static KioskPage_Main kioskPage_Main = new();
        private static KioskWindow? kioskWindow;
        //

        // Properties
        // Pages
        public static KioskPage_ScanWaiting KioskPage_ScanWaiting
        {
            get => kioskScanWaiting_Page;
        }
        public static KioskPage_Main KioskPage_Main
        {
            get => kioskPage_Main;
        }
        //
        // Current values
        public static ObservableCollection<BasketProduct>? BasketContent { get; set; }
        public static Page ?CurrentPage
        {
            get => window_ViewModel.CurrentPage;
            set
            {
                window_ViewModel.CurrentPage = value;
            }
        }
        //
        //

        // Constructors
        public KioskController(KioskWindow initial)
        {
            kioskWindow = initial;
            kioskWindow.DataContext = window_ViewModel;
            kioskPage_Main.DataContext = kioskPage_Main_ViewModel;
            CurrentPage = kioskScanWaiting_Page;

            kioskWindow.Show();
        }
        //

        // Methods
        /// <summary>
        /// Call CashPayment method from main page view model
        /// </summary>
        /// <param name="scannedProduct"></param>
        public static void GetCashPayment(double cash)
        {
            kioskPage_Main_ViewModel.CashPayment(cash);
        }

        /// <summary>
        /// Call CashPayment method from main page view model
        /// </summary>
        /// <param name="scannedProduct"></param>
        public static void GetCardPayment(double cardMoney)
        {
            kioskPage_Main_ViewModel.CardPayment(cardMoney);
        }

        /// <summary>
        /// Call CashPayment method from main page view model
        /// </summary>
        /// <param name="scannedProduct"></param>
        public static void GetBonusesPayment(int bonuses)
        {
            kioskPage_Main_ViewModel.BonusesPayment(bonuses);
        }

        /// <summary>
        /// Call WeighProduct method from main page view model
        /// </summary>
        /// <param name="scannedProduct"></param>
        public static void GetWeighProduct(int weight)
        {
            kioskPage_Main_ViewModel.WeighProduct(weight);
        }

        /// <summary>
        /// Open main kiosk page if not opened and call ScanProduct method from main page view model
        /// </summary>
        /// <param name="scannedProduct"></param>
        public static void GetScanProduct(Product scannedProduct)
        {
            window_ViewModel.CurrentPage = kioskPage_Main;
            kioskPage_Main_ViewModel.ScanProduct(scannedProduct);
        }

        /// <summary>
        /// Resetting kiosk data
        /// </summary>
        public static void ResetData()
        {
            kioskWindow.Hide();
            kioskPage_Main_ViewModel = new();
            kioskScanWaiting_Page = new();
            kioskPage_Main = new();
            BasketContent = new();

            kioskPage_Main.DataContext = kioskPage_Main_ViewModel;
            CurrentPage = kioskScanWaiting_Page;

            kioskWindow.Show();
        }
        //
    }
}
