using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplePaint
{
    class cPolygon : Shape
    {
        Point x, y, p1R, p2R;
        private int pShapeWidth;
        private bool fillShape;
        private Color pLColor, pFColor;
        List<Point> lPoint = new List<Point>();

        bool bSolid = false, bDash = false, bDashDot = false, bDashDotDot = false, bDot = false;

        public List<Point> LPoint { get => lPoint; set => lPoint = value; }
        public int PShapeWidth { get => pShapeWidth; set => pShapeWidth = value; }
        public bool FillShape { get => fillShape; set => fillShape = value; }
        public Color PLColor { get => pLColor; set => pLColor = value; }
        public bool BSolid { get => bSolid; set => bSolid = value; }
        public bool BDash { get => bDash; set => bDash = value; }
        public bool BDashDot { get => bDashDot; set => bDashDot = value; }
        public bool BDashDotDot { get => bDashDotDot; set => bDashDotDot = value; }
        public bool BDot { get => bDot; set => bDot = value; }
        public GraphicsPath Path { get; set; }
        public Point P1R { get => p1R; set => p1R = value; }
        public Point P2R { get => p2R; set => p2R = value; }
        public Color PFColor { get => pFColor; set => pFColor = value; }
        public Point X { get => x; set => x = value; }
        public Point Y { get => y; set => y = value; }

        public override void Draw(Graphics gp)
        {
            Pen p = new Pen(pLColor, pShapeWidth);
            if (fillShape == true)
            {
                if (lPoint.Count < 3)
                {
                    SolidBrush b = new SolidBrush(pFColor);
                    gp.FillPolygon(b, lPoint.ToArray());
                }
            }
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
            gp.DrawPolygon(p, lPoint.ToArray());
        }
        public override bool IsHit(Point e)
        {
            X = e;
            P1R = P2R = lPoint[0];
            for (int i =0; i<lPoint.Count; i++)
            {
                if (LPoint[i].X < P1R.X)
                    p1R.X = LPoint[i].X;
                if (LPoint[i].Y < P1R.Y)
                    p1R.Y = LPoint[i].Y;
                if (LPoint[i].X > P2R.X)
                    p2R.X = LPoint[i].X;
                if (LPoint[i].Y > P2R.Y)
                    p2R.Y = LPoint[i].Y;
            }

            Path = new GraphicsPath();
            Path.AddPolygon(lPoint.ToArray());
            var result = false;
            var pen = new Pen(pLColor, pShapeWidth + 2);
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
            Pen p = new Pen(Color.Blue, 2);
            p.DashStyle = DashStyle.Dash;
            gp.DrawRectangle(p, P1R.X, P1R.Y, P2R.X - P1R.X, P2R.Y - P1R.Y);
        }
        public override bool CanResize(Point e)
        {
            if (P2R.X - 10 <= e.X && e.X <= P2R.X+10)
                if (P2R.Y - 10 <= e.Y && e.Y <= P2R.Y + 10)
                return true;
            return false;
        }
        public override void Resize(Point e)
        {
            //P1R = polLocation;
            //if (e.X > P1R.X && e.Y > P1R.Y)
            //{
            //    float trongsoX = (float)(e.X - P1R.X) / (float)(P2R.X - P1R.X);
            //    float trongsoY = (float)(e.Y - P1R.Y) / (float)(P2R.Y - P1R.Y);
            //    if (trongsoX == 0 || trongsoX == 0)
            //        return;
            //    for (int i = 0; i < lPoint.Count; i++)
            //    {
            //        lPoint[i] = new Point((int)((((float)lPoint[i].X - P1R.X) * trongsoX) +P1R.X), (int)((((float)lPoint[i].Y - P1R.Y) * trongsoY) +P1R.Y));
            //    }
            //}
        }
    }
}
