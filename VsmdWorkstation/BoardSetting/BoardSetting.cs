using Newtonsoft.Json;
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
    public enum BoardType
    {
        Site = 1,
        Grid = 2,
    }
    public class BoardMeta
    {
        public int ID { get; set; }
        public int Type { get; set; }
        public string Name { get; set; }
        public int GridCount { get; set; }
        public int SiteCount { get; set; }
        public int RowCount { get; set; }
        public int ColumnCount { get; set; }
        
        public int Site1FirstTubeX { get; set; }
        public int Site1FirstTubeY { get; set; }
        public int Site1LastTubeX { get; set; }
        public int Site1LastTubeY { get; set; }
        public int Site2FirstTubeX { get; set; }
        public int Site2FirstTubeY { get; set; }

        public int GridFirstTubeX { get; set; }
        public int GridFirstTubeY { get; set; }
        public int GridLastTubeX { get; set; }
        public int GridLastTubeY { get; set; }

        public override string ToString()
        {
            return this.Name;
        }
    }
    public delegate void DelDataUpdated(object data);
    public class BoardSetting
    {
        private List<BoardMeta> m_boardSettings = new List<BoardMeta>();
        private double m_tubeDistX, m_tubeDistY;
        private int m_siteDistY;

        private BoardMeta m_curBoard;
        public BoardMeta CurrentBoard
        {
            get
            {
                return m_curBoard;
            }
            set
            {
                m_curBoard = value;
                if(m_curBoard.Type == (int)BoardType.Site)
                {
                    m_tubeDistX = (m_curBoard.Site1LastTubeX - m_curBoard.Site1FirstTubeX) * 1.0f / (m_curBoard.ColumnCount - 1);
                    m_tubeDistY = (m_curBoard.Site1LastTubeY - m_curBoard.Site1FirstTubeY) * 1.0f / (m_curBoard.RowCount - 1);
                    m_siteDistY = m_curBoard.Site2FirstTubeY - m_curBoard.Site1FirstTubeY;
                }
                else if(m_curBoard.Type == (int)BoardType.Grid)
                {
                    m_tubeDistX = (m_curBoard.GridLastTubeX - m_curBoard.GridFirstTubeX) * 1.0f / (m_curBoard.ColumnCount - 1);
                    m_tubeDistY = (m_curBoard.GridLastTubeY - m_curBoard.GridFirstTubeY) * 1.0f / (m_curBoard.RowCount - 1);
                }
            }
        }
        public DelDataUpdated OnDataUpdate;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="axis"></param>
        /// <param name="coord">start from 1</param>
        /// <returns></returns>
        public int Convert2PhysicalPos(VsmdAxis axis, int block, int coord)
        {
            int fpox = 0;
            switch (axis)
            {
                case VsmdAxis.X:
                    if(m_curBoard.Type == (int)BoardType.Site)
                    {
                        fpox = m_curBoard.Site1FirstTubeX + (int)((coord - 1) * m_tubeDistX);
                    }
                    else
                    {
                        fpox = m_curBoard.GridFirstTubeX + (int)((block - 1) * m_tubeDistX);
                    }
                    break;
                case VsmdAxis.Y:
                    if (m_curBoard.Type == (int)BoardType.Site)
                    {
                        fpox = m_curBoard.Site1FirstTubeY + (block - 1) * m_siteDistY + (int)((coord - 1) * m_tubeDistY);
                    }
                    else
                    {
                        fpox = m_curBoard.GridFirstTubeY + (int)((coord - 1) * m_tubeDistY);
                    }
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
                if(OnDataUpdate != null)
                {
                    OnDataUpdate(m_boardSettings);
                }
            }
            catch (Exception)
            {
                //MessageBox.Show(e.ToString());
                ret = false;
            }
            return ret;
        }
        public int GetNextBoardNum()
        {
            int no = 1;
            m_boardSettings.ForEach((board) => {
                if(board.ID >= no)
                {
                    no = board.ID + 1;
                }
            });

            return no;
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
