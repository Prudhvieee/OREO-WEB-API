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
    public class AdminRL : IAdminRL
    {
        public readonly IConfiguration configuration;
        public SqlConnection connection;
        public AdminRL(IConfiguration configuration)
        {
            this.configuration = configuration;
            connection = new SqlConnection(this.configuration.GetConnectionString("OreoDB"));
        }
        public static string Encryptdata(string password)
        {
            string strmsg = string.Empty;
            byte[] encode = new byte[password.Length];
            encode = Encoding.UTF8.GetBytes(password);
            strmsg = Convert.ToBase64String(encode);
            return strmsg;
        }
        public static string Decryptdata(string encryptpwd)
        {
            string decryptpwd = string.Empty;
            UTF8Encoding encodepwd = new UTF8Encoding();
            Decoder Decode = encodepwd.GetDecoder();
            byte[] todecode_byte = Convert.FromBase64String(encryptpwd);
            int charCount = Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
            char[] decoded_char = new char[charCount];
            Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
            decryptpwd = new String(decoded_char);
            return decryptpwd;
        }
        public bool RegisterAdmin(AdminRegister Admin)
        {
            try
            {
                using (this.connection)
                {
                    var password = Encryptdata(Admin.Password);
                    SqlCommand sqlCommand = new SqlCommand("SPregisterAdmin", this.connection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@Fullname", Admin.FullName);
                    sqlCommand.Parameters.AddWithValue("@Email", Admin.Email);
                    sqlCommand.Parameters.AddWithValue("@Password", password);
                    sqlCommand.Parameters.AddWithValue("@PhoneNumber", Admin.PhoneNumber);
                    sqlCommand.Parameters.AddWithValue("@Role", "Admin");
                    this.connection.Open();
                    int result = sqlCommand.ExecuteNonQuery();
                    if (result != 0)
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
        public AdminResponse AdminLogin(AdminLogin Admin)
        {
            AdminResponse registration = new AdminResponse();
            try
            {
                using (this.connection)
                {
                    var password = Encryptdata(Admin.Password);
                    SqlCommand sqlCommand = new SqlCommand("SPloginAdmin", this.connection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@Email", Admin.Email);
                    sqlCommand.Parameters.AddWithValue("@Password", password);
                    this.connection.Open();
                    SqlDataReader Reader = sqlCommand.ExecuteReader();
                    if (Reader.HasRows)
                    {
                        while (Reader.Read())
                        {
                            registration.AdminId = Reader.GetInt32(0);
                            registration.FullName = Reader.GetString(1);
                            registration.Email = Reader.GetString(2);
                            registration.Password = Reader.GetString(3);
                            registration.PhoneNumber = Reader.GetString(4);
                            registration.Role = Reader.GetString(5);
                        }
                        return registration;
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
    }
}
