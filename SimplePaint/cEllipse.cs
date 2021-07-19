using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplePaint
{
    class cEllipse : Shape
    {
        Point x, y, p1R, p2R;
        private Point p1;
        private Point p2;
        private Point eLocation;
        private int eHeight;
        private int eWidth;
        private Color eLColor, eFColor;
        private float eShapeWidth;
        bool fillShape, isDrawn=false;

        bool bSolid = false, bDash = false, bDashDot = false, bDashDotDot = false, bDot = false;

        public Point P1 { get => p1; set => p1 = value; }
        public Point P2 { get => p2; set => p2 = value; }
        public Point ELocation { get => eLocation; set => eLocation = value; }
        public Color ELColor { get => eLColor; set => eLColor = value; }
        public float EShapeWidth { get => eShapeWidth; set => eShapeWidth = value; }
        public bool FillShape { get => fillShape; set => fillShape = value; }
        public bool BSolid { get => bSolid; set => bSolid = value; }
        public bool BDash { get => bDash; set => bDash = value; }
        public bool BDashDot { get => bDashDot; set => bDashDot = value; }
        public bool BDashDotDot { get => bDashDotDot; set => bDashDotDot = value; }
        public bool BDot { get => bDot; set => bDot = value; }
        public bool IsDrawn { get => isDrawn; set => isDrawn = value; }
        public int EHeight { get => eHeight; set => eHeight = value; }
        public int EWidth { get => eWidth; set => eWidth = value; }
        public GraphicsPath Path { get; set; }
        public Point P1R { get => p1R; set => p1R = value; }
        public Point P2R { get => p2R; set => p2R = value; }
        public Color EFColor { get => eFColor; set => eFColor = value; }
        public Point X { get => x; set => x = value; }
        public Point Y { get => y; set => y = value; }

        private void SetEllipse()
        {
            eWidth = Math.Abs(p1.X - p2.X);
            eHeight = Math.Abs(P1.Y - p2.Y);
            eLocation = p1;
            if (p1.X > p2.X && p1.Y > p2.Y)
            {
                eLocation = p2;
            }
            if (p1.X < p2.X && p1.Y > p2.Y)
            {
                eLocation.Y = p2.Y;
            }
            if (p1.X > p2.X && p1.Y < p2.Y)
            {
                eLocation.X = p2.X;
            }
        }
        public override void Draw(Graphics gp)
        {
            if (IsDrawn==false)
                SetEllipse();

            if (FillShape)
            {
                SolidBrush b = new SolidBrush(eFColor);
                gp.FillEllipse(b, eLocation.X, eLocation.Y, eWidth, eHeight);
            }
            Pen p = new Pen(eLColor, EShapeWidth);
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
            gp.DrawEllipse(p, eLocation.X, eLocation.Y, eWidth, eHeight);
        }
        public override bool IsHit(Point e)
        {
            P1R = eLocation;
            P2R = new Point(eLocation.X + eWidth, eLocation.Y + eHeight);
            X = e;
            Path = new GraphicsPath();
            Path.AddEllipse(eLocation.X, eLocation.Y, eWidth, eHeight);
            var result = false;
            var pen = new Pen(eLColor, eShapeWidth + 2);
            if (fillShape == false)
                result = Path.IsOutlineVisible(e, pen);
            else
                result = Path.IsVisible(e);
            return result;
        }
        public override void Move(Point e)
        {
            Y = e;
            int dx = Y.X - X.X, dy = Y.Y - X.Y;
            eLocation = new Point(eLocation.X + dx, eLocation.Y + dy);
            P1R = new Point(P1R.X + dx, P1R.Y + dy);
            P2R = new Point(P2R.X + dx, P2R.Y + dy);
            X = Y;
        }
        public override void DrawSelectArea(Graphics gp)
        {
            Pen p = new Pen(Color.Blue, 2);
            p.DashStyle = DashStyle.Dash;
            gp.DrawRectangle(p, P1R.X, P1R.Y, P2R.X - P1R.X, P2R.Y - P1R.Y);
        }
        public override bool CanResize(Point e)
        {
            if (P2R.X - 5 <= e.X && e.X <= P2R.X + 5)
                if (P2R.Y - 5 <= e.Y && e.Y <= P2R.Y + 5)
                    return true;
            return false;
        }
        public override void Resize(Point e)
        {
            if (e.X > eLocation.X && e.Y > eLocation.Y)
            {
                p1 = eLocation;
                p2 = e;
                SetEllipse();
                P2R = new Point(eLocation.X + eWidth, eLocation.Y + eHeight);
            }
        }
    }
}
