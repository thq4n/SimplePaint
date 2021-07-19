using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplePaint
{
    class cRectangle : Shape
    {
        Point x, y, p1R, p2R;
        private Point p1;
        private Point p2;
        private Point rLocation;
        private int rHeight;
        private int rWidth;
        private Color rLColor, rFColor;
        private int rShapeWidth;
        bool fillShape, isDrawn= false;

        bool bSolid = false, bDash = false, bDashDot = false, bDashDotDot = false, bDot = false;

        public Point P1 { get => p1; set => p1 = value; }
        public Point P2 { get => p2; set => p2 = value; }
        public Color RLColor { get => rLColor; set => rLColor = value; }
        public int RShapeWidth { get => rShapeWidth; set => rShapeWidth = value; }
        public bool FillShape { get => fillShape; set => fillShape = value; }
        public bool BSolid { get => bSolid; set => bSolid = value; }
        public bool BDash { get => bDash; set => bDash = value; }
        public bool BDashDot { get => bDashDot; set => bDashDot = value; }
        public bool BDashDotDot { get => bDashDotDot; set => bDashDotDot = value; }
        public bool BDot { get => bDot; set => bDot = value; }
        public bool IsDrawn { get => isDrawn; set => isDrawn = value; }
        public Point RLocation { get => rLocation; set => rLocation = value; }
        public int RHeight { get => rHeight; set => rHeight = value; }
        public int RWidth { get => rWidth; set => rWidth = value; }
        public GraphicsPath Path { get; set; }
        public Point P1R { get => p1R; set => p1R = value; }
        public Point P2R { get => p2R; set => p2R = value; }
        public Color RFColor { get => rFColor; set => rFColor = value; }
        public Point X { get => x; set => x = value; }
        public Point Y { get => y; set => y = value; }

        public void SetRec()
        {
            rLocation = p1;
            rWidth = Math.Abs(p1.X - p2.X);
            rHeight = Math.Abs(p1.Y - p2.Y);
            if (p1.X > p2.X && p1.Y > p2.Y)
            {
                rLocation = p2;
            }
            if (p1.X < p2.X && p1.Y > p2.Y)
            {
                rLocation.Y = p2.Y;
            }
            if (p1.X > p2.X && p1.Y < p2.Y)
            {
                rLocation.X = p2.X;
            }
        }

        public override void Draw(Graphics gp)
        {
            if (isDrawn == false)
            {
                SetRec();
            }

            if (fillShape)
            {
                SolidBrush b = new SolidBrush(rFColor);
                gp.FillRectangle(b, rLocation.X, rLocation.Y, rWidth, rHeight);
            }
            Pen p = new Pen(rLColor, rShapeWidth);
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
            gp.DrawRectangle(p, rLocation.X, rLocation.Y, rWidth, rHeight); 
        }
        public override bool IsHit(Point e)
        {
            X = e;
            P1R = new Point(rLocation.X - (rShapeWidth / 2), 
                rLocation.Y - (rShapeWidth / 2));
            P2R = new Point(rLocation.X + rWidth + (rShapeWidth / 2), 
                rLocation.Y + rHeight + (rShapeWidth / 2));
            Path = new GraphicsPath();
            Path.AddRectangle(new Rectangle(rLocation.X, rLocation.Y, rWidth, rHeight));
            var result = false;
            var pen = new Pen(rLColor, rShapeWidth + 2);
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
            rLocation = new Point(rLocation.X + dx, rLocation.Y + dy);
            p1 = rLocation;
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
            if (e.X > rLocation.X && e.Y > rLocation.Y)
            {
                p2 = e;
                SetRec();
                P2R = new Point(rLocation.X + rWidth + (rShapeWidth / 2),
                                rLocation.Y + rHeight + (rShapeWidth / 2));
            }
        }
    }
}
