// Decompiled with JetBrains decompiler
// Type: VsmdLib.VsmdInfo
// Assembly: VsmdLib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 80BC418D-8177-4FA4-A057-7E2A3FD77244
// Assembly location: G:\code\VsmdWorkstation\trunk\VsmdWorkstation\sdk\VsmdLib.dll

using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Runtime.InteropServices;
using System.Text;

namespace VsmdLib
{
    public class VsmdInfo
    {
        /// <summary>command list</summary>
        private List<string> cmdList = new List<string>();
        /// <summary>dev info</summary>
        private string devInfo = "";
        /// <summary>param baudrate</summary>
        private int bdr = 9600;
        /// <summary>slave device id</summary>
        private int cid = 1;
        /// <summary>serail port</summary>
        private SerialPort com_port;
        /// <summary>current speed</summary>
        private float cur_spd;
        /// <summary>current position</summary>
        private int cur_pos;
        /// <summary>current status bits</summary>
        private uint cur_status;

        /// <summary>constructor</summary>
        /// <param name="cid">slave device id</param>
        public VsmdInfo(int cid)
        {
            this.cid = cid;
            this.isOnline = true;
        }

        public SerialPort comPort
        {
            set
            {
                this.com_port = value;
            }
        }

        /// <summary>flag for automatically updating (sending command sts)</summary>
        public bool flgAutoUpdate { set; get; }

        /// <summary>check device is online</summary>
        public bool isOnline { set; get; }

        /// <summary>add command to list</summary>
        /// <param name="cmd">command string</param>
        public void addCommand(string cmd)
        {
            if (this.com_port == null || !this.com_port.IsOpen)
                return;
            lock (this.cmdList)
              this.cmdList.Add(cmd);
        }

        /// <summary>send command process from cmdList</summary>
        public virtual string sendCmdProcess()
        {
            string str = (string)null;
            if (this.cmdList.Count > 0)
            {
                str = this.cmdList[0];
                lock (this.cmdList)
                  this.cmdList.RemoveAt(0);
            }
            else if (this.flgAutoUpdate)
                str = "sts";
            if (str != null && this.com_port.IsOpen)
                str = this.cid != 0 ? this.cid.ToString("d") + " " + str + "\n" : str + "\n";
            return str;
        }

        /// <summary>convert a stream data to float</summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private float convertFloat(byte[] data)
        {
            VsmdInfo.TokenValue tokenValue = new VsmdInfo.TokenValue();
            tokenValue.uData = (uint)data[0];
            tokenValue.uData <<= 7;
            tokenValue.uData |= (uint)data[1];
            tokenValue.uData <<= 7;
            tokenValue.uData |= (uint)data[2];
            tokenValue.uData <<= 7;
            tokenValue.uData |= (uint)data[3];
            tokenValue.uData <<= 7;
            tokenValue.uData |= (uint)data[4];
            return tokenValue.fData;
        }

        /// <summary>convert a stream data to uint</summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private uint convertUint(byte[] data)
        {
            VsmdInfo.TokenValue tokenValue = new VsmdInfo.TokenValue();
            tokenValue.uData = (uint)data[0];
            tokenValue.uData <<= 7;
            tokenValue.uData |= (uint)data[1];
            tokenValue.uData <<= 7;
            tokenValue.uData |= (uint)data[2];
            tokenValue.uData <<= 7;
            tokenValue.uData |= (uint)data[3];
            tokenValue.uData <<= 7;
            tokenValue.uData |= (uint)data[4];
            return tokenValue.uData;
        }

        /// <summary>convert a stream data to int</summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private int convertInt(byte[] data)
        {
            VsmdInfo.TokenValue tokenValue = new VsmdInfo.TokenValue();
            tokenValue.uData = (uint)data[0];
            tokenValue.uData <<= 7;
            tokenValue.uData |= (uint)data[1];
            tokenValue.uData <<= 7;
            tokenValue.uData |= (uint)data[2];
            tokenValue.uData <<= 7;
            tokenValue.uData |= (uint)data[3];
            tokenValue.uData <<= 7;
            tokenValue.uData |= (uint)data[4];
            return tokenValue.iData;
        }

        /// <summary>parse response</summary>
        /// <param name="res">response data</param>
        public void parse(byte[] res)
        {
            switch (res[2])
            {
                case 1:
                    this.devInfo = Encoding.Default.GetString(res, 3, res.Length - 6);
                    break;
                case 2:
                    byte[] data = new byte[5];
                    Buffer.BlockCopy((Array)res, 3, (Array)data, 0, 5);
                    this.cur_spd = this.convertFloat(data);
                    Buffer.BlockCopy((Array)res, 8, (Array)data, 0, 5);
                    this.cur_pos = this.convertInt(data);
                    Buffer.BlockCopy((Array)res, 13, (Array)data, 0, 5);
                    this.cur_status = this.convertUint(data);
                    int num = (int)this.cur_status & 512;
                    break;
            }
        }

