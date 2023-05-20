using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace CoolStoreProject.User
{
    /// <summary>
    /// Represents app controller
    /// </summary>
    internal class UserController
    {
        // Fields
        public static readonly Window_ViewModel Window_ViewModel = new();
        public static readonly UserPage_Selection_ViewModel userPage_Selection_ViewModel = new();
        public static readonly UserPage_Actions_ViewModel userPage_Actions_ViewModel = new();
        private static readonly UserPage_Selection userPage_Selection = new();
        private static readonly UserPage_Actions userPage_Actions = new();
        private readonly UserWindow userWindow;
        //

        // Properties
        public static UserPage_Selection UserPage_Selection
        {
            get => userPage_Selection;
        }
        public static UserPage_Actions UserPage_Actions
        {
            get => userPage_Actions;
        }
        public static User ?CurrentUser
        {
            get => userPage_Selection_ViewModel.CurrentUser;
            set
            {
                userPage_Selection_ViewModel.CurrentUser = value;
            }
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

        public UserController(UserWindow initial)
        {
            userWindow = initial;
            userWindow.DataContext = Window_ViewModel;
            userPage_Selection.DataContext = userPage_Selection_ViewModel;
            userPage_Actions.DataContext = userPage_Actions_ViewModel;
            CurrentPage = userPage_Selection;
        }
    }
}
