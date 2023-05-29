using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CoolStoreProject.UserVVM
{
    internal class UserPage_Payment_ViewModel : INotifyPropertyChanged
    {
        // Properties
        /// <summary>
        /// Represents current user ID
        /// </summary>
        public string? CurrentUser_ID
        {
            get => UserController.CurrentUser.ID_Representation;
        }

        /// <summary>
        /// Represents current user ID
        /// </summary>
        public string? CurrentUser_Cash
        {
            get => UserController.CurrentUser.Cash_Representation;
        }

        /// <summary>
        /// Represents current user ID
        /// </summary>
        public string? CurrentUser_CardMoney
        {
            get => UserController.CurrentUser.CardMoney_Representation;
        }

        /// <summary>
        /// Represents current user bonuses
        /// </summary>
        public string? CurrentUser_Bonuses
        {
            get => UserController.CurrentUser.Bonuses_Representation;
        }
        //

        // Commands

        private RelayCommand? scanCommand;

        /// <summary>
        /// Move to the actions view
        /// </summary>
        public RelayCommand? ScanCommand
        {
            get
            {
                return scanCommand ??
                  (scanCommand = new RelayCommand(obj =>
                  {
                      UserController.CurrentPage = UserController.UserPage_Actions;
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
