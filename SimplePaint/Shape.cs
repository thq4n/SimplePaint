using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplePaint
{
    abstract class Shape
    {
        abstract public bool CanResize(Point e);
        abstract public bool IsHit(Point e);
        abstract public void Move(Point e);
        abstract public void Draw(Graphics gp);
        abstract public void DrawSelectArea(Graphics gp);
        abstract public void Resize(Point e);
    }
}
