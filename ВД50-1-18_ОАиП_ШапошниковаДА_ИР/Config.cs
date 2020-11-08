using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace ВД50_1_18_ОАиП_ШапошниковаДА_ИР
{
    class Config
    {
        public MySqlConnection connection = new MySqlConnection("server=localhost; user=root;password=;database=db23;port=3306");
        public string GetValue(object Object, string Key)
        {
          string Value = Object.GetType().GetProperty(Key).GetValue(Object, null).ToString(); 
          return Value;
        }
          public async System.Threading.Tasks.Task Alert(string Message, string Title)
        { 
          var messageDialog = new MessageDialog(Message, Title); 
          await messageDialog.ShowAsync();
        }
        public Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
        public string salt = "encryption";
        string Hash(string input)
        {
            byte[] asciiBytes = ASCIIEncoding.ASCII.GetBytes(input + salt); byte[] hashedBytes = MD5CryptoServiceProvider.Create().ComputeHash(asciiBytes);
            string hashedString = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            return hashedString;
        }
        public void Log(int user, int action)
        {
            Database data_base = new Database();
            string sql = "INSERT INTO `logs` (`id_user`, `id_action`) VALUES ('" + user + "', '" + action + "')";
            data_base.Insert(sql); 
        }
        public string GenerateToken(int user)
        {
            Database data_base = new Database();
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789"; string token = new string(Enumerable.Repeat(chars, 64).Select(s => s[random.Next(s.Length)]).ToArray());
            string sql = "INSERT INTO `tokens` (`id_user`, `token`) VALUES ("+ user + ", '" + token + "')";
            data_base.Insert(sql);
            Log(user, 1);
            return token;
        }

    }

    }
