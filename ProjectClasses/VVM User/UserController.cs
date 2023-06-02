using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

using CoolStoreProject.KioskVVM;

namespace CoolStoreProject.UserVVM
{
    /// <summary>
    /// Represents user controller
    /// </summary>
    internal class UserController
    {
        // Fields
        // Static
        // ViewModels
        private static readonly Window_ViewModel Window_ViewModel = new();
        private static readonly UserPage_Selection_ViewModel userPage_Selection_ViewModel = new();
        private static readonly UserPage_Actions_ViewModel userPage_Actions_ViewModel = new();
        private static readonly UserPage_Payment_ViewModel userPage_Payment_ViewModel = new();
        //
        // Pages
        private static readonly UserPage_Selection userPage_Selection = new();
        private static readonly UserPage_Actions userPage_Actions = new();
        private static readonly UserPage_Payment userPage_Payment = new();
        //
        // Current values
        private static string inputWeight = "0";
        private static Product? currentProduct;
        private static User? currentUser;
        //
        private static KioskController ?kioskController;
        private readonly UserWindow userWindow;
        //

        // Properties
        // Pages
        public static UserPage_Selection UserPage_Selection
        {
            get => userPage_Selection;
        }
        public static UserPage_Actions UserPage_Actions
        {
            get => userPage_Actions;
        }
        public static UserPage_Payment UserPage_Payment
        {
            get => userPage_Payment;
        }
        public static Page? CurrentPage
        {
            get => Window_ViewModel.CurrentPage;
            set
            {
                Window_ViewModel.CurrentPage = value;
            }
        }
        //
        // Current values
        public static string InputWeight
        {
            get => inputWeight;
            set
            {
                inputWeight = value;
            }
        }
        public static Product? CurrentProduct
        {
            get => currentProduct;
            set
            {
                currentProduct = value;
            }
        }
        public static User ?CurrentUser
        {
            get => currentUser;
            set
            {
                currentUser = value;
            }
        }
        //
        //

        // Constructors
        public UserController(UserWindow initial)
        {
            userWindow = initial;
            userWindow.DataContext = Window_ViewModel;
            userPage_Selection.DataContext = userPage_Selection_ViewModel;
            CurrentPage = userPage_Selection;
        }
        //

        // Methods
        /// <summary>
        /// Launch kiosk app
        /// </summary>
        public static void LaunchKiosk()
        {
            kioskController = new KioskController(new KioskWindow());

            userPage_Actions.DataContext = userPage_Actions_ViewModel;
            userPage_Payment.DataContext = userPage_Payment_ViewModel;
        }

        /// <summary>
        /// Send weighing result to kiosk
        /// </summary>
        public static void SendWeighProduct()
        {
            KioskController.GetWeighProduct(Convert.ToInt32(InputWeight));
        }

        /// <summary>
        /// Send scanned product info to kiosk
        /// </summary>
        public static void SendScanProduct()
        {
            KioskController.GetScanProduct(CurrentProduct);
        }

        /// <summary>
        /// Send cash data to kiosk
        /// </summary>
        public static void SendCashPayment()
        {
            KioskController.GetCashPayment(CurrentUser.Cash);
        }

        /// <summary>
        /// Send card data to kiosk
        /// </summary>
        public static void SendCardPayment()
        {
            KioskController.GetCardPayment(CurrentUser.CardMoney);
        }

        /// <summary>
        /// Send bonuses data to kiosk
        /// </summary>
        public static void SendBonusesPayment()
        {
            KioskController.GetBonusesPayment(CurrentUser.Bonuses);
        }
        //
    }
}
