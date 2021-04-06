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

            if(!EmailTxtBox.Text.Contains("@"))
            {

                MessageBox.Show("Error: not a proper Email addresss");
                return;

            }
            
            if(UserManager.Exist(EmailTxtBox.Text))
            {

                MessageBox.Show("This Email Address already has an account");
                return;

            }

            if (ConfirmPasswordTxtBox.Password != PasswordTxtBox.Password) return;

            UserManager.Create(FirstNameTxtBox.Text, LastNameTxtBox.Text, EmailTxtBox.Text);
            MasterPasswordManager.Create(UserManager.Retrieve(EmailTxtBox.Text).Id, PasswordTxtBox.Password);
            MessageBox.Show("Account Created");
            Window.MainLogin.Content = new Login(Window);
            
        }
    }
}
