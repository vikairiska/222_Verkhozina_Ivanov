using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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

namespace _222_Verkhozina_Ivanov.Pages
{
    /// <summary>
    /// Логика взаимодействия для ChangePassPage.xaml
    /// </summary>
    public partial class ChangePassPage : Page
    {
        public ChangePassPage()
        {
            InitializeComponent();
        }

        private void ButtonChangePassword_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(CurrentPasswordBox.Password) || string.IsNullOrEmpty(NewPasswordBox.Password) ||string.IsNullOrEmpty(ConfirmPasswordBox.Password) || string.IsNullOrEmpty(TbLogin.Text))
            {
                MessageBox.Show("Все поля обязательны к заполнению!"); return;
            }
            using (var Entities = new VerkhozinaIvanov_DB_PaymentEntities())
            {
                string hashedPass = GetHash(CurrentPasswordBox.Password);
                var user = Entities.User
                 .FirstOrDefault(u => u.Login == TbLogin.Text && u.Password == hashedPass);
                if (user == null)
                {
                    MessageBox.Show("Текущий пароль/Логин неверный!");
                    return;
                }
                user.Password = GetHash(NewPasswordBox.Password);
                Entities.SaveChanges();
            }                
            MessageBox.Show("Пароль успешно изменен!");
            NavigationService?.Navigate(new AuthPage());
        }
        public static string GetHash(String password)
        {
            using (var hash = SHA1.Create())
            {
                return string.Concat(hash.ComputeHash(Encoding.UTF8.GetBytes(password)).Select(x => x.ToString("X2")));
            }
        }
    }
}
