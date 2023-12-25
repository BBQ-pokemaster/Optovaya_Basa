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
using System.Windows.Shapes;

namespace Optovaya_Basa
{
    /// <summary>
    /// Логика взаимодействия для TovarWindow.xaml
    /// </summary>
    public partial class TovarWindow : Window
    {
        OptomEntities context = new OptomEntities();
        public static CollectionViewSource source;
        public TovarWindow()
        {
            InitializeComponent();
            source = (CollectionViewSource)FindResource("tovarTableViewSource");
            DataContext = this;
        }
        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            if(grid1.Visibility != Visibility.Visible)
            grid1.Visibility = Visibility.Visible;
            TovarWindow1.Width = 1000;
        }
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            if (grid1.Visibility == Visibility.Visible)
                grid1.Visibility = Visibility.Collapsed;
            if (deleteDial.Visibility == Visibility.Visible)
                deleteDial.Visibility = Visibility.Collapsed;
            TovarWindow1.Width = 600;
        }
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            int maxIdTovara = (from us in context.TovarTable select us.IdTovara).Max();
            if (grid1.Visibility == Visibility.Visible)
            {
                TovarTable tovar = new TovarTable();
                tovar.IdTovara = maxIdTovara + 1;
                tovar.Kod = Convert.ToInt32(kodTextBox.Text);
                tovar.Name = nameTextBox.Text;
                tovar.Quantity = Convert.ToInt32(quantityTextBox.Text);
                tovar.UnitofMeasurement = unitofMeasurementTextBox.Text;
                tovar.Cost = Convert.ToInt32(costTextBox.Text);
                tovar.Comment = commentTextBox.Text;
                context.TovarTable.Add(tovar);
                context.SaveChanges();

                MessageBox.Show("Товар успешно добавлен");
                grid1.Visibility = Visibility.Collapsed;
                TovarWindow1.Width = 600;
            }
            tovarTableDataGrid.ItemsSource = context.TovarTable.ToList();
            source.View.Refresh();
        }
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (deleteDial.Visibility != Visibility.Visible)
                deleteDial.Visibility = Visibility.Visible;
            TovarWindow1.Width = 1000;
        }
        private void delete_Click_1(object sender, RoutedEventArgs e)
        {
            if (deleteDial.Visibility == Visibility.Visible)
            {
                TovarTable del = new TovarTable();
                del.Name = deleteText.Text;
                foreach (var ord in context.TovarTable.Where(x => x.Name == del.Name))
                    context.TovarTable.Remove(ord);
                context.SaveChanges();
                tovarTableDataGrid.ItemsSource = context.TovarTable.ToList();
                source.View.Refresh();
                deleteDial.Visibility = Visibility.Collapsed;
                TovarWindow1.Width = 550;
            }
        }
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            tovarTableDataGrid.ItemsSource = context.TovarTable.ToList();
            source.View.Refresh();
            tovarTableDataGrid.CancelEdit();
            context.SaveChanges();
            MessageBox.Show("Данные успешно сохранены");
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            Optovaya_Basa.OptomDataSet optomDataSet = ((Optovaya_Basa.OptomDataSet)(this.FindResource("optomDataSet")));
            // Загрузить данные в таблицу TovarTable. Можно изменить этот код как требуется.
            Optovaya_Basa.OptomDataSetTableAdapters.TovarTableTableAdapter optomDataSetTovarTableTableAdapter = new Optovaya_Basa.OptomDataSetTableAdapters.TovarTableTableAdapter();
            optomDataSetTovarTableTableAdapter.Fill(optomDataSet.TovarTable);
            System.Windows.Data.CollectionViewSource tovarTableViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("tovarTableViewSource")));
            tovarTableViewSource.View.MoveCurrentToFirst();
        }
    }
}
