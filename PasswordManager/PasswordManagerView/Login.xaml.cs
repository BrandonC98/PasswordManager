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
using PasswordManager;

namespace PasswordManagerView
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Page
    {

        private LoginWindow _window;

        public Login(LoginWindow window)
        {
            _window = window;
            InitializeComponent();

        }

        private void BtnClickLogin(object sender, RoutedEventArgs e)
        {

            var emailController = new EmailController();
            var user = UserManager.Retrieve(EmailTxtBox.Text);

            if(!emailController.IsEmailInUse(EmailTxtBox.Text))
            {

                MessageBox.Show("No Account exists with this email");
                return;
                

            }

            if (MasterPasswordManager.CompareHash(Encoding.ASCII.GetBytes(PasswordTxtBox.Password), user.Id))
            {

                MainWindow main = new MainWindow(user.Id);
                _window.Visibility = Visibility.Hidden;
                main.Show();

            }
            else MessageBox.Show("Failed to login");

        }
    }
}
