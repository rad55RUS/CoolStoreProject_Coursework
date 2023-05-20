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
