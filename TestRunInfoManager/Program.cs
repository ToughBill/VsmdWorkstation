using EasySqlCe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestRunInfoManager
{
    class Program
    {
        static void Main(string[] args)
        {
            DatabaseHelper databaseHelper = new DatabaseHelper();
            var time = DateTime.Now.Subtract(TimeSpan.FromDays(35));
            //RunInfoTable newRunInfo = new RunInfoTable("prj1", time, 12);
            //string errMsg = "";
            //databaseHelper.Add(newRunInfo, ref errMsg);
            //var runInfos = databaseHelper.GetAllRunInfos();
            var runInfos = databaseHelper.GetRunInfo("prj1", time, SpecifyDateTimeDegree.ToMonth);
            Console.WriteLine("found {0} results",runInfos.Count);
            foreach(var runInfo in runInfos)
            {
                var dateTime = runInfo.CreateDateTime;
                Console.WriteLine("{0},{1},{2}", runInfo.ProjectName, ((DateTime)dateTime).ToString("yy-MM-dd hh:mm:ss"), runInfo.SampleCount);
            }
            Console.WriteLine("Press any key to exit!");
            Console.ReadKey();
        }


    }
}