        /// <summary>current speed</summary>
        public float curSpd
        {
            get
            {
                return this.cur_spd;
            }
        }

        /// <summary>current position</summary>
        public int curPos
        {
            get
            {
                return this.cur_pos;
            }
        }

        /// <summary>current status bits</summary>
        public uint curStatus
        {
            get
            {
                return this.cur_status;
            }
        }

        private int Bdr
        {
            get
            {
                return this.bdr;
            }
        }

        /// <summary>config baudrate</summary>
        /// <param name="bdr">baudrate (2400 - 921600)</param>
        public void cfgBdr(int bdr)
        {
            this.addCommand("cfg bdr=" + bdr.ToString("d"));
        }

        public int Cid
        {
            get
            {
                return this.cid;
            }
        }

        /// <summary>config slave device id</summary>
        /// <param name="cid">slave device id, 0 for vsmd102, (1 - 254) for vsmd103</param>
        public void cfgCid(int cid)
        {
            this.addCommand("cfg cid=" + cid.ToString("d"));
        }

        /// <summary>config microstep</summary>
        /// <param name="mcs">microstep (0-5)</param>
        public void cfgMcs(int mcs)
        {
            this.addCommand("cfg mcs=" + mcs.ToString("d"));
        }

        /// <summary>config target speed</summary>
        /// <param name="spd">target speed (-192000,192000)</param>
        public void cfgSpd(float spd)
        {
            this.addCommand("cfg spd=" + spd.ToString("f"));
        }

        /// <summary>config acceleration</summary>
        /// <param name="acc">acceleration (0,19200000)</param>
        public void cfgAcc(float acc)
        {
            this.addCommand("cfg acc=" + acc.ToString("f"));
        }

        /// <summary>config deceleration</summary>
        /// <param name="dec">deceleration (0,19200000)</param>
        public void cfgDec(float dec)
        {
            this.addCommand("cfg dec=" + dec.ToString("f"));
        }

        /// <summary>config current</summary>
        /// <param name="cra">current in acceleration-state</param>
        /// <param name="crn">current in normal-state</param>
        /// <param name="crh">current in stop-state</param>
        public void cfgCur(float cra, float crn, float crh)
        {
            this.addCommand("cfg cra=" + cra.ToString("f") + " crn=" + crn.ToString("f") + " crh=" + crh.ToString("f"));
        }

        /// <summary>config s1 falling-edge function</summary>
        /// <param name="s1f"></param>
        public void cfgS1f(int s1f)
        {
            this.addCommand("cfg s1f=" + s1f.ToString("d"));
        }

        /// <summary>config s1 raising-edge function</summary>
        /// <param name="s1r"></param>
        public void cfgS1r(int s1r)
        {
            this.addCommand("cfg s1r=" + s1r.ToString("d"));
        }

        /// <summary>config s2 falling-edge function</summary>
        /// <param name="s2f"></param>
        public void cfgS2f(int s2f)
        {
            this.addCommand("cfg s2f=" + s2f.ToString("d"));
        }

        /// <summary>config s2 raising-edge function</summary>
        /// <param name="s2r"></param>
        public void cfgS2r(int s2r)
        {
            this.addCommand("cfg s2r=" + s2r.ToString("d"));
        }

        /// <summary>config s3 falling-edge function</summary>
        /// <param name="s3f"></param>
        public void cfgS3f(int s3f)
        {
            this.addCommand("cfg s3f=" + s3f.ToString("d"));
        }

        /// <summary>config s3 raising-edge function</summary>
        /// <param name="s3r"></param>
        public void cfgS3r(int s3r)
        {
            this.addCommand("cfg s3r=" + s3r.ToString("d"));
        }

        /// <summary>config s4 falling-edge function</summary>
        /// <param name="s4f"></param>
        public void cfgS4f(int s4f)
        {
            this.addCommand("cfg s4f=" + s4f.ToString("d"));
        }

        /// <summary>config s4 raising-edge function</summary>
        /// <param name="s4r"></param>
        public void cfgS4r(int s4r)
        {
            this.addCommand("cfg s4r=" + s4r.ToString("d"));
        }

