// Decompiled with JetBrains decompiler
// Type: VsmdLib.VsmdTimer
// Assembly: VsmdLib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6A01B76B-246D-4E84-8347-E0FC1DCA7B8D
// Assembly location: C:\Data\code\VsmdWorkstation\trunk\VsmdWorkstation\sdk\VsmdLib.dll

using System;

namespace VsmdLib
{
    /// <summary>
    /// 
    /// </summary>
    internal class VsmdTimer
    {
        /// <summary>for internal timer.</summary>
        private long lastTick = -1;
        private long interval = -1;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="us"></param>
        public VsmdTimer(long us)
        {
            this.interval = us * 10L;
        }

        /// <summary>start a timer</summary>
        /// <param name="us"></param>
        public void start(long us)
        {
            this.interval = us * 10L;
            this.restart();
        }

        /// <summary>restart timer using last interval</summary>
        public void restart()
        {
            this.lastTick = DateTime.Now.Ticks;
        }

        /// <summary>wait for timeout</summary>
        /// <param name="us"></param>
        public void wait(long us)
        {
            this.start(us);
            do
                ;
            while (!this.isTimeout());
        }

        /// <summary>stop timer.</summary>
        public void stop()
        {
            this.lastTick = -1L;
        }

        /// <summary>check timeout</summary>
        /// <returns></returns>
        public bool isTimeout()
        {
            bool flag = false;
            if (this.lastTick != -1L)
                flag = DateTime.Now.Ticks - this.lastTick > this.interval;
            return flag;
        }
    }
}
