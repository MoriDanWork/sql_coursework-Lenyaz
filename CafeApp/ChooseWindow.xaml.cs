using System;
using System.Collections.Generic;
using System.Data;
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
    /// Логика взаимодействия для ChooseWindow.xaml
    /// </summary>
    public partial class ChooseWindow : Window
    {
        public MainWindow Mw { get; }
        public int Mode { get; }
        public ChooseWindow(MainWindow mw, int mode)
        {
            InitializeComponent();
            Mw = mw;
            Mode = mode;
            LoadGrid();
        }

        private void LoadGrid()
        {
            try
            {
                switch (Mode)
                {
                    case 1:
                        ContentDataGrid.ItemsSource = DataBase.FillDataGrid($"select * from Contents");
                        ModeLabel.Content = "Select Ingridient from list:";
                        this.SelectButton.Click += new RoutedEventHandler(SelectDishButton_Click);
                        break;
                    case 3:
                        ContentDataGrid.ItemsSource = DataBase.FillDataGrid($"select * from ingredients");
                        ModeLabel.Content = "Select Ingridient from list:";
                        this.SelectButton.Click += new RoutedEventHandler(SelectIngridientButton_Click);
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Hola oops");
            }
        }

        private void SelectDishButton_Click(object sender, RoutedEventArgs e)
        {

            if ((DataRowView)ContentDataGrid.SelectedItem != null)
            {
                MainWindow.CurrentDishId = Convert.ToInt32(((DataRowView)ContentDataGrid.SelectedItem)[0]);
                Mw.currentDishLabel.Content = ((DataRowView)ContentDataGrid.SelectedItem)[1].ToString();
                this.Close();
            }
            else { MessageBox.Show("Choose Dish"); }
        }

        private void SelectIngridientButton_Click(object sender, RoutedEventArgs e)
        {

            if ((DataRowView)ContentDataGrid.SelectedItem != null)
            {
                MainWindow.CurrentIngridId = Convert.ToInt32(((DataRowView)ContentDataGrid.SelectedItem)[0]);
                Mw.CurrentIngridientLabel.Content = ((DataRowView)ContentDataGrid.SelectedItem)[1].ToString();
                this.Close();
            }
            else { MessageBox.Show("Choose Ingridient"); }
        }
    }
}
