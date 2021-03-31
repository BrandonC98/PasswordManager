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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PasswordManagerView
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Page
    {
        NavigationService nav;
        public Login()
        {
            InitializeComponent();
        }

        private void SignUpBtnClick(object sender, RoutedEventArgs e)
        {

            SignUp signUp = new SignUp();
            MainWindow mainWindow = new MainWindow();
            //this.Content = mainWindow.Content
            //nav.Navigate(signUp);

        }
    }
}
