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
    /// Interaction logic for NewEntryPage.xaml
    /// </summary>
    public partial class NewEntryPage : Page,   IPasswordProtectable
    {

        public int UserId { get; set; }


        private MainWindow _mainWindow;


        public NewEntryPage(int userId, MainWindow window)
        {
            InitializeComponent();
            UserId = userId;
            _mainWindow = window;

        }

        private void BtnClickCreateWebsite(object sender, RoutedEventArgs e)
        {

            if (WebsiteNameTxtBox.Text == null || WebsitePasswordTxtBox.Password == null) return;

            EnterMasterPassword enterMasterPasswordWindow = new EnterMasterPassword(UserId, this);
            enterMasterPasswordWindow.Show();

        }

        public void OnPasswordConfirmation(byte[] hashKey)
        {

            var encryptedPassword = SymmetricEncryption.Encrypt(Convert.ToBase64String(hashKey), WebsitePasswordTxtBox.Password);

            WebsiteManager.Create(UserId, WebsiteNameTxtBox.Text, encryptedPassword, UsernameTxtBox.Text, UrlTxtBox.Text);
            _mainWindow.PopulateWebsiteList();
            _mainWindow.DetailsWindow.Content = new BlankPage();


        }

    }
}
