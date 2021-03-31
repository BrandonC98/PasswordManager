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

namespace PasswordManagerView
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
            MainLogin.Content = new Login();
        }

        private void BtnClickLoginPage(object sender, RoutedEventArgs e)
        {
            MainLogin.Content = new Login();
        }

        private void BtnClickSignUpPage(object sender, RoutedEventArgs e)
        {
            MainLogin.Content = new SignUp();
        }
    }
}
