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

        private RelayCommand? cashCommand;

        /// <summary>
        /// Pay by cash command
        /// </summary>
        public RelayCommand? CashCommand
        {
            get
            {
                return cashCommand ??
                  (cashCommand = new RelayCommand(obj =>
                  {
                      UserController.SendCashPayment();
                  }));
            }
        }

        private RelayCommand? cardCommand;

        /// <summary>
        /// Pay by card command
        /// </summary>
        public RelayCommand? CardCommand
        {
            get
            {
                return cardCommand ??
                  (cardCommand = new RelayCommand(obj =>
                  {
                      UserController.SendCardPayment();
                  }));
            }
        }

        private RelayCommand? bonusesCommand;

        /// <summary>
        /// Pay by bonuses command
        /// </summary>
        public RelayCommand? BonusesCommand
        {
            get
            {
                return bonusesCommand ??
                  (bonusesCommand = new RelayCommand(obj =>
                  {
                      UserController.SendBonusesPayment();
                  }));
            }
        }

        private RelayCommand? returnCommand;

        /// <summary>
        /// Move to the actions view comand
        /// </summary>
        public RelayCommand? ReturnCommand
        {
            get
            {
                return returnCommand ??
                  (returnCommand = new RelayCommand(obj =>
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
