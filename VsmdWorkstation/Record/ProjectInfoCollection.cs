using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace VsmdWorkstation.Record
{
    public class ProjectInfo:BindableBase
    {
        string name;
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                SetProperty(ref name, value);
            }
        }

        public ProjectInfo()
        {

        }

        public ProjectInfo(string name)
        {
            this.name = name;
        }
    }
    public class ProjectInfoCollection : BindableBase
    {
        ProjectInfo selectedProjectInfo = null;
        public ProjectInfo SelectedProjectInfo
        {
            get
            {
                return selectedProjectInfo;
            }
            set
            {
                SetProperty(ref selectedProjectInfo, value);
            }
        }
        public ObservableCollection<ProjectInfo> ProjectInfos
        {
            get;set;
        }

        public ProjectInfoCollection()
        {
            ProjectInfos = new ObservableCollection<ProjectInfo>();
            
        }

        static public string GetExeFolder()
        {
            string s = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            return s + "\\";
        }


        public static ProjectInfoCollection Load()
        {
            ProjectInfoCollection prjInfoCollection = new ProjectInfoCollection();
            string savePath = prjInfoCollection.SavePath;
            if (!File.Exists(savePath))
            {
                return prjInfoCollection;
            }
            string str = File.ReadAllText(savePath);
            if (string.IsNullOrWhiteSpace(str.Trim()))
            {
                return prjInfoCollection;
            }
            prjInfoCollection = JsonConvert.DeserializeObject<ProjectInfoCollection>(str);
            var name = prjInfoCollection.SelectedProjectInfo.Name;
            if(name != "")
                prjInfoCollection.SelectedProjectInfo = prjInfoCollection.ProjectInfos.Where(x => x.Name == name).First();
            return prjInfoCollection;
        }

        internal void Remove()
        {
            if (selectedProjectInfo != null)
            {
                if (ProjectInfos.Count > 0)
                    ProjectInfos.Remove(selectedProjectInfo);
            }
          
            if (ProjectInfos.Count > 0)
                SelectedProjectInfo = ProjectInfos.Last();
        }

        internal void Add()
        {
            string prefix = "project";
            int id = 1;
            List<string> names = ProjectInfos.Select(x => x.Name).ToList();
            while(true)
            {
                string name = prefix + id.ToString();
                if (!names.Contains(name))
                {
                    var newPrjInfo = new ProjectInfo(name);
                    ProjectInfos.Add(newPrjInfo);
                    SelectedProjectInfo = newPrjInfo;
                    break;
                }
                else
                    id++;
            }
        }

        string SavePath
        {
            get
            {
                return GetExeFolder() + "projectNames.txt"; ;
            }
        }
        public bool Save()
        {
            bool ret = true;
            string savePath = SavePath;
            try
            {
                if (File.Exists(savePath))
                {
                    File.Delete(savePath);
                }
                string str = JsonConvert.SerializeObject(this);
                File.WriteAllText(savePath, str);
                
            }
            catch (Exception)
            {
                ret = false;
            }
            return ret;
        }
    }
    public abstract class BindableBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName]string propertyName = null)
        {
            if (object.Equals(storage, value)) return false;
            storage = value;
            this.OnPropertyChanged(propertyName);
            return true;
        }
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var eventHandler = this.PropertyChanged;
            if (eventHandler != null)
                eventHandler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
