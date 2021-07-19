using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimplePaint
{
    class cLine : Shape
    {
        Point x, y;
        private Point p1, p1R;
        private Point p2, p2R;
        int lWidth;
        Color lColor;
        int resizePoint = -1;

        bool bSolid = false, bDash = false, bDashDot = false, bDashDotDot = false, bDot = false;

        public Point P1 { get => p1; set => p1 = value; }
        public Point P2 { get => p2; set => p2 = value; }
        public int LWidth { get => lWidth; set => lWidth = value; }
        public Color LColor { get => lColor; set => lColor = value; }
        public bool BSolid { get => bSolid; set => bSolid = value; }
        public bool BDash { get => bDash; set => bDash = value; }
        public bool BDashDot { get => bDashDot; set => bDashDot = value; }
        public bool BDashDotDot { get => bDashDotDot; set => bDashDotDot = value; }
        public bool BDot { get => bDot; set => bDot = value; }
        public GraphicsPath Path { get; set; }
        public Point P1R { get => p1R; set => p1R = value; }
        public Point P2R { get => p2R; set => p2R = value; }
        public Point X { get => x; set => x = value; }
        public Point Y { get => y; set => y = value; }

        public override void Draw(Graphics gp)
        {
            Pen p = new Pen(lColor, lWidth);
            if (bDash == true)
            {
                p.DashStyle = DashStyle.Dash;
            }
            if (bDashDot == true)
            {
                p.DashStyle = DashStyle.DashDot;
            }
            if (bDashDotDot == true)
            {
                p.DashStyle = DashStyle.DashDotDot;
            }
            if (bDot == true)
            {
                p.DashStyle = DashStyle.Dot;
            }
            if (bSolid == true)
            {
                p.DashStyle = DashStyle.Solid;
            }
            gp.DrawLine(p, p1, p2);
        }
        
        public override bool IsHit(Point e)
        {
            X = e;
            Path = new GraphicsPath();
            Path.AddLine(p1, p2);
            var result = false;
            var pen = new Pen(lColor, lWidth + 2);
            result = Path.IsOutlineVisible(e, pen);
            return result;
        }

        public override void Move(Point e)
        {
            Y = e;
            int dx = Y.X - X.X, dy = Y.Y - X.Y;
            p1 = new Point(p1.X + dx, p1.Y + dy);
            p2 = new Point(p2.X + dx, p2.Y + dy);
            p1R = new Point(p1R.X + dx, p1R.Y + dy);
            p2R = new Point(p2R.X + dx, p2R.Y + dy);
            X = Y;
        }
        public override void DrawSelectArea(Graphics gp)
        {
            if (p1.X < p2.X)
            {
                p1R.X = p1.X;
                p2R.X = p2.X;
            }
            else
            {
                p1R.X = p2.X;
                p2R.X = p1.X;
            }
            if (p1.Y < p2.Y)
            {
                p1R.Y = p1.Y;
                p2R.Y = p2.Y;
            }
            else
            {
                p1R.Y = p2.Y;
                p2R.Y = p1.Y;
            }
            SolidBrush b = new SolidBrush(Color.BlueViolet);
            gp.FillEllipse(b, p1.X - 7 - this.lWidth / 2, p1.Y - 7 - this.lWidth / 2, 14 + this.lWidth, 14 + this.lWidth);
            gp.FillEllipse(b, p2.X - 7 - this.lWidth / 2, p2.Y - 7 - this.lWidth / 2, 14 + this.lWidth, 14 + this.lWidth);
        }
        public override bool CanResize(Point e)
        {
            if (p2.X - 5 <= e.X && e.X <= p2.X + 5)
                if (p2.Y - 5 <= e.Y && e.Y <= p2.Y + 5)
                {
                    resizePoint = 2;
                    return true;
                }
            if (p1.X - 5 <= e.X && e.X <= p1.X + 5)
                if (p1.Y - 5 <= e.Y && e.Y <= p1.Y + 5)
                {
                    resizePoint = 1;
                    return true;
                }
            resizePoint = -1;
            return false;
        }
        public override void Resize(Point e)
        {
            if (resizePoint == 1)
            {
                p1 = e;
            }
            if (resizePoint == 2)
            {
                p2 = e;
            }
        }
    }
}
