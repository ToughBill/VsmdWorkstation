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
                return;
            }
            CallJS("JsExecutor.startDrip()");
        }
        public void StartDrip(string args)
        {
            //if (m_moveThread != null && m_moveThread.IsAlive)
            //{
            //    m_moveThread.Abort();
            //}
            m_moveThread = new Thread(new ParameterizedThreadStart(DripThread));
            m_moveThread.Start(args);
        }
        public void StopMove()
        {
            m_dripStatus = DripStatus.Idle;
            //if (m_moveThread != null)
            //{
            //    m_moveThread.Abort();
            //    AfterMove();
            //}
        }
        public void PauseMove()
        {
            CallJS("JsExecutor.pauseMove()");
            m_dripStatus = DripStatus.PauseMove;
        }
        public void ResumeMove()
        {
            CallJS("JsExecutor.startDrip()");
            m_isFromPause = true;
            m_dripStatus = DripStatus.Moving;
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
        private async void DripThread(object args)
        {
            BoardSetting curBoardSetting = BoardSetting.GetInstance();
            JArray jsArr = (JArray)JsonConvert.DeserializeObject(args.ToString());
            BeforeMove();
            VsmdController vsmdController = VsmdController.GetVsmdController();
            await vsmdController.MoveToSync(VsmdAxis.X, 0);
            await vsmdController.MoveToSync(VsmdAxis.Y, 0);
            
            for (int i = 0; i < jsArr.Count; i++)
            {
                if (m_dripStatus != DripStatus.Moving)
                    break;
                JObject obj = (JObject)jsArr[i];
                int row = int.Parse(obj["row"].ToString());
                int col = int.Parse(obj["column"].ToString());
                await vsmdController.MoveToSync(VsmdAxis.X, curBoardSetting.Convert2PhysicalPos(VsmdAxis.X, col));
                await vsmdController.MoveToSync(VsmdAxis.Y, curBoardSetting.Convert2PhysicalPos(VsmdAxis.Y, row));
                if (i > 0 || m_isFromPause)
                {
                    vsmdController.SetS3Mode(VsmdAxis.Z, 1);
                    vsmdController.SetS3Mode(VsmdAxis.Z, 0);
                    Thread.Sleep(1500);
                }
                vsmdController.SetS3Mode(VsmdAxis.Z, 1);
                vsmdController.SetS3Mode(VsmdAxis.Z, 0);
                Thread.Sleep(4000);
                MoveCallBack(row, col);
                
                //await Task.Delay(1000);
            }
            m_isFromPause = false;
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
            CallJS("JsExecutor.afterMove();");
            if (m_dripStatus == DripStatus.Moving && onDripFinished != null)
            {
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
