using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace Optovaya_Basa
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        OptomEntities context = new OptomEntities();
        public static CollectionViewSource source;
        public MainWindow()
        {
            InitializeComponent();
            postavkaDataGrid.ItemsSource = context.Postavka.ToList();
            CmbSelectTovar.ItemsSource = context.TovarTable.ToList();
            CmbSelectPostavchik.ItemsSource = context.PostavchikiTable.ToList();
            tovarsComboBox.ItemsSource = context.TovarTable.ToList();
            postavchikComboBox.ItemsSource = context.PostavchikiTable.ToList();
            source = (CollectionViewSource)FindResource("postavkaViewSource");
            DataContext = this;
        }
        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            if (grid1.Visibility != Visibility.Visible)
                grid1.Visibility = Visibility.Visible;
        }
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            if (grid1.Visibility == Visibility.Visible)
                grid1.Visibility = Visibility.Collapsed;
            this.Width = 800;
        }
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            int maxNomerScheta = (from us in context.Postavka select us.NomerScheta).Max();
            var tovar = tovarsComboBox.SelectedItem as TovarTable;
            var post = postavchikComboBox.SelectedItem as PostavchikiTable;
            if (grid1.Visibility == Visibility.Visible)
            {
                Postavka postavka = new Postavka();
                postavka.NomerScheta = maxNomerScheta + 1;
                postavka.Date = Convert.ToDateTime(dateDatePicker.Text);
                postavka.QuanitityOfTovar = Convert.ToInt32(quanitityOfTovarTextBox.Text);
                postavka.Cost = Convert.ToInt32(costTextBox.Text);
                postavka.Tovars = tovar.IdTovara;
                postavka.Postavchik = post.Nomer;
                context.Postavka.Add(postavka);
                context.SaveChanges();

                MessageBox.Show("Товар успешно добавлен");
                grid1.Visibility = Visibility.Collapsed;
                this.Width = 800;
            }
            postavkaDataGrid.ItemsSource = context.Postavka.ToList();
            source.View.Refresh();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
        }
        private void CmbSelectTovar_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CmbSelectTovar.SelectedItem == null) postavkaDataGrid.ItemsSource = context.Postavka.ToList(); 
            var currentTovar = (TovarTable)CmbSelectTovar.SelectedItem;
            if (currentTovar != null)
            {
                List<Postavka> ListTovar = context.Postavka.ToList();
                postavkaDataGrid.ItemsSource = ListTovar.Where(x => x.TovarTable.Name == currentTovar.Name).ToList();
            }
        }
        private void CmbSelectPostavchik_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CmbSelectPostavchik.SelectedItem == null) postavkaDataGrid.ItemsSource = context.Postavka.ToList(); 
            var currentPost = (PostavchikiTable)CmbSelectPostavchik.SelectedItem;
            if (currentPost != null)
            {
                List<Postavka> ListTovar = context.Postavka.ToList();
                postavkaDataGrid.ItemsSource = ListTovar.Where(x => x.PostavchikiTable.FirstName == currentPost.FirstName).ToList();
            }
        }
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var row = postavkaDataGrid.SelectedItem as Postavka;
            if (row == null)
            {
                MessageBox.Show("Строка не выбрана");
                return;
            }
            MessageBoxResult result = MessageBox.Show("Вы уверены", "Вопрос", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                context.Postavka.Remove(row);
                context.SaveChanges();
                postavkaDataGrid.ItemsSource = context.Postavka.ToList();
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            postavkaDataGrid.ItemsSource = context.Postavka.ToList();
            CmbSelectTovar.SelectedItem = null;
            CmbSelectPostavchik.SelectedItem = null;
            Calc.SelectedItem = null;
        }

        private void Calc_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = Calc.SelectedIndex;
            switch (index)
            {
                case 0:
                postavkaDataGrid.Items.SortDescriptions.Clear();
                postavkaDataGrid.Items.SortDescriptions.Add(new SortDescription("Cost", ListSortDirection.Descending));
                source.View.Refresh();break;
                case 1:
                    postavkaDataGrid.Items.SortDescriptions.Clear();
                    postavkaDataGrid.Items.SortDescriptions.Add(new SortDescription("Cost", ListSortDirection.Ascending));
                    source.View.Refresh(); break;
                case 2:
                    var sel = (TovarTable)CmbSelectTovar.SelectedItem;
                    System.Nullable<double> averge =
                    (from ord in context.Postavka.Where(x => x.TovarTable.Name == sel.Name) select ord.Cost).Average();
                    List<Postavka> ListTovar = context.Postavka.ToList();
                    postavkaDataGrid.ItemsSource = ListTovar.Where(x => x.Cost == averge & x.TovarTable.Name == sel.Name).ToList();break;
            }
        }
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            postavkaDataGrid.CancelEdit();
            context.SaveChanges();
            MessageBox.Show("Данные успешно сохранены");
        }
        private void Reload_Click(object sender, RoutedEventArgs e)
        {
            postavkaDataGrid.ItemsSource = context.Postavka.ToList();
            CmbSelectTovar.ItemsSource = context.TovarTable.ToList();
            CmbSelectPostavchik.ItemsSource = context.PostavchikiTable.ToList();
            tovarsComboBox.ItemsSource = context.TovarTable.ToList();
            postavchikComboBox.ItemsSource = context.PostavchikiTable.ToList();
            source.View.Refresh();
        }
        private void ShowTovarPage(object sender, RoutedEventArgs e)
        {
            var win = new TovarWindow();
            win.Show();
        }
        private void ShowPostPage(object sender, RoutedEventArgs e)
        {
            var win = new PostWindow();
            //this.NavigationService.Navigate(win);
            win.Show();
        }
    }
}
