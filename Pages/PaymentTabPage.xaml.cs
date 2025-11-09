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
    /// Логика взаимодействия для PaymentTabPage.xaml
    /// </summary>
    public partial class PaymentTabPage : Page
    {
        public PaymentTabPage()
        {
            InitializeComponent();

            using (var Entities = new VerkhozinaIvanov_DB_PaymentEntities())
            {
                DataGridPayment.ItemsSource = Entities.Payment.ToList();
                this.IsVisibleChanged += Page_IsVisibleChanged;
            }
        }
        private void Page_IsVisibleChanged(object sender,
       DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                using (var Entities = new VerkhozinaIvanov_DB_PaymentEntities())
                {
                    Entities.ChangeTracker.Entries().ToList().ForEach(x => x.Reload());
                    DataGridPayment.ItemsSource = Entities.Payment.ToList();
                }
            }
        }
        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new AddPaymentPage(null));
        }
        private void ButtonDel_Click(object sender, RoutedEventArgs e)
        {
            var paymentForRemoving =
           DataGridPayment.SelectedItems.Cast<Payment>().ToList();
            if (MessageBox.Show($"Вы точно хотите удалить записи в количестве {paymentForRemoving.Count()} элементов?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) ==MessageBoxResult.Yes)
            {
                try
                {
                    using (var Entities = new VerkhozinaIvanov_DB_PaymentEntities())
                    {
                        Entities.Payment.RemoveRange(paymentForRemoving);
                        Entities.SaveChanges();
                        MessageBox.Show("Данные успешно удалены!");
                        DataGridPayment.ItemsSource = Entities.Payment.ToList();
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
            NavigationService.Navigate(new Pages.AddPaymentPage((sender as Button).DataContext as Payment));
        }
    }
}
