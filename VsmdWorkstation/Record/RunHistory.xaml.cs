using System;
using System.Collections.Generic;
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
using EasySqlCe;
using VsmdWorkstation.Utils;

namespace VsmdWorkstation.Record
{
    /// <summary>
    /// RunHistory.xaml 的交互逻辑
    /// </summary>
    public partial class RunHistory : Window
    {
        RunInfoModel runInfoModel = new RunInfoModel();
        DateTime dateTime = DateTime.Now;
        
        TimeAccuracy timeAccuracy = new TimeAccuracy();
       
        public RunHistory()
        {
            InitializeComponent();
            this.Loaded += RunHistory_Loaded;
            this.PreviewKeyUp += RunHistory_PreviewKeyUp;
        }

        private void RunHistory_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Delete)
            {
                if(runInfoModel.SelectedRunInfo == null)
                {
                    SetInfo("请先选中一条记录！");
                }
                else
                {
                    var res = MessageBox.Show("确定删除？", "警告", MessageBoxButton.YesNo);
                    if(res == MessageBoxResult.Yes)
                    {
                        runInfoModel.DeleteRunInfo(runInfoModel.SelectedRunInfo);
                        BtnSearch_Click(this, null);
                    }
                }
            }
        }

        private void RunHistory_Loaded(object sender, RoutedEventArgs e)
        {
            
            cmbProjects.DataContext = GlobalVars.Instance.PrjInfoCollection;
            runInfoModel.GetCertainRunInfos(GlobalVars.Instance.PrjInfoCollection.SelectedProjectInfo.Name, dateTime, EasySqlCe.SpecifyDateTimeDegree.ToDay);
            //lstviewResult.DataContext = 
            txtCurrentDate.Text = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            lstviewResult.DataContext = runInfoModel;
            
            cmbDegree.DataContext = timeAccuracy;

        }

        private void btnSetTime_Click(object sender, RoutedEventArgs e)
        {
            DatePicker datePicker = new DatePicker(timeAccuracy.GetSpecifyDegree());
            datePicker.ShowDialog();
            if (datePicker.SearchDate == null)
                return;
            dateTime = (DateTime)datePicker.SearchDate;
            txtCurrentDate.Text = dateTime.ToString("yyyy-MM-dd hh:mm:ss");
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            string prjName = ((bool)chkAllProjects.IsChecked) ? null : GlobalVars.Instance.PrjInfoCollection.SelectedProjectInfo.Name;
            runInfoModel.GetCertainRunInfos(prjName, dateTime,timeAccuracy.GetSpecifyDegree());
            var dict = runInfoModel.Statistic();
            MainViewModel mainViewModel = new MainViewModel(dict);
            oxyPlotPieChart.DataContext = mainViewModel;
            txtTotalCnt.Text = dict.Sum(x => x.Value).ToString();
        }

        private void BtnExport_Click(object sender, RoutedEventArgs e)
        {
            var dict = runInfoModel.Statistic();
            string exportFile = FolderHelper.GetOutputFolder() + "statistic.csv";
            List<string> strs = new List<string>();
            strs.Add("项目名,数量");
            foreach(var pair in dict)
            {
                strs.Add(string.Format("{0},{1}", pair.Key, pair.Value));
            }
            
            File.WriteAllLines(exportFile, strs);
            SetInfo(string.Format("结果保存到{0}", exportFile));
        }


        void SetInfo(string s)
        {
            txtHint.Text = s;
        }

        private void BtnOpenFolder_Click(object sender, RoutedEventArgs e)
        {
            var folder = FolderHelper.GetOutputFolder();
            System.Diagnostics.Process.Start(folder);
        }
    }



    class TimeAccuracy:BindableBase
    {
        List<string> degrees = new List<string>() { "年", "月","日" };
        string selectedDegree = "日";
        public List<string> Degrees { get
            {
                return degrees;
            }
        }

        

        public string SelectedDegree
        {
            get
            {
                return selectedDegree;
            }
            set
            {
                SetProperty(ref selectedDegree, value);
            }
        }

        internal SpecifyDateTimeDegree GetSpecifyDegree()
        {
            SpecifyDateTimeDegree degree = SpecifyDateTimeDegree.ToDay;
            switch (selectedDegree)
            {
                case "月":
                    degree = SpecifyDateTimeDegree.ToMonth;
                    break;
                case "年":
                    degree = SpecifyDateTimeDegree.ToYear;
                    break;
                default:
                    break;
            }
            return degree;
        }
    }
}
