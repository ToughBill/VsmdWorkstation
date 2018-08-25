using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Threading;

namespace VsmdLib
{
    /// <summary>Vsmd Class</summary>
    public class Vsmd
    {
        /// <summary>device list</summary>
        private List<VsmdInfo> objList = new List<VsmdInfo>();
        /// <summary>serial port object</summary>
        private SerialPort com_port = new SerialPort();
        /// <summary>
        /// 
        /// </summary>
        private Thread serial_port_thread = new Thread(new ParameterizedThreadStart(Vsmd.serial_port_thread_process));
        /// <summary>serial port recieve thread</summary>
        private Thread serial_port_recieve_thread = new Thread(new ParameterizedThreadStart(Vsmd.serial_port_recieve_thread_process));
        /// <summary>
        /// 
        /// </summary>
        private byte[] recieveBuffer = new byte[1024];
        private VsmdTimer waitResTimer = new VsmdTimer(1000L);
        /// <summary>retry counter</summary>
        private int retryCnt;
        private string curCommand;
        private int recieveBufferSize;
        private bool flgResWaiting;

        /// <summary>
        /// 
        /// </summary>
        public SerialPort comPort
        {
            get
            {
                return this.com_port;
            }
        }

        /// <summary>open serail port</summary>
        /// <param name="port"></param>
        /// <param name="baudrate"></param>
        /// <returns></returns>
        public bool openSerailPort(string port, int baudrate)
        {
            if (baudrate < 2400 || baudrate > 921600)
                return false;
            this.comPort.PortName = port;
            this.comPort.BaudRate = baudrate;
            this.comPort.Parity = Parity.None;
            this.comPort.DataBits = 8;
            this.comPort.StopBits = StopBits.One;
            try
            {
                this.comPort.Open();
            }
            catch
            {
                return false;
            }
            this.isSerialPortThreadRunning = true;
            this.serial_port_thread.Priority = ThreadPriority.Highest;
            this.serial_port_thread.Start((object)this);
            this.isSerialPortRecieveThreadRunning = true;
            this.serial_port_recieve_thread.Priority = ThreadPriority.Highest;
            this.serial_port_recieve_thread.Start((object)this);
            return true;
        }

        /// <summary>open serial port</summary>
        /// <param name="port"></param>
        /// <param name="baudrate"></param>
        /// <returns></returns>
        public bool openSerialPort(string port, int baudrate)
        {
            return this.openSerailPort(port, baudrate);
        }

        /// <summary>close serial port</summary>
        /// <returns></returns>
        public bool closeSerailPort()
        {
            bool flag = true;
            this.isSerialPortThreadRunning = false;
            this.isSerialPortRecieveThreadRunning = false;
            try
            {
                this.comPort.DiscardInBuffer();
                this.comPort.DiscardOutBuffer();
                this.comPort.Close();
            }
            catch
            {
                flag = this.comPort.IsOpen && false;
            }
            return flag;
        }

        /// <summary>close serial port</summary>
        /// <returns></returns>
        public bool closeSerialPort()
        {
            return this.closeSerailPort();
        }

        private bool isSerialPortThreadRunning { get; set; }

        /// <summary>
        /// 
        /// </summary>
        private void serialPortSendProcess()
        {
            int index = 0;
            VsmdInfo vsmdInfo = (VsmdInfo)null;
            while (this.isSerialPortThreadRunning)
            {
                if (this.objList.Count > 0 && !this.flgResWaiting)
                {
                    string str = (string)null;
                    if (this.objList[index].isOnline)
                        str = this.objList[index].sendCmdProcess();
                    if (str != null && this.comPort.IsOpen)
                    {
                        this.curCommand = str;
                        this.retryCnt = 0;
                        vsmdInfo = this.objList[index];
                        this.waitResTimer.start(500000L);
                        this.flgResWaiting = true;
                        this.comPort.Write(this.curCommand);
                    }
                    ++index;
                    if (index >= this.objList.Count)
                        index = 0;
                }
                else if (this.flgResWaiting && this.waitResTimer.isTimeout())
                {
                    ++this.retryCnt;
                    if (this.retryCnt >= 3)
                    {
                        this.flgResWaiting = false;
                        this.retryCnt = 0;
                        vsmdInfo.isOnline = false;
                    }
                    else
                        this.comPort.Write(this.curCommand);
                }
                Thread.Sleep(0);
            }
        }

