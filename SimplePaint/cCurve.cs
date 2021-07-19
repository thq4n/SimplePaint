using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplePaint
{
    class cCurve : Shape
    {
        Point x, y, p1R, p2R;
        private int curShapeWidth;
        private Color curColor;
        List<Point> lPoint = new List<Point>();
        int resizePoint = -1;
        bool bSolid = false, bDash = false, bDashDot = false, bDashDotDot = false, bDot = false;

        public int CurShapeWidth { get => curShapeWidth; set => curShapeWidth = value; }
        public Color CurColor { get => curColor; set => curColor = value; }
        public List<Point> LPoint { get => lPoint; set => lPoint = value; }
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
            Pen p = new Pen(curColor, curShapeWidth);
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
            gp.DrawCurve(p, lPoint.ToArray());
        }
        public override bool IsHit(Point e)
        {
            X = e;
            Path = new GraphicsPath();
            Path.AddCurve(lPoint.ToArray());
            var result = false;
            var pen = new Pen(curColor, curShapeWidth + 2);
            result = Path.IsOutlineVisible(e, pen);
            return result;
        }
        public override void Move(Point e)
        {
            Y = e;
            int dx = Y.X - X.X, dy = Y.Y - X.Y;
            for (int i = 0; i < lPoint.Count; i++)
            {
                lPoint[i] = new Point(lPoint[i].X + dx, lPoint[i].Y + dy);
            }
            P1R = new Point(P1R.X + dx, P1R.Y + dy);
            P2R = new Point(P2R.X + dx, P2R.Y + dy);
            X = Y;
        }
        public override void DrawSelectArea(Graphics gp)
        { 
            SolidBrush b = new SolidBrush(Color.BlueViolet); 
            p1R = lPoint[0];
            p2R = p1R;
            for (int i = 0; i < lPoint.Count; i++)
            {
                if (p1R.X > lPoint[i].X)
                {
                    p1R.X = lPoint[i].X; 
                }
                if (p1R.Y > lPoint[i].Y)
                {
                    p1R.Y = lPoint[i].Y;
                }
                if (p2R.X < lPoint[i].X)
                {
                    p2R.X = lPoint[i].X;
                }
                if (p2R.Y < lPoint[i].Y)
                {
                    p2R.Y = lPoint[i].Y;
                }
                gp.FillEllipse(b, lPoint[i].X - 7 - this.curShapeWidth / 2, lPoint[i].Y - 7 - this.curShapeWidth / 2, 14 + this.curShapeWidth, 14 + this.curShapeWidth);
            }
        }
        public override bool CanResize(Point e)
        {
            for(int i =0; i<lPoint.Count;i++)
            {
                if (lPoint[i].X - 5 <= e.X && e.X <= lPoint[i].X + 5)
                    if (lPoint[i].Y - 5 <= e.Y && e.Y <= lPoint[i].Y + 5)
                    {
                        resizePoint = i;
                        return true;
                    }
            }
            resizePoint = -1;
            return false;
        }
        public override void Resize(Point e)
        {
            if (resizePoint != -1)
                lPoint[resizePoint] = e;
        }
    }
}
