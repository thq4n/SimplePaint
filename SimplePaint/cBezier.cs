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
    class cBezier : Shape
    {
        private int bShapeWidth;
        private Color bColor;
        Point x, y, p1R, p2R;
        List<Point> lPoint = new List<Point>();
        int resizePoint = -1;
        bool bSolid = false, bDash = false, bDashDot = false, bDashDotDot = false, bDot = false;

        public int BShapeWidth { get => bShapeWidth; set => bShapeWidth = value; }
        public Color BColor { get => bColor; set => bColor = value; }
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
            Pen p = new Pen(BColor, BShapeWidth);
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
            if (lPoint.Count == 2)
            {
                gp.DrawBezier(p, LPoint[0], LPoint[1], LPoint[1], LPoint[1]);
                return;
            }
            if (lPoint.Count == 3)
            {
                gp.DrawBezier(p, LPoint[0], LPoint[1], LPoint[2], LPoint[2]);
                return;
            }
            if (lPoint.Count == 4)
            {
                gp.DrawBezier(p, LPoint[0], LPoint[1], LPoint[2], LPoint[3]);
                return;
            }
            gp.DrawBezier(p, LPoint[0], LPoint[1], LPoint[2], LPoint[3]);
        }
        public override bool IsHit(Point e)
        {
            while(lPoint.Count<4)
            {
                lPoint.Add(lPoint.Last());
            }
            X = e;
            Path = new GraphicsPath();
            Path.AddBezier(lPoint[0], lPoint[1], lPoint[2], lPoint[3]);
            var result = false;
            var pen = new Pen(bColor, BShapeWidth + 2);
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
            List<SolidBrush> b = new List<SolidBrush>();
            b.Add(new SolidBrush(Color.IndianRed));
            b.Add(new SolidBrush(Color.Orange));
            b.Add(new SolidBrush(Color.HotPink));
            b.Add(new SolidBrush(Color.GreenYellow));
            //p.DashStyle = DashStyle.Dash;
            p1R = lPoint[0];
            p2R = P1R;
            for (int i = 0; i < 4; i++)
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
                gp.FillEllipse(b[i], lPoint[i].X - 7 - this.bShapeWidth / 2, lPoint[i].Y - 7 - this.bShapeWidth / 2, 14 + this.bShapeWidth, 14 + this.bShapeWidth);
            }
        }
        public override bool CanResize(Point e)
        {
            for (int i = 0; i < lPoint.Count; i++)
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
