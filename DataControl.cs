using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SQLite;
using System.Data.Common;

namespace qiquanui
{
    class DataControl
    {
       //static string dataPath = "E:/Data.sqlite;";

        static string dataPath = "db/Data.sqlite;";    //相对路径
 

        //////////////////////////////查询 
        public static DataTable QueryTable(string queryStr)
        {
            DataTable dt = new DataTable();
            try
            {
                SQLiteConnection conn = new SQLiteConnection("Data Source="+dataPath);
                conn.Open();
                SQLiteCommand cmd = new SQLiteCommand(conn);
                //cmd.CommandText = "SELECT * FROM Book";
                cmd.CommandText = queryStr;
                cmd.CommandType = CommandType.Text;
                //Console.WriteLine(cmd.CommandText);  
                SQLiteDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dt.Load(dr);
                }
                else
                {
                    //throw new NullReferenceException("No Record Available.");  
                }

                dr.Close();
                conn.Close();

            }
            catch (ArgumentException ae)
            {
                Console.WriteLine(ae.Message + " \n\n" + ae.Source + "\n\n" + ae.StackTrace + "\n\n" + ae.Data);
            }
            catch (Exception ex)
            {
                //throw new Exception(ex.Message);  
                Console.WriteLine(ex.Message + " \n\n" + ex.Source + "\n\n" + ex.StackTrace + "\n\n" + ex.Data);
            }

            return dt;
        }

        public static void InsertOrUpdate(string _sql)
        {
            SQLiteConnection conn = new SQLiteConnection("Data Source=" + dataPath);
            conn.Open();
            SQLiteCommand cmd = new SQLiteCommand(conn);

            try
            {
                cmd.CommandText = _sql;
                cmd.ExecuteNonQuery();
               
            }
            catch (SQLiteException se)
            {
               // dbTrans.Rollback();
                 Console.WriteLine(se.Message + " \n\n" + se.Source + "\n\n" + se.StackTrace + "\n\n" + se.Data + " \n");
                //return false;
            }
            catch (ArgumentException ae)
            {
                //dbTrans.Rollback();
                Console.WriteLine(ae.Message + " \n\n" + ae.Source + "\n\n" + ae.StackTrace + "\n\n" + ae.Data + " \n");
                // return false;
            }
            catch (Exception ex)
            {
                //dbTrans.Rollback();
                //Do　any　logging　operation　here　if　necessary  
               Console.WriteLine (ex.Message + "\n\n" + ex.Source + "\n\n" + ex.StackTrace + "\n\n" + ex.Data + " \n");
                // return false;
            }
            catch
            {
                //dbTrans.Rollback();
                Console.WriteLine("Fail");   
            }
        }



    }
}
