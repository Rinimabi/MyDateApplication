using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDate
{
    //总资源类
    public class DateClass : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string prop)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }
        //日期
        private string _date;
        public string Date
        {
            get { return _date; }
            set
            {
                _date = value;
                OnPropertyChanged("Date");
            }
        }
        //当天的存档
        private List<Data> _dataList;
        public List<Data> DataList
        {
            get { return _dataList; }
            set
            {
                _dataList = value;
                OnPropertyChanged("DataList");
            }
        }
    }
}
