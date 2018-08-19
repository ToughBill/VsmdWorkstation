using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using VsmdLib;
using VsmdWorkstation.Controls;

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
        private Vsmd m_vsmd = null;
        private VsmdInfo m_axisX = null;
        private VsmdInfo m_axisY = null;
        private VsmdInfo m_axisZ = null;
        private bool m_initialized = false;
        private const int MAX_STROKE_Y = 32000;
        private string m_port;
        private int m_baudrate;
         
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
            
            m_vsmd = new Vsmd();
            bool ret = m_vsmd.openSerailPort(port, baudrate);
            if (!ret)
            {
                return new InitResult() { ErrorMsg="打开串口失败!", IsSuccess = false };
            }

            m_axisX = m_vsmd.createVsmdInfo(1);
            m_axisX.enable();
            m_axisX.flgAutoUpdate = true;
            await CfgSync(VsmdAxis.X);

            m_axisY = m_vsmd.createVsmdInfo(2);
            m_axisY.enable();
            m_axisY.flgAutoUpdate = true;
            await CfgSync(VsmdAxis.Y);

            m_axisZ = m_vsmd.createVsmdInfo(3);
            m_axisZ.enable();
            m_axisZ.flgAutoUpdate = true;
            await CfgSync(VsmdAxis.Z);

            int tryCount = 3;
            List<string> errAxis = new List<string>();
            while (tryCount > 0)
            {
                await Task.Delay(1000);
                errAxis.Clear();
                if (!m_axisX.isOnline)
                {
                    errAxis.Add("X");
                }
                if (!m_axisY.isOnline)
                {
                    errAxis.Add("Y");
                }
                if (!m_axisZ.isOnline)
                {
                    errAxis.Add("Z");
                }
                if(errAxis.Count > 0)
                {
                    tryCount--;
                    continue;
                }
                else
                {
                    m_initialized = true;
                    break;
                }
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

            //m_axisX.flgAutoUpdate = false;
            //m_axisX.enable();
            //m_axisX.cfgSpd(128000);
            //m_axisX.cfgCur(1.6f, 1.4f, 0.8f);
            
        }
        public async Task<InitResult> ResetVsmdController()
        {
            bool ret = true;
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
        public VsmdInfo GetAxis(VsmdAxis axis)
        {
            VsmdInfo ret = null;
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
        public void SetSpeed(VsmdAxis axis, float speed)
        {
            GetAxis(axis).cfgSpd(speed);
        }

        public void Pos(VsmdAxis axis, int pos)
        {
            GetAxis(axis).moveto(pos);
        }
        public void Ena(VsmdAxis axis)
        {
            GetAxis(axis).enable();
        }
        public void Off(VsmdAxis axis)
        {
            GetAxis(axis).disable();
        }
        public void Move(VsmdAxis axis)
        {
            GetAxis(axis).move();
        }
        public void Stop(VsmdAxis axis)
        {
            GetAxis(axis).stop((int)GetAxis(axis).curSpd);
        }
        public void MoveTo(VsmdAxis axis, int pos)
        {
            GetAxis(axis).moveto(pos);
        }
        public void ZeroStart(VsmdAxis axis)
        {
            GetAxis(axis).zeroStart();
        }
        public void ZeroStop(VsmdAxis axis)
        {
            GetAxis(axis).zeroStop();
        }
        public void Org(VsmdAxis axis)
        {
            GetAxis(axis).org();
        }
        public void Sts(VsmdAxis axis)
        {
            GetAxis(axis).sts();
        }
        public void S3On(VsmdAxis axis)
        {
            GetAxis(axis).S3On();
        }
        public void S3Off(VsmdAxis axis)
        {
            GetAxis(axis).S3Off();
        }
        public async Task<bool> CfgSync(VsmdAxis axis)
        {
            GetAxis(axis).cfg();
            await Task.Delay(100);
            return true;
        }
        public void Cfg(VsmdAxis axis)
        {
            //GetAxis(axis).cfg();
        }
        public void SetZsd(VsmdAxis axis, float speed)
        {
            //GetAxis(axis).cfgZsd(speed);
        }
        public void SetS3Mode(VsmdAxis axis, int mode)
        {
            GetAxis(axis).cfgS3(mode);
        }
        public async Task<bool> MoveSync(VsmdAxis axis)
        {
            VsmdInfo vsmdAxis = GetAxis(axis);
            vsmdAxis.move();
            int tryCount = 0;
            while (tryCount < 200)
            {
                await Task.Delay(20);
                if (vsmdAxis.curPos == MAX_STROKE_Y)
                {
                    break;
                }
                tryCount++;
            }
            if (tryCount >= 200)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public async Task<bool> MoveToSync(VsmdAxis axis, int pos)
        {
            Debug.WriteLine("### MoveToSync, axis: " + axis.ToString() + ", pos: " + pos.ToString());
            VsmdInfo vsmdAxis = GetAxis(axis);
            vsmdAxis.moveto(pos);
            int tryCount = 0;
            while(tryCount < 50)
            {
                await Task.Delay(20);
                if(vsmdAxis.curPos == pos)
                {
                    break;
                }
                tryCount++;
            }
            if(tryCount >= 50)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public async Task<bool> ZeroStartSync(VsmdAxis axis)
        {
            VsmdInfo vsmdAxis = GetAxis(axis);
            vsmdAxis.addCommand("cfg zsd=1200\n");
            await Task.Delay(20);
            vsmdAxis.zeroStart();
            int tryCount = 0;
            //await Task.Delay(8000);
            
            float zsd = VsmdController.GetVsmdController().GetAxis(axis).GetAttributeValue(VsmdAttribute.Zsd);
            //await Task.Delay((int)(MAX_STROKE_Y / zsd - 2) * 1000);
            int delayTime = (int)(MAX_STROKE_Y / zsd + 1) * 1000;
            int maxTryCount = delayTime / 50;
            while (tryCount < maxTryCount)
            {
                await Task.Delay(50);
                if (vsmdAxis.curPos == 0)
                {
                    break;
                }
                tryCount++;
            }
            if (tryCount >= maxTryCount)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public void ZeroStopSync(VsmdAxis axis)
        {
            VsmdInfo vsmdAxis = GetAxis(axis);
            vsmdAxis.zeroStop();
        }
        public async void MoveTo(int xpox, int ypox)
        {
            await MoveToSync(VsmdAxis.X, xpox);
            await MoveToSync(VsmdAxis.Y, ypox);
            //GetAxis(VsmdAxis.X).moveto(xpox);
            //GetAxis(VsmdAxis.Y).moveto(ypox);
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
