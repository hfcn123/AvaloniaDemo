using Avalonia.Input;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AvaloniaApplicationDemo.ViewModels;
using AvaloniaApplicationDemo.Models;
using System.Data;
using System.Collections;
using System.Xml.Linq;
using System.Reflection.PortableExecutable;
using System.Collections.ObjectModel;
using DynamicData;
using Avalonia.Controls;
using System.Reactive;
using MessageBox.Avalonia.DTO;

namespace AvaloniaApplicationDemo.ViewModels
{
    public class TestWindow1ViewModel : ViewModelBase
    {

        //public event PropertyChangedEventHandler? PropertyChanged;
        private string _text = "123123";
        public string Text
        {
            get => _text;
            set => this.RaiseAndSetIfChanged(ref _text, value);
        }
        //搜索关键字
        private string searchName = null;
        public string SearchName { get => searchName; set => this.RaiseAndSetIfChanged(ref searchName, value); }

        //搜索 ，声明-注册-实现
        //按钮声明
        public ReactiveCommand<Button, Unit> btnSearch { get; }

        DataTable userinfo = null;
        public DataTable Userinfo
        {
            get => userinfo;
            set => this.RaiseAndSetIfChanged(ref userinfo, value);
        }
        public ObservableCollection<TstuDbo> MyList { get; } = new ObservableCollection<TstuDbo>() { };
        public TestWindow1ViewModel()
        {
            //按钮注册
            btnSearch = ReactiveCommand.Create<Button>(btn);
            string sql = "select * from names";
            var ds = DBHelper.CX(sql);
            TstuDbo tstuDbo = new TstuDbo();
            var list = new List<TstuDbo>();
            var tables = ds.Tables;
            foreach (DataTable table in tables)
            {
                if (table.Rows.Count > 0)
                {
                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                        var tsdbo = new TstuDbo
                        {
                            id = table.Rows[i].Field<int>("id"),
                            mingzi = table.Rows[i].Field<string>("mingzi"),
                            age = (int)table.Rows[i].Field<int>("age"),
                            sex = table.Rows[i].Field<string>("sex"),
                            Number = table.Rows[i].Field<string>("Number"),
                            adress = table.Rows[i].Field<string>("adress")
                        };
                        MyList.Add(tsdbo);
                        //list.Add(tsdbo);
                    }
                }
            }
        }
        //按钮的实现
        public void btn(Button button)
        {
            MyList.Clear();
            //模糊查询
            string sql = "select * from names where mingzi like '%{0}%'";
            sql = String.Format(sql, SearchName);
            DataSet ds = DBHelper.CX(sql);
            string sqlall = "select id,mingzi,age,sex,Number,adress from names";
            DataSet dsall = DBHelper.CX(sqlall);
            if (String.IsNullOrWhiteSpace(SearchName))
            {
    
                var tablesSeachall = dsall.Tables;
                foreach (DataTable tableall in tablesSeachall)
                {
                    if (tableall.Rows.Count > 0)
                    {
                        for (int i = 0; i < tableall.Rows.Count; i++)
                        {
                            var tsdbo = new TstuDbo
                            {
                                id = tableall.Rows[i].Field<int>("id"),
                                mingzi = tableall.Rows[i].Field<string>("mingzi"),
                                age = (int)tableall.Rows[i].Field<int>("age"),
                                sex = tableall.Rows[i].Field<string>("sex"),
                                Number = tableall.Rows[i].Field<string>("Number"),
                                adress = tableall.Rows[i].Field<string>("adress")
                            };
                            MyList.Add(tsdbo);
                        }
                    }
                }
            }
            else
            {

                if (ds.Tables[0].Rows.Count == 0)
                {
                    var mes = MessageBox.Avalonia.MessageBoxManager
                         .GetMessageBoxStandardWindow(new MessageBoxStandardParams
                         {
                             ContentTitle = "提醒",
                             ContentHeader = "未查询到包含" + SearchName + "的任何信息！Not found "
                         });
                    mes.Show();
                }
                else
                {
                    MyList.Clear();
                    var tablesSeach = ds.Tables;
                    foreach (DataTable table in tablesSeach)
                    {
                        if (table.Rows.Count > 0)
                        {
                            for (int i = 0; i < table.Rows.Count; i++)
                            {
                                var tsdbo = new TstuDbo
                                {
                                    id = table.Rows[i].Field<int>("id"),
                                    mingzi = table.Rows[i].Field<string>("mingzi"),
                                    age = (int)table.Rows[i].Field<int>("age"),
                                    sex = table.Rows[i].Field<string>("sex"),
                                    Number = table.Rows[i].Field<string>("Number"),
                                    adress = table.Rows[i].Field<string>("adress")
                                };
                                MyList.Add(tsdbo);
                              
                                //list.Add(tsdbo);
                            }
                        }
                    }
                }

            }

                }
            }
        }
    

