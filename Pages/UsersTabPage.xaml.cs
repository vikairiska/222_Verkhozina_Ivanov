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

namespace _222_Verkhozina_Ivanov.Pages
{
    /// <summary>
    /// Логика взаимодействия для UsersTabPage.xaml
    /// </summary>
    public partial class UsersTabPage : Page
    {
        public UsersTabPage()
        {
            InitializeComponent();
            using (var db = new VerkhozinaIvanov_DB_PaymentEntities())
            {
                var currentUsers = db.User.ToList();
                DataGridUser.ItemsSource = currentUsers;
            }
            this.IsVisibleChanged += Page_IsVisibleChanged;

        }
        private void Page_IsVisibleChanged(object sender,DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                using (var db = new VerkhozinaIvanov_DB_PaymentEntities())
                {
                    db.ChangeTracker.Entries().ToList().ForEach(x => x.Reload());
                    var currentUsers = db.User.ToList();
                    DataGridUser.ItemsSource = currentUsers;
                }
            }
        }
        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new AddUserPage(null));
        }
        private void ButtonDel_Click(object sender, RoutedEventArgs e)
        {
            var usersForRemoving =
           DataGridUser.SelectedItems.Cast<User>().ToList();
            if (MessageBox.Show($"Вы точно хотите удалить записи в количестве {usersForRemoving.Count()} элементов?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {

                    using (var db = new VerkhozinaIvanov_DB_PaymentEntities())
                    {
                        db.User.RemoveRange(usersForRemoving); 
                        db.SaveChanges();
                        MessageBox.Show("Данные успешно удалены!");
                        DataGridUser.ItemsSource = db.User.ToList();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }
        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Pages.AddUserPage((sender as Button).DataContext as User));
        }
    }
} 