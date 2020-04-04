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
using EasySqlCe;

namespace VsmdWorkstation.Record
{
    /// <summary>
    /// RunHistory.xaml 的交互逻辑
    /// </summary>
    public partial class RunHistory : Window
    {
        RunInfoModel runInfoModel = new RunInfoModel();
        DateTime dateTime = DateTime.Now;
        ProjectInfoCollection prjInfoCollection;
        TimeAccuracy timeAccuracy = new TimeAccuracy();
        public RunHistory()
        {
            InitializeComponent();
            this.Loaded += RunHistory_Loaded;
        }

        private void RunHistory_Loaded(object sender, RoutedEventArgs e)
        {
            prjInfoCollection = ProjectInfoCollection.Load();
            cmbProjects.DataContext = prjInfoCollection;
            runInfoModel.GetCertainRunInfos(prjInfoCollection.SelectedProjectInfo.Name, dateTime, EasySqlCe.SpecifyDateTimeDegree.ToDay);
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
           
            runInfoModel.GetCertainRunInfos(prjInfoCollection.SelectedProjectInfo.Name, dateTime,timeAccuracy.GetSpecifyDegree());
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
