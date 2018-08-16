using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace ULib
{
    //v = 6.9.13
  public class MySqlManager
    {
        string connectionString;
        public MySqlManager(string ServerName,string userid,string password,string database)
        {
            connectionString 
            =
            "server=" + ServerName + ";" +
            "userid=" + userid + ";" +
            "password=" + password + ";" +
            "database=" + database + ";";

            testConnection();
        }
      public  MySqlConnection MySqlConnection;
         void testConnection()
         {
            try
            {
                MySqlConnection = new MySqlConnection(connectionString);
                MySqlConnection.Open();
            } catch(MySqlException)
            {
                Rocket.Core.Logging.Logger.Log("Ulib : MySql Connection Failed",ConsoleColor.Red);
            } finally
            {
                MySqlConnection.Close();
            }
        }
        MySqlConnection MakeConnection()
        {
            MySqlConnection = new MySqlConnection(connectionString);
            return MySqlConnection;
        }
        public delegate string MySqlCommandReturnDelegate();
        public void MySqlCommand(string Querry)
        {
            var connection = MakeConnection();
            MySqlCommand cmd = new MySqlCommand(Querry, connection);
            connection.Open();
           
                cmd.ExecuteNonQuery();
                connection.Close();
        }
        public object[] MySqlCommandR(string querry)
        {
            
            var connection = MakeConnection();
            MySqlCommand cmd = new MySqlCommand(querry, connection);
            connection.Open();

            using (var reader = cmd.ExecuteReader())
            {
                object[] obj = new object[reader.FieldCount];
                while (reader.Read())
                {

                    reader.GetValues(obj);
                }
                connection.Close();
                return obj;
                
                
            }
        }
        public bool Exsists(string Table, decimal value, string col)
        {
            string sqlString = "Select "+ col + " FROM " + Table + " where " + col +"= " + value;
            var connection = MakeConnection();
            MySqlCommand cmd = new MySqlCommand(sqlString, MySqlConnection);
            connection.Open();
            using (var reader = cmd.ExecuteReader()) {
                if (reader.Read()) {
                    return true;
                }
                else
                {
                    return false;
                }

            }           
        }
        public bool Exsists(string Table, float value, string col)
        {
            string sqlString = "Select " + col + " FROM " + Table + " where " + col + "= " + value;
            var connection = MakeConnection();
            MySqlCommand cmd = new MySqlCommand(sqlString, MySqlConnection);
            connection.Open();
            using (var reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
        }
        public bool Exsists(string Table, ulong value, string col)
        {
            string sqlString = "Select " + col + " FROM " + Table + " where " + col + "= " + value;
            var connection = MakeConnection();
            MySqlCommand cmd = new MySqlCommand(sqlString, MySqlConnection);
            connection.Open();
            using (var reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
        }
        public bool Exsists(string Table, string value, string col)
        {
            string sqlString = "Select " + col + " FROM " + Table + " where " + col + "= '" + value.ToLower().Replace(" ",string.Empty)+ "'";
            var connection = MakeConnection();
            MySqlCommand cmd = new MySqlCommand(sqlString, MySqlConnection);
            connection.Open();
            using (var reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
        }
    }
}
