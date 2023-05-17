using MessageBox.Avalonia.DTO;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaApplicationDemo.Models
{
    public class DBHelper
    {
        //1、连接数据库的字符串
        //static string str = "Data Source=172.16.0.235; port:3306; Initial Catalog=Logininfo;Uid=root;Pwd=henghua123";
        static string str = "Data Source=.;Initial Catalog=Tstu;Integrated Security=True";//运行成功
                                                                                          //定义连接数据库的静态方法
        public static int DBfunZSG(string sql)
        {
            //2、连接数据库的对象
            MySqlConnection conn = new MySqlConnection(str); ;
            //异常处理机制
            try
            {  //可能出现异常的代码
               //3、打开数据库
                conn.Open();
                //4、执行sql语句
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                int i = cmd.ExecuteNonQuery();
                return i;
            }
            catch (System.Exception e)
            {
                //异常处理
                var mes = MessageBox.Avalonia.MessageBoxManager
                    .GetMessageBoxStandardWindow(new MessageBoxStandardParams
                    {
                        ContentTitle = "异常提醒",
                        ContentHeader = e.Message,
                    });
                mes.Show();
            }
            finally
            {
                //有没有异常都要执行的
                //5、关闭数据库
                conn.Close();
            }
            return -1;
        }

        public static DataSet CX(string sql)
        {
            try
            {
                SqlDataAdapter sad = new SqlDataAdapter(sql, str);
                //MySqlDataAdapter sad = new MySqlDataAdapter(sql,str);
                DataSet ds = new DataSet();
                sad.Fill(ds);
                return ds;
            }
            catch (Exception e)
            {
                //异常处理
                var mes = MessageBox.Avalonia.MessageBoxManager
                    .GetMessageBoxStandardWindow(new MessageBoxStandardParams
                    {
                        ContentTitle = "异常提醒",
                        ContentHeader = e.Message,
                    });
                mes.Show();
            }
            return null;

        }

    }
}
