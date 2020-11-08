using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ВД50_1_18_ОАиП_ШапошниковаДА_ИР
{
    class Database : Config 
    {
        public DataTable Select(string sql)
        {
            connection.Open();
            MySqlCommand command = new MySqlCommand(sql, connection);
            MySqlDataReader reader = command.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(reader);
            connection.Close();
            return dt;
        }
        public void Insert(string sql)
        {
            connection.Open();
            MySqlCommand command = new MySqlCommand(sql, connection); 
            command.ExecuteReader(); 
            connection.Close(); 
        }

    }
}

