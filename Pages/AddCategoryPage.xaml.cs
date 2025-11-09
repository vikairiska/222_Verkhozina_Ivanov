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
    /// Логика взаимодействия для AddCategoryPage.xaml
    /// </summary>
    public partial class AddCategoryPage : Page
    {
        private Category _currentCategory = new Category();
        public AddCategoryPage(Category selectedCategory)
        {
            InitializeComponent();
            if (selectedCategory != null)
                _currentCategory = selectedCategory;
            DataContext = _currentCategory;
        }
        private void ButtonSaveCategory_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrWhiteSpace(_currentCategory.Name)) errors.AppendLine("Укажите название категории!"); if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            using (var Entities = new VerkhozinaIvanov_DB_PaymentEntities())
            {
                if (_currentCategory.ID == 0)
                    Entities.Category.Add(_currentCategory);
                try
                {
                    Entities.SaveChanges();
                    MessageBox.Show("Данные успешно сохранены!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }
        private void ButtonClean_Click(object sender, RoutedEventArgs e)
        {
            TBCategoryName.Text = "";
        }
    }
} 

