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

        public LoginWindow Window { get; set; }

        public Login()
        {
            InitializeComponent();

        }

        private void BtnClickLogin(object sender, RoutedEventArgs e)
        {

            var userManager = new UserManager();
            var mPasswordManager = new MasterPasswordManager();

            var u = userManager.Retrieve(EmailTxtBox.Text);

            if (u == null) return;

            var mp = mPasswordManager.RetrieveByUserId(u.Id);

            var hash = Hash.GenerateHash(Encoding.ASCII.GetBytes(PasswordTxtBox.Password), mp.Salt, mp.Iterations, 16);

            if (Hash.CompareHash(hash, mp.Hash))
            {

                MainWindow main = new MainWindow(u.Id);
                Window.Visibility = Visibility.Hidden;
                main.Show();

            }
            else MessageBox.Show("Failed to login");

        }
    }
}
