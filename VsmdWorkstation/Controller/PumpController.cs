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


        static void OpenPort(int portNum, SerialPort comPort)
        {
            string s = string.Format("0XA{0}", portNum);
            byte[] buffer = new byte[1];
            buffer[0] = (byte)(0xA0 + portNum);
            comPort.Write(buffer, 0, 1);
        }


        static void ClosePort(int portNum, SerialPort comPort)
        {
            byte[] buffer = new byte[1];
            buffer[0] = (byte)(0xB0 + portNum);
            comPort.Write(buffer, 0, 1);
        }
        private void OnOff(int portNum,bool on)
        {
            if (!pumpExist)
                return;

            string s = on ? string.Format("0XA{0}", portNum) : string.Format("0XB{0}", portNum);
            Logger.Instance.Write(s);
            byte val = on ? (byte)0xA0 : (byte)0XB0;
            byte[] buffer = new byte[1];
            buffer[0] = (byte)(val + portNum);
            m_comPort.Write(buffer, 0, 1);
        }

        public void On(int portNum)
        {
            OnOff(portNum, true);
        }
        public void Off(int portNum)
        {
            OnOff(portNum, false);
        }
        public async Task<bool> SwitchOnOff(int i)
        {
            int portNum = i % 4 + 1;
            Logger.Instance.Write("switch On Off");
            On(portNum);
            await Task.Delay(100);
            Off(portNum);
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
