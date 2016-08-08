using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace SpriteSheetDecomposer
{
    public class SpriteSheetPicture : PictureBox
    {
        private float _zoom = 1f;
        private float _zoomIncDec = .5f;

        private SpriteRectangle[,] _rects = null;
        private List<SpriteRectangle> _rectsList = null;
        private SpriteRectangle _lastSelected = null;

        private int _imageOriginalWidth = 0;
        private int _imageOriginalHeight = 0;
        private int _spliceWidth = 0;
        private int _spliceHeight = 0;

        private MainWindow _mainForm;

        private Bitmap _originalImage;
        private Bitmap _resizedImage;

        public SpriteSheetPicture(MainWindow mainForm)
        {
            _mainForm = mainForm;

            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "SpriteSheet";
            this.TabIndex = 0;
            this.TabStop = false;
            this.Padding = Padding.Empty;
            this.Margin = Padding.Empty;
            this.MouseClick += new MouseEventHandler(SelectSquareClick);
            this.LoadCompleted += NewImageLoaded;
            this.DoubleBuffered = true;
            this.ResizeRedraw = true;
        }

        void NewImageLoaded(Object sender, AsyncCompletedEventArgs e)
        {
            _imageOriginalWidth = this.Image.Width;
            _imageOriginalHeight = this.Image.Height;
            _originalImage = new Bitmap(this.Image);
            _mainForm.zoomInBtn.Enabled = true;
            _mainForm.zoomOutBtn.Enabled = true;
            UpdateBitmap();
        }

        private void UpdateBitmap()
        {
            this.Width = Convert.ToInt32(_imageOriginalWidth * _zoom);
            this.Height = Convert.ToInt32(_imageOriginalHeight * _zoom);
            if (this.Image == null)
                return;

            _resizedImage?.Dispose();

            _resizedImage = ResizeImage(_originalImage, Convert.ToInt32(_originalImage.Width * _zoom),
                Convert.ToInt32(_originalImage.Height * _zoom));

            UpdateRectsSizes();
        }

        public void IncZoom()
        {
            if (_zoom < 5)
                _zoom += _zoomIncDec;
            else
                _mainForm.zoomInBtn.Enabled = false;

            _mainForm.zoomOutBtn.Enabled = true;
            UpdateBitmap();
        }

        public void DecZoom()
        {
            if (_zoom > .5f)
                _zoom -= _zoomIncDec;
            else
                _mainForm.zoomOutBtn.Enabled = false;

            _mainForm.zoomInBtn.Enabled = true;
            UpdateBitmap();
        }

        void SelectSquareClick(object sender, MouseEventArgs e)
        {
            if (_rects == null || _rectsList == null)
                return;

            if (Control.ModifierKeys == Keys.Shift && _lastSelected != null)
            {
                //select multiple rects
                var startX = _lastSelected.value.X < e.X ? _lastSelected.value.X : e.X;
                var startY = _lastSelected.value.Y < e.Y ? _lastSelected.value.Y : e.Y;

                var lastFurtherestX = _lastSelected.value.Width + _lastSelected.value.X;
                var furthestX = lastFurtherestX > e.X ? lastFurtherestX : e.X;

                var lastFurtherestY = _lastSelected.value.Height + _lastSelected.value.Y;
                var furthestY = lastFurtherestY > e.Y ? lastFurtherestY : e.Y;

                var mouseRect = new Rectangle(startX, startY, furthestX - startX, furthestY - startY);
                var selectedRectangles = new List<SpriteRectangle>();

                //Show Restore button?
                var anyAreDeleted = false;

                foreach (var spriteRectangle in _rectsList)
                {
                    spriteRectangle.IsSelected = spriteRectangle.value.IntersectsWith(mouseRect);

                    if (!spriteRectangle.IsSelected) continue;
                    if (spriteRectangle.IsDeleted)
                        anyAreDeleted = true;
                    selectedRectangles.Add(spriteRectangle);
                }

                _lastSelected = null;
                foreach (var selectedRectangle in selectedRectangles)
                    SelectEverythingAroundLastSelected(selectedRectangle);

                var allSelectedRects = _rectsList.Where(r => r.IsSelected).ToList();

                var isAllSinglesOrAllGroupsSelected = allSelectedRects.Count(r => r.IsGrouped) == allSelectedRects.Count ||
                                                      allSelectedRects.Count(r => !r.IsGrouped) == allSelectedRects.Count;

                if (isAllSinglesOrAllGroupsSelected)
                {
                    var allHaveTheSameExportNameId =
                        allSelectedRects.Select(r => r.ExportNameGroupId).Distinct().Count(r => r.HasValue) == 1;
                    _mainForm.exportName.Text = allHaveTheSameExportNameId ? allSelectedRects.ElementAt(0).ExportName : string.Empty;
                }

                UpdateButtons(true, false, !anyAreDeleted, !anyAreDeleted, anyAreDeleted, isAllSinglesOrAllGroupsSelected);
            }
            else
            {
                //select single
                foreach (var spriteRectangle in _rectsList)
                {
                    spriteRectangle.IsSelected = spriteRectangle.value.Contains(e.Location);
                    if (!spriteRectangle.IsSelected) continue;
                    _lastSelected = spriteRectangle;
                }

                //If click was outside of Image
                if (_lastSelected == null)
                {
                    UpdateButtons(true, false, false, true, false, false);
                }
                else //Found a square to select
                {
                    _mainForm.exportName.Text = _lastSelected.ExportNameGroupId.HasValue ? _lastSelected.ExportName : string.Empty;
                    SelectEverythingAroundLastSelected(_lastSelected);
                    UpdateButtons(false, _lastSelected.IsGrouped, _lastSelected.IsSelected, !_lastSelected.IsDeleted, _lastSelected.IsSelected, true);
                }
            }
            Invalidate();
        }

        private void SelectEverythingAroundLastSelected(SpriteRectangle currentClickedRectangle)
        {
            if (currentClickedRectangle == null || !currentClickedRectangle.IsGrouped)
                return;

            currentClickedRectangle.IsGroupValidated = true;
            if (currentClickedRectangle.IsGrouped)
            {
                IterateAndSelect(currentClickedRectangle.Top, currentClickedRectangle.GroupId.Value);
                IterateAndSelect(currentClickedRectangle.Right, currentClickedRectangle.GroupId.Value);
                IterateAndSelect(currentClickedRectangle.Bottom, currentClickedRectangle.GroupId.Value);
                IterateAndSelect(currentClickedRectangle.Left, currentClickedRectangle.GroupId.Value);
            }
            ResetValidated();
        }

        private void IterateAndSelect(SpriteRectangle currentRect, Guid groupId)
        {
            if (currentRect == null || currentRect.IsGroupValidated || !currentRect.IsGrouped || (currentRect.GroupId.HasValue && currentRect.GroupId.Value != groupId))
                return;

            currentRect.IsGroupValidated = true;
            foreach (var spriteRectangle in _rectsList)
            {
                if (currentRect.Id != spriteRectangle.Id || !spriteRectangle.IsGrouped)
                    continue;

                spriteRectangle.IsSelected = true;
                IterateAndSelect(currentRect.Top, groupId);
                IterateAndSelect(currentRect.Right, groupId);
                IterateAndSelect(currentRect.Bottom, groupId);
                IterateAndSelect(currentRect.Left, groupId);
                return;
            }
        }

        public void SplitSelectedRects()
        {
            foreach (var spriteRectangle in _rectsList)
            {
                if (!spriteRectangle.IsSelected) continue;
                spriteRectangle.GroupId = null;
                spriteRectangle.IsGrouped = false;
            }
            Invalidate();
        }

        public void DeleteSelectedRects()
        {
            foreach (var spriteRectangle in _rectsList.Where(r => !r.IsDeleted))
            {
                spriteRectangle.IsDeleted = spriteRectangle.IsSelected;
                spriteRectangle.ExportName = spriteRectangle.Id.ToString();
                spriteRectangle.ExportNameGroupId = null;
                spriteRectangle.IsSelected = false;
            }
            UpdateButtons(false, false, false, true, false, false);
            Invalidate();
        }

        public void RestoreSelectedRects()
        {
            foreach (var spriteRectangle in _rectsList.Where(r => r.IsSelected))
            {
                spriteRectangle.IsDeleted = false;
                spriteRectangle.IsSelected = false;
            }
            UpdateButtons(false, false, false, true, false, false);
            Invalidate();
        }

        public void NameSelectedRects(string newName)
        {
            var newGuid = Guid.NewGuid();
            foreach (var spriteRectangle in _rectsList.Where(r => r.IsSelected && !r.IsDeleted && r.ExportName != newName))
            {
                spriteRectangle.ExportName = newName;
                spriteRectangle.ExportNameGroupId = newGuid;
            }
            Invalidate();
        }

        public void MergeSelectedRects()
        {
            var newGroupId = Guid.NewGuid();
            foreach (var spriteRectangle in _rectsList.Where(r => r.IsSelected && !r.IsDeleted))
            {
                spriteRectangle.IsGrouped = true;
                spriteRectangle.GroupId = newGroupId;
            }

            UpdateButtons(false, true, true, true, false, true);
            Invalidate();
        }

        private void UpdateButtons(bool enableMerge, bool enableSplit, bool enableDelete, bool visibleDelete, bool restoreButton, bool nameSelectionPanel)
        {
            _mainForm.mergeBtn.Enabled = enableMerge;
            _mainForm.splitBtn.Enabled = enableSplit;
            _mainForm.deleteBtn.Enabled = enableDelete;
            _mainForm.deleteBtn.Visible = visibleDelete;
            _mainForm.restoreBtn.Enabled = restoreButton;
            _mainForm.restoreBtn.Visible = !visibleDelete;
            _mainForm.namePanel.Enabled = nameSelectionPanel;
        }

        private void ResetValidated()
        {
            _rectsList.ForEach(r => r.IsGroupValidated = false);
        }

        public void SpliceRects(int spliceWidth, int spliceHeight)
        {
            if (spliceWidth <= 0 || spliceHeight <= 0)
                return;

            _spliceWidth = spliceWidth;
            _spliceHeight = spliceHeight;
            var maxColumns = Convert.ToInt32(Math.Floor(this.Image.Width / (spliceWidth * 1.0f)));
            var maxRows = Convert.ToInt32(Math.Floor(this.Image.Height / (spliceHeight * 1.0f)));
            _rects = new SpriteRectangle[maxRows, maxColumns];
            _rectsList = new List<SpriteRectangle>();

            //create array
            for (int row = 0; row < _rects.GetLength(0); row++)
            {
                for (int col = 0; col < _rects.GetLength(1); col++)
                {
                    _rects[row, col] = new SpriteRectangle
                    {
                        value = Rectangle.Empty,
                    };
                }
            }

            //assign neighbors
            for (int row = 0; row < _rects.GetLength(0); row++)
            {
                for (int col = 0; col < _rects.GetLength(1); col++)
                {
                    var currentRect = _rects[row, col];
                    currentRect.Left = col - 1 >= 0 ? _rects[row, col - 1] : null;
                    currentRect.Bottom = row + 1 < _rects.GetLength(0) ? _rects[row + 1, col] : null;
                    currentRect.Right = col + 1 < _rects.GetLength(1) ? _rects[row, col + 1] : null;
                    currentRect.Top = row - 1 >= 0 ? _rects[row - 1, col] : null;
                    _rectsList.Add(currentRect);
                }
            }

            UpdateRectsSizes();
        }

        public void UpdateRectsSizes(bool redraw = true)
        {
            if (_rects == null || _rects.Length == 0)
                return;

            var zoomedWidth = Convert.ToInt32(_zoom * _spliceWidth);
            var zoomedHeight = Convert.ToInt32(_zoom * _spliceHeight);

            for (int row = 0; row < _rects.GetLength(0); row++)
            {
                for (int col = 0; col < _rects.GetLength(1); col++)
                {
                    var startX = this.Padding.Left + (col * zoomedWidth);
                    var startY = this.Padding.Top + (row * zoomedHeight);
                    _rects[row, col].value = new Rectangle(startX, startY, zoomedWidth, zoomedHeight);
                }
            }
            if (redraw)
                Invalidate();
        }

        public Bitmap ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
                graphics.SmoothingMode = SmoothingMode.None;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }


        private readonly Pen _addPen = new Pen(Color.Green, 1);
        private readonly SolidBrush _addBrush = new SolidBrush(Color.FromArgb(40, 0, 255, 0));

        private readonly SolidBrush _deleteBrush = new SolidBrush(Color.FromArgb(60, 255, 0, 0));
        private readonly Pen _namePen = new Pen(Color.FromArgb(255, 165, 0), 3);

        private readonly Pen _selectPen = new Pen(Color.Blue, 2);
        private readonly SolidBrush _selectBrush = new SolidBrush(Color.FromArgb(40, 0, 0, 255));
        protected override void OnPaint(PaintEventArgs e)
        {
            if (_resizedImage == null)
                return;

            e.Graphics.DrawImage(_resizedImage, Point.Empty);
            if (_rects == null)
                return;

            _rectsList.Where(r => r.IsDeleted && !r.IsSelected).ToList().ForEach(r => DrawRect(r, e.Graphics, null, _deleteBrush));
            _rectsList.Where(r => r.ExportNameGroupId.HasValue).ToList().ForEach(r => DrawNameBorder(e.Graphics, _namePen, r));
            _rectsList.Where(r => !r.IsDeleted && !r.IsSelected).ToList().ForEach(r => DrawRect(r, e.Graphics, _addPen, _addBrush));
            _rectsList.Where(r => r.IsSelected).ToList().ForEach(r => DrawRect(r, e.Graphics, _selectPen, _selectBrush));

        }

        private void DrawRect(SpriteRectangle r, Graphics g, Pen p, Brush b)
        {
            if (p != null)
                DrawBorder(g, p, r);

            if (b != null)
                g.FillRectangle(b, r.value);
        }

        protected override void Dispose(bool disposing)
        {
            _addPen.Dispose();
            _addBrush.Dispose();
            _deleteBrush.Dispose();
            _selectPen.Dispose();
            _selectBrush.Dispose();
            _namePen.Dispose();
            base.Dispose(disposing);
        }

        private void DrawNameBorder(Graphics e, Pen p, SpriteRectangle rect)
        {
            if (rect.Top == null || !rect.Top.ExportNameGroupId.HasValue || rect.ExportNameGroupId != rect.Top.ExportNameGroupId)
                e.DrawLine(p, rect.value.X, rect.value.Y, rect.value.X + rect.value.Width, rect.value.Y);
            if (rect.Right == null || !rect.Right.ExportNameGroupId.HasValue || rect.ExportNameGroupId != rect.Right.ExportNameGroupId)
                e.DrawLine(p, rect.value.X + rect.value.Width, rect.value.Y, rect.value.X + rect.value.Width, rect.value.Y + rect.value.Height);
            if (rect.Bottom == null || !rect.Bottom.ExportNameGroupId.HasValue || rect.ExportNameGroupId != rect.Bottom.ExportNameGroupId)
                e.DrawLine(p, rect.value.X, rect.value.Y + rect.value.Height, rect.value.X + rect.value.Width, rect.value.Y + rect.value.Height);
            if (rect.Left == null || !rect.Left.ExportNameGroupId.HasValue || rect.ExportNameGroupId != rect.Left.ExportNameGroupId)
                e.DrawLine(p, rect.value.X, rect.value.Y, rect.value.X, rect.value.Y + rect.value.Height);
        }

        private void DrawBorder(Graphics e, Pen p, SpriteRectangle rect)
        {
            if (rect.Top == null || !rect.Top.IsGrouped || rect.Top.GroupId != null && rect.GroupId != null && rect.Top.GroupId != rect.GroupId)
                e.DrawLine(p, rect.value.X, rect.value.Y, rect.value.X + rect.value.Width, rect.value.Y);
            if (rect.Right == null || !rect.Right.IsGrouped || rect.Right.GroupId != null && rect.GroupId != null && rect.Right.GroupId != rect.GroupId)
                e.DrawLine(p, rect.value.X + rect.value.Width, rect.value.Y, rect.value.X + rect.value.Width, rect.value.Y + rect.value.Height);
            if (rect.Bottom == null || !rect.Bottom.IsGrouped || rect.Bottom.GroupId != null && rect.GroupId != null && rect.Bottom.GroupId != rect.GroupId)
                e.DrawLine(p, rect.value.X, rect.value.Y + rect.value.Height, rect.value.X + rect.value.Width, rect.value.Y + rect.value.Height);
            if (rect.Left == null || !rect.Left.IsGrouped || rect.Left.GroupId != null && rect.GroupId != null && rect.Left.GroupId != rect.GroupId)
                e.DrawLine(p, rect.value.X, rect.value.Y, rect.value.X, rect.value.Y + rect.value.Height);
        }

        public void Save(string saveLocation)
        {
            using (Stream stream = File.Open(saveLocation, FileMode.Create))
            {
                BinaryFormatter bformatter = new BinaryFormatter();
                _rectsList.ForEach(r => r.IsSelected = false);
                _rectsList.ForEach(r => r.IsGroupValidated = false);
                var newSaveFile = new SaveFile()
                {
                    Rectangles = _rects,
                    SpliceWidth = _spliceWidth,
                    SpliceHeight = _spliceHeight
                };
                bformatter.Serialize(stream, newSaveFile);
            }
        }

        public void Load(string loadLocation)
        {
            using (Stream stream = File.Open(loadLocation, FileMode.Open))
            {
                BinaryFormatter bformatter = new BinaryFormatter();
                var saveFile = (SaveFile)bformatter.Deserialize(stream);
                _rects = saveFile.Rectangles;
                _rectsList = new List<SpriteRectangle>();
                for (int row = 0; row < _rects.GetLength(0); row++)
                    for (int col = 0; col < _rects.GetLength(1); col++)
                        _rectsList.Add(_rects[row, col]);

                _spliceWidth = saveFile.SpliceWidth;
                _spliceHeight = saveFile.SpliceHeight;
            }
            UpdateRectsSizes();
        }

        public async void ExportImages(string selectedPath, bool intoSubfolder)
        {
            _mainForm.progressBar.Step = 1;
            _mainForm.progressBar.Value = 0;
            _mainForm.progressBar.Visible = true;
            _mainForm.Enabled = false;
            var zoomCopy = _zoom;
            _zoom = 1;
            UpdateRectsSizes(false);

            var rectsNonDeletedList = _rectsList.Where(r => !r.IsDeleted).ToList();
            var allSingleRects = rectsNonDeletedList.Where(r => !r.IsGrouped).ToList();
            var allGroupedRects = rectsNonDeletedList.Where(r => r.IsGrouped).ToList();

            _mainForm.progressBar.Maximum = allSingleRects.Count +
                                            allGroupedRects.Select(r => r.GroupId).Distinct().Count();

            //Update Progress Bar Value
            var progress = new Progress<int>(v =>
            {
                _mainForm.progressBar.Value = v;
            });

            // Run operation in another thread
            await Task.Run(() => ProcessImages(allSingleRects, allGroupedRects, selectedPath, progress, intoSubfolder));
            ResetValidated();
            //Done exporting images, re-enable forms.
            _mainForm.progressBar.Visible = false;
            _mainForm.Enabled = true;
            _zoom = zoomCopy;
            UpdateRectsSizes();
        }

        private void ProcessImages(List<SpriteRectangle> allSingleRects, List<SpriteRectangle> allGroupedRects, string selectedPath, IProgress<int> progress, bool intoSubfolder)
        {
            var progressValue = 0;
            //export singles
            foreach (var singleRect in allSingleRects)
            {
                ExportImageFile(selectedPath, new[] { singleRect.value }, singleRect.ExportNameGroupId.HasValue ? singleRect.ExportName : singleRect.Id.ToString(), intoSubfolder);
                progressValue += 1;
                progress.Report(progressValue);
            }

            //export groups
            foreach (var groupedRect in allGroupedRects)
            {
                if (groupedRect.IsGroupValidated)
                    continue;

                var specificGroupRects = allGroupedRects.Where(r => r.GroupId == groupedRect.GroupId).ToList();
                specificGroupRects.ForEach(r => r.IsGroupValidated = true);
                ExportImageFile(selectedPath, specificGroupRects.Select(r => r.value).ToArray(), groupedRect.ExportNameGroupId.HasValue ? groupedRect.ExportName : groupedRect.GroupId.ToString(), intoSubfolder);
                progressValue += 1;
                progress.Report(progressValue);
            }
        }

        private void ExportImageFile(string selectedPath, Rectangle[] destRectangles, string name, bool intoSubfolder)
        {
            var mostLeftX = int.MaxValue;
            var mostTopY = int.MaxValue;
            var mostRightX = 0;
            var mostBottomY = 0;

            foreach (var destRect in destRectangles)
            {
                mostLeftX = destRect.X < mostLeftX ? destRect.X : mostLeftX;
                mostTopY = destRect.Y < mostTopY ? destRect.Y : mostTopY;

                mostRightX = destRect.X > mostRightX ? destRect.X : mostRightX;
                mostBottomY = destRect.Y > mostBottomY ? destRect.Y : mostBottomY;
            }


            var destImage = new Bitmap(mostRightX - mostLeftX + _spliceWidth, mostBottomY - mostTopY + _spliceHeight);
            destImage.SetResolution(_originalImage.HorizontalResolution, _originalImage.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
                graphics.SmoothingMode = SmoothingMode.None;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                foreach (var destRect in destRectangles)
                    graphics.DrawImage(_originalImage, destRect.X - mostLeftX, destRect.Y - mostTopY, destRect, GraphicsUnit.Pixel);

                var count = 1;
                var folder = intoSubfolder ? $"/{name}/" : string.Empty;
                var pathWithFolder = $"{selectedPath}{folder}";
                if (!Directory.Exists(pathWithFolder))
                {
                    Directory.CreateDirectory(pathWithFolder);
                }
                var filename = $"{pathWithFolder}/{name}{count}.png";
                while (File.Exists(filename))
                {
                    count++;
                    filename = $"{pathWithFolder}/{name}{count}.png";
                }
                destImage.Save(filename, ImageFormat.Png);
                destImage.Dispose();
            }
        }


    }
}
