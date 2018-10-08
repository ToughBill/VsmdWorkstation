using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VsmdWorkstation.Controls;

namespace VsmdWorkstation
{
    public class GeneralSettingMeta
    {
        
        public float MoveSpeed { get; set; }
        public int ZTravel { get; set; }
        public int ZDispense { get; set; }
        public bool OutputCommandLog { get; set; }
        public bool OutputStsCommandLog { get; set; }
        public bool AutoConnect { get; set; }
        public int DispenseInterval { get; internal set; }

        public Dictionary<string, string> VolumeDelay { get; set; }
    }
    public class GeneralSettings
    {
        private GeneralSettingMeta m_settingMeta;
        public static GeneralSettings m_instance;
      
        public static GeneralSettings GetInstance()
        {
            if(m_instance == null)
            {
                m_instance = new GeneralSettings();
            }
            return m_instance;
        }


        public Dictionary<string,string> VolumeDelay
        {   get
            {
                return m_settingMeta.VolumeDelay;
            }
           
        }
        //public int DispenseInterval
        //{
        //    get
        //    {
        //        return m_settingMeta.DispenseInterval;
        //    }
        //}
        public float MoveSpeed
        {
            get
            {
                return m_settingMeta.MoveSpeed;
            }
        }
        public bool AutoConnect
        {
            get
            {
                return m_settingMeta.AutoConnect;
            }
        }
        public bool OutputCommandLog
        {
            get
            {
                return m_settingMeta.OutputCommandLog;
            }
        }
        public bool OutputStsCommandLog
        {
            get
            {
                return m_settingMeta.OutputStsCommandLog;
            }
        }

        public int DispenseInterval { get; internal set; }

        public string GetSettingFilePath()
        {
            return Application.StartupPath + "\\generalSetting.json";
        }
        public void InitDefaultSetting()
        {
            m_settingMeta = new GeneralSettingMeta();
         
            m_settingMeta.MoveSpeed = 500.0f;
            m_settingMeta.AutoConnect = false;
            m_settingMeta.OutputCommandLog = false;
            m_settingMeta.VolumeDelay = new Dictionary<string, string>();
        }
        public void LoadGeneralSettings()
        {
            string filePath = GetSettingFilePath();
            if (!File.Exists(filePath))
            {
                InitDefaultSetting();
                return;
            }
            string str = File.ReadAllText(filePath);
            if (string.IsNullOrWhiteSpace(str.Trim()))
            {
                InitDefaultSetting();
                return;
            }
            m_settingMeta = JsonConvert.DeserializeObject<GeneralSettingMeta>(str);
        }
        public GeneralSettingMeta GetSettingMeta()
        {
            return m_settingMeta;
        }
        public bool Save()
        {
            string configFile = GetSettingFilePath();
            bool ret = true;
            try
            {
                if (File.Exists(configFile))
                {
                    File.Delete(configFile);
                }
                using (FileStream stream = File.Create(configFile))
                {
                    string str = JsonConvert.SerializeObject(m_settingMeta);
                    byte[] bytes = System.Text.Encoding.Default.GetBytes(str);
                    stream.Write(bytes, 0, bytes.Length);
                }
            }
            catch (Exception)
            {
                ret = false;
            }
            return ret;
        }
    }
}
