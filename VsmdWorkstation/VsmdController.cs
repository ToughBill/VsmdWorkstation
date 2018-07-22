using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    public class VsmdController
    {
        private Vsmd m_vsmd = null;
        private VsmdInfo m_axisX = null;
        private VsmdInfo m_axisY = null;
        private VsmdInfo m_axisZ = null;
        private bool initialized = false;
         
        public bool Init(string port, int baudrate)
        {
            m_vsmd = new Vsmd();
            bool ret = m_vsmd.openSerailPort(port, baudrate);
            if (!ret)
            {
                return false;
            }
            m_axisX = m_vsmd.createVsmdInfo(1);
            m_axisX.enable();
            m_axisY = m_vsmd.createVsmdInfo(2);
            m_axisY.enable();
            m_axisZ = m_vsmd.createVsmdInfo(3);
            m_axisZ.enable();
            initialized = true;
            //m_axisX.flgAutoUpdate = false;
            //m_axisX.enable();
            //m_axisX.cfgSpd(128000);
            //m_axisX.cfgCur(1.6f, 1.4f, 0.8f);

            return true;
        }
        public bool IsInitialized()
        {
            return initialized;
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
        public void MoveTo(int xpox, int ypox)
        {
            GetAxis(VsmdAxis.X).moveto(xpox);
            GetAxis(VsmdAxis.Y).moveto(ypox);
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
