using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpriteSheetDecomposer
{
    public partial class Splice : Form
    {
        public int SplicerWidth = 0;
        public int SplicerHeight = 0;
        public Splice()
        {
            InitializeComponent();
        }

        private void spliceBtn_Click(object sender, EventArgs e)
        {
            SplicerWidth = Convert.ToInt32(splicerWidth.Value);
            SplicerHeight = Convert.ToInt32(splicerHeight.Value);
            this.DialogResult = DialogResult.OK;
        }
    }
}
