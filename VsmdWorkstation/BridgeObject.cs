using CefSharp.WinForms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VsmdWorkstation
{
    public class BridgeObject
    {
        private ChromiumWebBrowser m_browser;
        public BridgeObject(ChromiumWebBrowser browser)
        {
            m_browser = browser;
        }
        public void Move(string args)
        {
            VsmdController vsmdController = VsmdController.GetVsmdController();
            if (!vsmdController.IsInitialized())
            {
                StatusMessage.DisplayMessage(MessageType.Error, "未初始化控制器！");
                //return;
            }
            BoardSettings curBoardSetting = BoardSettings.GetCurrentBoardSetting();
            JArray jsArr = (JArray)JsonConvert.DeserializeObject(args);
            
           // vsmdController.MoveTo(VsmdAxis.X, 0);
            //vsmdController.MoveTo(VsmdAxis.Y, 0);
            for (int i = 0; i < jsArr.Count; i++)
            {
                JObject obj = (JObject)jsArr[i];
                int row = int.Parse(obj["row"].ToString());
                int col = int.Parse(obj["column"].ToString());
               // vsmdController.MoveTo(VsmdAxis.X, curBoardSetting.Convert2PhysicalPos(VsmdAxis.X, col));
                //vsmdController.MoveTo(VsmdAxis.Y, curBoardSetting.Convert2PhysicalPos(VsmdAxis.Y, row));
                MoveCallBack(row, col);
                System.Threading.Thread.Sleep(1000);
            }
            
        }
        private void MoveCallBack(int row, int col)
        {
            CallJS("moveCallBack(" + row + "," + col + ");");
        }
        public void CallJS(string jsCode)
        {
            m_browser.GetBrowser().MainFrame.ExecuteJavaScriptAsync(jsCode);
        }
    }
}
