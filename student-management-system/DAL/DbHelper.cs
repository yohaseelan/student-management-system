using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace student_management_system.DAL
{
    internal class DbHelper
    {
        private static readonly string connectionString = ConfigurationManager.ConnectionStrings["MySqlStudentDB"].ConnectionString;


        public static MySqlConnection GetConnection()
        {
            return new MySqlConnection(connectionString);
        }


        public static DataTable GetData(string query, params MySqlParameter[] parameters)
        {
            using (var connection = GetConnection())
            using (var command = new MySqlCommand(query, connection))
            {
                if (parameters != null)
                    command.Parameters.AddRange(parameters);

                var adapter = new MySqlDataAdapter(command);
                var dataTable = new DataTable();
                adapter.Fill(dataTable);
                return dataTable;
            }
        }


        public static int ExecuteNonQuery(string query, params MySqlParameter[] parameters)
        {
            using (var connection = GetConnection())
            using (var command = new MySqlCommand(query, connection))
            {
                connection.Open();
                if (parameters != null)
                    command.Parameters.AddRange(parameters);

                return command.ExecuteNonQuery();
            }
        }


        public static object ExecuteScalar(string query, params MySqlParameter[] parameters)
        {
            using (var connection = GetConnection())
            using (var command = new MySqlCommand(query, connection))
            {
                connection.Open();
                if (parameters != null)
                    command.Parameters.AddRange(parameters);

                return command.ExecuteScalar();
            }
        }

    }
}
