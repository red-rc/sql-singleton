using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using WindowsFormsApp2.Service;
using WindowsFormsApp2.Entity;

namespace WindowsFormsApp2.Repository
{
    public class ProductRepository
    {
        string sql = "";
        public Product getById(int id)
        {
            Singleton.GetInstance().getConnection().Open();

            sql = "SELECT * " +
                  "FROM products " +
                  "WHERE productId = " + id;

            MySqlCommand command = new MySqlCommand(sql, Singleton.GetInstance().getConnection());

            MySqlDataReader myReader = command.ExecuteReader();

            try
            {
                myReader.Read();
                Product product = new Product
                {
                    id = Convert.ToInt32(myReader["productId"]),
                    categoryId = Convert.ToInt32(myReader["categoryId"]),
                    name = myReader["name"].ToString(),
                    priceInDollars = Convert.ToInt32(myReader["priceInDollars"]),
                    amount = Convert.ToInt32(myReader["amount"])
                };

                return product;
            }
            finally
            {
                myReader.Close();
                sql = "";
                Singleton.GetInstance().getConnection().Close();
            }
        }

        public List<Product> getMany(int start, int limit)
        {
            List<Product> list = new List<Product>();
            Singleton.GetInstance().getConnection().Open();

            sql = "SELECT * " +
                  "FROM products " +
                  $"LIMIT {start},{limit}";

            MySqlCommand command = new MySqlCommand(sql, Singleton.GetInstance().getConnection());

            MySqlDataReader myReader = command.ExecuteReader();

            try
            {
                while (myReader.Read())
                {
                    Product product = new Product
                    {
                        id = Convert.ToInt32(myReader["productId"]),
                        categoryId = Convert.ToInt32(myReader["categoryId"]),
                        name = myReader["name"].ToString(),
                        priceInDollars = Convert.ToInt32(myReader["priceInDollars"]),
                        amount = Convert.ToInt32(myReader["amount"])
                    };

                    list.Add(product);
                }

                return list;
            }
            finally
            {
                myReader.Close();
                sql = "";
                Singleton.GetInstance().getConnection().Close();
            }
        }
        public void createProduct(int categoryId, string name, int priceInDollars, int amount)
        {
            Singleton.GetInstance().getConnection().Open();

            sql = "INSERT INTO products(categoryId, name, priceInDollars, amount) " +
                "VALUES ('" + categoryId + "', '" + name + "', '" + priceInDollars + "', '" + amount + "')";

            MySqlCommand command = new MySqlCommand(sql, Singleton.GetInstance().getConnection());

            MySqlDataReader myReader = command.ExecuteReader();

            try
            {
                myReader.Read();
            }
            finally
            {
                myReader.Close();
                sql = "";
                Singleton.GetInstance().getConnection().Close();
            }
        }
        public void updateProduct(int id, int categoryId, string name, int priceInDollars, int amount)
        {
            Singleton.GetInstance().getConnection().Open();

            sql = $"UPDATE products SET categoryId = {categoryId}, " +
                $"name = '{name}', priceInDollars = {priceInDollars}, " +
                $"amount = {amount} WHERE productId = {id}";


            MySqlCommand command = new MySqlCommand(sql, Singleton.GetInstance().getConnection());

            MySqlDataReader myReader = command.ExecuteReader();

            try
            {
                myReader.Read();
            }
            finally
            {
                myReader.Close();
                sql = "";
                Singleton.GetInstance().getConnection().Close();
            }
        }
        public void deleteById(int id)
        {
            Singleton.GetInstance().getConnection().Open();

            sql = "DELETE " +
                  "FROM products " +
                  "WHERE productId = " + id;

            MySqlCommand command = new MySqlCommand(sql, Singleton.GetInstance().getConnection());

            MySqlDataReader myReader = command.ExecuteReader();

            try
            {
                myReader.Read();
            }
            finally
            {
                myReader.Close();
                sql = "";
                Singleton.GetInstance().getConnection().Close();
            }
        }
        public void deleteMany(int start, int limit)
        {
            Singleton.GetInstance().getConnection().Open();

            sql = $"DELETE FROM products WHERE productId BETWEEN {start} and {limit}";


            MySqlCommand command = new MySqlCommand(sql, Singleton.GetInstance().getConnection());

            MySqlDataReader myReader = command.ExecuteReader();

            try
            {
                myReader.Read();
            }
            finally
            {
                myReader.Close();
                sql = "";
                Singleton.GetInstance().getConnection().Close();
            }
        }
    }
}