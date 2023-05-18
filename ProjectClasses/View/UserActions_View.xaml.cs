using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CoolStoreProject
{
    /// <summary>
    /// Логика взаимодействия для View_UserActions.xaml
    /// </summary>
    public partial class View_UserActions : Window
    {
        public View_UserActions()
        {
            InitializeComponent();

            DataContext = new UserActions_ViewModel();
        }
    }
}
