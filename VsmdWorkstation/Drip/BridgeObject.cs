using CefSharp;
using CefSharp.WinForms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Threading;
using System.Threading.Tasks;
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
        private JArray m_selectedTubes = new JArray();
        private int m_dripIndex;
        private static bool m_cefInitialized;

        public static void InitializeCef()
        {
            if (!m_cefInitialized)
            {
                CefSharpSettings.LegacyJavascriptBindingEnabled = true;
                CefSettings setting = new CefSettings();
                setting.RemoteDebuggingPort = 7073;
                Cef.Initialize(setting);
                m_cefInitialized = true;
            }
        }

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
            float spdX = vsmdController.GetAxis(VsmdAxis.X).GetAttributeValue(VsmdLib.VsmdAttribute.Spd);
            float spdY = vsmdController.GetAxis(VsmdAxis.Y).GetAttributeValue(VsmdLib.VsmdAttribute.Spd);
            float spdZ = vsmdController.GetAxis(VsmdAxis.Z).GetAttributeValue(VsmdLib.VsmdAttribute.Spd);
            string errAxis = "";
            if(spdX <= 0.0)
            {
                errAxis += "X";
            }
            if (spdY <= 0.0)
            {
                if(errAxis.Length > 0)
                {
                    errAxis += ",";
                }
                errAxis += "Y";
            }
            if (spdZ <= 0.0)
            {
                if (errAxis.Length > 0)
                {
                    errAxis += ",";
                }
                errAxis += "Z";
            }
            if(errAxis.Length > 0)
            {
                StatusBar.DisplayMessage(MessageType.Error, "控制轴" + errAxis + "速度不能为0！");
            }
            else
            {
                CallJS("JsExecutor.startDrip()");
            }
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
            VsmdController.GetVsmdController().Stop();
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
            m_isFromPause = true;
            m_dripStatus = DripStatus.Moving;
            m_moveThread = new Thread(new ThreadStart(DripThread));
            m_moveThread.Start();
        }
        public void SelectAllTubes()
        {
            CallJS("JsExecutor.selectAllTubes()");
        }
        public void ReverseSelect()
        {
            CallJS("JsExecutor.reverseSelect()");
        }
        public void ResetBoard()
        {
            CallJS("JsExecutor.resetTube()");
        }
        public void BuildGrid(BoardMeta board)
        {
            JObject opts = new JObject();
            opts.Add("type", board.Type);
            opts.Add("gridCount", board.GridCount);
            opts.Add("siteCount", board.SiteCount);
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
            PumpController pumpController = PumpController.GetPumpController();
            BoardSetting curBoardSetting = BoardSetting.GetInstance();
            JArray jsArr = m_selectedTubes;
            await BeforeMove();
            int dripInterval = GeneralSettings.GetInstance().DripInterval;
            int blockNum, row, col = 1;
            //await vsmdController.SetS3Mode(VsmdAxis.Z, 1);
            for (int i = m_dripIndex + 1; i < jsArr.Count; i++)
            {
                if (m_dripStatus != DripStatus.Moving)
                    break;
                JObject obj = (JObject)jsArr[i];
                GetPositionInfo(obj, out blockNum, out row, out col);
                SetDrippingTube(blockNum, row, col);
                //await vsmdController.MoveTo(VsmdAxis.X, curBoardSetting.Convert2PhysicalPos(VsmdAxis.X, blockNum, col));
                //await vsmdController.MoveTo(VsmdAxis.Y, curBoardSetting.Convert2PhysicalPos(VsmdAxis.Y, blockNum, row));

                var moveXTask = vsmdController.MoveTo(VsmdAxis.X, curBoardSetting.Convert2PhysicalPos(VsmdAxis.X, blockNum, col));
                var moveYTask = vsmdController.MoveTo(VsmdAxis.Y, curBoardSetting.Convert2PhysicalPos(VsmdAxis.Y, blockNum, row));
                await Task.WhenAll(moveXTask, moveYTask);

                // TODO
                await vsmdController.MoveTo(VsmdAxis.Z, curBoardSetting.CurrentBoard.ZDispense);

                // start drip
                //await vsmdController.ClickPump();
                await pumpController.Drip();
                // wait 5 seconds, this time should be changed according to the volume dripped
                Thread.Sleep(dripInterval);
                await vsmdController.MoveTo(VsmdAxis.Z, curBoardSetting.CurrentBoard.ZTravel);

                // change the screen to start
                await pumpController.Drip();
                //await Task.Delay(1000);

                MoveCallBack(blockNum, row, col);
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
                await vsmdController.ZeroStart(VsmdAxis.Z);
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
            int blockNum, row, col;
            GetPositionInfo(targetTube, out blockNum, out row, out col);
            VsmdController vsmdController = VsmdController.GetVsmdController();
            BoardSetting curBoardSetting = BoardSetting.GetInstance();
            //await vsmdController.MoveTo(VsmdAxis.X, curBoardSetting.Convert2PhysicalPos(VsmdAxis.X, blockNum, col));
            //await vsmdController.MoveTo(VsmdAxis.Y, curBoardSetting.Convert2PhysicalPos(VsmdAxis.Y, blockNum, row));

            var moveXTask = vsmdController.MoveTo(VsmdAxis.X, curBoardSetting.Convert2PhysicalPos(VsmdAxis.X, blockNum, col));
            var moveYTask = vsmdController.MoveTo(VsmdAxis.Y, curBoardSetting.Convert2PhysicalPos(VsmdAxis.Y, blockNum, row));
            await Task.WhenAll(moveXTask, moveYTask);
        }
        public async void DripTube(string args)
        {
            JObject targetTube = (JObject)JsonConvert.DeserializeObject(args);
            int type = int.Parse(targetTube["type"].ToString());
            int blockNum, row, col = 1;
            if (type == (int)BoardType.Site)
            {
                blockNum = int.Parse(targetTube["site"].ToString());
                col = int.Parse(targetTube["col"].ToString());
            }
            else
            {
                blockNum = int.Parse(targetTube["grid"].ToString());
            }
            row = int.Parse(targetTube["row"].ToString());
            SetDrippingTube(blockNum, row, col);
            VsmdController vsmdController = VsmdController.GetVsmdController();
            PumpController pumpController = PumpController.GetPumpController();
            BoardSetting curBoardSetting = BoardSetting.GetInstance();
            await vsmdController.MoveTo(VsmdAxis.Z, curBoardSetting.CurrentBoard.ZDispense);
            await pumpController.Drip();

            Thread.Sleep(GeneralSettings.GetInstance().DripInterval);

            await pumpController.Drip();
            await vsmdController.MoveTo(VsmdAxis.Z, curBoardSetting.CurrentBoard.ZTravel);

            MoveCallBack(blockNum, row, col);
        }

        private void GetPositionInfo(JObject jobj, out int blockNum, out int row, out int col)
        {
            blockNum = row = col = 1;
            int type = int.Parse(jobj["type"].ToString());
            if (type == (int)BoardType.Site)
            {
                blockNum = int.Parse(jobj["site"].ToString());
                col = int.Parse(jobj["col"].ToString());
            }
            else
            {
                blockNum = int.Parse(jobj["grid"].ToString());
            }
            row = int.Parse(jobj["row"].ToString());
        }
        private void SetDrippingTube(int blockNum, int row, int col)
        {
            CallJS("JsExecutor.setDrippingTube(" + blockNum + "," + row + "," + col + ");");
        }
        private void MoveCallBack(int blockNum, int row, int col)
        {
            CallJS("JsExecutor.moveCallBack(" + blockNum + "," + row + "," + col + ");");
        }
        public void CallJS(string jsCode)
        {
            m_browser.GetBrowser().MainFrame.ExecuteJavaScriptAsync(jsCode);
        }
    }
}
