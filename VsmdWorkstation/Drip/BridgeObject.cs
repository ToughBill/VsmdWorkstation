using CefSharp;
using CefSharp.WinForms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using VsmdWorkstation.Controls;

namespace VsmdWorkstation
{
    public delegate void GridPageDomLoaded();
    public delegate void GridTubeSelected(int totalSelectedCount);
    public delegate void DelDripFinished();
    public class BridgeObject
    {
        protected static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public event GridPageDomLoaded onGridPageDomLoaded = null;
        public event GridTubeSelected onGridTubeSelected = null;
        public event DelDripFinished onPipettingFinished = null;
        private ChromiumWebBrowser m_browser;
        private Thread m_moveThread;
        private PipettingStatus m_dripStatus = PipettingStatus.Idle;
        private bool m_isFromPause = false;
        private JArray m_selectedTubes = new JArray();
        private JToken[] m_sortedTubesArr = null;
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
            m_sortedTubesArr = new JToken[m_selectedTubes.Count];
            m_selectedTubes.CopyTo(m_sortedTubesArr, 0);
            Array.Sort(m_sortedTubesArr, (token1, token2) =>
            {
                int ret = 1;
                JObject obj1 = (JObject)token1;
                JObject obj2 = (JObject)token2;
                int type = int.Parse(obj1["type"].ToString());
                if (type == (int)BoardType.Site)
                {
                    int site1, site2, row1, row2, col1, col2;
                    site1 = int.Parse(obj1["site"].ToString());
                    site2 = int.Parse(obj2["site"].ToString());
                    row1 = int.Parse(obj1["row"].ToString());
                    row2 = int.Parse(obj2["row"].ToString());
                    col1 = int.Parse(obj1["col"].ToString());
                    col2 = int.Parse(obj2["col"].ToString());
                    if (col1 < col2)
                    {
                        ret = -1;
                    }
                    else if (col1 == col2)
                    {
                        ret = ((site1 - 1) * BoardSetting.GetInstance().CurrentBoard.RowCount + row1) < ((site2 - 1) * BoardSetting.GetInstance().CurrentBoard.RowCount + row2) ? -1 : 1;
                    }
                    else
                    {
                        ret = 1;
                    }
                }
                else
                {
                    int grid1, grid2, row1, row2;
                    grid1 = int.Parse(obj1["grid"].ToString());
                    grid2 = int.Parse(obj2["grid"].ToString());
                    row1 = int.Parse(obj1["row"].ToString());
                    row2 = int.Parse(obj2["row"].ToString());
                    if (grid1 < grid2)
                    {
                        ret = -1;
                    }
                    else if (grid1 == grid2)
                    {
                        ret = row1 < row2 ? -1 : 1;
                    }
                    else
                    {
                        ret = 1;
                    }
                }
                return ret;
            });
            m_pipettingIndex = -1;
            DoPipetting();
            m_selectedTubes.Clear();
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
        public void OnTubeSelected(int totalCount)
        {
            if(onGridTubeSelected != null)
            {
                onGridTubeSelected(totalCount);
            }
        }
        public void SelectAllTubes()
        {
            CallJS("JsExecutor.selectAllTubes()");
        }
        public void SelectTubes(int count)
        {
            CallJS("JsExecutor.selectTubes(" + count.ToString() + ")");
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
            //Logger.Instance.Write("DoPipetting");
            log.Info("Do Pipetting");
            VsmdController vsmdController = VsmdController.GetVsmdController();
            PumpController pumpController = PumpController.GetPumpController();
            BoardSetting curBoardSetting = BoardSetting.GetInstance();
            await BeforeMove(m_selectedTubes.Count);
            int touchDelaySeconds = (int)(1000*curBoardSetting.CurrentBoard.DelaySeconds);
            int delayMicroSecondsBetweenSamples = (int)(Preference.GetInstace().DelaySeconds * 1000);
            int blockNum, row, col = 1;
            //await vsmdController.SetS3Mode(VsmdAxis.Z, 1);
            for (int i = m_pipettingIndex + 1; i < m_sortedTubesArr.Length; i++)
            {
                if (m_dripStatus != PipettingStatus.Moving)
                    break;
                JObject obj = (JObject)m_sortedTubesArr[i];
                GetPositionInfo(obj, out blockNum, out row, out col);
                SetPipettingWell(blockNum, row, col);
                int xPos = curBoardSetting.Convert2PhysicalPos(VsmdAxis.X, blockNum, col);
                int offset = curBoardSetting.CurrentBoard.TouchEdgeOffset;
                await vsmdController.MoveTo(VsmdAxis.X, xPos);
                await vsmdController.MoveTo(VsmdAxis.Y, curBoardSetting.Convert2PhysicalPos(VsmdAxis.Y, blockNum, row));
                // TODO
                await vsmdController.MoveTo(VsmdAxis.Z, curBoardSetting.CurrentBoard.ZDispense);
                // start pipetting
                await pumpController.SwitchOnOff(i);
                // wait several seconds, this time should be changed according to the volume dispensed
                Thread.Sleep(delayMicroSecondsBetweenSamples);
                await vsmdController.MoveTo(VsmdAxis.X, xPos+offset);
                Thread.Sleep(touchDelaySeconds);
                await vsmdController.MoveTo(VsmdAxis.Z, curBoardSetting.CurrentBoard.ZTravel);

                // change the UI to start
                Thread.Sleep(100);

                MoveCallBack(blockNum, row, col);
                m_pipettingIndex = i;
            }
            
            bool bok = await AfterMove();
            log.InfoFormat("finished pipetting, result is: {0}", bok);
            //Logger.Instance.Write("error happened in afterMove");
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
            return true;
        }
        private async Task<bool> AfterMove()
        {
            //await VsmdController.GetVsmdController().Off(VsmdAxis.X);
            //await VsmdController.GetVsmdController().Off(VsmdAxis.Y);
            bool bok = await VsmdController.GetVsmdController().MoveTo(VsmdAxis.Y, 100);
            
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
            return bok;
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
            //pumpController.SwitchOnOff();

            Thread.Sleep(GeneralSettings.GetInstance().DispenseInterval);

         
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
