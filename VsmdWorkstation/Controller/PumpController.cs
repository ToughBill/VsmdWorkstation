using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO.Ports;
using System.Threading.Tasks;

namespace VsmdWorkstation
{
    public class PumpController
    {
        private string m_portName;
        private SerialPort m_comPort;
        bool pumpExist;
        string relayType;
        protected static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public string GetPort()
        {
            return m_portName;
        }


        private PumpController()
        {
            pumpExist = bool.Parse(ConfigurationManager.AppSettings["PumpExist"]);
            relayType = ConfigurationManager.AppSettings["RelayType"];
        }
        public InitResult Init(string port)
        {
            log.InfoFormat("pump exist: {0}", pumpExist);
            //Logger.Instance.Write(string.Format("pump exist: {0}", pumpExist.ToString()));
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
            
            {
                string s = on ? string.Format("0XA{0}", portNum) : string.Format("0XB{0}", portNum);
                //Logger.Instance.Write(s);
                byte val = on ? (byte)0xA0 : (byte)0XB0;
                byte[] buffer = new byte[1];
                buffer[0] = (byte)(val + portNum);
                m_comPort.Write(buffer, 0, 1);
            }




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
            byte portNum = (byte)(i % 4 + 1);
            if (relayType == "LH")
            {
                SwitchOnOffQuick(portNum);
            }
            else// old one,only works in 重庆
            {
                On(portNum);
                await Task.Delay(100);
                Off(portNum);
            }
            
            return true;
        }

        private void SwitchOnOffQuick(byte portNum)
        {
            byte[] buffer1 = new byte[] { 0x01, 0x34, 0xF0, 0x00, 0x00, 0x01, 0x42, 0xCE };
            byte[] buffer2 = new byte[] { 0x01, 0x34, 0xF0, 0x01, 0x00, 0x01, 0x13, 0x0E };
            byte[] buffer3 = new byte[] { 0x01, 0x34, 0xF0, 0x02, 0x00, 0x01, 0xE3, 0x0E };
            byte[] buffer4 = new byte[] { 0x01, 0x34, 0xF0, 0x03, 0x00, 0x01, 0xB2, 0xCE };
            Dictionary<byte, byte[]> port_buffer = new Dictionary<byte, byte[]>();
            port_buffer.Add(1, buffer1);
            port_buffer.Add(2, buffer2);
            port_buffer.Add(3, buffer3);
            port_buffer.Add(4, buffer4);
            portNum = (byte)((portNum - 1) % 4 + 1);
            log.InfoFormat("port number:{0}", portNum);
            byte[] buffer = port_buffer[portNum];
            m_comPort.Write(buffer, 0, buffer.Length);
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
