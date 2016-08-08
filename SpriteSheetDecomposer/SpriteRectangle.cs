using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpriteSheetDecomposer
{
    [Serializable]
    public class SpriteRectangle
    {
        public Guid Id;
        public Rectangle value;
        public bool IsSelected = false;
        public bool IsGroupValidated = false;
        public bool IsDeleted = false;

        public string ExportName;
        public Guid? ExportNameGroupId;

        public bool IsGrouped = false;
        public Guid? GroupId;

        public SpriteRectangle Top;
        public SpriteRectangle Right;
        public SpriteRectangle Bottom;
        public SpriteRectangle Left;

        public SpriteRectangle()
        {
            Id = Guid.NewGuid();
            ExportName = Id.ToString();
        }
    }
}
