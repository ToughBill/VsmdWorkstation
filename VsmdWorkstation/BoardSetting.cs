﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VsmdWorkstation.Controls;

namespace VsmdWorkstation
{
    public class BoardMeta
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

        public override string ToString()
        {
            return this.Name;
        }
    }
    public class BoardSetting
    {
        private List<BoardMeta> m_boardSettings = new List<BoardMeta>();
        public BoardMeta CurrentBoard { get; set; }

        public int Convert2PhysicalPos(VsmdAxis axis, int coord)
        {
            int fpox = 0;
            switch (axis)
            {
                case VsmdAxis.X:
                    int blockIdx = (coord - 1) / CurrentBoard.ColumnCount;
                    fpox = CurrentBoard.FirstTubeX + CurrentBoard.BlockDistanceX * blockIdx + CurrentBoard.TubeDistanceX * (blockIdx * (CurrentBoard.ColumnCount - 1) + (coord - 1) % CurrentBoard.ColumnCount);
                    //fpox = FirstTubeX + (coord - 1) * TubeDistanceX;
                    break;
                case VsmdAxis.Y:
                    fpox = CurrentBoard.FirstTubeY + (coord - 1) * CurrentBoard.TubeDistanceY;
                    break;
                default:
                    break;
            }
            return fpox;
        }
        public string GetBoardMetaFilePath()
        {
            return Application.StartupPath + "\\boardSettings.json";
        }
        public void LoadBoardSettings()
        {
            string configFile = GetBoardMetaFilePath();
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
            m_boardSettings = JsonConvert.DeserializeObject<List<BoardMeta>>(str);
        }
        public List<BoardMeta> GetAllBoardMetaes()
        {
            return m_boardSettings;
        }
        public bool AddNewBoard(BoardMeta board)
        {
            m_boardSettings.Add(board);
            return Save();
        }
        public bool DeleteBoard(BoardMeta board)
        {
            m_boardSettings.Remove(board);
            return Save();
        }
        public bool Save()
        {
            string configFile = GetBoardMetaFilePath();
            bool ret = true;
            try
            {
                if (File.Exists(configFile))
                {
                    File.Delete(configFile);
                }
                using (FileStream stream = File.Create(configFile))
                {
                    string str = JsonConvert.SerializeObject(m_boardSettings);
                    byte[] bytes = System.Text.Encoding.Default.GetBytes(str);
                    stream.Write(bytes, 0, bytes.Length);
                    //File.WriteAllText(configFile, str);
                }
            } catch (Exception e)
            {
                ret = false;
            }
            return ret;
        }
        private static BoardSetting m_curBoardSetting = null;
        public static void SetCurrentBoardSetting(BoardSetting setting)
        {
            m_curBoardSetting = setting;
        }
        public static BoardSetting GetInstance()
        {
            if(m_curBoardSetting == null)
            {
                m_curBoardSetting = new BoardSetting();
            }
            return m_curBoardSetting;
        }
    }
}
