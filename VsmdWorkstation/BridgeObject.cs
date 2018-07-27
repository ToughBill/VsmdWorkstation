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

namespace VsmdWorkstation
{
    public delegate void GridPageDomLoaded();
    public class BridgeObject
    {
        public event GridPageDomLoaded onGridPageDomLoaded = null;
        private ChromiumWebBrowser m_browser;
        private Thread m_moveThread;
        public BridgeObject(ChromiumWebBrowser browser)
        {
            m_browser = browser;
        }
        public void Move()
        {
            VsmdController vsmdController = VsmdController.GetVsmdController();
            if (!vsmdController.IsInitialized())
            {
                StatusBar.DisplayMessage(MessageType.Error, "未初始化控制器！");
                return;
            }
            CallJS("JsExecutor.startDrip()");
        }
        public void StartDrip(string args)
        {
            if (m_moveThread != null && m_moveThread.IsAlive)
            {
                m_moveThread.Abort();
            }
            m_moveThread = new Thread(new ParameterizedThreadStart(MoveThread));
            m_moveThread.Start(args);
        }
        public void StopMove()
        {
            if (m_moveThread != null && m_moveThread.IsAlive)
            {
                m_moveThread.Abort();
                AfterMove();
            }
        }
        public void PauseMove()
        {

        }
        public void BuildGrid(BoardMeta board)
        {
            JObject opts = new JObject();
            opts.Add("blockCount", board.BlockCount);
            opts.Add("rowCount", board.RowCount);
            opts.Add("columnCount", board.ColumnCount);

            CallJS("JsExecutor.buildGrid(" + opts.ToString() + ");");
        }
        private async void MoveThread(object args)
        {
            BoardSetting curBoardSetting = BoardSetting.GetInstance();
            JArray jsArr = (JArray)JsonConvert.DeserializeObject(args.ToString());
            BeforeMove();
            VsmdController vsmdController = VsmdController.GetVsmdController();
            await vsmdController.MoveToSync(VsmdAxis.X, 0);
            await vsmdController.MoveToSync(VsmdAxis.Y, 0);

            for (int i = 0; i < jsArr.Count; i++)
            {
                JObject obj = (JObject)jsArr[i];
                int row = int.Parse(obj["row"].ToString());
                int col = int.Parse(obj["column"].ToString());
                await vsmdController.MoveToSync(VsmdAxis.X, curBoardSetting.Convert2PhysicalPos(VsmdAxis.X, col));
                await vsmdController.MoveToSync(VsmdAxis.Y, curBoardSetting.Convert2PhysicalPos(VsmdAxis.Y, row));
                MoveCallBack(row, col);
                System.Threading.Thread.Sleep(1000);
                //await Task.Delay(1000);
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
        private void BeforeMove()
        {
            CallJS("JsExecutor.beforeMove();");
        }
        private void AfterMove()
        {
            CallJS("JsExecutor.afterMove();");
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
