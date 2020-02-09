using System;
using System.Configuration;
using System.IO.Ports;
using System.Threading.Tasks;

namespace VsmdWorkstation
{
    public class PumpController
    {
        private string m_portName;
        private SerialPort m_comPort;
        bool pumpExist;
        protected static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public string GetPort()
        {
            return m_portName;
        }


        private PumpController()
        {
            pumpExist = bool.Parse(ConfigurationManager.AppSettings["PumpExist"]);
        }
        public InitResult Init(string port)
        {
           
            Logger.Instance.Write(string.Format("pump exist: {0}", pumpExist.ToString()));
            if(!pumpExist)
                return new InitResult() { IsSuccess = true, Message = "" };
            bool success = true;
            string errMsg = string.Empty;
            try
            {
                m_comPort = new SerialPort(port);
                m_comPort.Open();
                m_portName = port;
            }
            catch (Exception)
            {
                success = false;
                errMsg = "蠕动泵连接失败！";
            }
            return new InitResult() { IsSuccess = success, Message = errMsg };
        }
        public bool Dispose()
        {
            if (!pumpExist)
                return true;
            bool ret = true;
            try
            {
                m_comPort.DiscardInBuffer();
                m_comPort.DiscardOutBuffer();
                m_comPort.Close();
            }
            catch (Exception)
            {
                ret = false;
            }
            return ret;
        }
        private void On()
        {
            if (!pumpExist)
                return;
            Logger.Instance.Write("write A");
            m_comPort.Write("A");
        }
        private void Off()
        {
            if (!pumpExist)
                return;
            Logger.Instance.Write("write a");
            m_comPort.Write("a");
        }
        public async Task<bool> SwitchOnOff()
        {
            Logger.Instance.Write("switch On Off");
            Off();
            await Task.Delay(100);
            On();
            return true;
        }

        private static PumpController m_instance;

        public bool PumpExist
        {
            get
            {
                return pumpExist;
            }
        }

        public static PumpController GetPumpController()
        {
            if(m_instance == null)
            {
               
                m_instance = new PumpController();
            }
            return m_instance;
        }
    }
}
