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
        public int DripInterval { get; set; }
        public float MoveSpeed { get; set; }
        public int ZTravel { get; set; }
        public int ZDispense { get; set; }
        public bool OutputCommandLog { get; set; }
        public bool OutputStsCommandLog { get; set; }
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

        public int DripInterval
        {
            get
            {
                return m_settingMeta.DripInterval;
            }
        }
        public float MoveSpeed
        {
            get
            {
                return m_settingMeta.MoveSpeed;
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


        public string GetSettingFilePath()
        {
            return Application.StartupPath + "\\generalSetting.json";
        }
        public void InitDefaultSetting()
        {
            m_settingMeta = new GeneralSettingMeta();
            m_settingMeta.DripInterval = 5000;
            m_settingMeta.MoveSpeed = 500.0f;
            m_settingMeta.OutputCommandLog = false;
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
