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
    /// Логика взаимодействия для search.xaml
    /// </summary>
    public partial class search : Window
    {
        string result;
        int mode;
        public search()
        {
            InitializeComponent();
        }
        public search(string _result, int _mode)
        {
            result = _result;
            mode = _mode;
            InitializeComponent();
        }



        private void DataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            string final_command = null;
            switch (mode)
            {
                case 0: final_command = $"select * from ingredients where lower(ingredient_name) like '%{result}%'"; break;
                case 1: final_command = $"select * from contents where lower(content_name) like '%{result}%'"; break;
            }

            label.Content = final_command;

            try
            {
                dataGrid.DataContext = DataBase.FillDataGrid(final_command);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Hola oops");
            }
        }
    }
}
