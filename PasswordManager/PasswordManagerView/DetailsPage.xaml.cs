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
using PasswordManagerData;

namespace PasswordManagerView
{
    /// <summary>
    /// Interaction logic for DetailsPage.xaml
    /// </summary>
    public partial class DetailsPage : Page,    IPasswordProtectable
    {

        private int _userId;
        private Website _currentWebsite;
        private MainWindow _mainWindow;

        public DetailsPage(int userId, PasswordManagerData.Website website, string plainTextPassword, MainWindow mainWindow)
        {
            DetailsPagePasswordShowTxtBox = new TextBox();
            DetailsPageUrlTxtBox = new TextBox();
            DetailsPageWebsiteNameTxtBox = new TextBox();
            DetailsPageUsernameTxtBox = new TextBox();
            DetailsPagePasswordShowTxtBox = new TextBox();

            InitializeComponent();

            _userId = userId;
            _currentWebsite = website;
            _mainWindow = mainWindow;

            PopulateDetails(plainTextPassword);

        }

        private void PopulateDetails(string plainTextPassword)
        {

            DetailsPagePasswordShowTxtBox.Text = plainTextPassword;
            DetailsPageUrlTxtBox.Text = _currentWebsite.Url;
            DetailsPageWebsiteNameTxtBox.Text = _currentWebsite.Name;
            DetailsPageUsernameTxtBox.Text = _currentWebsite.Username;


        }

        private void BtnClickUpdateWebsite(object sender, RoutedEventArgs e)
        {

            EnterMasterPassword masterPasswordWindow = new EnterMasterPassword(_userId, this);
            masterPasswordWindow.Show();

        }

        public void OnPasswordConfirmation(byte [] hashKey)
        {

            var newEncryptedPassword = SymmetricEncryption.Encrypt(Convert.ToBase64String(hashKey), DetailsPagePasswordShowTxtBox.Text);
            var newUserName = DetailsPageUsernameTxtBox.Text;

            WebsiteManager.Update(_currentWebsite.Id, DetailsPageWebsiteNameTxtBox.Text, newEncryptedPassword, newUserName, DetailsPageUrlTxtBox.Text);
            _currentWebsite = WebsiteManager.Retrieve(_currentWebsite.Id);
            PopulateDetails(DetailsPagePasswordShowTxtBox.Text);
            _mainWindow.PopulateWebsiteList();

        }

        private void BtnClickDeleteWebsite(object sender, RoutedEventArgs e)
        {

            WebsiteManager.Delete(_currentWebsite.Id);
            _mainWindow.PopulateWebsiteList();
            _mainWindow.DetailsWindow.Content = new BlankPage();

        }
    }
}
