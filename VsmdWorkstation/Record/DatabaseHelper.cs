using EasySqlCe;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Reflection;

namespace EasySqlCe
{
    public class DatabaseHelper
    {
        private EasySqlCe.AccessPoint DataBase;
        public DatabaseHelper()
        {
            this.DataBase = new AccessPoint();
            DataBase.Filename = "Pipetting.sdf";
            DataBase.Filepath = FolderHelper.GetDataBaseFolder(); ;
            DataBase.Password = "123";
            DataBase.UseLikeStatement = false; // default = false
            DataBase.WildCardLeft = true; // In conjunction with UseLikeStatement
            DataBase.WildCardRight = true;
            //bool bok = OpenTable();
            OpenTables();
           
        }

        #region runinfo

        public RunInfoTable GetRunInfo(string projectName,DateTime dateTime, SpecifyDateTimeDegree degree)
        {
            RunInfoTable runInfoTable = new RunInfoTable(projectName, dateTime, null);
            var founddata = DataBase.Select(runInfoTable); // this is the actual database operation
            return founddata.Count > 0? founddata.First() :null;
        }

        public List<RunInfoTable> GetAllRunInfos()
        {
            RunInfoTable runInfoTable = new RunInfoTable( null, null, null);
            var founddata = DataBase.Select(runInfoTable); // this is the actual database operation
            return founddata;
        }

        public bool Add(RunInfoTable runInfoTable, ref string errMsg)
        {
            runInfoTable.SampleCount = null;
            var founddata = DataBase.Select(runInfoTable); // this is the actual database operation
            if (founddata != null && founddata.Count != 0)
            {
                errMsg = "already exist!";
                return false;
            }
            else
            {
                int ret = DataBase.Insert(runInfoTable);
                return ret == 1;
            }
        }

        public bool Update(RunInfoTable runInfoTable, ref string errMsg)
        {
            RunInfoTable tmpRunInfoTable = new RunInfoTable(runInfoTable.ProjectName, runInfoTable.CreateDateTime, null);

            var founddata = DataBase.Select(runInfoTable); // this is the actual database operation
            if (founddata.Count == 0)
            {
                errMsg = "Cannot find userInfo";
                return false;
            }
            int val = DataBase.Update(runInfoTable, founddata.First());
            return val == 1;

        }

        public void Delete(RunInfoTable runInfo)
        {
            RunInfoTable runInfoTable = new RunInfoTable(runInfo.CreateDateTime, null,  null, null);
            var founddata = DataBase.Select(runInfoTable); // this is the actual database operation
            if (founddata.Count == 0)
            {
                return;
            }
            DataBase.Delete(founddata.First());
        }
        #endregion


     
        private bool OpenTables()
        {
            string fullPath = DataBase.Filepath + DataBase.Filename;
            string errMsg = "";
            if (!DataBase.CreateDataBase(true, ref errMsg))
            {
                //SetInfo(errMsg, Brushes.Red);
                return false;
            }
            //2 create tables
            if (!DataBase.CheckTable(new RunInfoTable(), ref errMsg))
            {
                if (!DataBase.CreateTable(new RunInfoTable(), ref errMsg))
                {
                    return false;
                }
                else
                {
                    //DataBase.Insert(new RunInfoTable(UserTypes.Engineer, "WTEngineer", "WT@2019"));
                }
            }

          
            return true;
        }

       

        private void RenameFile(string path, string name, string date)
        {
            string sNewName = date + ".sdf";
            File.Move(path + name, path + sNewName);
        }

      
     

  
    }

    class FolderHelper
    {
        static public string GetExeFolder()
        {
            string s = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            return s + "\\";
        }
        static public string GetExeParentFolder()
        {
            string s = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            int index = s.LastIndexOf("\\");
            return s.Substring(0, index) + "\\";
        }

        internal static string GetDataBaseFolder()
        {
            string sOutputFolder = GetExeParentFolder() + "Database\\";
            CreateIfNotExist(sOutputFolder);
            return sOutputFolder;
        }

        public static string GetOutputFolder()
        {
            string sOutputFolder = GetExeParentFolder() + "Output\\";
            CreateIfNotExist(sOutputFolder);
            return sOutputFolder;
        }


        private static void CreateIfNotExist(string sFolder)
        {
            if (!Directory.Exists(sFolder))
            {
                Directory.CreateDirectory(sFolder);
            }
        }


    }
}
