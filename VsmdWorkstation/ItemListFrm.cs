using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VsmdWorkstation
{
    // to be a general choose form, now is only for board setting
    public partial class ItemListFrm : Form
    {
        public Object SelectedObject { get; set; }
        public ItemListFrm()
        {
            InitializeComponent();
        }

        private void ItemListFrm_Load(object sender, EventArgs e)
        {
            InitListView();
        }
        private void InitListView()
        {
            List<BoardMeta> list = BoardSetting.GetInstance().GetAllBoardMetaes();
            int no = 1;
            list.ForEach((meta) =>
            {
                ListViewItem lvm = new ListViewItem(no.ToString());
                lvm.SubItems.Add(meta.Name);
                lvm.Tag = meta;
                listView.Items.Add(lvm);
            });
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if(listView.SelectedItems.Count <= 0)
            {
                StatusBar.DisplayMessage(MessageType.Warming, "请先选择一项！");
                return;
            }
            SelectedObject = listView.SelectedItems[0].Tag;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
