using System;
using System.IO.Ports;
using System.Threading.Tasks;

namespace VsmdWorkstation
{
    public class PumpController
    {
        private SerialPort m_port;
        public InitResult Init(string port)
        {
            bool success = true;
            string errMsg = string.Empty;
            try
            {
                m_port = new SerialPort(port);
                m_port.Open();
            }
            catch (Exception)
            {
                success = false;
                errMsg = "蠕动泵连接失败！";
            }
            return new InitResult() { IsSuccess = success, ErrorMsg = errMsg };
        }
        private void On()
        {
            m_port.Write("A");
        }
        private void Off()
        {
            m_port.Write("a");
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
