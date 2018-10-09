using CefSharp;
using CefSharp.WinForms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
        public event DelDripFinished onPipettingFinished = null;
        private ChromiumWebBrowser m_browser;
        private Thread m_moveThread;
        private PipettingStatus m_dripStatus = PipettingStatus.Idle;
        private bool m_isFromPause = false;
        private JArray m_selectedTubes = new JArray();
        private int m_pipettingIndex;
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
            m_pipettingIndex = -1;
            DoPipetting();
            //m_moveThread = new Thread(new ThreadStart(PipettingThread));
            //m_moveThread.Start();
        }
        public void StopMove()
        {
            PipettingStatus preMode = m_dripStatus;
            m_dripStatus = PipettingStatus.Idle;
            m_selectedTubes.Clear();
            VsmdController.GetVsmdController().Stop();
            if (preMode == PipettingStatus.PauseMove)
            {
                AfterMove();
            }
        }
        public void PauseMove()
        {
            CallJS("JsExecutor.pauseMove()");
            m_dripStatus = PipettingStatus.PauseMove;
        }
        public void ResumeMove()
        {
            m_isFromPause = true;
            m_dripStatus = PipettingStatus.Moving;
            m_moveThread = new Thread(new ThreadStart(DoPipetting));
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
        private async void DoPipetting()
        {
            VsmdController vsmdController = VsmdController.GetVsmdController();
            PumpController pumpController = PumpController.GetPumpController();
            BoardSetting curBoardSetting = BoardSetting.GetInstance();
            JArray jsArr = m_selectedTubes;

            await BeforeMove(m_selectedTubes.Count);
            int pipettingInterval = (int)(Preference.GetInstace().Volume * GeneralSettings.GetInstance().PipettingSpeed/(1000.0));
            int blockNum, row, col = 1;
            //await vsmdController.SetS3Mode(VsmdAxis.Z, 1);
            for (int i = m_pipettingIndex + 1; i < jsArr.Count; i++)
            {
                if (m_dripStatus != PipettingStatus.Moving)
                    break;
                JObject obj = (JObject)jsArr[i];
                GetPositionInfo(obj, out blockNum, out row, out col);
                SetPipettingWell(blockNum, row, col);
                await vsmdController.MoveTo(VsmdAxis.X, curBoardSetting.Convert2PhysicalPos(VsmdAxis.X, blockNum, col));
                await vsmdController.MoveTo(VsmdAxis.Y, curBoardSetting.Convert2PhysicalPos(VsmdAxis.Y, blockNum, row));
                // TODO
                await vsmdController.MoveTo(VsmdAxis.Z, curBoardSetting.CurrentBoard.ZDispense);

                // start pipetting
                await pumpController.SwitchOnOff();
                // wait several seconds, this time should be changed according to the volume dispensed
                Thread.Sleep(pipettingInterval);
                await vsmdController.MoveTo(VsmdAxis.Z, curBoardSetting.CurrentBoard.ZTravel);
                await pumpController.SwitchOnOff();
                // change the UI to start
                await Task.Delay(100);

                MoveCallBack(blockNum, row, col);
                m_pipettingIndex = i;
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
        private async Task<bool> BeforeMove(int wellCnt)
        {
            m_dripStatus = PipettingStatus.Moving;
            CallJS("JsExecutor.beforeMove();");
            //if (!m_isFromPause && wellCnt > 0)
            //{
            //    VsmdController vsmdController = VsmdController.GetVsmdController();
            //    await vsmdController.ZeroStart(VsmdAxis.Z);
            //    await vsmdController.ZeroStart(VsmdAxis.X);
            //    await vsmdController.ZeroStart(VsmdAxis.Y);
            //    //vsmdController.Homed = true;
            //}

            return true;
        }
        private void AfterMove()
        {
            //await VsmdController.GetVsmdController().Off(VsmdAxis.X);
            //await VsmdController.GetVsmdController().Off(VsmdAxis.Y);
            //await VsmdController.GetVsmdController().Off(VsmdAxis.Z);
            if (m_dripStatus != PipettingStatus.PauseMove)
            {
                CallJS("JsExecutor.afterMove();");

                m_isFromPause = false;

                m_dripStatus = PipettingStatus.Idle;
                m_selectedTubes.Clear();
                if (onPipettingFinished != null)
                {
                    onPipettingFinished();
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
            SetPipettingWell(blockNum, row, col);
            VsmdController vsmdController = VsmdController.GetVsmdController();
            PumpController pumpController = PumpController.GetPumpController();
            BoardSetting curBoardSetting = BoardSetting.GetInstance();
            await vsmdController.MoveTo(VsmdAxis.Z, curBoardSetting.CurrentBoard.ZDispense);
            await pumpController.SwitchOnOff();

            Thread.Sleep(GeneralSettings.GetInstance().DispenseInterval);

            await pumpController.SwitchOnOff();
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
        private void SetPipettingWell(int blockNum, int row, int col)
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
