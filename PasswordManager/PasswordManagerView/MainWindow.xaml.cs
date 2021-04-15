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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IPasswordProtectable
    {

        public int UserId { get; set; }
        private Website _currentWebsite;

        public MainWindow(int userId)
        {
            InitializeComponent();
            UserId = userId;

            PopulateWebsiteList();

        }

        public void PopulateWebsiteList()
        { 

            var websiteManager = new WebsiteManager();

            WebsiteList.ItemsSource = websiteManager.GetAll(UserId);

        }

        private void WebsiteList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if(WebsiteList.SelectedItem != null)
            {
                _currentWebsite = (Website)WebsiteList.SelectedItem;
                EnterMasterPassword enterMasterPasswordWindow = new EnterMasterPassword(UserId, this);
                enterMasterPasswordWindow.Show();

            }

        }

        private void BtnClickNew(object sender, RoutedEventArgs e)
        {

            DetailsWindow.Content = new NewEntryPage(UserId, this);

        }

        public void OnPasswordConfirmation(byte[] hashKey)
        {
            var websiteManager = new WebsiteManager();
            var plainTextPassword = websiteManager.DecryptPasswordForWebsite(_currentWebsite.Id, hashKey);
            var website = (Website)WebsiteList.SelectedItem;
            DetailsWindow.Content = new DetailsPage(UserId, website, plainTextPassword, this);
            
        }

        private void BtnClickLogout(object sender, RoutedEventArgs e)
        {

            var loginWindow = new LoginWindow();
            this.Close();
            loginWindow.Visibility = Visibility.Visible;

        }

        private void BtnClickSettingsPage(object sender, RoutedEventArgs e)
        {

            DetailsWindow.Content = new AccountPage(UserId);

        }
    }
}
