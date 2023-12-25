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

namespace Optovaya_Basa
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            Optovaya_Basa.OptomDataSet optomDataSet = ((Optovaya_Basa.OptomDataSet)(this.FindResource("optomDataSet")));
            // Загрузить данные в таблицу Postavka. Можно изменить этот код как требуется.
            Optovaya_Basa.OptomDataSetTableAdapters.PostavkaTableAdapter optomDataSetPostavkaTableAdapter = new Optovaya_Basa.OptomDataSetTableAdapters.PostavkaTableAdapter();
            optomDataSetPostavkaTableAdapter.Fill(optomDataSet.Postavka);
            System.Windows.Data.CollectionViewSource postavkaViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("postavkaViewSource")));
            postavkaViewSource.View.MoveCurrentToFirst();
        }
    }
}
