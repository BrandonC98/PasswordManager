﻿using System;
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
    public partial class MainWindow : Window, IPasswordProtectable
    {

        public int UserId { get; set; }
        private WebsiteManager _websiteManager;
        private PasswordManagerData.Website _currentWebsite;

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
                _currentWebsite = (PasswordManagerData.Website)WebsiteList.SelectedItem;
                EnterMasterPassword enterMasterPasswordWindow = new EnterMasterPassword(UserId, this);
                enterMasterPasswordWindow.Show();

            }

        }

        private void BtnClickNew(object sender, RoutedEventArgs e)
        {

            DetailsWindow.Content = new NewEntryPage(UserId, this);

        }

        public void FillDetails(byte[] hashKey)
        {
            var encrpytedPassword = _websiteManager.Retrieve(_currentWebsite.Id).Password;
            var plainTextPassword = SymmetricEncryption.Decrypt(Convert.ToBase64String(hashKey), encrpytedPassword);
            var website = (PasswordManagerData.Website)WebsiteList.SelectedItem;
            DetailsWindow.Content = new DetailsPage(UserId, website, plainTextPassword, this);
            
        }

        private void BtnClickLogout(object sender, RoutedEventArgs e)
        {

            var loginWindow = new LoginWindow();
            this.Close();
            loginWindow.Visibility = Visibility.Visible;


        }
    }
}
