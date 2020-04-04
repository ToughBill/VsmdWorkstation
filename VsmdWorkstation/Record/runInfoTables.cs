using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EasySqlCe;
using System.Runtime.Serialization;
using System.Security.Cryptography;

namespace EasySqlCe
{

    public enum SpecifyDateTimeDegree
    {
        ToYear,
        ToMonth,
        ToDay,
        ToMinute,
    };

    public class RunInfoTable : EasySqlCe.Template
    {

        //this ctor only deep into day
        public RunInfoTable(string projectName, DateTime? createDateTime,int? sampleCnt) 
        {
            ProjectName = projectName;
            SampleCount = sampleCnt;
            CreateDateTime = createDateTime;
            if(createDateTime != null)
            {
                var dateTime = (DateTime)createDateTime;
                Year = dateTime.ToString("yyyy");
                Month = dateTime.ToString("MM");
                Day = dateTime.ToString("dd");
                Time = dateTime.ToString("hhmmss");
            }
           
        }


       void SetTimeAccuracyDegree(SpecifyDateTimeDegree degree)
        {
            switch (degree)
            {
               
                case SpecifyDateTimeDegree.ToYear:
                    Day = null;
                    Month = null;
                    Time = null;
                    break;
                case SpecifyDateTimeDegree.ToMonth:
                    Day = null;
                    Time = null;
                    break;
                case SpecifyDateTimeDegree.ToDay:
                    Time = null;
                    break;
               

            }
        }

     

        public RunInfoTable()
        {

        }



        public string Year { get; set; }
        public string Month { get; set; }
        public string Day { get; set; }
        public string Time { get; set; }

        public DateTime? CreateDateTime { get; set; }
 
        public string ProjectName { get; set; }
        public int? SampleCount { get; set; }

      


    }


  

}
