using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimplePaint
{
    public partial class frPaint : Form
    { 
        List<cLine> lLine = new List<cLine>();
        List<cRectangle> lRectangle = new List<cRectangle>();
        List<cCircle> lCircle = new List<cCircle>();
        List<cEllipse> lEllipse = new List<cEllipse>();
        List<cPolygon> lPolygon = new List<cPolygon>();
        List<cSquare> lSquare = new List<cSquare>();
        List<cCurve> lCurve = new List<cCurve>();
        List<cBezier> lBezier = new List<cBezier>();
        List<Point> lPoint = new List<Point>();
        
        List<Shape> lDrawn = new List<Shape>();
        List<int> lSelected = new List<int>();

        int sResize;
        Graphics gp;
        Pen myPen;
        Color myColor, myFColor;
        bool isClicked = false, isResizing = false, isMoving = false, multiSelect = false;
        bool bBezier = false, bCircle = false, bCurve = false, bElipse = false, bLine = true, bPolygon = false, bRectangle = false, bSquare = false, bSelect = false;
        bool isFill = false;
        bool bSolid = true, bDash = false, bDashDot = false, bDashDotDot = false, bDot = false;

        private void butGroup_Click(object sender, EventArgs e)
        {
            if (lSelected.Count>1)
            {
                cGroup group = new cGroup();
                for (int i = 0; i < lSelected.Count; i++)
                {
                    group.Shapes.Add(lDrawn[lSelected[i]]);
                }
                List<int> temp = lSelected;
                temp.Sort();
                for (int i = temp.Count-1; i>= 0; i--)
                {
                    lDrawn.RemoveAt(lSelected[i]);
                }
                lSelected.Clear();
                lDrawn.Add(group);
                lSelected.Add(lDrawn.Count - 1);
                pnlMain.Invalidate();
            }
        }

        private void butUngroup_Click(object sender, EventArgs e)
        {
            int stopPos = lSelected.Count;
            for (int i =0; i< stopPos; i++)
            {
                if (lDrawn[lSelected[i]] is cGroup)
                {
                    foreach (Shape shape in ((cGroup)lDrawn[lSelected[i]]).Shapes)
                    {
                        lDrawn.Add(shape);
                        lSelected.Add(lDrawn.Count - 1);
                    }
                    lDrawn.RemoveAt(lSelected[i]);
                    lSelected.RemoveAt(i);
                    for (int j = 0; j< lSelected.Count;j++)
                    {
                        if (lSelected[j] > i)
                            lSelected[j]-- ;
                    }
                }
            }
            pnlMain.Invalidate();
        }

        private void butFColor_Click(object sender, EventArgs e)
        {
            ColorDialog tempDialog = new ColorDialog();
            tempDialog.ShowDialog();
            butFColor.BackColor = tempDialog.Color;
        }

        private void butDelete_Click(object sender, EventArgs e)
        {
            string a =""; 
            for (int m = 0; m < lSelected.Count - 1; m++)
            {
                for (int n = m; n < lSelected.Count; n++)
                {
                    if (lSelected[m] > lSelected[n])
                    {
                        int temp = lSelected[m];
                        lSelected[m] = lSelected[n];
                        lSelected[n] = temp;
                    }
                }
                a += lSelected[m].ToString() + " ";
            } 
            for (int i = lSelected.Count-1; i>=0; i--)
            {
                lDrawn.RemoveAt(lSelected[i]);
            }
            lSelected.Clear();
            pnlMain.Invalidate();
            if (lDrawn.Count==0)
            {
                butSelect_Click(sender, e);
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            multiSelect = false; 
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ControlKey)
            {
                multiSelect = true; 
            }
        }

        private void butSelect_Click(object sender, EventArgs e)
        {
            TurnOffBools();
            if (bSelect == false)
            {
                pnlMain.Cursor = Cursors.Default;
                bSelect = true;
                butDelete.Enabled = true;
                butGroup.Enabled = true;
                butUngroup.Enabled = true;
                boxDash.Enabled = false;
                boxFill.Enabled = false;
                barWidth.Enabled = false;
                boxShape.Enabled = false;
                butLColor.Enabled = false;
                butFColor.Enabled = false;
                butSelect.ForeColor = Color.LimeGreen;
            }
            else
            {
                pnlMain.Cursor = Cursors.Cross;
                butSelect.ForeColor = Color.Black;
                bSelect = false;
                boxDash.Enabled = true;
                boxFill.Enabled = true;
                butDelete.Enabled = false;
                butGroup.Enabled = false;
                butUngroup.Enabled = false;
                barWidth.Enabled = true;
                boxShape.Enabled = true;
                butLColor.Enabled = true;
                if (boxFill.SelectedItem.ToString() == "Fill Shape")
                    butFColor.Enabled = true;
                else
                    butFColor.Enabled = false;
                switch (boxShape.SelectedItem.ToString())
                {
                    case "Bezier":
                        bBezier = true;
                        break;
                    case "Circle":
                        bCircle = true;
                        break;
                    case "Curve":
                        bCurve = true;
                        break;
                    case "Elipse":
                        bElipse = true;
                        break;
                    case "Line":
                        bLine = true;
                        break;
                    case "Polygon":
                        bPolygon = true;
                        break;
                    case "Rectangle":
                        bRectangle = true;
                        break;
                    case "Square":
                        bSquare = true;
                        break;
                }
                lSelected.Clear();
                pnlMain.Invalidate();
                isClicked = false;
            }
        }

        public frPaint()
        {
            InitializeComponent();
            boxShape.SelectedItem = "Line";
            boxFill.SelectedItem = "No Fill Shape";
            boxDash.SelectedItem = "Solid";

            myColor = butLColor.BackColor;
            myFColor = butFColor.BackColor;
            myPen = new Pen(myColor, barWidth.Value);
        }

        private void butClear_Click(object sender, EventArgs e)
        {
            gp = pnlMain.CreateGraphics();
            gp.Clear(Color.White);
            lLine.Clear();
            lRectangle.Clear();
            lCircle.Clear();
            lEllipse.Clear();
            lPolygon.Clear();
            lSquare.Clear();
            lCurve.Clear();
            lBezier.Clear();
            lDrawn.Clear();
            isClicked = false;
            bSelect = true;
            butSelect_Click(sender, e);
        }

        private void butLColor_Click(object sender, EventArgs e)
        {
            ColorDialog tempDialog = new ColorDialog();
            tempDialog.ShowDialog();
            butLColor.BackColor = tempDialog.Color;
        }

        private void pnlMain_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (bBezier == false && bCircle == false && bCurve == false && bElipse == false && bLine == false
                && bPolygon == false && bRectangle == false && bSquare == false && bSelect == false)
                {
                    MessageBox.Show("Please choose a shape you wanna draw!");
                    return;
                }
                if (bLine == true)
                {
                    cLine curLine = new cLine();
                    curLine.P1 = e.Location;
                    curLine.P2 = e.Location;
                    curLine.LWidth = barWidth.Value;
                    curLine.LColor = butLColor.BackColor;
                    if (bDash ==  true)
                    {
                        curLine.BDash = true;
                    }
                    if (bDashDot == true)
                    {
                        curLine.BDashDot = true;
                    }
                    if (bDashDotDot == true)
                    {
                        curLine.BDashDotDot = true;
                    }
                    if (bDot == true)
                    {
                        curLine.BDot = true;
                    }
                    if (bSolid == true)
                    {
                        curLine.BSolid = true;
                    }
                    lLine.Add(curLine);
                    isClicked = true;
                }
                if (bRectangle == true)
                {
                    cRectangle curRec = new cRectangle();
                    curRec.P1 = e.Location;
                    curRec.P2 = e.Location;
                    curRec.RShapeWidth = barWidth.Value;
                    curRec.RLColor = butLColor.BackColor;
                    curRec.RFColor = butFColor.BackColor;
                    curRec.FillShape = isFill;
                    if (bDash == true)
                    {
                        curRec.BDash = true;
                    }
                    if (bDashDot == true)
                    {
                        curRec.BDashDot = true;
                    }
                    if (bDashDotDot == true)
                    {
                        curRec.BDashDotDot = true;
                    }
                    if (bDot == true)
                    {
                        curRec.BDot = true;
                    }
                    if (bSolid == true)
                    {
                        curRec.BSolid = true;
                    }
                    lRectangle.Add(curRec);
                    isClicked = true;
                }
                if (bCircle == true)
                {
                    cCircle curCir = new cCircle();
                    curCir.P1 = e.Location;
                    curCir.P2 = e.Location;
                    curCir.CShapeWidth = barWidth.Value;
                    curCir.CLColor = butLColor.BackColor;
                    curCir.CFColor = butFColor.BackColor;
                    curCir.FillShape = isFill;
                    if (bDash == true)
                    {
                        curCir.BDash = true;
                    }
                    if (bDashDot == true)
                    {
                        curCir.BDashDot = true;
                    }
                    if (bDashDotDot == true)
                    {
                        curCir.BDashDotDot = true;
                    }
                    if (bDot == true)
                    {
                        curCir.BDot = true;
                    }
                    if (bSolid == true)
                    {
                        curCir.BSolid = true;
                    }
                    lCircle.Add(curCir);
                    isClicked = true;
                }
                if (bElipse == true)
                {
                    cEllipse curEllip = new cEllipse();
                    curEllip.P1 = e.Location;
                    curEllip.P2 = e.Location;
                    curEllip.EShapeWidth = barWidth.Value;
                    curEllip.ELColor = butLColor.BackColor;
                    curEllip.EFColor = butFColor.BackColor;
                    curEllip.FillShape = isFill;
                    if (bDash == true)
                    {
                        curEllip.BDash = true;
                    }
                    if (bDashDot == true)
                    {
                        curEllip.BDashDot = true;
                    }
                    if (bDashDotDot == true)
                    {
                        curEllip.BDashDotDot = true;
                    }
                    if (bDot == true)
                    {
                        curEllip.BDot = true;
                    }
                    if (bSolid == true)
                    {
                        curEllip.BSolid = true;
                    }
                    lEllipse.Add(curEllip);
                    isClicked = true;
                }
                if (bSquare == true)
                {
                    cSquare curSquare = new cSquare();
                    curSquare.P1 = e.Location;
                    curSquare.P2 = e.Location;
                    curSquare.SShapeWidth = barWidth.Value;
                    curSquare.SLColor = butLColor.BackColor;
                    curSquare.SFColor = butFColor.BackColor;
                    curSquare.FillShape = isFill;
                    if (bDash == true)
                    {
                        curSquare.BDash = true;
                    }
                    if (bDashDot == true)
                    {
                        curSquare.BDashDot = true;
                    }
                    if (bDashDotDot == true)
                    {
                        curSquare.BDashDotDot = true;
                    }
                    if (bDot == true)
                    {
                        curSquare.BDot = true;
                    }
                    if (bSolid == true)
                    {
                        curSquare.BSolid = true;
                    }
                    lSquare.Add(curSquare);
                    isClicked = true;
                }
                if (bPolygon == true)
                {
                    if (isClicked == true)
                    {
                        lPolygon[lPolygon.Count - 1].LPoint[lPolygon[lPolygon.Count - 1].LPoint.Count - 1] = e.Location;
                        lPolygon[lPolygon.Count - 1].LPoint.Add(e.Location);
                        pnlMain.Invalidate();
                        return;
                    }
                    cPolygon curPol = new cPolygon();
                    curPol.PShapeWidth = barWidth.Value;
                    curPol.PLColor = butLColor.BackColor;
                    curPol.PFColor = butFColor.BackColor;
                    curPol.FillShape = isFill;
                    curPol.LPoint.Add(e.Location);
                    curPol.LPoint.Add(e.Location);
                    if (bDash == true)
                    {
                        curPol.BDash = true;
                    }
                    if (bDashDot == true)
                    {
                        curPol.BDashDot = true;
                    }
                    if (bDashDotDot == true)
                    {
                        curPol.BDashDotDot = true;
                    }
                    if (bDot == true)
                    {
                        curPol.BDot = true;
                    }
                    if (bSolid == true)
                    {
                        curPol.BSolid = true;
                    }
                    lPolygon.Add(curPol);
                    isClicked = true;
                }
                if(bBezier == true)
                {
                    if (isClicked==true)
                    {
                        lBezier[lBezier.Count - 1].LPoint[lBezier[lBezier.Count - 1].LPoint.Count - 1] = e.Location;
                        lBezier[lBezier.Count - 1].LPoint.Add(e.Location);
                        if (lBezier[lBezier.Count - 1].LPoint.Count == 5)
                        {
                            isClicked = false;
                            lDrawn.Add(lBezier.Last());
                            lBezier.Clear();
                        }
                        pnlMain.Invalidate();
                        return;
                    }
                    cBezier cBezier = new cBezier();
                    cBezier.BShapeWidth = barWidth.Value;
                    cBezier.BColor = butLColor.BackColor;
                    cBezier.LPoint.Add(e.Location);
                    cBezier.LPoint.Add(e.Location);
                    if (bDash == true)
                    {
                        cBezier.BDash = true;
                    }
                    if (bDashDot == true)
                    {
                        cBezier.BDashDot = true;
                    }
                    if (bDashDotDot == true)
                    {
                        cBezier.BDashDotDot = true;
                    }
                    if (bDot == true)
                    {
                        cBezier.BDot = true;
                    }
                    if (bSolid == true)
                    {
                        cBezier.BSolid = true;
                    }
                    lBezier.Add(cBezier);
                    isClicked = true;
                }
                if (bCurve == true)
                {
                    if (isClicked == true)
                    {
                        lCurve[lCurve.Count - 1].LPoint[lCurve[lCurve.Count - 1].LPoint.Count - 1] = e.Location;
                        lCurve[lCurve.Count - 1].LPoint.Add(e.Location);
                        pnlMain.Invalidate();
                        return;
                    }
                    cCurve curCurve = new cCurve();
                    curCurve.CurShapeWidth = barWidth.Value;
                    curCurve.CurColor = butLColor.BackColor;
                    curCurve.LPoint.Add(e.Location);
                    curCurve.LPoint.Add(e.Location);
                    if (bDash == true)
                    {
                        curCurve.BDash = true;
                    }
                    if (bDashDot == true)
                    {
                        curCurve.BDashDot = true;
                    }
                    if (bDashDotDot == true)
                    {
                        curCurve.BDashDotDot = true;
                    }
                    if (bDot == true)
                    {
                        curCurve.BDot = true;
                    }
                    if (bSolid == true)
                    {
                        curCurve.BSolid = true;
                    }
                    lCurve.Add(curCurve);
                    isClicked = true;
                }
                if (bSelect == true)
                {
                    for (int i=lDrawn.Count-1; i>= 0; i--)
                    {
                        if (lDrawn[i].IsHit(e.Location))
                        { 
                            if (multiSelect == false)
                            {
                                lSelected.Clear();
                            }

                            lSelected.Add(i);
                            for (int m=0; i< lSelected.Count-1; i++)
                            {
                                if (lSelected[m]==i)
                                {
                                    lSelected.Remove(lSelected.Last());
                                    break;
                                }
                            }
                            if(multiSelect==false)
                                isMoving = true;
                            pnlMain.Invalidate();
                            return; 
                        } 
                    }
                    lSelected.Clear();
                    pnlMain.Invalidate();
                    isMoving = false;
                }
            }
            if (e.Button == MouseButtons.Right)
            {
                if (bPolygon == true)
                {
                    if (isClicked == true)
                    {
                        lPolygon[lPolygon.Count - 1].LPoint[lPolygon[lPolygon.Count - 1].LPoint.Count - 1] = e.Location;
                        lDrawn.Add(lPolygon[lPolygon.Count - 1]);
                        lPolygon.Clear();
                        isClicked = false;
                    }
                }
                if (bBezier == true)
                {
                    if (isClicked == true)
                    {
                        lBezier[lBezier.Count - 1].LPoint[lBezier[lBezier.Count - 1].LPoint.Count - 1] = e.Location;
                        lDrawn.Add(lBezier[lBezier.Count - 1]);
                        lBezier.Clear();
                        isClicked = false;
                    }
                }
                if (bCurve == true)
                {
                    if (isClicked == true)
                    {
                        lCurve[lCurve.Count - 1].LPoint[lCurve[lCurve.Count - 1].LPoint.Count - 1] = e.Location;
                        lDrawn.Add(lCurve[lCurve.Count - 1]);
                        lCurve.Clear();
                        isClicked = false;
                    }
                }
                if (bSelect==true)
                {
                    if (pnlMain.Cursor == Cursors.SizeAll)
                    {
                        lSelected.Clear();
                        lSelected.Add(sResize);
                        isResizing = true;
                        pnlMain.Invalidate();
                        return;
                    }
                }
            }
        }

        private void pnlMain_MouseMove(object sender, MouseEventArgs e)
        {
            if (bLine == true)
            {
                if (isClicked == true)
                {
                    lLine[lLine.Count - 1].P2 = e.Location;
                    pnlMain.Invalidate();
                }
            }
            if (bRectangle == true)
            {
                if (isClicked == true)
                {
                    lRectangle[lRectangle.Count - 1].P2 = e.Location;
                    pnlMain.Invalidate();
                }
            }
            if (bCircle == true)
            {
                if (isClicked == true)
                {
                    lCircle[lCircle.Count - 1].P2 = e.Location;
                    pnlMain.Invalidate();
                }
            }
            if (bElipse == true)
            {
                if (isClicked == true)
                {
                    lEllipse[lEllipse.Count - 1].P2 = e.Location;
                    pnlMain.Invalidate();
                }
            }
            if (bSquare == true)
            {
                if (isClicked == true)
                {
                    lSquare[lSquare.Count - 1].P2 = e.Location;
                    pnlMain.Invalidate();
                }
            }
            if (bPolygon == true)
            {
                if (isClicked == true)
                {
                    lPolygon[lPolygon.Count - 1].LPoint[lPolygon[lPolygon.Count - 1].LPoint.Count - 1] = e.Location;
                    pnlMain.Invalidate();
                }
            }
            if (bBezier == true)
            {
                if (isClicked == true)
                {
                    lBezier[lBezier.Count - 1].LPoint[lBezier[lBezier.Count - 1].LPoint.Count - 1] = e.Location;
                    pnlMain.Invalidate();
                }
            }
            if (bCurve == true)
            {
                if (isClicked == true)
                {
                    lCurve[lCurve.Count - 1].LPoint[lCurve[lCurve.Count - 1].LPoint.Count - 1] = e.Location;
                    pnlMain.Invalidate();
                }
            }
            if(bSelect==true)
            {
                pnlMain.Cursor = Cursors.Arrow;
                if (isMoving == true)
                {
                    for(int i =0; i<lSelected.Count; i++)
                        lDrawn[lSelected[i]].Move(e.Location);
                    pnlMain.Invalidate();
                }
                if (isResizing == true) 
                {
                    lDrawn[lSelected[0]].Resize(e.Location);
                    pnlMain.Invalidate();
                }
                for (int i = 0; i < lSelected.Count; i++)
                {
                    if (lDrawn[lSelected[i]].CanResize(e.Location))
                    {
                        sResize = lSelected[i];
                        pnlMain.Cursor = Cursors.SizeAll;
                        return;
                    }
                }
                for (int i = 0; i < lDrawn.Count; i++)
                {
                    if (lDrawn[i].IsHit(e.Location))
                        pnlMain.Cursor = Cursors.Hand;
                }
                
            }
        }

        private void pnlMain_MouseUp(object sender, MouseEventArgs e)
        {
            if (bLine == true)
            {
                if (isClicked == true)
                {
                    lLine[lLine.Count - 1].P2 = e.Location;
                    lDrawn.Add(lLine[lLine.Count - 1]);
                    lLine.Clear();
                    pnlMain.Invalidate();
                    isClicked = false;
                }
            }
            if (bRectangle == true)
            {
                if (isClicked == true)
                {
                    lRectangle[lRectangle.Count - 1].P2 = e.Location;
                    lRectangle[lRectangle.Count - 1].IsDrawn = true;
                    lDrawn.Add(lRectangle[lRectangle.Count - 1]);
                    lRectangle.Clear();
                    pnlMain.Invalidate();
                    isClicked = false;
                }
            }
            if (bCircle == true)
            {
                if (isClicked == true)
                {
                    lCircle[lCircle.Count - 1].P2 = e.Location;
                    lCircle[lCircle.Count - 1].IsDrawn = true;
                    lDrawn.Add(lCircle[lCircle.Count - 1]);
                    lCircle.Clear();
                    pnlMain.Invalidate();
                    isClicked = false;
                }
            }
            if (bElipse == true)
            {
                if (isClicked == true)
                {
                    lEllipse[lEllipse.Count - 1].P2 = e.Location;
                    lEllipse[lEllipse.Count - 1].IsDrawn = true;
                    lDrawn.Add(lEllipse[lEllipse.Count - 1]);
                    lEllipse.Clear();
                    pnlMain.Invalidate();
                    isClicked = false;
                }
            }
            if (bSquare == true)
            {
                if (isClicked == true)
                {
                    lSquare[lSquare.Count - 1].P2 = e.Location;
                    lSquare[lSquare.Count - 1].IsDrawn = true;
                    lDrawn.Add(lSquare[lSquare.Count - 1]);
                    lSquare.Clear();
                    pnlMain.Invalidate();
                    isClicked = false;
                }
            }
            if (bSelect == true)
            {
                if (isMoving == true) 
                {
                    for (int i =0; i<lSelected.Count; i++)
                        lDrawn[lSelected[i]].Move(e.Location);
                    pnlMain.Invalidate();
                    isMoving = false;
                }
                if (isResizing == true)
                {
                    lDrawn[lSelected[0]].Resize(e.Location);
                    pnlMain.Invalidate();
                    isResizing = false;
                }
            }
        }

        private void pnlMain_Paint(object sender, PaintEventArgs e)
        {
            gp = e.Graphics;
            if (bSelect == false)
            {
                if (lLine.Count > 0)
                {
                    for (int i = 0; i < lLine.Count; i++)
                    {
                        lLine[i].Draw(gp);
                    }
                }
                if (lRectangle.Count > 0)
                {
                    for (int i = 0; i < lRectangle.Count; i++)
                    {
                        lRectangle[i].Draw(gp);
                    }
                }
                if (lCircle.Count > 0)
                {
                    for (int i = 0; i < lCircle.Count; i++)
                    {
                        lCircle[i].Draw(gp);
                    }
                }
                if (lEllipse.Count > 0)
                {
                    for (int i = 0; i < lEllipse.Count; i++)
                    {
                        lEllipse[i].Draw(gp);
                    }
                }
                if (lSquare.Count > 0)
                {
                    for (int i = 0; i < lSquare.Count; i++)
                    {
                        lSquare[i].Draw(gp);
                    }
                }
                if (lPolygon.Count > 0)
                {
                    for (int i = 0; i < lPolygon.Count; i++)
                    {
                        lPolygon[i].Draw(gp);
                    }
                }
                if (lBezier.Count > 0)
                {
                    for (int i = 0; i < lBezier.Count; i++)
                    {
                        lBezier[i].Draw(gp);
                    }
                }
                if (lCurve.Count > 0)
                {
                    for (int i = 0; i < lCurve.Count; i++)
                    {
                        lCurve[i].Draw(gp);
                    }
                }
                for (int i = 0; i < lDrawn.Count; i++)
                {
                    lDrawn[i].Draw(gp);
                }
            }
            else
            {
                for (int i = 0;i <lDrawn.Count; i++)
                {
                    lDrawn[i].Draw(gp);
                }
                for (int j = 0; j<lSelected.Count; j++)
                {
                    lDrawn[lSelected[j]].DrawSelectArea(gp);
                }
            }
        }

        private void boxFill_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (boxFill.SelectedItem.ToString() == "Fill Shape")
            {
                butFColor.Enabled = true;
                boxShape.Items.Remove("Bezier");
                boxShape.Items.Remove("Curve");
                boxShape.Items.Remove("Line");
                bBezier = false;
                bCurve = false;
                bLine = false;
                isFill = true;
            }
            if (boxFill.SelectedItem.ToString() == "No Fill Shape")
            {
                butFColor.Enabled = false;
                if (!(boxShape.FindStringExact("Bezier") >= 0))
                    boxShape.Items.Add("Bezier");
                if (!(boxShape.FindStringExact("Line") >= 0))
                    boxShape.Items.Add("Line");
                if (!(boxShape.FindStringExact("Curve") >= 0))
                    boxShape.Items.Add("Curve");
                isFill = false;
            }
        }

        private void boxShape_SelectedIndexChanged(object sender, EventArgs e)
        {
            TurnOffBools();
            switch (boxShape.SelectedItem.ToString())
            {
                case "Bezier":
                    bBezier = true;
                    break;
                case "Circle":
                    bCircle = true;
                    break;
                case "Curve":
                    bCurve = true;
                    break;
                case "Elipse":
                    bElipse = true;
                    break;
                case "Line":
                    bLine = true;
                    break;
                case "Polygon":
                    bPolygon = true;
                    break;
                case "Rectangle":
                    bRectangle = true;
                    break;
                case "Square":
                    bSquare = true;
                    break;
            }
            isClicked = false;
        }

        private void boxDash_SelectedIndexChanged(object sender, EventArgs e)
        {
            bSolid = false;
            bDash = false;
            bDashDot = false; 
            bDashDotDot = false;
            bDot = false;
            switch (boxDash.SelectedItem.ToString())
            {
                case "Solid":
                    bSolid = true;
                    break;
                case "Dash":
                    bDash = true;
                    break;
                case "Dash Dot":
                    bDashDot = true;
                    break;
                case "Dash Dot Dot":
                    bDashDotDot = true;
                    break;
                case "Dot":
                    bDot = true;
                    break;
            }
        }

        private void TurnOffBools()
        {
            bBezier = false;
            bCircle = false;
            bCurve = false;
            bElipse = false;
            bLine = false;
            bPolygon = false;
            bRectangle = false;
            bSquare = false;
        }

    }
    public class MyPanel : Panel
    {
        public MyPanel():base()
        {
            this.DoubleBuffered = true;
        }
    }
}
