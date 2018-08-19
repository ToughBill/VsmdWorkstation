using CefSharp.WinForms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using VsmdWorkstation.Controls;

namespace VsmdWorkstation
{
    public delegate void GridPageDomLoaded();
    public delegate void DelDripFinished();
    public class BridgeObject
    {
        public event GridPageDomLoaded onGridPageDomLoaded = null;
        public event DelDripFinished onDripFinished = null;
        private ChromiumWebBrowser m_browser;
        private Thread m_moveThread;
        private DripStatus m_dripStatus = DripStatus.Idle;
        private bool m_isFromPause = false;
        private JArray m_selectedTubes;
        private int m_dripIndex;
        public BridgeObject(ChromiumWebBrowser browser)
        {
            m_browser = browser;
        }
        public void Move()
        {
            VsmdController vsmdController = VsmdController.GetVsmdController();
            if (!vsmdController.IsInitialized())
            {
                StatusBar.DisplayMessage(MessageType.Error, "设备未连接！");
                //return;
            }
            CallJS("JsExecutor.startDrip()");
        }
        /// <summary>
        /// called from JS
        /// </summary>
        /// <param name="args"></param>
        public void StartDrip(string args)
        {
            m_selectedTubes = (JArray)JsonConvert.DeserializeObject(args.ToString());
            m_dripIndex = 0;
            m_moveThread = new Thread(new ThreadStart(DripThread));
            m_moveThread.Start();
        }
        public void StopMove()
        {
            m_dripStatus = DripStatus.Idle;
        }
        public void PauseMove()
        {
            CallJS("JsExecutor.pauseMove()");
            m_dripStatus = DripStatus.PauseMove;
        }
        public void ResumeMove()
        {
            //CallJS("JsExecutor.startDrip()");
            m_isFromPause = true;
            m_dripStatus = DripStatus.Moving;
            m_moveThread = new Thread(new ThreadStart(DripThread));
            m_moveThread.Start();
        }
        public void ResetBoard()
        {
            CallJS("JsExecutor.resetTube()");
        }
        public void BuildGrid(BoardMeta board)
        {
            JObject opts = new JObject();
            opts.Add("blockCount", board.BlockCount);
            opts.Add("rowCount", board.RowCount);
            opts.Add("columnCount", board.ColumnCount);

            CallJS("JsExecutor.buildGrid(" + opts.ToString() + ");");
        }
        /// <summary>
        /// 
        /// </summary>
        private async void DripThread()
        {
            VsmdController vsmdController = VsmdController.GetVsmdController();
            //await vsmdController.ResetVsmdController();
            BoardSetting curBoardSetting = BoardSetting.GetInstance();
            JArray jsArr = m_selectedTubes;
            BeforeMove();
            float oriSpeedY = vsmdController.GetAxis(VsmdAxis.Y).GetAttributeValue(VsmdLib.VsmdAttribute.Spd);
            float oriZsdX = vsmdController.GetAxis(VsmdAxis.X).GetAttributeValue(VsmdLib.VsmdAttribute.Zsd);
            float oriZsdY = vsmdController.GetAxis(VsmdAxis.Y).GetAttributeValue(VsmdLib.VsmdAttribute.Zsd);
            if (!m_isFromPause)
            {

                //await vsmdController.MoveToSync(VsmdAxis.X, 0);
                //await vsmdController.MoveToSync(VsmdAxis.Y, 0);

                vsmdController.SetZsd(VsmdAxis.X, oriZsdX > 0.0 ? -oriZsdX : oriZsdX);
                vsmdController.SetZsd(VsmdAxis.Y, oriZsdY > 0.0 ? -oriZsdY : oriZsdY);
                Thread.Sleep(20);
                await vsmdController.ZeroStartSync(VsmdAxis.X);
                await vsmdController.ZeroStartSync(VsmdAxis.Y);

                await vsmdController.MoveSync(VsmdAxis.Y);
                vsmdController.Org(VsmdAxis.Y);
                vsmdController.SetSpeed(VsmdAxis.Y, oriSpeedY > 0.0 ? -oriSpeedY : oriSpeedY);
            }

            Thread.Sleep(20);
            vsmdController.SetS3Mode(VsmdAxis.Z, 1);
            Thread.Sleep(20);
            //vsmdController.SetS3Mode(VsmdAxis.Z, 0);
            //vsmdController.SetS3Mode(VsmdAxis.Z, 1);
            for (int i = m_dripIndex; i < jsArr.Count; i++)
            {
                if (m_dripStatus != DripStatus.Moving)
                    break;
                JObject obj = (JObject)jsArr[i];
                int row = int.Parse(obj["row"].ToString());
                int col = int.Parse(obj["column"].ToString());
                await vsmdController.MoveToSync(VsmdAxis.X, curBoardSetting.Convert2PhysicalPos(VsmdAxis.X, col));
                await vsmdController.MoveToSync(VsmdAxis.Y, curBoardSetting.Convert2PhysicalPos(VsmdAxis.Y, row));
                Thread.Sleep(500);
                // start drip
                //vsmdController.SetS3Mode(VsmdAxis.Z, 1);
                //vsmdController.SetS3Mode(VsmdAxis.Z, 0);
                vsmdController.S3On(VsmdAxis.Z);
                Thread.Sleep(500);
                vsmdController.S3Off(VsmdAxis.Z);
                // wait 5 seconds, this time should be changed according to the volume dripped
                Thread.Sleep(5000);

                // change the screen to start
                //vsmdController.SetS3Mode(VsmdAxis.Z, 1);
                //vsmdController.SetS3Mode(VsmdAxis.Z, 0);
                vsmdController.S3On(VsmdAxis.Z);
                Thread.Sleep(500);
                vsmdController.S3Off(VsmdAxis.Z);
                Thread.Sleep(1000);
                //await Task.Delay(1000);

                MoveCallBack(row, col);
                m_dripIndex = i;
            }
            m_isFromPause = false;
            vsmdController.SetZsd(VsmdAxis.X, oriZsdX);
            vsmdController.SetZsd(VsmdAxis.Y, oriZsdY);
            vsmdController.SetSpeed(VsmdAxis.Y, oriSpeedY);
            AfterMove();
        }
        public void DomLoaded()
        {
            if(onGridPageDomLoaded != null)
            {
                onGridPageDomLoaded();
            }
            //BuildGrid(BoardSettings.GetCurrentBoardSetting());
        }
        private void BeforeMove()
        {
            m_dripStatus = DripStatus.Moving;
            CallJS("JsExecutor.beforeMove();");
        }
        private void AfterMove()
        {
            if (m_dripStatus == DripStatus.Moving && onDripFinished != null)
            {
                CallJS("JsExecutor.afterMove();");
                m_dripStatus = DripStatus.Idle;
                onDripFinished();

            }
        }
        private void MoveCallBack(int row, int col)
        {
            CallJS("JsExecutor.moveCallBack(" + row + "," + col + ");");
        }
        public void CallJS(string jsCode)
        {
            m_browser.GetBrowser().MainFrame.ExecuteJavaScriptAsync(jsCode);
        }
    }
}