        /// <summary>serial port thread process</summary>
        /// <param name="obj"></param>
        private static void serial_port_thread_process(object obj)
        {
            ((Vsmd)obj).serialPortSendProcess();
        }

        private bool isSerialPortRecieveThreadRunning { get; set; }

        /// <summary>serial port recieve process</summary>
        private void serialPortRecieveProcess()
        {
            while (this.isSerialPortRecieveThreadRunning)
            {
                try
                {
                    if (this.comPort.BytesToRead > 0)
                    {
                        byte[] buffer = new byte[this.comPort.BytesToRead];
                        this.comPort.Read(buffer, 0, buffer.Length);
                        foreach (byte data in buffer)
                            this.parse(data);
                    }
                }
                catch
                {
                }
                Thread.Sleep(0);
            }
        }

        /// <summary>parse response data</summary>
        /// <param name="data"></param>
        private void parse(byte data)
        {
            switch (data)
            {
                case 254:
                    if (this.recieveBuffer[0] == byte.MaxValue)
                    {
                        this.recieveBuffer[this.recieveBufferSize] = data;
                        ++this.recieveBufferSize;
                        byte[] res = new byte[this.recieveBufferSize];
                        Buffer.BlockCopy((Array)this.recieveBuffer, 0, (Array)res, 0, this.recieveBufferSize);
                        if (this.bcc_checksum(res))
                        {
                            for (int index = 0; index < this.objList.Count; ++index)
                            {
                                if (this.objList[index].Cid == (int)res[1])
                                {
                                    this.objList[index].parse(res);
                                    break;
                                }
                            }
                        }
                        this.flgResWaiting = false;
                        break;
                    }
                    goto default;
                case byte.MaxValue:
                    this.recieveBufferSize = 0;
                    this.recieveBuffer[this.recieveBufferSize] = data;
                    ++this.recieveBufferSize;
                    break;
                default:
                    if (this.recieveBuffer[0] == byte.MaxValue)
                    {
                        this.recieveBuffer[this.recieveBufferSize] = data;
                        ++this.recieveBufferSize;
                        break;
                    }
                    break;
            }
            if (this.recieveBufferSize < 3)
                return;
            this.waitResTimer.start(2000000L);
        }

        /// <summary>bcc check</summary>
        /// <param name="res"></param>
        /// <returns></returns>
        private bool bcc_checksum(byte[] res)
        {
            byte num = (byte)((uint)(byte)((uint)res[res.Length - 3] << 7) | (uint)res[res.Length - 2]);
            for (int index = 1; index < res.Length - 3; ++index)
                num ^= res[index];
            return num == (byte)0;
        }

        /// <summary>serial port recieve thread process</summary>
        /// <param name="obj"></param>
        private static void serial_port_recieve_thread_process(object obj)
        {
            ((Vsmd)obj).serialPortRecieveProcess();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cid"></param>
        /// <returns></returns>
        public VsmdInfo createVsmdInfo(int cid)
        {
            VsmdInfo vsmdInfo = new VsmdInfo(cid);
            vsmdInfo.comPort = this.comPort;
            this.objList.Add(vsmdInfo);
            return vsmdInfo;
        }

        /// <summary>remove VsmdInfo object</summary>
        /// <param name="info"></param>
        public void removeVsmdInfo(VsmdInfo info)
        {
            this.objList.Remove(info);
        }
    }
}
