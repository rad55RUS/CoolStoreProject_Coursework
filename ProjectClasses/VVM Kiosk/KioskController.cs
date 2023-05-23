using System;
using System.Collections.Generic;
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
        public static readonly Window_ViewModel window_ViewModel = new();
        public static readonly KioskPage_Main_ViewModel kioskPage_Main_ViewModel = new();
        private static readonly KioskPage_ScanWaiting kioskScanWaiting_Page = new();
        private static readonly KioskPage_Main kioskPage_Main = new();
        private readonly KioskWindow kioskWindow;
        //

        // Properties
        public static KioskPage_ScanWaiting KioskPage_ScanWaiting
        {
            get => kioskScanWaiting_Page;
        }
        public static Page ?CurrentPage
        {
            get => window_ViewModel.CurrentPage;
            set
            {
                window_ViewModel.CurrentPage = value;
            }
        }
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
        public static void ScanProduct(Product scannedProduct)
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
            window_ViewModel.CurrentPage = kioskPage_Main;

            kioskPage_Main_ViewModel.CurrentPicture = scannedProduct.ImagePath;
            kioskPage_Main_ViewModel.ProductName = scannedProduct.Name;
            kioskPage_Main_ViewModel.ProductWeight = displayedWeightVolumeString;
            kioskPage_Main_ViewModel.ProductPrice = Convert.ToString(Math.Round(scannedProduct.Price,2)) + "₽";
        }
        //
    }
}
