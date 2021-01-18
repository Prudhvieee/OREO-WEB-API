using Microsoft.Extensions.Configuration;
using OreoAppCommonLayer.Model;
using OreoAppRepositoryLayer.IRepository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace OreoAppRepositoryLayer.Repository
{
    public class ProductRL : IProductRL
    {
        public readonly IConfiguration configuration;
        public SqlConnection connection;
        public ProductRL(IConfiguration configuration)
        {
            this.configuration = configuration;
            connection = new SqlConnection(this.configuration.GetConnectionString("OreoDB"));
        }
        public List<Product> GetAllProducts()
        {
            List<Product> productList = new List<Product>();
            try
            {
                using (this.connection)
                {
                    SqlCommand sqlCommand = new SqlCommand("SPproduct", this.connection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    this.connection.Open();
                    SqlDataReader Reader = sqlCommand.ExecuteReader();
                    if (Reader.HasRows)
                    {
                        while (Reader.Read())
                        {
                            Product product = new Product();
                            product.ProductId = Reader.GetInt32(0);
                            product.ProductName = Reader.GetString(1);
                            product.ProductImage = Reader.GetString(2);
                            product.ProductQuantity = Reader.GetInt32(3);
                            product.ActualPrice = Reader.GetDouble(4);
                            product.DiscountedPrice = Reader.GetDouble(5);
                            product.AddedToCart = Reader.GetBoolean(6);
                            productList.Add(product);
                        }
                        return productList;
                    }
                }
                return null;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                this.connection.Close();
            }
        }
        public bool AddProduct(Product product)
        {
            try
            {
                using (this.connection)
                {
                    SqlCommand sqlCommand = new SqlCommand("SPaddProduct", this.connection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@ProductName", product.ProductName);
                    sqlCommand.Parameters.AddWithValue("@ProductImage", product.ProductImage);
                    sqlCommand.Parameters.AddWithValue("@ProductQuantity", product.ProductQuantity);
                    sqlCommand.Parameters.AddWithValue("@ActualPrice", product.ActualPrice);
                    sqlCommand.Parameters.AddWithValue("@DiscountedPrice", product.DiscountedPrice);
                    sqlCommand.Parameters.AddWithValue("@AddedToCart", product.AddedToCart);
                    this.connection.Open();
                    int result = sqlCommand.ExecuteNonQuery();
                    if(result != 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception e)
            {

                throw e;
            }
            finally
            {
                this.connection.Close();
            }
        }
    }
}
