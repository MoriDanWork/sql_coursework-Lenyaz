using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeApp
{
    static class DataBase
    {

        static readonly SqlConnection Connection = new SqlConnection("Data Source=DEMONTINO-LAPTO\\FLEXBD;Initial Catalog=coursework;Integrated Security=True;MultipleActiveResultSets=True"); //подключение к sql

        public static void OpenConnection() { if (Connection.State == System.Data.ConnectionState.Closed) Connection.Open(); }
        public static void CloseConnection() { if (Connection.State == System.Data.ConnectionState.Open) Connection.Close(); }
        public static SqlConnection GetConnection() { return Connection; }

        public static void AddIngridientItem(string name, string proteins, string fats, string carbohydr, string calories, string price)
        {
            using SqlCommand command = new SqlCommand("insert into ingredients (ingredient_name, proteins, fats, carbohydr, calories, price) values (@name, @proteins, @fats, @carbohydr, @calories, @price)", Connection);
            command.Parameters.Add("@name", SqlDbType.VarChar).Value = name;
            command.Parameters.Add("@proteins", SqlDbType.VarChar).Value = proteins;
            command.Parameters.Add("@fats", SqlDbType.VarChar).Value = fats;
            command.Parameters.Add("@carbohydr", SqlDbType.VarChar).Value = carbohydr;
            command.Parameters.Add("@calories", SqlDbType.VarChar).Value = calories;
            command.Parameters.Add("@price", SqlDbType.VarChar).Value = price;

            command.ExecuteNonQuery();
        }

        public static string AddDish(string name, string weight)
        {
            using SqlCommand command = new SqlCommand($"insert into contents (content_name, weight_info) values ('{name}', {weight})", Connection);
            command.ExecuteNonQuery();

            SqlDataAdapter adapter = new SqlDataAdapter(new SqlCommand($"select id_content from contents where content_name = '{name}'", Connection));
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            return ((DataRowView)dt.DefaultView[0])[0].ToString();
        }

        public static void IngredientsToContents(string IngridientID, string DishID)
        {
            using SqlCommand command = new SqlCommand($"insert into ingredients_in_contents (id_ingredient, id_content) values ('{IngridientID}', {DishID})", Connection);
            command.ExecuteNonQuery();
        }

        public static DataView FillDataGrid(string sqlCommand)
        {
            SqlDataAdapter adapter = new SqlDataAdapter(new SqlCommand(sqlCommand, Connection));
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            return dt.DefaultView;
        }

        public static void RemoveByID(string table, string idColumn, int id)
        {
            using SqlCommand command = new SqlCommand($"delete from {table} where {idColumn} = '{id}'", Connection);
            command.ExecuteNonQuery();
        }

        public static void RemoveDishes(string DishId)
        {
            using SqlCommand command = new SqlCommand($"delete from ingredients_in_contents where  id_content = '{DishId}'", Connection);
            command.ExecuteNonQuery();

            command.CommandText = $"delete from Contents where  id_content = '{DishId}'";
            command.ExecuteNonQuery();
        }

        public static DataRowView GetInfo(string table, string idColumn, int id)
        {
            SqlDataAdapter adapter = new SqlDataAdapter(new SqlCommand($"select * from {table} where {idColumn} = '{id}'", Connection));
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            return dt.DefaultView[0];
        }

        public static string PriceDish(string id)
        {
            SqlDataAdapter adapter = new SqlDataAdapter(new SqlCommand($"select Sum(price) from ingredients join ingredients_in_contents on ingredients.id_ingredient = ingredients_in_contents.id_ingredient join contents on ingredients_in_contents.id_content = contents.id_content where ingredients_in_contents.id_content = '{id}'", Connection));
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            return ((DataRowView)dt.DefaultView[0])[0].ToString();
        }

        public static string CaloriesDish(string id)
        {
            SqlDataAdapter adapter = new SqlDataAdapter(new SqlCommand($"select Sum(Calories) from ingredients join ingredients_in_contents on ingredients.id_ingredient = ingredients_in_contents.id_ingredient join contents on ingredients_in_contents.id_content = contents.id_content where ingredients_in_contents.id_content = '{id}'", Connection));
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            return ((DataRowView)dt.DefaultView[0])[0].ToString();
        }
    }
}
