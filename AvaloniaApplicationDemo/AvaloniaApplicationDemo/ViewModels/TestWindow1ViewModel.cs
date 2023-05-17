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

namespace AvaloniaApplicationDemo.ViewModels
{
    public class TestWindow1ViewModel : ViewModelBase
    {
  
        //public event PropertyChangedEventHandler? PropertyChanged;
        private string _text ="123123";
        public string Text
        {
            get => _text;
            set => this.RaiseAndSetIfChanged(ref _text, value);
        }
        DataTable userinfo = null;
        public DataTable Userinfo
        {
            get => userinfo;
            set => this.RaiseAndSetIfChanged(ref userinfo, value);
        }
        public ObservableCollection<TstuDbo> MyList { get; } = new ObservableCollection<TstuDbo>() { };
        public TestWindow1ViewModel()
        {         
            string sql = "select * from names";
            var ds = DBHelper.CX(sql);
            TstuDbo tstuDbo = new TstuDbo();
            var list = new List<TstuDbo>();
            var tables = ds.Tables;
            foreach (DataTable table in tables)
            {
                if (table.Rows.Count > 0)
                {
                    var tsdbo = new TstuDbo
                    {
                        id = table.Rows[0].Field<int>("id"),
                        mingzi = table.Rows[0].Field<string>("mingzi")
                    };
                    MyList.Add(tsdbo);
                    //list.Add(tsdbo);
                }
            }
        }   
    }
}
