using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDate
{
    //控制类，储存要新增的存档
    public class Controller : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string prop)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }
        private string _isEditable = "Hidden";
        public string IsEditable
        {
            get { return _isEditable; }
            set
            {
                _isEditable = value;
                OnPropertyChanged("IsEditable");
            }
        }
        private string _common;
        public string Common
        {
            get { return _common; }
            set
            {
                _common = value;
                OnPropertyChanged("Common");
            }
        }

        private string _site;
        public string Site
        {
            get { return _site; }
            set
            {
                _site = value;
                OnPropertyChanged("Site");
            }
        }
        private string _id;
        public string Id
        {
            get { return _id; }
            set
            {
                _id = value;
                OnPropertyChanged("Id");
            }
        }
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
    }
}
