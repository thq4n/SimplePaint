using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplePaint
{
    class cCircle : Shape
    {
        Point x, y, p1R, p2R;
        private Point p1;
        private Point p2;
        private Point cLocation;
        private int cDiameter;
        private Color cLColor, cFColor;
        private float cShapeWidth;
        bool fillShape, isDrawn = false;

        bool bSolid = false, bDash = false, bDashDot = false, bDashDotDot = false, bDot = false;

        public Point P1 { get => p1; set => p1 = value; }
        public Point P2 { get => p2; set => p2 = value; }
        public Color CLColor { get => cLColor; set => cLColor = value; }
        public float CShapeWidth { get => cShapeWidth; set => cShapeWidth = value; }
        public bool FillShape { get => fillShape; set => fillShape = value; }
        public bool BSolid { get => bSolid; set => bSolid = value; }
        public bool BDash { get => bDash; set => bDash = value; }
        public bool BDashDot { get => bDashDot; set => bDashDot = value; }
        public bool BDashDotDot { get => bDashDotDot; set => bDashDotDot = value; }
        public bool BDot { get => bDot; set => bDot = value; }
        public bool IsDrawn { get => isDrawn; set => isDrawn = value; }
        public Point CLocation { get => cLocation; set => cLocation = value; }
        public int CDiameter { get => cDiameter; set => cDiameter = value; }
        public GraphicsPath Path { get; set; }
        public Point P1R { get => p1R; set => p1R = value; }
        public Point P2R { get => p2R; set => p2R = value; }
        public Color CFColor { get => cFColor; set => cFColor = value; }
        public Point X { get => x; set => x = value; }
        public Point Y { get => y; set => y = value; }

        private void SetCircle()
        {
            if (Math.Abs(p1.X - p2.X) < Math.Abs(p1.Y - p2.Y))
            {
                cDiameter = Math.Abs(p1.X - p2.X);
            }
            else
            {
                cDiameter = Math.Abs(p1.Y - p2.Y);
            }
            cLocation = p1;
            if (p1.X > p2.X && p1.Y > p2.Y)
            {
                cLocation.Y = p1.Y - cDiameter;
                cLocation.X = p1.X - cDiameter;
            }
            if (p1.X < p2.X && p1.Y > p2.Y)
            {
                cLocation.Y = p1.Y - cDiameter;
            }
            if (p1.X > p2.X && p1.Y < p2.Y)
            {
                cLocation.X = p1.X - cDiameter;
            }
        }
        public override void Draw(Graphics gp)
        {
            if (isDrawn == false)
                SetCircle();
    
            if (fillShape)
            {
                SolidBrush b = new SolidBrush(cFColor);
                gp.FillEllipse(b, cLocation.X, cLocation.Y, cDiameter, cDiameter);
            }
            Pen p = new Pen(cLColor, cShapeWidth);
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
            gp.DrawEllipse(p, cLocation.X, cLocation.Y, cDiameter, cDiameter);
        }
        public override bool IsHit(Point e)
        {
            P1R = cLocation;
            P2R = new Point(cLocation.X + cDiameter, cLocation.Y + cDiameter);
            X = e;
            Path = new GraphicsPath();
            Path.AddEllipse(cLocation.X, cLocation.Y, cDiameter, cDiameter);
            var result = false;
            var pen = new Pen(cLColor, cShapeWidth + 2);
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
            cLocation = new Point(cLocation.X + dx, cLocation.Y + dy);
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
            if (e.X > cLocation.X && e.Y > cLocation.Y)
            {
                if (Math.Abs(cLocation.X - e.X) < Math.Abs(cLocation.Y - e.Y))
                {
                    cDiameter = Math.Abs(cLocation.X - e.X);
                }
                else
                {
                    cDiameter = Math.Abs(cLocation.Y - e.Y);
                }
                P2R = new Point(cLocation.X + cDiameter, cLocation.Y + cDiameter);
            }
        }
    }
}
