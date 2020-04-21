using EasySqlCe;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VsmdWorkstation.Record
{
    class RunInfoModel : BindableBase
    {

        ObservableCollection<RunInfoTable> allRunInfos = new ObservableCollection<RunInfoTable>();
        RunInfoTable selectedRunInfo;
        DatabaseHelper databseHelper = new DatabaseHelper();
        public ObservableCollection<RunInfoTable> AllRunInfos
        {
            get
            {
                return allRunInfos;
            }
            set
            {
                allRunInfos = value;
            }
        }



        public RunInfoTable SelectedRunInfo
        {
            get
            {
                return selectedRunInfo;
            }
            set
            {
                SetProperty(ref selectedRunInfo, value);
            }
        }

        public void GetCertainRunInfos(string projectName, DateTime dateTime, SpecifyDateTimeDegree degree)
        {
            AllRunInfos.Clear();
            var runInfos = databseHelper.GetRunInfo(projectName, dateTime, degree);
            runInfos.ForEach(x=>AllRunInfos.Add(x));
            Debug.WriteLine(AllRunInfos.Count);
        }
        public void AddRunInfo(string prjName, int cnt)
        {
            var runInfo = new RunInfoTable(prjName, DateTime.Now, cnt);
            AllRunInfos.Add(runInfo);
            SelectedRunInfo = runInfo;
            string errMsg = "";
            databseHelper.Add(runInfo, ref errMsg);
        }
        public void DeleteRunInfo(RunInfoTable runInfoTable)
        {
            databseHelper.Delete(runInfoTable);
            AllRunInfos.Remove(runInfoTable);
        }

        public Dictionary<string,int>  Statistic()
        {
            Dictionary<string, int> eachProjectCnt = new Dictionary<string, int>();
            foreach (var runInfo in allRunInfos)
            {
                int cnt = runInfo.SampleCount == null ? 0 : (int)runInfo.SampleCount;
                if (eachProjectCnt.ContainsKey(runInfo.ProjectName))
                    eachProjectCnt[runInfo.ProjectName] += cnt;
                else
                    eachProjectCnt[runInfo.ProjectName] = cnt;
            }
            return eachProjectCnt;
        }
    }
}
