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
    /// Логика взаимодействия для Dishes.xaml
    /// </summary>
    public partial class Dishes : Window
    {
        public Dishes()
        {
            InitializeComponent();

            LoadGrid();
        }

        public struct IngridientToDish
        {
            public string Id { get; set; }
            public string Name { get; set; }
        }

        private void LoadGrid()
        {
            try
            {
                IngridientsDataGrid.ItemsSource = DataBase.FillDataGrid($"select id_ingredient, ingredient_name from ingredients");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Hola oops");
            }
        }

        private void IngridientsDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (IngridientsDataGrid.SelectedIndex != -1) HaveIngridientsGrid.Items.Add(new IngridientToDish()
            { Id = ((DataRowView)IngridientsDataGrid.SelectedItem)[0].ToString(), Name = ((DataRowView)IngridientsDataGrid.SelectedItem)[1].ToString() });
        }

        private void HaveIngridientsGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (HaveIngridientsGrid.SelectedIndex != -1) HaveIngridientsGrid.Items.Remove(HaveIngridientsGrid.SelectedItem);
        }

        private void AppedButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (HaveIngridientsGrid.Items.Count != 0)
                {
                    string DishID = DataBase.AddDish(this.NameTextBox.Text, this.WeightTextBox.Text);
                    foreach (IngridientToDish ingridientToDish in HaveIngridientsGrid.Items)
                    {
                        DataBase.IngredientsToContents(ingridientToDish.Id, DishID);
                    }
                    MessageBox.Show("Добавлено");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