        /// <summary>config s5 falling-edge function</summary>
        /// <param name="s5f"></param>
        public void cfgS5f(int s5f)
        {
            this.addCommand("cfg s5f=" + s5f.ToString("d"));
        }

        /// <summary>config s5 raising-edge function</summary>
        /// <param name="s5r"></param>
        public void cfgS5r(int s5r)
        {
            this.addCommand("cfg s5r=" + s5r.ToString("d"));
        }

        /// <summary>config s6 falling-edge function</summary>
        /// <param name="s6f"></param>
        public void cfgS6f(int s6f)
        {
            this.addCommand("cfg s6f=" + s6f.ToString("d"));
        }

        /// <summary>config s6 raising-edge function</summary>
        /// <param name="s6r"></param>
        public void cfgS6r(int s6r)
        {
            this.addCommand("cfg s6r=" + s6r.ToString("d"));
        }

        /// <summary>config zero parameters</summary>
        /// <param name="zmd">zero mode</param>
        /// <param name="osv">open-state level</param>
        /// <param name="snr">sensor number</param>
        /// <param name="zsd">zero speed</param>
        /// <param name="zsp">safe position</param>
        public void cfgZero(int zmd, int osv, int snr, float zsd, int zsp)
        {
            this.addCommand("cfg zmd=" + zmd.ToString("d") + " osv=" + osv.ToString("d") + " snr=" + snr.ToString("d") + " zsd=" + zsd.ToString("f") + " zsp=" + zsp.ToString("d"));
        }

        /// <summary>enable motor</summary>
        public void enable()
        {
            this.addCommand("ena");
        }

        /// <summary>disable motor</summary>
        public void disable()
        {
            this.addCommand("off");
        }

        /// <summary>speed move</summary>
        public void move()
        {
            this.addCommand("mov");
        }

        /// <summary>relative move</summary>
        public void rmove(int distance)
        {
            this.addCommand("rmv " + distance.ToString("d"));
        }

        /// <summary>position move</summary>
        /// <param name="pos"></param>
        public void moveto(int pos)
        {
            this.addCommand("pos " + pos.ToString("d"));
        }

        /// <summary>stop</summary>
        /// <param name="mode"></param>
        public void stop(int mode)
        {
            this.addCommand("stp " + mode.ToString("d"));
        }

        /// <summary>set current position as 0 position</summary>
        public void org()
        {
            this.addCommand(nameof(org));
        }

        /// <summary>s3 on (high level)</summary>
        public void S3On()
        {
            this.addCommand("s3 on");
        }

        /// <summary>s3 off (low level)</summary>
        public void S3Off()
        {
            this.addCommand("s3 off");
        }

        /// <summary>s4 on (high level)</summary>
        public void S4On()
        {
            this.addCommand("s4 on");
        }

        /// <summary>s4 off (low level)</summary>
        public void S4Off()
        {
            this.addCommand("s4 off");
        }

        /// <summary>s5 on (high level)</summary>
        public void S5On()
        {
            this.addCommand("s5 on");
        }

        /// <summary>s5 off (low level)</summary>
        public void S5Off()
        {
            this.addCommand("s5 off");
        }

        /// <summary>s6 on (high level)</summary>
        public void S6On()
        {
            this.addCommand("s6 on");
        }

        /// <summary>s6 off (low level)</summary>
        public void S6Off()
        {
            this.addCommand("s6 off");
        }
        /// <summary>
        /// set s3 mode
        /// </summary>
        /// <param name="mode">0: input mode, 1: output mode</param>
        public void S3Mode(int mode)
        {
            this.addCommand("cfg s3=" + mode.ToString());
        }

        /// <summary>pre-position (set position)</summary>
        /// <param name="pos"></param>
        public void pps(int pos)
        {
            this.addCommand("pps " + pos.ToString("d"));
        }

        /// <summary>pre-position (moveto position)</summary>
        public void pps()
        {
            this.addCommand(nameof(pps));
        }

        /// <summary>get status (speed, position, status bits)</summary>
        public void sts()
        {
            this.addCommand(nameof(sts));
        }

        /// <summary>start zero function</summary>
        public void zeroStart()
        {
            this.addCommand("zero start");
        }

        /// <summary>stop zero function</summary>
        public void zeroStop()
        {
            this.addCommand("zero stop");
        }

        /// <summary>value definition</summary>
        [StructLayout(LayoutKind.Explicit, Size = 4)]
        private struct TokenValue
        {
            [FieldOffset(0)]
            public int iData;
            [FieldOffset(0)]
            public uint uData;
            [FieldOffset(0)]
            public float fData;
        }
    }
}
