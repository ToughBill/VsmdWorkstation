using System;
using System.IO.Ports;
using System.Threading.Tasks;

namespace VsmdWorkstation
{
    public class PumpController
    {
        private string m_portName;
        private SerialPort m_comPort;
        public string GetPort()
        {
            return m_portName;
        }
        public InitResult Init(string port)
        {
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
            m_comPort.Write("A");
        }
        private void Off()
        {
            m_comPort.Write("a");
        }
        public async Task<bool> Drip()
        {
            Off();
            await Task.Delay(100);
            On();
            return true;
        }

        private static PumpController m_instance;
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
