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
        // Fields
        private int? left;
        private int? top;
        private Page? currentPage;
        //

        // Properties
        public Page? CurrentPage {
            get => currentPage;
            set
            {
                currentPage = value;
                OnPropertyChanged("CurrentPage");
            }
        }

        public int? Left {
            get => left;
            set
            {
                left = value;
                OnPropertyChanged("Left");
            }
        }
        public int? Top { 
            get => top;
            set
            {
                top = value;
                OnPropertyChanged("Top");
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
