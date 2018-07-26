using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VsmdWorkstation
{
    public class BoardSetting
    {
        public string Name { get; set; }
        public int BlockCount { get; set; }
        public int RowCount { get; set; }
        public int ColumnCount { get; set; }
        /// <summary>
        /// 第一个孔距离原点X距离
        /// </summary>
        public int FirstTubeX { get; set; }
        /// <summary>
        /// 第一个孔距离原点Y距离
        /// </summary>
        public int FirstTubeY { get; set; }
        /// <summary>
        /// 孔距X
        /// </summary>
        public int TubeDistanceX { get; set; }
        /// <summary>
        /// 孔距Y
        /// </summary>
        public int TubeDistanceY { get; set; }
        /// <summary>
        /// 组距
        /// </summary>
        public int BlockDistanceX { get; set; }
        /// <summary>
        /// 圆孔直径
        /// </summary>
        public int TubeDiameter { get; set; }

        public int Convert2PhysicalPos(VsmdAxis axis, int coord)
        {
            int fpox = 0;
            switch (axis)
            {
                case VsmdAxis.X:
                    int blockIdx = (coord - 1) / ColumnCount;
                    fpox = FirstTubeX + BlockDistanceX * blockIdx + TubeDistanceX * (blockIdx * (ColumnCount - 1) + (coord - 1) % ColumnCount);
                    //fpox = FirstTubeX + (coord - 1) * TubeDistanceX;
                    break;
                case VsmdAxis.Y:
                    fpox = FirstTubeY + (coord - 1) * TubeDistanceY;
                    break;
                default:
                    break;
            }
            return fpox;
        }
        public void LoadBoardSettings()
        {
            string configFile = Application.StartupPath + "\\boardSettings.json";
            if (!File.Exists(configFile))
            {
                StatusBar.DisplayMessage(MessageType.Error, "载物架配置文件未找到，请重新配置载物架信息！");
                return;
            }
            string str = File.ReadAllText(configFile);
            if (string.IsNullOrWhiteSpace(str.Trim()))
            {
                StatusBar.DisplayMessage(MessageType.Warming, "载物架配置文件为空！");
                return;
            }
            JArray jsArr = (JArray)JsonConvert.DeserializeObject(str);

        }

        private static BoardSetting m_curBoardSetting = null;
        public static void SetCurrentBoardSetting(BoardSetting setting)
        {
            m_curBoardSetting = setting;
        }
        public static BoardSetting GetCurrentBoardSetting()
        {
            return m_curBoardSetting;
        }
    }
}
