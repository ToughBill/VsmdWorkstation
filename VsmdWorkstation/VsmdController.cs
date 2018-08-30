using System.Collections.Generic;
using System.Threading.Tasks;
using VsmdLib;

namespace VsmdWorkstation
{
    public enum VsmdAxis
    {
        X,
        Y,
        Z
    }
    public class InitResult
    {
        public bool IsSuccess;
        public string ErrorMsg;
    }
    public delegate void VsmdInitCallback(bool isOnline,List<string> errAxis);
    public class VsmdController
    {
        private VsmdSync m_vsmd = null;
        private VsmdInfoSync m_axisX = null;
        private VsmdInfoSync m_axisY = null;
        private VsmdInfoSync m_axisZ = null;
        private bool m_initialized = false;
        private const int MAX_STROKE_Y = 32000;
        private string m_port;
        private int m_baudrate;
        
        public void SetOutputCommandLogFlag(bool flag)
        {
            m_vsmd.OutputCommandLog = flag;
        }
        public async Task<InitResult> Init(string port, int baudrate)
        {
            if(m_initialized && port == m_port && baudrate == m_baudrate)
            {
                return new InitResult() { ErrorMsg = "设备连接成功!", IsSuccess = true };
            }
            
            m_port = port;
            m_baudrate = baudrate;
            if (m_initialized)
            {
                m_vsmd.closeSerailPort();
            }
            
            m_vsmd = new VsmdSync();
            bool ret = m_vsmd.openSerailPort(port, baudrate);
            if (!ret)
            {
                return new InitResult() { ErrorMsg="打开串口失败!", IsSuccess = false };
            }
            m_vsmd.OutputCommandLog = GeneralSettings.GetInstance().OutputCommandLog;

            List<string> errAxis = new List<string>();
            m_axisX = m_vsmd.createVsmdInfo(1);
            await m_axisX.CheckAxisIsOnline();
            if (m_axisX.isOnline)
            {
                await m_axisX.enable();
                m_axisX.flgAutoUpdate = true;
                await m_axisX.cfg();
            }
            else
            {
                errAxis.Add("X");
            }

            m_axisY = m_vsmd.createVsmdInfo(2);
            await m_axisY.CheckAxisIsOnline();
            if (m_axisY.isOnline)
            {
                await m_axisY.enable();
                m_axisY.flgAutoUpdate = true;
                await m_axisY.cfg();
            }
            else
            {
                errAxis.Add("Y");
            }

            m_axisZ = m_vsmd.createVsmdInfo(3);
            await m_axisZ.CheckAxisIsOnline();
            if (m_axisY.isOnline)
            {
                await m_axisZ.enable();
                m_axisZ.flgAutoUpdate = true;
                await m_axisZ.cfg();
            }
            else
            {
                errAxis.Add("Z");
            }

            if(errAxis.Count <= 0)
            {
                m_initialized = true;
            }
            
            string errMsg = "";
            if(errAxis.Count > 0)
            {
                errMsg = "设备 ";
                for (int i = 0; i < errAxis.Count; i++)
                {
                    if (i > 0)
                    {
                        errMsg += ", ";
                    }
                    errMsg += errAxis[i];
                }
                errMsg += "连接失败！";
            }
            if (!m_initialized)
            {
                m_vsmd.closeSerailPort();
                m_vsmd = null;
            }
            
            return new InitResult() { IsSuccess = m_initialized, ErrorMsg = errMsg };
        }
        public async Task<InitResult> ResetVsmdController()
        {
            if (m_initialized)
            {
                this.Dispose();
            }
            InitResult result = await Init(m_port, m_baudrate);
            if (!result.IsSuccess)
            {
                //StatusBar.DisplayMessage(MessageType.Error, result.ErrorMsg);
            }
            return result;
        }
        public bool IsInitialized()
        {
            return m_initialized;
        }
        public VsmdInfoSync GetAxis(VsmdAxis axis)
        {
            VsmdInfoSync ret = null;
            switch (axis)
            {
                case VsmdAxis.X:
                    ret = m_axisX;
                    break;
                case VsmdAxis.Y:
                    ret = m_axisY;
                    break;
                case VsmdAxis.Z:
                    ret = m_axisZ;
                    break;
                default:
                    break;
            }
            return ret;
        }
        
        public string GetPort()
        {
            return m_port;
        }
        public int GetBaudrate()
        {
            return m_baudrate;
        }

