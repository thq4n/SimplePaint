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
    class cSquare : Shape
    {
        Point x, y, p1R, p2R;
        private Point p1;
        private Point p2;
        private Point sLocation;
        private int sWidth;
        private Color sLColor, sFColor;
        private int sShapeWidth;
        bool fillShape, isDrawn = false;

        bool bSolid = false, bDash = false, bDashDot = false, bDashDotDot = false, bDot = false;

        public Color SLColor { get => sLColor; set => sLColor = value; }
        public int SShapeWidth { get => sShapeWidth; set => sShapeWidth = value; }
        public bool FillShape { get => fillShape; set => fillShape = value; }
        public Point P1 { get => p1; set => p1 = value; }
        public Point P2 { get => p2; set => p2 = value; }
        public bool BSolid { get => bSolid; set => bSolid = value; }
        public bool BDash { get => bDash; set => bDash = value; }
        public bool BDashDot { get => bDashDot; set => bDashDot = value; }
        public bool BDashDotDot { get => bDashDotDot; set => bDashDotDot = value; }
        public bool BDot { get => bDot; set => bDot = value; }
        public bool IsDrawn { get => isDrawn; set => isDrawn = value; }
        public Point SLocation { get => sLocation; set => sLocation = value; }
        public int SWidth { get => sWidth; set => sWidth = value; }
        public GraphicsPath Path { get; set; }
        public Point P1R { get => p1R; set => p1R = value; }
        public Point P2R { get => p2R; set => p2R = value; }
        public Color SFColor { get => sFColor; set => sFColor = value; }
        public Point X { get => x; set => x = value; }
        public Point Y { get => y; set => y = value; }

        public void SetSquare()
        {
            sLocation = p1;
            sWidth = Math.Min(Math.Abs(p1.X - p2.X), Math.Abs(p1.Y - p2.Y));
            if (p1.X > p2.X && p1.Y > p2.Y)
            {
                sLocation.Y = p1.Y - sWidth;
                sLocation.X = p1.X - sWidth;
            }
            if (p1.X < p2.X && p1.Y > p2.Y)
            {
                sLocation.Y = p1.Y - sWidth;
            }
            if (p1.X > p2.X && p1.Y < p2.Y)
            {
                sLocation.X = p1.X - sWidth;
            }
        }

        public override void Draw(Graphics gp)
        {
            if (isDrawn == false)
                SetSquare();

            if (fillShape)
            {
                SolidBrush b = new SolidBrush(sFColor);
                gp.FillRectangle(b, sLocation.X, sLocation.Y, sWidth, sWidth);
            }
            Pen p = new Pen(sLColor, sShapeWidth);
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
            gp.DrawRectangle(p, sLocation.X, sLocation.Y, sWidth, sWidth);
        }
        public override bool IsHit(Point e)
        {
            X = e;
            P1R = new Point (sLocation.X- (sShapeWidth / 2),sLocation.Y- (sShapeWidth / 2));
            P2R = new Point(sLocation.X + sWidth + (sShapeWidth / 2), sLocation.Y + sWidth + (sShapeWidth / 2));

            Path = new GraphicsPath();
            Path.AddRectangle(new Rectangle(sLocation.X, sLocation.Y, sWidth, sWidth));
            var result = false;
            var pen = new Pen(sLColor, sShapeWidth + 2); 
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
            sLocation = new Point(sLocation.X + dx, sLocation.Y + dy);
            p1 = sLocation;
            P1R = new Point(P1R.X + dx, P1R.Y + dy); 
            P2R = new Point(P2R.X + dx, P2R.Y + dy);
            X = Y;
        }
        public override void DrawSelectArea(Graphics gp)
        {
            Pen p = new Pen(Color.Blue,2);
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
            if (e.X > sLocation.X && e.Y > sLocation.Y)
            {
                p2 = e;
                SetSquare();
                P2R = new Point(sLocation.X + sWidth + (sShapeWidth / 2), sLocation.Y + sWidth + (sShapeWidth / 2));
            }
        }
    }
}
