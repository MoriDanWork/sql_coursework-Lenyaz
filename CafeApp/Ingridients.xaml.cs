using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CafeApp
{
    /// <summary>
    /// Логика взаимодействия для Ingridients.xaml
    /// </summary>
    public partial class Ingridients : Window
    {

        public Ingridients()
        {
            InitializeComponent();
        }

        private void AppendBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataBase.AddIngridientItem(NameBox.Text, ProteinsBox.Text, FatsBox.Text, CarbohydrBox.Text, CaloriesBox.Text, PriceBox.Text);
                MessageBox.Show("Добавлено"); this.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

    }
}