        public float GetSpeed(VsmdAxis axis)
        {
            return GetAxis(axis).curSpd;
        }
        public async Task<bool> SetSpeed(VsmdAxis axis, float speed)
        {
            if(speed == 0.0)
            {
                System.Windows.Forms.MessageBox.Show("dangrous!!! speed is zero!");
            }
            return await GetAxis(axis).cfgSpd(speed);
        }
        public async Task<bool> Dev(VsmdAxis axis)
        {
            return await GetAxis(axis).dev();
        }

        public async Task<bool> Pos(VsmdAxis axis, int pos)
        {
            return await GetAxis(axis).moveto(pos);
        }
        public async Task<bool> Ena(VsmdAxis axis)
        {
            return await GetAxis(axis).enable();
        }
        public async Task<bool> Off(VsmdAxis axis)
        {
            return await GetAxis(axis).disable();
        }
        public async Task<bool> Move(VsmdAxis axis)
        {
            return await GetAxis(axis).move();
        }
        public async Task<bool> Stop(VsmdAxis axis)
        {
            return await GetAxis(axis).stop(0);
        }
        public async Task<bool> MoveTo(VsmdAxis axis, int pos)
        {
            return await GetAxis(axis).moveto(pos);
        }
        public async Task<bool> ZeroStart(VsmdAxis axis)
        {
            //await m_vsmdController.SetZsd(axis, 1200);
            return await GetAxis(axis).zeroStart();
        }
        public async Task<bool> ZeroStop(VsmdAxis axis)
        {
            return await GetAxis(axis).zeroStop();
        }
        public async Task<bool> Org(VsmdAxis axis)
        {
            return await GetAxis(axis).org();
        }
        public async Task<bool> Sts(VsmdAxis axis)
        {
            return await GetAxis(axis).sts();
        }
        public async Task<bool> S3On(VsmdAxis axis)
        {
            return await GetAxis(axis).S3On();
        }
        public async Task<bool> S3Off(VsmdAxis axis)
        {
            return await GetAxis(axis).S3Off();
        }
        public async Task<bool> CfgSync(VsmdAxis axis)
        {
            return await GetAxis(axis).cfg();
        }
        public async Task<bool> Cfg(VsmdAxis axis)
        {
            return await GetAxis(axis).cfg();
        }
        public async Task<bool> SetZsd(VsmdAxis axis, float speed)
        {
            return await GetAxis(axis).cfgZsd(speed);
        }
        public async Task<bool> SetS3Mode(VsmdAxis axis, int mode)
        {
            return await GetAxis(axis).cfgS3(mode);
        }
        //public async Task<bool> MoveSync(VsmdAxis axis)
        //{
        //    VsmdInfoSync vsmdAxis = GetAxis(axis);
        //    return await vsmdAxis.move();
        //}
        //public async Task<bool> MoveToSync(VsmdAxis axis, int pos)
        //{
        //    VsmdInfoSync vsmdAxis = GetAxis(axis);
        //    return await vsmdAxis.moveto(pos);
        //}

        //public async Task<bool> ZeroStartSync(VsmdAxis axis)
        //{
        //    VsmdInfoSync vsmdAxis = GetAxis(axis);
        //    await m_vsmdController.SetZsd(axis, 1200);

        //    //float zsd = VsmdController.GetVsmdController().GetAxis(axis).GetAttributeValue(VsmdAttribute.Zsd);
        //    //int delayTime = (int)(MAX_STROKE_Y / zsd + 1) * 1000;
        //    //int maxTryCount = delayTime / 10;
            
        //    return await vsmdAxis.zeroStart();

        //}
        //public async Task<bool> ZeroStopSync(VsmdAxis axis)
        //{
        //    VsmdInfoSync vsmdAxis = GetAxis(axis);
        //    return await vsmdAxis.zeroStop();
        //}
        public async void MoveTo(int xpox, int ypox)
        {
            await MoveTo(VsmdAxis.X, xpox);
            await MoveTo(VsmdAxis.Y, ypox);
        }
        public void Dispose()
        {
            if (m_vsmd != null)
            {
                m_axisX = null;
                m_axisY = null;
                m_axisZ = null;
                m_vsmd.closeSerailPort();
                m_initialized = false;
            }
        }

        private static VsmdController m_vsmdController = null;
        public static VsmdController GetVsmdController()
        {
            if(m_vsmdController == null)
            {
                m_vsmdController = new VsmdController();
            }
            return m_vsmdController;
        }
    }
}
