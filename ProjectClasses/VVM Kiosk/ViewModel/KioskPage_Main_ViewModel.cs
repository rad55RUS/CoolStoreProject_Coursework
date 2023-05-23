using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CoolStoreProject.KioskVVM
{
    internal class KioskPage_Main_ViewModel : INotifyPropertyChanged
    {
        // Fields
        private string? currentPicture;
        private string? productName;
        private string? productWeight;
        private string? productPrice;
        //

        // Properties
        public string? CurrentPicture
        {
            get => currentPicture;
            set
            {
                currentPicture = value;
                OnPropertyChanged("CurrentPicture");
            }
        }
        public string? ProductName
        {
            get => productName;
            set
            {
                productName = value;
                OnPropertyChanged("ProductName");
            }
        }
        public string? ProductWeight
        {
            get => productWeight;
            set
            {
                productWeight = value;
                OnPropertyChanged("ProductWeight");
            }
        }
        public string? ProductPrice
        {
            get => productPrice;
            set
            {
                productPrice = value;
                OnPropertyChanged("ProductPrice");
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
