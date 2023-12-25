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
    /// Логика взаимодействия для PostWindow.xaml
    /// </summary>
    public partial class PostWindow : Window
    {
        OptomEntities context = new OptomEntities();
        public static CollectionViewSource source;
        public PostWindow()
        {
            InitializeComponent();
            source = (CollectionViewSource)FindResource("postavchikiTableViewSource");
            DataContext = this;
        }
        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            if (grid1.Visibility != Visibility.Visible)
                grid1.Visibility = Visibility.Visible;
            PostWindow1.Width = 1000;
        }
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            if (grid1.Visibility == Visibility.Visible)
                grid1.Visibility = Visibility.Collapsed;
            if (deleteDial.Visibility == Visibility.Visible)
                deleteDial.Visibility = Visibility.Collapsed;
            PostWindow1.Width = 550;
        }
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (grid1.Visibility == Visibility.Visible)
            {
                PostavchikiTable post = new PostavchikiTable();
                post.Nomer = Convert.ToInt32(nomerTextBox.Text);
                post.Adres = adresTextBox.Text;
                post.PhoneNumber =phoneNumberTextBox.Text;
                post.FirstName = firstNameTextBox.Text;
                post.SecondName = secondNameTextBox.Text;
                post.ThirdName = thirdNameTextBox.Text;
                context.PostavchikiTable.Add(post);
                context.SaveChanges();

                MessageBox.Show("Поставщик успешно добавлен");
                grid1.Visibility = Visibility.Collapsed;
                PostWindow1.Width = 550;
            }
            postavchiki.ItemsSource = context.PostavchikiTable.ToList();
            source.View.Refresh();
        }
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (deleteDial.Visibility != Visibility.Visible)
                deleteDial.Visibility = Visibility.Visible;
            PostWindow1.Width = 1000;
        }
        private void delete_Click_1(object sender, RoutedEventArgs e)
        {
            if (deleteDial.Visibility == Visibility.Visible)
            {
                PostavchikiTable del = new PostavchikiTable();
                del.Nomer = Convert.ToInt32(deleteText.Text);
                foreach (var ord in context.PostavchikiTable.Where(x => x.Nomer == del.Nomer))
                    context.PostavchikiTable.Remove(ord);
                context.SaveChanges();
                postavchiki.ItemsSource = context.PostavchikiTable.ToList();
                source.View.Refresh();
                deleteDial.Visibility = Visibility.Collapsed;
                PostWindow1.Width = 550;
            }
        }
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            postavchiki.ItemsSource = context.PostavchikiTable.ToList();
            source.View.Refresh();
            postavchiki.CancelEdit();
            context.SaveChanges();
            MessageBox.Show("Данные успешно сохранены");
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            Optovaya_Basa.OptomDataSet optomDataSet = ((Optovaya_Basa.OptomDataSet)(this.FindResource("optomDataSet")));
            // Загрузить данные в таблицу PostavchikiTable. Можно изменить этот код как требуется.
            Optovaya_Basa.OptomDataSetTableAdapters.PostavchikiTableTableAdapter optomDataSetPostavchikiTableTableAdapter = new Optovaya_Basa.OptomDataSetTableAdapters.PostavchikiTableTableAdapter();
            optomDataSetPostavchikiTableTableAdapter.Fill(optomDataSet.PostavchikiTable);
            System.Windows.Data.CollectionViewSource postavchikiTableViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("postavchikiTableViewSource")));
            postavchikiTableViewSource.View.MoveCurrentToFirst();
        }
    }
}
