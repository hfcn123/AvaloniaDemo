
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using Avalonia.Controls;
using Avalonia.Media;
using ReactiveUI;
using Avalonia.Collections;
using MessageBox.Avalonia.DTO;
using Microsoft.CodeAnalysis.Operations;
using System.Data;
using Avalonia.Data;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using Microsoft.Win32.SafeHandles;
using System;
using System.Configuration;
using AvaloniaApplicationDemo.Views;
using MessageBox.Avalonia.ViewModels.Commands;
using System.Windows.Input;

namespace AvaloniaApplicationDemo.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public ReactiveCommand<Button,Unit> btncom { get; }
        public string Greeting = "Welcome to Avalonia!";
        public string FirstDemo = "下拉框";
        public string UName = "用户名";
        public string Upwds = "密码";

        #region 查询表
        

        private DataTable dbo = null;
        public DataTable Dbo { get => dbo; set => this.RaiseAndSetIfChanged(ref dbo, value); }

        #endregion

        ////定义下拉框数据
        private List<string> comboxItems =new List<string>() { "测试1","测试2","测试3"};
        public List<string> ComboxItems
        {
            get => comboxItems;
            set => this.RaiseAndSetIfChanged(ref comboxItems, value);
        }
        //用户名文本框
        public string uTXTName = "";
        public string UTXTName
        {
            get=>uTXTName; 
            set=>this.RaiseAndSetIfChanged(ref uTXTName,value);
        }
        //密码文本框
        public string uTxtpwd = "";

        public string UTxtpwd
        {
            get => uTxtpwd;
            set => this.RaiseAndSetIfChanged(ref uTxtpwd, value);
        }
        // 下拉框数据  构造了一个集合
        public ObservableCollection<jihe> MyList { get; } = new ObservableCollection<jihe>(){ };
        //构造方法（无参）
        public MainWindowViewModel()
        {
            string sql = "select * from names;";
            DataSet ds = DBHelper.CX(sql);
            btncom = ReactiveCommand.Create<Button>(btn);
            var c = new jihe();
            //获取数据源
            List<string> name = new List<string>(){ ds.Tables[0].Rows[0].Field<string>("mingzi").ToString() };
            foreach (var a in name)
            {
                c = new jihe();
                c.name = a;
                MyList.Add(c);
            }
            // 
            Dbo = ds.Tables[0];
        }
        #region 按钮事件
        //按钮事件
        public void btn(Button b)
        {
            string sql = "select * from names where mingzi='{0}';";
            sql = String.Format(sql, UTXTName);
            DataSet ds = DBHelper.CX(sql);
            if (ds.Tables[0].Rows.Count == 0)
            {
                var mes = MessageBox.Avalonia.MessageBoxManager
                     .GetMessageBoxStandardWindow(new MessageBoxStandardParams
                     {
                         ContentTitle = "提醒",
                         ContentHeader = "该用户不存在！/The user does not exist/"
                     });
                mes.Show();
            }
            else
            {

                string pwd = ds.Tables[0].Rows[0].Field<string>(columnName: "pwd");
                if (pwd.Equals(UTxtpwd.ToString()))
                {
                    var mes = MessageBox.Avalonia.MessageBoxManager
                      .GetMessageBoxStandardWindow(new MessageBoxStandardParams
                      {
                          ContentTitle = "提醒",
                          ContentHeader = "Successful login！"

                      });
                
                    mes.Show();

                    TestWindow1 window1 = new TestWindow1();
                   window1.Show();
                   
                }
                else
                {
                    var mes = MessageBox.Avalonia.MessageBoxManager
                          .GetMessageBoxStandardWindow(new MessageBoxStandardParams
                          {
                              ContentTitle = "提醒",
                              ContentHeader = "Wrong password!"
                          });
                    mes.Show();
                }
            }
        }
        #endregion
        #region 连接数据库

        public class DBHelper
        {
            //string str = "Data Source=.;Initial Catalog=Logininfo;Uid=root;Pwd=henghua123;";

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
            #endregion
           
        #region 查询验证用户（未调用
            //public static int DBSelct(string sql)
            //{
            //    //货车  把数据库里的数据放到dataset里
            //    MySqlDataAdapter ada = new MySqlDataAdapter(sql, str);
            //    if (ada == null)
            //    {
            //        return 0;
            //    }
            //    else
            //    {
            //        //2、把数据放到DataSet对象里
            //        DataSet ds = new DataSet();
            //        ada.Fill(ds);
            //        return 1;
            //    }
            //} 
            #endregion

        #region 查询方法
            // 查询-方法
            public static DataSet CX(string sql)
            {
                //string str = "Data Source=.;Initial Catalog=logininfo;Uid=root;Pwd=henghua;";
                //1、连接数据库的字符串
                  //str = "Data Source=172.16.0.235; port:3306; Initial Catalog=Logininfo;Uid=root;Pwd=henghua123";
                //货车 把数据库里的数据放到dataset里
                //MySqlDataAdapter ada = new MySqlDataAdapter(sql, str);
                //2、把数据放到DataSet对象里
                //DataSet ds = new DataSet();
                //ada.Fill(ds);
                //return ds;
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

            #endregion

        }
        //定义一个”学生“对象
        object content = new student("三","张");
        //把学生对象封装在Content 属性中
        public object Content
        {
            get => content;//获取content
            set => this.RaiseAndSetIfChanged(ref content, value);//重新赋值给Content (相当于修改)
        }
    }
    //定义一个集合类
    public class jihe
    { 
        public string name { get; set; }
    }
    //定义一个学生类
    public class student
    {
        //构造一个带参数的构造函数
        public student(string firstname,string lastname)
        {
            FirstName = firstname;
            LastName = lastname;
        }

        public string FirstName { get; }
        public string LastName { get; }   
    }
}