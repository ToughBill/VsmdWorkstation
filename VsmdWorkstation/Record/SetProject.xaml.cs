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

namespace VsmdWorkstation.Record
{
    /// <summary>
    /// SetProject.xaml 的交互逻辑
    /// </summary>
    public partial class SetProject : Window
    {
        ProjectInfoCollection prjInfoCollection;
        public SetProject()
        {
            InitializeComponent();
            this.Loaded += SetProject_Loaded;
        }

        private void SetProject_Loaded(object sender, RoutedEventArgs e)
        {
            prjInfoCollection = ProjectInfoCollection.Load();
            this.DataContext = prjInfoCollection;
        }

        private void BtnAddName_Click(object sender, RoutedEventArgs e)
        {
            prjInfoCollection.Add();
        }

        private void BtnRemoveName_Click(object sender, RoutedEventArgs e)
        {
            prjInfoCollection.Remove();
        }

        private void BtnConfirm_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("保存成功！");
            prjInfoCollection.Save();
            this.Close();
        }
    }
}
