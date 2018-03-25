using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDate
{
    public class Data : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string prop)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
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
        private string _isSite = "Hidden";
        public string IsSite
        {
            get { return _isSite; }
            set
            {
                _isSite = value;
                OnPropertyChanged("IsSite");
            }
        }
    }
}
