using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Xml;

namespace CoolStoreProject
{
    internal class Window_ViewModel : INotifyPropertyChanged
    {
        private Page ?currentPage;

        // Properties
        public Page ?CurrentPage
        {
            get => currentPage;
            set
            {
                currentPage = value;
                OnPropertyChanged("CurrentPage");
            }
        }
        //

        // Constructors
        public Window_ViewModel()
        {

        }
        //

        // INotifyPropertyChanged realization
        public event PropertyChangedEventHandler ?PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
        //
    }
}
