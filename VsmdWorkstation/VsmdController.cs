using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
        private Vsmd m_vsmd = null;
        private VsmdInfo m_axisX = null;
        private VsmdInfo m_axisY = null;
        private VsmdInfo m_axisZ = null;
        private bool m_initialized = false;
         
        public async Task<InitResult> Init(string port, int baudrate)
        {
            m_vsmd = new Vsmd();
            bool ret = m_vsmd.openSerailPort(port, baudrate);
            if (!ret)
            {
                return new InitResult() { ErrorMsg="打开串口失败!", IsSuccess = false };
            }

            m_axisX = m_vsmd.createVsmdInfo(1);
            m_axisX.enable();
            m_axisX.flgAutoUpdate = true;

            m_axisY = m_vsmd.createVsmdInfo(2);
            m_axisY.enable();
            m_axisY.flgAutoUpdate = true;

            m_axisZ = m_vsmd.createVsmdInfo(3);
            m_axisZ.enable();
            m_axisZ.flgAutoUpdate = true;

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
                errMsg = "驱动器 ";
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
            m_vsmd.closeSerailPort();
            m_vsmd = null;
            return new InitResult() { IsSuccess = m_initialized, ErrorMsg = errMsg };

            //m_axisX.flgAutoUpdate = false;
            //m_axisX.enable();
            //m_axisX.cfgSpd(128000);
            //m_axisX.cfgCur(1.6f, 1.4f, 0.8f);
            
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

        public float GetSpeed(VsmdAxis axis)
        {
            return GetAxis(axis).curSpd;
        }
        public void SetSpeed(VsmdAxis axis, float speed)
        {
            GetAxis(axis).cfgSpd(speed);
        }

        public void Move(VsmdAxis axis)
        {
            GetAxis(axis).move();
        }
        public void MoveTo(VsmdAxis axis, int pos)
        {
            GetAxis(axis).moveto(pos);
        }
        public async Task<bool> MoveToSync(VsmdAxis axis, int pos)
        {
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
