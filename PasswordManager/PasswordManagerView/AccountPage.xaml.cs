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
    /// Interaction logic for AccountPage.xaml
    /// </summary>
    public partial class AccountPage : Page
    {

        private int _userId;

        public AccountPage(int userId)
        {
            InitializeComponent();
            var userManager = new UserManager();

            _userId = userId;
            var user = userManager.Retrieve(_userId);
            FirstNameTxtBoxAccountSettings.Text = user.FirstName;
            LastNameTxtBoxAccountSettings.Text = user.LastName;
            EmailTxtBoxAccountSettings.Text = user.EmailAddress;

        }

        private void BtnClickUpdateAccount(object sender, RoutedEventArgs e)
        {
            var userManager = new UserManager();

            userManager.Update(_userId, FirstNameTxtBoxAccountSettings.Text, LastNameTxtBoxAccountSettings.Text, EmailTxtBoxAccountSettings.Text);
            MessageBox.Show("Account Updated");

        }
    }
}
