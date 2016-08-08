using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpriteSheetDecomposer
{
    public partial class MainWindow : Form
    {
        private SpriteSheetPicture _spriteSheet;
        public MainWindow()
        {
            InitializeComponent();
            _spriteSheet = new SpriteSheetPicture(this);
            scrollImagePangel.Controls.Add(_spriteSheet);
        }

        private void openSpriteSheetBtn_Click(object sender, EventArgs e)
        {
            openSprite();
        }

        private void openSprite()
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    this.Height = this.Height < 800 ? 800 : this.Height;
                    loadXmlBtn.Enabled = true;
                    sliceBtn.Enabled = true;
                    _spriteSheet.LoadAsync(openFileDialog1.FileName);
                    sliceBtn.Enabled = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }

        private void sliceBtn_Click(object sender, EventArgs e)
        {
            slice();
        }

        private void slice()
        {
            using (Splice dialog = new Splice())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    exportBtn.Enabled = true;
                    saveXmlBtn.Enabled = true;
                    _spriteSheet.SpliceRects(dialog.SplicerWidth, dialog.SplicerHeight);
                }
            }
        }

        private void zoomInBtn_Click(object sender, EventArgs e)
        {
            _spriteSheet.IncZoom();
        }

        private void zoomOutBtn_Click(object sender, EventArgs e)
        {
            _spriteSheet.DecZoom();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (exportName.Focused)
            {
                return base.ProcessCmdKey(ref msg, keyData);
            }

            if (keyData == Keys.ControlKey && keyData == Keys.C && openSpriteSheetBtn.Enabled)
            {
                slice();
                return true;
            }

            if (keyData == (Keys.Control | Keys.O) && openSpriteSheetBtn.Enabled)
            {
                openSprite();
                return true;
            }

            if (keyData == (Keys.Control | Keys.E) && exportBtn.Enabled)
            {
                export();
                return true;
            }

            if (keyData == (Keys.Control | Keys.L) && loadXmlBtn.Enabled)
            {
                load();
                return true;
            }

            if (keyData == (Keys.Control | Keys.S) && saveXmlBtn.Enabled)
            {
                save();
                return true;
            }

            if (keyData == Keys.W && restoreBtn.Enabled)
            {
                _spriteSheet.RestoreSelectedRects();
                return true;
            }

            if (keyData == Keys.A && mergeBtn.Enabled)
            {
                _spriteSheet.MergeSelectedRects();
                return true;
            }

            if (keyData == Keys.S && splitBtn.Enabled)
            {
                _spriteSheet.SplitSelectedRects();
                return true;
            }

            if ((keyData == Keys.Delete || keyData == Keys.D) && deleteBtn.Enabled)
            {
                _spriteSheet.DeleteSelectedRects();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void scrollImagePangel_MouseWheel(object sender, MouseEventArgs e)
        {
            if (Control.ModifierKeys == Keys.Alt)
            {
                scrollImagePangel.VerticalScroll.Value = scrollImagePangel.VerticalScroll.Minimum;
                if (e.Delta > 0) //scrolled up
                    _spriteSheet.IncZoom();
                else
                    _spriteSheet.DecZoom();
            }
            else
            {
                if (e.Delta > 0)
                {

                    if (scrollImagePangel.VerticalScroll.Value - 2 >= scrollImagePangel.VerticalScroll.Minimum)
                        scrollImagePangel.VerticalScroll.Value -= 2;
                    else
                        scrollImagePangel.VerticalScroll.Value = scrollImagePangel.VerticalScroll.Minimum;
                }
                else
                {
                    if (scrollImagePangel.VerticalScroll.Value + 2 <= scrollImagePangel.VerticalScroll.Maximum)
                        scrollImagePangel.VerticalScroll.Value += 2;
                    else
                        scrollImagePangel.VerticalScroll.Value = scrollImagePangel.VerticalScroll.Maximum;
                }
            }

        }

        private void mergeBtn_Click(object sender, EventArgs e)
        {
            _spriteSheet.MergeSelectedRects();
        }

        private void splitBtn_Click(object sender, EventArgs e)
        {
            _spriteSheet.SplitSelectedRects();
        }

        private void deleteBtn_Click(object sender, EventArgs e)
        {
            _spriteSheet.DeleteSelectedRects();
        }

        private void restoreBtn_Click(object sender, EventArgs e)
        {
            _spriteSheet.RestoreSelectedRects();
        }

        private void exportName_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(exportName.Text))
                return;

            _spriteSheet.NameSelectedRects(exportName.Text);
        }

        private void exportBtn_Click(object sender, EventArgs e)
        {
            export();
        }

        private void export()
        {
            var folderDialog = new FolderBrowserDialog();
            folderDialog.Description = "Export PNG Image Sprites";
            folderDialog.ShowNewFolderButton = true;
            folderDialog.RootFolder = System.Environment.SpecialFolder.MyComputer;

            DialogResult dialogResult = MessageBox.Show("Save to Subfolder?", "Split Named Files Into Folders", MessageBoxButtons.YesNo);


            if (DialogResult.OK == folderDialog.ShowDialog())
            {
                _spriteSheet.ExportImages(folderDialog.SelectedPath, dialogResult == DialogResult.Yes);
            }
        }

        private void saveXmlBtn_Click(object sender, EventArgs e)
        {
            save();
        }

        private void save()
        {
            // Displays a SaveFileDialog so the user can save the Image
            // assigned to Button2.
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "SpriteDecomposer Data File|*.sdd";
            saveFileDialog1.Title = "Save an SpriteDecomposer File";
            saveFileDialog1.ShowDialog();

            // If the file name is not an empty string open it for saving.
            if (string.IsNullOrEmpty(saveFileDialog1.FileName))
                return;

            _spriteSheet.Save(saveFileDialog1.FileName);
        }

        private void loadXmlBtn_Click(object sender, EventArgs e)
        {
            load();
        }

        private void load()
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.Filter = "SpriteDecomposer File (*.sdd) | *.sdd";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                exportBtn.Enabled = true;
                saveXmlBtn.Enabled = true;
                namePanel.Enabled = false;
                try
                {
                    _spriteSheet.Load(openFileDialog1.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }

        private void MainWindow_SizeChanged(object sender, EventArgs e)
        {
            this.progressBar.Left = (this.ClientSize.Width - this.progressBar.Width) / 2;
            this.progressBar.Top = (this.ClientSize.Height - this.progressBar.Height) / 2;
        }

        private void exportName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                scrollImagePangel.Select();
            }
        }
    }
}
