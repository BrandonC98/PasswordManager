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
    /// Interaction logic for SignUp.xaml
    /// </summary>
    public partial class SignUp : Page
    {

        public LoginWindow Window { get; set; }

        public SignUp()
        {
            InitializeComponent();
            
        }

        private void BtnClickCreate(object sender, RoutedEventArgs e)
        {


            var userManager = new UserManager();
            var mPasswordManager = new MasterPasswordManager();
            
            if(userManager.Exist(EmailTxtBox.Text))
            {

                MessageBox.Show("This Email Address already has an account");
                return;

            }

            if (ConfirmPasswordTxtBox.Password != PasswordTxtBox.Password) return;

            userManager.Create(FirstNameTxtBox.Text, LastNameTxtBox.Text, EmailTxtBox.Text);
            var salt = Hash.GenerateSalt(20);
            var hash = Hash.GenerateHash(Encoding.ASCII.GetBytes(PasswordTxtBox.Password), salt, 1000, 16);
            mPasswordManager.Create(userManager.Retrieve(EmailTxtBox.Text).Id, salt, hash);
            MessageBox.Show("Account Created");
            Window.MainLogin.Content = new Login() { Window = Window };
            
        }
    }
}
