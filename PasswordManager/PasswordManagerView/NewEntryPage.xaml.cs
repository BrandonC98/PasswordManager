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
    public partial class NewEntryPage : Page
    {

        public int UserId { get; set; }

        WebsiteManager _websiteManager;
        MasterPasswordManager _masterPasswordManager;


        public NewEntryPage(int userId)
        {
            InitializeComponent();
            UserId = userId;
            _websiteManager = new WebsiteManager();
            _masterPasswordManager = new MasterPasswordManager();

        }

        private void BtnClickCreateWebsite(object sender, RoutedEventArgs e)
        {

            var masterPassword = _masterPasswordManager.RetrieveByUserId(UserId);

            var key = Hash.GenerateHash(Encoding.ASCII.GetBytes(MasterPasswordTxtBoxNewEntry.Password), masterPassword.Salt, masterPassword.Iterations, 16);

            if(Hash.CompareHash(key, masterPassword.Hash))
            {

                var encryptedPassword = SymmetricEncryption.Encrypt(Convert.ToBase64String(key), WebsitePasswordTxtBox.Password);

                _websiteManager.Create(UserId, WebsiteNameTxtBox.Text, encryptedPassword, UsernameTxtBox.Text, UrlTxtBox.Text);


            }


        }
    }
}
