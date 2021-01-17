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

namespace CafeApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static public int CurrentIngridId = -1;
        static public int CurrentDishId = -1;
        static public int CurrentMenuId = -1;
        public MainWindow()
        {
            InitializeComponent();
            DataBase.OpenConnection();
        }

        private void SearchBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key != System.Windows.Input.Key.Enter) return;
            e.Handled = true;
            switch (SwitchBox.SelectedIndex)
            {
                case -1: case 0: new search(searchBox.Text, 0).Show(); break;
                case 1: new search(searchBox.Text, 1).Show(); break;
            }

        }

        private void AddIngrid_Click(object sender, RoutedEventArgs e)
        {
            new Ingridients().Show();
        }


        private void CurrentIngridientLabel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            new ChooseWindow(this, 3).Show();
        }

        private void RemoveIngridientButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (CurrentIngridId == -1) throw new Exception("You must choose any Ingridient");
                DataBase.RemoveByID("ingredients", "id_ingredient", CurrentIngridId);
                MessageBox.Show("Removed!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void IngridientInfoButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (CurrentIngridId == -1) throw new Exception("You must choose any Ingridient");
                var Flex = DataBase.GetInfo("ingredients", "id_ingredient", CurrentIngridId);
                MessageBox.Show($"Info for {Flex[1]}:\nProteins:    {Flex[2]}\nFats:    {Flex[3]}\nCarbohydr:   {Flex[4]}\nCalories:    {Flex[5]}\nPrice:   {Flex[6]}", "Ingridient Info");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void addDish_Click(object sender, RoutedEventArgs e)
        {
            new Dishes().Show();
        }

        private void currentDishLabel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            new ChooseWindow(this, 1).Show();
        }

        private void deleteDish_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (CurrentDishId == -1) throw new Exception("You must choose any Dish");
                DataBase.RemoveDishes(CurrentDishId.ToString());
                MessageBox.Show("Removed!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void calculateDishPrice_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (CurrentDishId == -1) throw new Exception("You must choose any Dish");
                MessageBox.Show($"Price of your dish: { DataBase.PriceDish(CurrentDishId.ToString())}!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void calculateDishCalorie_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (CurrentDishId == -1) throw new Exception("You must choose any Dish");
                MessageBox.Show($"Calories in your dish: { DataBase.CaloriesDish(CurrentDishId.ToString())}!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void currentMenuLabel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            new ChooseWindow(this, 2).Show();
        }

        private void deleteMenu_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (CurrentMenuId == -1) throw new Exception("You must choose any Menu");
                DataBase.RemoveMenu(CurrentMenuId.ToString());
                MessageBox.Show("Removed!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void addMenu_Click(object sender, RoutedEventArgs e)
        {
            new Menu().Show();
        }

        private void GetMenuInfo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (CurrentMenuId == -1) throw new Exception("You must choose any Menu");
                var Flex = DataBase.GetInfo("menu", "id_menu", CurrentMenuId);
                MessageBox.Show($"Info for {Flex[1]}:\nDietic:    {Flex[2]}\nVegan:    {Flex[3]}\nFull:   {Flex[4]}");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
