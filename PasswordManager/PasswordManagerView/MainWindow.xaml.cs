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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public int UserId { get; set; }
        private WebsiteManager _websiteManager;

        public MainWindow(int userId)
        {
            InitializeComponent();
            _websiteManager = new WebsiteManager();
            UserId = userId;

            PopulateWebsiteList();

        }

        public void PopulateWebsiteList() => WebsiteList.ItemsSource = _websiteManager.GetAll(UserId);   

        private void WebsiteList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if(WebsiteList.SelectedItem != null)
            {

                var website = (PasswordManagerData.Website)WebsiteList.SelectedItem;
                DetailsWindow.Content = new DetailsPage(UserId, website);

            }

        }

        private void BtnClickNew(object sender, RoutedEventArgs e)
        {

            DetailsWindow.Content = new NewEntryPage(UserId, this);

        }
    }
}
