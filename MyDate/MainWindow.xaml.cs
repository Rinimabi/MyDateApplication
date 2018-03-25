using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;

namespace MyDate
{
    /// <summary>
    /// MainWindow
    /// </summary>
    public partial class MainWindow : Window
    {
        List<DateClass> listDate = new List<DateClass>();
        Controller controller = null;
        public MainWindow()
        {
            InitializeComponent();
            getData();
            controller = new Controller();
            this.editCommon.SetBinding(TextBox.TextProperty, new Binding("Common") { Source = controller });
            this.editSite.SetBinding(TextBox.TextProperty, new Binding("Site") { Source = controller });
            //this.txtContent.SetBinding(TextBox.TextProperty, new Binding("TxtContentData") { Source = data });
            //this.txtTitle.SetBinding(TextBox.TextProperty, new Binding("TxtTitle") { Source = data });  
        }
        //获取xml存档
        public void getData()
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load("Date.xml");
            XmlNode xmlNode = xmlDocument.SelectSingleNode("Main");
            XmlNodeList xmlNodeList = xmlNode.ChildNodes;
            foreach (XmlNode xn1 in xmlNodeList)
            {
                List<Data> listData = new List<Data>();
                DateClass dateClass = new DateClass();
                XmlElement xe = (XmlElement)xn1;
                dateClass.Date = xe.GetAttribute("date").ToString();
                XmlNodeList xnl0 = xe.ChildNodes;
                for (int i = 0; i < xnl0.Count; i++)
                {
                    Data data = new Data();
                    data.Common = xnl0.Item(i).InnerText + "   ";
                    XmlElement xe1 = (XmlElement)xnl0.Item(i);
                    data.Id = xe1.GetAttribute("id") + ". ";
                    data.Site = xe1.GetAttribute("site");
                    if (!string.IsNullOrWhiteSpace(data.Site))
                    {
                        data.IsSite = "Visible";
                    }
                    listData.Add(data);
                }
                dateClass.DataList = listData;
                listDate.Add(dateClass);
            }
            this.dateListBox.ItemsSource = listDate;
        }
        public void dateListBox_Selected(object sender, SelectionChangedEventArgs e)
        {
            if (this.dateListBox.ItemsSource != null)
            {
                this.commonListBox.ItemsSource = listDate[this.dateListBox.SelectedIndex].DataList;
            }
        }
        public void commonListBox_Selected(object sender, SelectionChangedEventArgs e)
        {
            if (commonListBox.SelectedIndex != -1)
            {
                controller.Common = listDate[this.dateListBox.SelectedIndex].DataList[commonListBox.SelectedIndex].Common;
                controller.Site = listDate[this.dateListBox.SelectedIndex].DataList[commonListBox.SelectedIndex].Site;
                if (!string.IsNullOrWhiteSpace(listDate[this.dateListBox.SelectedIndex].DataList[commonListBox.SelectedIndex].Site))
                {
                    System.Diagnostics.Process.Start("explorer.exe", listDate[this.dateListBox.SelectedIndex].DataList[commonListBox.SelectedIndex].Site);
                }
            }
        }
        private void add_Click(object sender, System.EventArgs e)
        {
            DateTime dt = DateTime.Now;
            string YMD = dt.ToString("yyyy.MM.dd");
            if (YMD.Equals(listDate[listDate.Count - 1].Date))
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load("Date.xml");
                XmlNode xmlNode = xmlDocument.SelectSingleNode("Main");
                XmlNodeList xmlNodeList = xmlNode.ChildNodes;
                XmlNode xn1 = xmlNodeList.Item(xmlNodeList.Count - 1);
                XmlElement xe = (XmlElement)xn1;
                XmlNodeList xnl0 = xe.ChildNodes;
                string date = xe.GetAttribute("date").ToString().Trim();
                if (date.Equals(YMD))
                {
                    XmlElement xelAuthor = xmlDocument.CreateElement("commen");
                    xelAuthor.InnerText = controller.Common;
                    xelAuthor.SetAttribute("id", (xnl0.Count+1).ToString());
                    xelAuthor.SetAttribute("site", controller.Site);
                    xe.AppendChild(xelAuthor);
                    xmlDocument.Save("Date.xml");
                    MessageBox.Show("+1");
                }
            }
            else
            {
                XmlDocument doc = new XmlDocument();
                doc.Load("Date.xml");
                XmlNode root = doc.SelectSingleNode("Main");
                XmlElement xelKey = doc.CreateElement("Date");
                XmlAttribute xelType = doc.CreateAttribute("date");
                xelType.InnerText = YMD;
                xelKey.SetAttributeNode(xelType);
                XmlElement xelAuthor = doc.CreateElement("commen");
                xelAuthor.InnerText = controller.Common;
                xelAuthor.SetAttribute("id", "1");
                xelAuthor.SetAttribute("site", controller.Site);
                xelKey.AppendChild(xelAuthor);
                root.AppendChild(xelKey);
                doc.Save("Date.xml");
                MessageBox.Show("新增");
            }
            //保存预先点击的时间节点
            int SelectedIndex = this.dateListBox.SelectedIndex;
            //注意，必须先置空，否则不更新
            this.dateListBox.ItemsSource = null;
            listDate.Clear();
            getData();
            this.dateListBox.SelectedIndex = SelectedIndex;
        }
        private void edit_Click(object sender, System.EventArgs e)
        {
            getData();
        }
    }
}
