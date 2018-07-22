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
        public void Move(string args)
        {
            JArray jsArr = (JArray)JsonConvert.DeserializeObject(args);
            VsmdController vsmdController = VsmdController.GetVsmdController();
            BoardSettings curBoardSetting = BoardSettings.GetCurrentBoardSetting();
            vsmdController.MoveTo(VsmdAxis.X, 0);
            vsmdController.MoveTo(VsmdAxis.Y, 0);
            for (int i = 0; i < jsArr.Count; i++)
            {
                JObject obj = (JObject)jsArr[i];
                int row = int.Parse(obj["row"].ToString());
                int col = int.Parse(obj["column"].ToString());
                //vsmdController.MoveTo(VsmdAxis.X, curBoardSetting.Convert2PhysicalPos(VsmdAxis.X, col));
                vsmdController.MoveTo(VsmdAxis.Y, curBoardSetting.Convert2PhysicalPos(VsmdAxis.Y, row));
                System.Threading.Thread.Sleep(1000);
            }
            
        }
    }
}
