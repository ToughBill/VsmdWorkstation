using EasySqlCe;
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
    /// DatePicker.xaml 的交互逻辑
    /// </summary>
    public partial class DatePicker : Window
    {
        public DateTime? SearchDate { get; set; }
        public DatePicker(SpecifyDateTimeDegree degree)
        {
            InitializeComponent();
            calendarCtl.DisplayMode = GetDisplayMode(degree);
           
        }

        private CalendarMode GetDisplayMode(SpecifyDateTimeDegree degree)
        {
            Dictionary<SpecifyDateTimeDegree, CalendarMode> dict = new Dictionary<SpecifyDateTimeDegree, CalendarMode>();
            dict.Add(SpecifyDateTimeDegree.ToYear, CalendarMode.Decade);
            dict.Add(SpecifyDateTimeDegree.ToMonth, CalendarMode.Year);
            dict.Add(SpecifyDateTimeDegree.ToDay, CalendarMode.Month);
            return dict[degree];
        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            SearchDate = calendarCtl.SelectedDate;
            this.Close();
        }
    }
}
