using System;
using System.Collections.Generic;
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
using VsmdWorkstation.Utils;

namespace VsmdWorkstation.Record
{
    /// <summary>
    /// SetProject.xaml 的交互逻辑
    /// </summary>
    public partial class SetProject : Window
    {
        public SetProject()
        {
            InitializeComponent();
            this.Loaded += SetProject_Loaded;
        }

        private void SetProject_Loaded(object sender, RoutedEventArgs e)
        {
           
            this.DataContext = GlobalVars.Instance.PrjInfoCollection;
        }

        private void BtnAddName_Click(object sender, RoutedEventArgs e)
        {
            GlobalVars.Instance.PrjInfoCollection.Add();
        }

        private void BtnRemoveName_Click(object sender, RoutedEventArgs e)
        {
            GlobalVars.Instance.PrjInfoCollection.Remove();
        }

        private void BtnConfirm_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("保存成功！");
            GlobalVars.Instance.PrjInfoCollection.Save();
            this.Close();
        }
    }
}
