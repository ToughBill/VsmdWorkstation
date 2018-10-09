using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VsmdWorkstation
{
    public class PreferenceMeta
    {
        public string VsmdPort { get; set; }
        public int Baudrate { get; set; }
        public string PumpPort { get; set; }
        public int BoardID { get; set; }

        public int Volume { get; set; }
    }
    public interface IPerference
    {
        bool SavePref();
    }

    public class Preference
    {
        private PreferenceMeta m_perfMeta;
        private bool m_hasPreference;
        public Preference()
        {
            m_hasPreference = false;
        }
        public bool HasPreference
        {
            get
            {
                return m_hasPreference;
            }
        }
        public string VsmdPort
        {
            get
            {
                return m_perfMeta.VsmdPort;
            }
            set
            {
                m_perfMeta.VsmdPort = value;
            }
        }
        public int Baudrate
        {
            get
            {
                return m_perfMeta.Baudrate;
            }
            set
            {
                m_perfMeta.Baudrate = value;
            }
        }
        public string PumpPort
        {
            get
            {
                return m_perfMeta.PumpPort;
            }
            set
            {
                m_perfMeta.PumpPort = value;
            }
        }
        public int BoardID
        {
            get
            {
                return m_perfMeta.BoardID;
            }
            set
            {
                m_perfMeta.BoardID = value;
            }
        }

        public int Volume
        {
            get
            {
                return m_perfMeta.Volume;
            }
            set
            {
                m_perfMeta.Volume = value;
            }
        }

        public void Load()
        {
            m_perfMeta = new PreferenceMeta();
            if (m_perfMeta.Volume == 0)
                m_perfMeta.Volume = 1000;

            string perfFilePath = GetPerfMetaFilePath();
            if (!File.Exists(perfFilePath))
            {
                return;
            }
            string str = File.ReadAllText(perfFilePath);
            if (string.IsNullOrWhiteSpace(str.Trim()))
            {
                return;
            }
            m_perfMeta = JsonConvert.DeserializeObject<PreferenceMeta>(str);
            m_hasPreference = true;
        }

        public bool Save()
        {
            bool ret = true;
            string perfFilePath = GetPerfMetaFilePath();
            try
            {
                if (File.Exists(perfFilePath))
                {
                    File.Delete(perfFilePath);
                }
                using (FileStream stream = File.Create(perfFilePath))
                {
                    string str = JsonConvert.SerializeObject(m_perfMeta);
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

        public string GetPerfMetaFilePath()
        {
            return Application.StartupPath + "\\preference.json";
        }
        private static Preference m_instance = null;
        public static Preference GetInstace()
        {
            if(m_instance == null)
            {
                m_instance = new Preference();
            }
            return m_instance;
        }
    }
}
