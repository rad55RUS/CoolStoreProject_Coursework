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
        private static UserController? instance;
        // ViewModels
        private static readonly Window_ViewModel Window_ViewModel = new();
        private static UserPage_Selection_ViewModel userPage_Selection_ViewModel = new();
        private static UserPage_Actions_ViewModel userPage_Actions_ViewModel = new();
        private static UserPage_Payment_ViewModel userPage_Payment_ViewModel = new();
        //
        // Views
        private static UserPage_Selection userPage_Selection = new();
        private static UserPage_Actions userPage_Actions = new();
        private static UserPage_Payment userPage_Payment = new();
        private static UserWindow userWindow;
        //
        // Current values
        private static string inputWeight = "0";
        private static Product? currentProduct;
        private static User? currentUser;
        //
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
        private UserController(UserWindow initial)
        {
            // User window initialization
            userWindow = initial;
            userWindow.DataContext = Window_ViewModel;
            userPage_Selection.DataContext = userPage_Selection_ViewModel;
            CurrentPage = userPage_Selection;
            //

            // Kiosk window initialization
            KioskWindow kioskWindow = new();
            //
        }

        /// <summary>
        /// Get single realization of the controller
        /// </summary>
        /// <returns></returns>
        public static UserController GetInstance(UserWindow initial)
        {
            if (instance == null)
            {
                instance = new(initial);
            }
            return instance;
        }
        //

        // Methods
        /// <summary>
        /// Launch kiosk app
        /// </summary>
        public static void Start()
        {
            userPage_Actions.DataContext = userPage_Actions_ViewModel;
            userPage_Payment.DataContext = userPage_Payment_ViewModel;

            KioskController.ResetData();
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

        /// <summary>
        /// Resetting controller data
        /// </summary>
        public static void ResetData()
        {
            userPage_Selection_ViewModel = new();
            userPage_Actions_ViewModel = new();
            userPage_Payment_ViewModel = new();

            userPage_Selection = new();
            userPage_Actions = new();
            userPage_Payment = new();

            userWindow.DataContext = Window_ViewModel;
            userPage_Selection.DataContext = userPage_Selection_ViewModel;
            CurrentPage = userPage_Selection;
        }
        //
    }
}
