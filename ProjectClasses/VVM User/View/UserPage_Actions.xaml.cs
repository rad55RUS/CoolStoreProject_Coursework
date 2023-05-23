using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CoolStoreProject.UserVVM
{
    /// <summary>
    /// Interaction logic for UserPage_Actions.xaml
    /// </summary>
    public partial class UserPage_Actions : Page
    {
        // Fields
        private static readonly Regex regex = new Regex("^[0-9]+$");
        //

        // Constructors
        public UserPage_Actions()
        {
            InitializeComponent();
        }
        //

        // Methods
        /// <summary>
        /// Disable letter input into page textboxes
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreviewTextInput(TextCompositionEventArgs e)
        {
            if (!regex.IsMatch(e.Text))
                e.Handled = true;
            base.OnPreviewTextInput(e);
        }
        //
    }
}
