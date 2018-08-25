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
            m_dripIndex = -1;
            m_moveThread = new Thread(new ThreadStart(DripThread));
            m_moveThread.Start();
        }
        public void StopMove()
        {
            DripStatus preMode = m_dripStatus;
            m_dripStatus = DripStatus.Idle;
            m_selectedTubes.Clear();
            if (preMode == DripStatus.PauseMove)
            {
                AfterMove();
            }
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
            await BeforeMove();
            int dripInterval = GeneralSettings.GetInstance().DripInterval;
            //await vsmdController.SetS3Mode(VsmdAxis.Z, 1);
            for (int i = m_dripIndex + 1; i < jsArr.Count; i++)
            {
                if (m_dripStatus != DripStatus.Moving)
                    break;
                JObject obj = (JObject)jsArr[i];
                int row = int.Parse(obj["row"].ToString());
                int col = int.Parse(obj["column"].ToString());
                SetDrippingTube(row, col);
                await vsmdController.MoveTo(VsmdAxis.X, curBoardSetting.Convert2PhysicalPos(VsmdAxis.X, col));
                await vsmdController.MoveTo(VsmdAxis.Y, curBoardSetting.Convert2PhysicalPos(VsmdAxis.Y, row));

                // start drip
                await vsmdController.SetS3Mode(VsmdAxis.Z, 1);
                Thread.Sleep(500);
                await vsmdController.S3On(VsmdAxis.Z);
                Thread.Sleep(500);
                await vsmdController.S3Off(VsmdAxis.Z);
                // wait 5 seconds, this time should be changed according to the volume dripped
                Thread.Sleep(dripInterval);

                // change the screen to start
                await vsmdController.S3On(VsmdAxis.Z);
                Thread.Sleep(500);
                await vsmdController.S3Off(VsmdAxis.Z);
                Thread.Sleep(1000);
                //await Task.Delay(1000);

                MoveCallBack(row, col);
                m_dripIndex = i;
            }
            
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
        private async Task<bool> BeforeMove()
        {
            m_dripStatus = DripStatus.Moving;
            CallJS("JsExecutor.beforeMove();");
            if (!m_isFromPause)
            {
                VsmdController vsmdController = VsmdController.GetVsmdController();
                await vsmdController.ZeroStart(VsmdAxis.X);
                await vsmdController.ZeroStart(VsmdAxis.Y);
            }

            return true;
        }
        private void AfterMove()
        {
            if (m_dripStatus != DripStatus.PauseMove)
            {
                CallJS("JsExecutor.afterMove();");

                m_isFromPause = false;

                m_dripStatus = DripStatus.Idle;
                m_selectedTubes.Clear();
                if (onDripFinished != null)
                {
                    onDripFinished();
                }
            }
        }
        public async void MoveToHere(string args)
        {
            JObject targetTube = (JObject)JsonConvert.DeserializeObject(args);
            int row = int.Parse(targetTube["row"].ToString());
            int col = int.Parse(targetTube["col"].ToString());

            VsmdController vsmdController = VsmdController.GetVsmdController();
            BoardSetting curBoardSetting = BoardSetting.GetInstance();
            await vsmdController.MoveTo(VsmdAxis.X, curBoardSetting.Convert2PhysicalPos(VsmdAxis.X, col));
            await vsmdController.MoveTo(VsmdAxis.Y, curBoardSetting.Convert2PhysicalPos(VsmdAxis.Y, row));
        }
        public async void DripTube(string args)
        {
            JObject targetTube = (JObject)JsonConvert.DeserializeObject(args);
            int row = int.Parse(targetTube["row"].ToString());
            int col = int.Parse(targetTube["col"].ToString());
            SetDrippingTube(row, col);
            VsmdController vsmdController = VsmdController.GetVsmdController();
            await vsmdController.SetS3Mode(VsmdAxis.Z, 1);
            Thread.Sleep(500);
            await vsmdController.S3On(VsmdAxis.Z);
            Thread.Sleep(500);
            await vsmdController.S3Off(VsmdAxis.Z);
            Thread.Sleep(GeneralSettings.GetInstance().DripInterval);

            //change the screen to start interface
            await vsmdController.S3On(VsmdAxis.Z);
            Thread.Sleep(500);
            await vsmdController.S3Off(VsmdAxis.Z);
            Thread.Sleep(500);

            MoveCallBack(row, col);
        }
        private void SetDrippingTube(int row, int col)
        {
            CallJS("JsExecutor.setDrippingTube(" + row + "," + col + ");");
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
