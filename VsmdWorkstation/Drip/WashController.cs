using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace VsmdWorkstation.Drip
{
    class WashController
    {
        protected static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public async static Task<bool> DoWash()
        {
            log.Info("Wash");
            VsmdController vsmdController = VsmdController.GetVsmdController();
            PumpController pumpController = PumpController.GetPumpController();
            BoardSetting curBoardSetting = BoardSetting.GetInstance();
            int washDelayMilliSeconds = (int)(1000 * curBoardSetting.CurrentBoard.WashDelaySeconds);

            int delayMicroSecondsBetweenSamples = (int)(Preference.GetInstace().DelaySeconds * 1000);
            await vsmdController.MoveTo(VsmdAxis.Z, curBoardSetting.CurrentBoard.ZTravel);
            await vsmdController.MoveTo(VsmdAxis.X, curBoardSetting.CurrentBoard.WashX);
            await vsmdController.MoveTo(VsmdAxis.Y, curBoardSetting.CurrentBoard.WashY);
            await vsmdController.MoveTo(VsmdAxis.Z, curBoardSetting.CurrentBoard.WashZ);
            Random rnd = new Random();
            var port = rnd.Next(1,4);
            for(int i = 0; i< curBoardSetting.CurrentBoard.WashTimes; i++)
            {
                await pumpController.SwitchOnOff(port);
                Thread.Sleep(washDelayMilliSeconds);
            }
            await vsmdController.MoveTo(VsmdAxis.Z, curBoardSetting.CurrentBoard.ZTravel);
            log.Info("finished wash");
            return true;
        }
    }
}
