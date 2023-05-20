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
    /// Represents app controller
    /// </summary>
    internal class KioskController
    {
        // Fields
        public static readonly Window_ViewModel Window_ViewModel = new();
        public static readonly KioskScanWaiting_Page_ViewModel kioskScanWaiting_Page_ViewModel = new();
        private static readonly KioskPage_ScanWaiting kioskScanWaiting_Page = new();
        private readonly KioskWindow kioskWindow;
        //

        // Properties
        public static KioskPage_ScanWaiting KioskPage_ScanWaiting
        {
            get => kioskScanWaiting_Page;
        }
        public static Page ?CurrentPage
        {
            get => Window_ViewModel.CurrentPage;
            set
            {
                Window_ViewModel.CurrentPage = value;
            }
        }
        //

        public KioskController(KioskWindow initial)
        {
            kioskWindow = initial;
            kioskWindow.DataContext = Window_ViewModel;
            kioskScanWaiting_Page.DataContext = kioskScanWaiting_Page_ViewModel;
            CurrentPage = kioskScanWaiting_Page;
        }
    }
}
