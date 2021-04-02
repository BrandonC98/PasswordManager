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
    /// Interaction logic for DetailsPage.xaml
    /// </summary>
    public partial class DetailsPage : Page
    {

        private int _userId;
        private WebsiteManager _websiteManager;

        public DetailsPage(int usderId, PasswordManagerData.Website website)
        {
            InitializeComponent();

            _websiteManager = new WebsiteManager();

            DetailsPageUrlTxtBox.Text = website.Url;
            DetailsPageWebsiteNameTxtBox.Text = website.Name;
            DetailsPageUsernameTxtBox.Text = website.Username;
            DetailsPageWebsitePasswordTxtBox.Password = website.Password;

        }

        private void BtnClickUpdateWebsite(object sender, RoutedEventArgs e)
        {

        }
    }
}
