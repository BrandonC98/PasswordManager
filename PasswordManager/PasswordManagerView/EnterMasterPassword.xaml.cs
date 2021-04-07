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
using System.Windows.Shapes;
using PasswordManager;

namespace PasswordManagerView
{
    /// <summary>
    /// Interaction logic for EnterMasterPassword.xaml
    /// </summary>
    public partial class EnterMasterPassword : Window
    {

        private int _userId;
        IPasswordProtectable _passwordProtectable;
        

        public EnterMasterPassword(int userId, IPasswordProtectable passwordProtectable)
        {
            InitializeComponent();
            _userId = userId;
            _passwordProtectable = passwordProtectable;
            IncorrectPasswordLabel.Visibility = Visibility.Hidden;
        }

        private void BtnClickContinue(object sender, RoutedEventArgs e)
        {

            //var masterPassword = MasterPasswordManager.RetrieveByUserId(_userId);

            //var key = Hash.GenerateHash(Encoding.ASCII.GetBytes(MPasswordTxtBox.Password), masterPassword.Salt, masterPassword.Iterations, 16);

            //if (Hash.CompareHash(key, masterPassword.Hash))
            if(MasterPasswordManager.CompareHash(Encoding.ASCII.GetBytes(MPasswordTxtBox.Password), _userId, out byte[] key))
            {

                this.Visibility = Visibility.Hidden;
                _passwordProtectable.OnPasswordConfirmation(key);

            }
            else IncorrectPasswordLabel.Visibility = Visibility.Visible;


        }

        private void BtnClickCancel(object sender, RoutedEventArgs e) => this.Visibility = Visibility.Hidden;

        
    }
}
