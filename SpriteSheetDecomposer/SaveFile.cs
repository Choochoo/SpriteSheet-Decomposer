using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpriteSheetDecomposer
{
    [Serializable]
    public class SaveFile
    {
        public SpriteRectangle[,] Rectangles;
        public int SpliceWidth;
        public int SpliceHeight;
    }
}
