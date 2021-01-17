using System;
using System.Data;
using System.Windows;
using System.Windows.Input;

namespace CafeApp
{
    /// <summary>
    /// Логика взаимодействия для Menu.xaml
    /// </summary>
    public partial class Menu : Window
    {
        public Menu()
        {
            InitializeComponent();

            LoadGrid();
        }

        public struct MenuToContents
        {
            public string Id { get; set; }
            public string Name { get; set; }
        }

        private void LoadGrid()
        {
            try
            {
                MenuDataGrid.ItemsSource = DataBase.FillDataGrid($"select id_content, content_name from contents");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Hola oops");
            }
        }

        private void IngridientsDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (MenuDataGrid.SelectedIndex != -1) HaveContentsGrid.Items.Add(new MenuToContents()
            { Id = ((DataRowView)MenuDataGrid.SelectedItem)[0].ToString(), Name = ((DataRowView)MenuDataGrid.SelectedItem)[1].ToString() });
        }

        private void HaveIngridientsGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (HaveContentsGrid.SelectedIndex != -1) HaveContentsGrid.Items.Remove(HaveContentsGrid.SelectedItem);
        }

        private void AppedButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (HaveContentsGrid.Items.Count != 0)
                {

                    string MenuID = DataBase.AddMenu(this.NameTextBox.Text, Convert.ToInt32(this.DieticBox.IsChecked), Convert.ToInt32(this.veganBox.IsChecked), Convert.ToInt32(this.FullBox.IsChecked));
                    foreach (MenuToContents menuToContents in HaveContentsGrid.Items)
                    {
                        DataBase.IngredientsToContents(menuToContents.Id, MenuID);
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