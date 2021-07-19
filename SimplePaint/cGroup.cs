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
    class cGroup: Shape
    {
        bool SetRecArea = false;
        //Danh sach hinh da chon
        private List<Shape> shapes = new List<Shape>();
        Point p1R, p2R, x, y;

        public GraphicsPath[] GraphicsPaths
        {
            get
            {
                GraphicsPath[] paths = new GraphicsPath[Shapes.Count];
                for (int i = 0; i < Shapes.Count; i++)
                {
                    paths[i] = new GraphicsPath();
                    if (Shapes[i] is cLine)
                        paths[i].AddLine(((cLine)Shapes[i]).P1, ((cLine)Shapes[i]).P2);
                    else if (Shapes[i] is cEllipse)
                        paths[i].AddEllipse(((cEllipse)Shapes[i]).ELocation.X, ((cEllipse)Shapes[i]).ELocation.Y, ((cEllipse)Shapes[i]).EWidth, ((cEllipse)Shapes[i]).EHeight);
                    else if (Shapes[i] is cCircle)
                        paths[i].AddEllipse(((cCircle)Shapes[i]).CLocation.X, ((cCircle)Shapes[i]).CLocation.Y, ((cCircle)Shapes[i]).CDiameter, ((cCircle)Shapes[i]).CDiameter);
                    else if (Shapes[i] is cRectangle)
                        paths[i].AddRectangle(new Rectangle(((cRectangle)Shapes[i]).RLocation.X, ((cRectangle)Shapes[i]).RLocation.Y, ((cRectangle)Shapes[i]).RWidth, ((cRectangle)Shapes[i]).RHeight));
                    else if (Shapes[i] is cSquare)
                        paths[i].AddRectangle(new Rectangle(((cSquare)Shapes[i]).SLocation.X, ((cSquare)Shapes[i]).SLocation.Y, ((cSquare)Shapes[i]).SWidth, ((cSquare)Shapes[i]).SWidth));
                    else if (Shapes[i] is cCurve)
                        paths[i].AddCurve(((cCurve)Shapes[i]).LPoint.ToArray());
                    else if (Shapes[i] is cBezier)
                        paths[i].AddCurve(((cBezier)Shapes[i]).LPoint.ToArray());
                    else if (Shapes[i] is cPolygon)
                        paths[i].AddPolygon(((cPolygon)Shapes[i]).LPoint.ToArray());
                    else if (Shapes[i] is cGroup group)
                    {
                        for (int j = 0; j < group.GraphicsPaths.Length; j++)
                            paths[i].AddPath(group.GraphicsPaths[j], false);
                    }
                }
                return paths;
            }
        }

        public Point P1R { get => p1R; set => p1R = value; }
        public Point P2R { get => p2R; set => p2R = value; }
        public Point X { get => x; set => x = value; }
        public Point Y { get => y; set => y = value; }
        internal List<Shape> Shapes { get => shapes; set => shapes = value; }

        public override bool CanResize(Point e) => false;

        public override void Draw(Graphics gp)
        {
            for (int i = 0; i < Shapes.Count; i++)
            {
                Shapes[i].Draw(gp);
            }
        }

        public override void DrawSelectArea(Graphics gp)
        {
            for (int i = 0; i < Shapes.Count; i++)
            {
                if (Shapes[i] is cLine)
                {
                    if (SetRecArea == false)
                    {
                        p1R = ((cLine)Shapes[i]).P1R;
                        p2R = ((cLine)Shapes[i]).P2R;
                        SetRecArea = true;
                    }
                    else
                    {
                        if (((cLine)Shapes[i]).P1R.X < P1R.X)
                            p1R.X = ((cLine)Shapes[i]).P1R.X;
                        if (((cLine)Shapes[i]).P1R.Y < P1R.Y)
                            p1R.Y = ((cLine)Shapes[i]).P1R.Y;
                        if (((cLine)Shapes[i]).P2R.X > P2R.X)
                            p2R.X = ((cLine)Shapes[i]).P2R.X;
                        if (((cLine)Shapes[i]).P2R.Y > P2R.Y)
                            p2R.Y = ((cLine)Shapes[i]).P2R.Y;
                    }
                }
                else if (Shapes[i] is cEllipse)
                {
                    if (SetRecArea == false)
                    {
                        p1R = ((cEllipse)Shapes[i]).P1R;
                        p2R = ((cEllipse)Shapes[i]).P2R;
                        SetRecArea = true;
                    }
                    else
                    {
                        if (((cEllipse)Shapes[i]).P1R.X < P1R.X)
                            p1R.X = ((cEllipse)Shapes[i]).P1R.X;
                        if (((cEllipse)Shapes[i]).P1R.Y < P1R.Y)
                            p1R.Y = ((cEllipse)Shapes[i]).P1R.Y;
                        if (((cEllipse)Shapes[i]).P2R.X > P2R.X)
                            p2R.X = ((cEllipse)Shapes[i]).P2R.X;
                        if (((cEllipse)Shapes[i]).P2R.Y > P2R.Y)
                            p2R.Y = ((cEllipse)Shapes[i]).P2R.Y;
                    }
                }
                else if (Shapes[i] is cCircle)
                {
                    if (SetRecArea == false)
                    {
                        p1R = ((cCircle)Shapes[i]).P1R;
                        p2R = ((cCircle)Shapes[i]).P2R;
                        SetRecArea = true;
                    }
                    else
                    {
                        if (((cCircle)Shapes[i]).P1R.X < P1R.X)
                            p1R.X = ((cCircle)Shapes[i]).P1R.X;
                        if (((cCircle)Shapes[i]).P1R.Y < P1R.Y)
                            p1R.Y = ((cCircle)Shapes[i]).P1R.Y;
                        if (((cCircle)Shapes[i]).P2R.X > P2R.X)
                            p2R.X = ((cCircle)Shapes[i]).P2R.X;
                        if (((cCircle)Shapes[i]).P2R.Y > P2R.Y)
                            p2R.Y = ((cCircle)Shapes[i]).P2R.Y;

                    }
                }
                else if (Shapes[i] is cRectangle)
                {
                    if (SetRecArea == false)
                    {
                        p1R = ((cRectangle)Shapes[i]).P1R;
                        p2R = ((cRectangle)Shapes[i]).P2R;
                        SetRecArea = true;
                    }
                    else
                    {
                        if (((cRectangle)Shapes[i]).P1R.X < P1R.X)
                            p1R.X = ((cRectangle)Shapes[i]).P1R.X;
                        if (((cRectangle)Shapes[i]).P1R.Y < P1R.Y)
                            p1R.Y = ((cRectangle)Shapes[i]).P1R.Y;
                        if (((cRectangle)Shapes[i]).P2R.X > P2R.X)
                            p2R.X = ((cRectangle)Shapes[i]).P2R.X;
                        if (((cRectangle)Shapes[i]).P2R.Y > P2R.Y)
                            p2R.Y = ((cRectangle)Shapes[i]).P2R.Y;

                    }
                }
                else if (Shapes[i] is cSquare)
                {
                    if (SetRecArea == false)
                    {
                        p1R = ((cSquare)Shapes[i]).P1R;
                        p2R = ((cSquare)Shapes[i]).P2R;
                        SetRecArea = true;
                    }
                    else
                    {
                        if (((cSquare)Shapes[i]).P1R.X < P1R.X)
                            p1R.X = ((cSquare)Shapes[i]).P1R.X;
                        if (((cSquare)Shapes[i]).P1R.Y < P1R.Y)
                            p1R.Y = ((cSquare)Shapes[i]).P1R.Y;
                        if (((cSquare)Shapes[i]).P2R.X > P2R.X)
                            p2R.X = ((cSquare)Shapes[i]).P2R.X;
                        if (((cSquare)Shapes[i]).P2R.Y > P2R.Y)
                            p2R.Y = ((cSquare)Shapes[i]).P2R.Y;

                    }
                }
                else if (Shapes[i] is cCurve)
                {
                    if (SetRecArea == false)
                    {
                        p1R = ((cCurve)Shapes[i]).P1R;
                        p2R = ((cCurve)Shapes[i]).P2R;
                        SetRecArea = true;
                    }
                    else
                    {
                        if (((cCurve)Shapes[i]).P1R.X < P1R.X)
                            p1R.X = ((cCurve)Shapes[i]).P1R.X;
                        if (((cCurve)Shapes[i]).P1R.Y < P1R.Y)
                            p1R.Y = ((cCurve)Shapes[i]).P1R.Y;
                        if (((cCurve)Shapes[i]).P2R.X > P2R.X)
                            p2R.X = ((cCurve)Shapes[i]).P2R.X;
                        if (((cCurve)Shapes[i]).P2R.Y > P2R.Y)
                            p2R.Y = ((cCurve)Shapes[i]).P2R.Y;

                    }
                }
                else if (Shapes[i] is cBezier)
                {
                    if (SetRecArea == false)
                    {
                        p1R = ((cBezier)Shapes[i]).P1R;
                        p2R = ((cBezier)Shapes[i]).P2R;
                        SetRecArea = true;
                    }
                    else
                    {
                        if (((cBezier)Shapes[i]).P1R.X < P1R.X)
                            p1R.X = ((cBezier)Shapes[i]).P1R.X;
                        if (((cBezier)Shapes[i]).P1R.Y < P1R.Y)
                            p1R.Y = ((cBezier)Shapes[i]).P1R.Y;
                        if (((cBezier)Shapes[i]).P2R.X > P2R.X)
                            p2R.X = ((cBezier)Shapes[i]).P2R.X;
                        if (((cBezier)Shapes[i]).P2R.Y > P2R.Y)
                            p2R.Y = ((cBezier)Shapes[i]).P2R.Y;

                    }
                }
                else if (Shapes[i] is cPolygon)
                {
                    if (SetRecArea == false)
                    {
                        p1R = ((cPolygon)Shapes[i]).P1R;
                        p2R = ((cPolygon)Shapes[i]).P2R;
                        SetRecArea = true;
                    }
                    else
                    {
                        if (((cPolygon)Shapes[i]).P1R.X < P1R.X)
                            p1R.X = ((cPolygon)Shapes[i]).P1R.X;
                        if (((cPolygon)Shapes[i]).P1R.Y < P1R.Y)
                            p1R.Y = ((cPolygon)Shapes[i]).P1R.Y;
                        if (((cPolygon)Shapes[i]).P2R.X > P2R.X)
                            p2R.X = ((cPolygon)Shapes[i]).P2R.X;
                        if (((cPolygon)Shapes[i]).P2R.Y > P2R.Y)
                            p2R.Y = ((cPolygon)Shapes[i]).P2R.Y;

                    }
                }
                else if (Shapes[i] is cGroup group)
                {
                    if (SetRecArea == false)
                    {
                        p1R = ((cGroup)Shapes[i]).P1R;
                        p2R = ((cGroup)Shapes[i]).P2R;
                        SetRecArea = true;
                    }
                    else
                    {
                        if (((cGroup)Shapes[i]).P1R.X < P1R.X)
                            p1R.X = ((cGroup)Shapes[i]).P1R.X;
                        if (((cGroup)Shapes[i]).P1R.Y < P1R.Y)
                            p1R.Y = ((cGroup)Shapes[i]).P1R.Y;
                        if (((cGroup)Shapes[i]).P2R.X > P2R.X)
                            p2R.X = ((cGroup)Shapes[i]).P2R.X;
                        if (((cGroup)Shapes[i]).P2R.Y > P2R.Y)
                            p2R.Y = ((cGroup)Shapes[i]).P2R.Y;

                    }
                }
            }
            //MessageBox.Show(p1R.ToString() + p2R.ToString());
            Pen p = new Pen(Color.Blue, 2);
            p.DashStyle = DashStyle.Dash;
            gp.DrawRectangle(p, P1R.X, P1R.Y, P2R.X - P1R.X, P2R.Y - P1R.Y);
        }

        public override bool IsHit(Point e)
        {
            //gan x cho tat ca cac hinh
            for (int i = 0; i < shapes.Count; i++)
            {
                if (Shapes[i] is cLine)
                {
                    ((cLine)shapes[i]).X = e;
                }
                else if (Shapes[i] is cEllipse)
                {
                    ((cEllipse)shapes[i]).X = e;
                }
                else if (Shapes[i] is cCircle)
                {
                    ((cCircle)shapes[i]).X = e;
                }
                else if (Shapes[i] is cRectangle)
                {
                    ((cRectangle)shapes[i]).X = e;
                }
                else if (Shapes[i] is cSquare)
                {
                    ((cSquare)shapes[i]).X = e;
                }
                else if (Shapes[i] is cCurve)
                {
                    ((cCurve)shapes[i]).X = e;
                }
                else if (Shapes[i] is cBezier)
                {
                    ((cBezier)shapes[i]).X = e;
                }
                else if (Shapes[i] is cPolygon)
                {
                    ((cPolygon)shapes[i]).X = e;
                }
                else if (Shapes[i] is cGroup group)
                {
                    ((cGroup)shapes[i]).X = e;
                }
            }
            bool result = false;
            for (int i = 0; i < shapes.Count; i++)
            {
                if (Shapes[i] is cLine)
                {
                    if (((cLine)Shapes[i]).IsHit(e))
                        return true;
                } 
                else if (Shapes[i] is cEllipse)
                {
                    if (((cEllipse)Shapes[i]).IsHit(e))
                        return true;
                }
                else if (Shapes[i] is cCircle)
                {
                    if (((cCircle)Shapes[i]).IsHit(e))
                        return true;
                }
                else if (Shapes[i] is cRectangle)
                {
                    if (((cRectangle)Shapes[i]).IsHit(e))
                        return true;
                }
                else if (Shapes[i] is cSquare)
                {
                    if (((cSquare)Shapes[i]).IsHit(e))
                        return true;
                }
                else if (Shapes[i] is cCurve)
                {
                    if (((cCurve)Shapes[i]).IsHit(e))
                        return true;
                }
                else if (Shapes[i] is cBezier)
                {
                    if (((cBezier)Shapes[i]).IsHit(e))
                        return true;
                }
                else if (Shapes[i] is cPolygon)
                {
                    if (((cPolygon)Shapes[i]).IsHit(e))
                        return true;
                }
                else if (Shapes[i] is cGroup group)
                {
                    if (((cGroup)Shapes[i]).IsHit(e))
                        return true;
                }
            }
            return result;
        }

        public override void Move(Point e)
        {
            Y = e;
            int dx = Y.X - X.X, dy = Y.Y - X.Y;
            P1R = new Point(P1R.X + dx, P1R.Y + dy);
            P2R = new Point(P2R.X + dx, P2R.Y + dy);
            if (X != Y)
            {
                for (int i = 0; i < Shapes.Count; i++)
                {
                    //Shapes[i].IsHit(e);
                    Shapes[i].Move(e);
                }
                SetRecArea = false;
                X = Y;
            }
            
        }

        public override void Resize(Point e)
        {
            //
        }
    }
}
