using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VsmdWorkstation
{
    public class BoardSettings
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

        private static BoardSettings m_curBoardSetting = null;
        public static void SetCurrentBoardSetting(BoardSettings setting)
        {
            m_curBoardSetting = setting;
        }
        public static BoardSettings GetCurrentBoardSetting()
        {
            return m_curBoardSetting;
        }
    }
}
