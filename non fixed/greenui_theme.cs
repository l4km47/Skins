
    /// <summary>
    /// GreenUI Theme
    /// Author : THE LORD
    /// Release Date : Saturday, December 17, 2016
    /// Last Update : Monday, January 9, 2017
    /// Update Purpose : Bugs Fixed, ComboBox & TabControl Added.
    /// </summary>

    #region Namespaces

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

#endregion

    #region Helper Methods

public class HelperMethods
{

    public GraphicsPath GP = null;

    public enum MouseMode
    {
        NormalMode,
        Hovered,
        Pushed
    };

    public void DrawImageFromBase64(Graphics G, string Base64Image, Rectangle Rect)
    {
        Image IM = null;
        using (System.IO.MemoryStream ms = new System.IO.MemoryStream(Convert.FromBase64String(Base64Image)))
        {
            IM = System.Drawing.Image.FromStream(ms);
        }
        G.DrawImage(IM, Rect);
    }

    public GraphicsPath RoundRec(Rectangle r, int Curve, bool TopLeft = true, bool TopRight = true, bool BottomLeft = true, bool BottomRight = true)
    {
        GraphicsPath CreateRoundPath = new GraphicsPath(FillMode.Winding);
        if (TopLeft)
        {
            CreateRoundPath.AddArc(r.X, r.Y, Curve, Curve, 180f, 90f);
        }
        else
        {
            CreateRoundPath.AddLine(r.X, r.Y, r.X, r.Y);
        }
        if (TopRight)
        {
            CreateRoundPath.AddArc(r.Right - Curve, r.Y, Curve, Curve, 270f, 90f);
        }
        else
        {
            CreateRoundPath.AddLine(r.Right - r.Width, r.Y, r.Width, r.Y);
        }
        if (BottomRight)
        {
            CreateRoundPath.AddArc(r.Right - Curve, r.Bottom - Curve, Curve, Curve, 0f, 90f);
        }
        else
        {
            CreateRoundPath.AddLine(r.Right, r.Bottom, r.Right, r.Bottom);

        }
        if (BottomLeft)
        {
            CreateRoundPath.AddArc(r.X, r.Bottom - Curve, Curve, Curve, 90f, 90f);
        }
        else
        {
            CreateRoundPath.AddLine(r.X, r.Bottom, r.X, r.Bottom);
        }
        CreateRoundPath.CloseFigure();
        return CreateRoundPath;
    }

        public LinearGradientBrush ShadowBrush(Rectangle R ,Color C , int Intesity, int angle ) 
        {
        return new LinearGradientBrush(R, Color.FromArgb(Intesity, C), Color.Transparent, angle);
        }

    public void FillRoundedPath(Graphics G, Color C, Rectangle Rect, int Curve, bool TopLeft = true, bool TopRight = true, bool BottomLeft = true, bool BottomRight = true)
    {
        G.FillPath(new SolidBrush(C), RoundRec(Rect, Curve,TopLeft,TopRight,BottomLeft,BottomRight));
    }

    public void FillRoundedPath(Graphics G, Brush B, Rectangle Rect, int Curve, bool TopLeft = true, bool TopRight = true, bool BottomLeft = true, bool BottomRight = true)
    {
        G.FillPath(B, RoundRec(Rect, Curve, TopLeft, TopRight, BottomLeft, BottomRight));
    }

    public void DrawRoundedPath(Graphics G, Color C, Single Size, Rectangle Rect, int Curve, bool TopLeft = true, bool TopRight = true, bool BottomLeft = true, bool BottomRight = true)
    {
        G.DrawPath(new Pen(C, Size), RoundRec(Rect, Curve, TopLeft, TopRight, BottomLeft, BottomRight));

    }

    public void DrawTriangle(Graphics G, Color C, Single Size, Point P1_0, Point P1_1, Point P2_0, Point P2_1, Point P3_0, Point P3_1)
    {

        G.DrawLine(new Pen(C, Size), P1_0, P1_1);
        G.DrawLine(new Pen(C, Size), P2_0, P2_1);
        G.DrawLine(new Pen(C, Size), P3_0, P3_1);

    }

    public Pen PenRGBColor(int R, int G, int B, Single size)
    { return new Pen(System.Drawing.Color.FromArgb(R, G, B), size); }

    public Pen PenHTMlColor(String C_WithoutHash, float Thick)
    { return new Pen(GetHTMLColor(C_WithoutHash), Thick); }

    public SolidBrush SolidBrushRGBColor(int R, int G, int B, int A = 0)
    { return new SolidBrush(System.Drawing.Color.FromArgb(A, R, G, B)); }

    public SolidBrush SolidBrushHTMlColor(String C_WithoutHash)
    { return new SolidBrush(GetHTMLColor(C_WithoutHash)); }

    public Color GetHTMLColor(String C_WithoutHash)
    { return ColorTranslator.FromHtml("#" + C_WithoutHash); }

    public String ColorToHTML(Color C)
    { return ColorTranslator.ToHtml(C); }

    public Color SetARGB(int A, int R, int G, int B)
    { return System.Drawing.Color.FromArgb(A, R, G, B); }

    public Color SetRGB(int R, int G, int B)
    { return System.Drawing.Color.FromArgb(R, G, B); }

    public void CentreString(Graphics G, String Text, Font font, Brush brush, Rectangle Rect)
    { G.DrawString(Text, font, brush, new Rectangle(Rect.X, Rect.Y, Rect.Width, Rect.Height), new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center }); }

    public void LeftString(Graphics G, String Text, Font font, Brush brush, Rectangle Rect)
    {
        G.DrawString(Text, font, brush, new Rectangle(4, Rect.Y + Convert.ToInt32(Rect.Height / 2) - Convert.ToInt32(G.MeasureString(Text, font).Height / 2) + 0, Rect.Width, Rect.Height), new StringFormat { Alignment = StringAlignment.Near });
    }

    public void RightString(Graphics G, string Text, Font font, Brush brush, Rectangle Rect)
    {
        G.DrawString(Text, font, brush, new Rectangle(4, Convert.ToInt32(Rect.Y + (Rect.Height / 2) - (G.MeasureString(Text, font).Height / 2)), Rect.Width - Rect.Height + 10, Rect.Height), new StringFormat { Alignment = StringAlignment.Far });
    }
}

#endregion

    #region Skin

    public class GreenUISkin : ContainerControl
    {

        #region Variables  

        bool Movable  = false;
        private TitlePostion _TitleTextPostion   = TitlePostion.Left;
        static Point MousePoint= new Point(0, 0);
        private int MoveHeight = 50;
        private static HelperMethods H = new HelperMethods();
        private bool _ShowIcon;

#endregion

        #region  Properties 

        public bool ShowIcon
        {
            get { return _ShowIcon; }
            set
            {
                if (value == _ShowIcon) { return; }
                FindForm().ShowIcon = value;
                Invalidate();
                _ShowIcon = value;

            }
        }

    
            public TitlePostion TitleTextPostion
        {
            get { return _TitleTextPostion;}

            set 
            { 
                _TitleTextPostion = value;
                Invalidate(); 
            }

         }


   public  enum TitlePostion{
        Left,
        Center
        };

        #endregion
        
        #region  Initialization 

    public GreenUISkin()
    {
        SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor,true);
        DoubleBuffered = true;
        UpdateStyles();
        Font = new Font("Proxima Nova", 14, FontStyle.Bold);
        BackColor = Color.Gainsboro;

    }

#endregion

        #region Mouse & other Events

    protected override void OnCreateControl()
    {
        base.OnCreateControl();
        ParentForm.FormBorderStyle = FormBorderStyle.None;
        ParentForm.TransparencyKey = Color.Fuchsia;
        Dock = DockStyle.Fill;
    }


    protected override void OnMouseMove(MouseEventArgs e)
    {
        base.OnMouseMove(e);
        int x = MousePosition.X;
        int y = MousePosition.Y;
        int x1 = MousePoint.X;
        int y1 = MousePoint.Y;
        if (Movable)

            Parent.Location = new Point(x - x1, y - y1);
        Focus();
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
        base.OnMouseDown(e);
        if (e.Button == System.Windows.Forms.MouseButtons.Left && new Rectangle(0, 0, Width, MoveHeight).Contains(e.Location))
        {
            Movable = true;
            MousePoint = e.Location;
        }

    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
        base.OnMouseUp(e);
        Movable = false;
    }

        #endregion

        #region Draw Control

    protected override void OnPaint(PaintEventArgs e)
    {

        Rectangle Rect = new Rectangle(1, 1, (int)(Width - 2.5), (int)(Height - 2.5));
        Bitmap B = new Bitmap(Width, Height);
        using (Graphics G = Graphics.FromImage(B))
        {
            G.Clear(Color.Fuchsia);

            G.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

             H.FillRoundedPath(G, Color.Gainsboro, Rect, 8);

             H.FillRoundedPath(G, H.GetHTMLColor("097A74"), new Rectangle(1, 1, Width - 2, 50), 5, true, true, false, false);
        
            G.DrawLine(H.PenHTMlColor("066762", 1), new Point(1, 51), new Point(Width - 2, 51));
           
            if (ShowIcon)
                 {
                     G.DrawIcon(FindForm().Icon, new Rectangle(5, 13, 20, 20));
                     switch (TitleTextPostion)
                     {
                         case TitlePostion.Left:
                             G.DrawString(Text, Font, Brushes.White, 27, 14);
                             break;
                         case TitlePostion.Center:
                             H.CentreString(G, Text, Font, Brushes.White, new Rectangle(0, 0, Width, 50));
                             break;
                     }
                 }
                 else
                 {
                     switch (TitleTextPostion)
                     {
                         case TitlePostion.Left:
                             G.DrawString(Text, Font, Brushes.White, 5, 14);
                             break;
                         case TitlePostion.Center:
                             H.CentreString(G, Text, Font, Brushes.White, new Rectangle(0, 0, Width, 50));
                             break;
                     }
                 }


         e.Graphics.DrawImage((Image)(B.Clone()), 0, 0);
         G.Dispose();
         B.Dispose();

        }
        
    }

        #endregion

    }

    #endregion

    #region Switch

  [DefaultEvent("CheckedChanged")] public class GreenUISwitch : Control
  {
      
      #region Variables

      private bool _Switch = false;
      protected HelperMethods.MouseMode State = HelperMethods.MouseMode.NormalMode;
      private Color _ForeColor  = Color.Gray;
      private static HelperMethods H = new HelperMethods();

      #endregion

      #region Initialization

      public GreenUISwitch()
      {
          SetStyle( ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint |
          ControlStyles.OptimizedDoubleBuffer | ControlStyles.SupportsTransparentBackColor, true);
          DoubleBuffered = true;
          UpdateStyles();
          BackColor = Color.Transparent;
          Cursor = Cursors.Hand;
          Size = new Size(70, 28);
      }

      #endregion

      #region Properties

      public bool Switched
            {
                get { return _Switch; }

                set { _Switch = value; }
            }

      #endregion

      #region DrawControl

      protected override void OnPaint(PaintEventArgs e)
      {
          base.OnPaint(e);
            Bitmap B = new Bitmap(Width, Height);
          using (Graphics G = Graphics.FromImage(B))
          {

                  G.SmoothingMode = SmoothingMode.HighQuality;
                G.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
                if (Switched) 
                {

                    H.FillRoundedPath(G, H.GetHTMLColor("097A74"), new Rectangle(0, 0, 70, 27), 8);

                    H.FillRoundedPath(G, Color.WhiteSmoke, new Rectangle(Convert.ToInt32(Width - 28.5), Convert.ToInt32(1.5), 25, 23), 8);
                    G.DrawLine(Pens.Gray, Width - 14, 8, Width - 14, 18);
                    G.DrawLine(Pens.Gray, Width - 16, 8, Width - 16, 18);
                    G.DrawLine(Pens.Gray, Width - 18, 8, Width - 18, 18);
                    H.DrawRoundedPath(G, H.GetHTMLColor("056b65"), 1, new Rectangle(0, 0, 69, 27), 8);
                    G.DrawString("ON", new Font("Proxima Nova", 10, FontStyle.Bold), Brushes.WhiteSmoke, new Point(Width - 62, 6));
               } 
              else
                {
                    H.FillRoundedPath(G, Color.Silver, new Rectangle(0, 0, 70, 27), 8);

                    H.FillRoundedPath(G, Color.WhiteSmoke, new Rectangle(3, Convert.ToInt32(1.5), 25, 23), 8);
                    G.DrawLine(Pens.Gray, 13, 8, 13, 18);
                    G.DrawLine(Pens.Gray, 15, 8, 15, 18);
                    G.DrawLine(Pens.Gray, 17, 8, 17, 18);
                    H.DrawRoundedPath(G, H.GetHTMLColor("bbbaba"), 1, new Rectangle(0, 0, 69, 27), 8);
                    G.DrawString("OFF", new Font("Proxima Nova", 10, FontStyle.Bold), Brushes.WhiteSmoke, new Point(31, 6));

                }

          }
          e.Graphics.DrawImage((Image)(B.Clone()), 0, 0);
      }

      #endregion

      #region   Mouse & Other Events  

        public event CheckedChangedEventHandler CheckedChanged;
        public delegate void CheckedChangedEventHandler(object sender);

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
              State = HelperMethods.MouseMode.Pushed;
              Invalidate();
        }
        
        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
              State = HelperMethods.MouseMode.Hovered;
              Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            State = HelperMethods.MouseMode.NormalMode;
            Invalidate();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            State = HelperMethods.MouseMode.Hovered;
            Invalidate();
        }

        protected override void OnClick(EventArgs e)
        {
           
             _Switch = ! _Switch;
             if (CheckedChanged != null)
             {
                 CheckedChanged(this);
             }
        base.OnClick(e);
        }

        protected override void OnResize(EventArgs e)
      {
          base.OnResize(e);
          Size = new Size(70, 28);
      }

#endregion

  }

    #endregion

    #region Trackbar

    [DefaultEvent("Scroll")]  public class GreenUITrackbar : Control
  {

      #region Variables

      private int _Maximum = 100;
      private int _Minimum;
      private int _Value;
      private int CurrentValue;
      private Point P1, P2, P3, P4, P5, P6 ;
      bool Variable;
      Rectangle Track;
      private static HelperMethods H = new HelperMethods();

      #endregion

      #region properties

      public int Minimum
      {
          get
          {
              return _Minimum;

          }

          set
          {
              if (!( value < 0))
              {
                _Minimum = value;
                RenewCurrentValue();
                MoveTrack();
                Invalidate();
              }

          }
      }

      public int Maximum
      {
          get
          {
              return _Maximum;

          }
          set
          {
            _Maximum = value;
            RenewCurrentValue();
            MoveTrack();
            Invalidate();

          }
      }

      public int Value
      {
          get
          {
              return _Value;
          }
          set
          {
              if (value != _Value)
              {
                  _Value = value;
                  RenewCurrentValue();
                  MoveTrack();
                  Invalidate();
                  if (Scroll != null)
                      Scroll(this);

              }

          }
      }

      
      #endregion

      #region Initialization

      public GreenUITrackbar()
      {
          SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint |
          ControlStyles.ResizeRedraw | ControlStyles.UserPaint |
          ControlStyles.SupportsTransparentBackColor, true);
          DoubleBuffered = true;
          UpdateStyles();
          CurrentValue=(int)Math.Round((double)(Value / Maximum) * (Double)Width);
      }

      #endregion

      #region DrawControl

      protected override void OnPaint(PaintEventArgs e)
      {
          base.OnPaint(e);
          Bitmap B = new Bitmap(Width, Height);
          using (Graphics G = Graphics.FromImage(B))
          {

              Cursor = Cursors.Hand;
                G.SmoothingMode = SmoothingMode.AntiAlias;
                G.PixelOffsetMode = PixelOffsetMode.HighQuality;
                G.InterpolationMode = InterpolationMode.HighQualityBicubic;

              H.FillRoundedPath(G, Color.Silver, new Rectangle(0, 6, Width, 12), 8);
              H.DrawRoundedPath(G, Color.FromArgb(200, Color.DarkGray), 1, new Rectangle(0, Convert.ToInt32(5.5), Width, 12), 8);
            
              if (CurrentValue != 0)
              {
                  H.FillRoundedPath(G, H.GetHTMLColor("097A74"), new Rectangle(0, 6, CurrentValue + 2, 12), 5);
              }

              G.PixelOffsetMode = PixelOffsetMode.None;

              H.FillRoundedPath(G, H.GetHTMLColor("c2c2c2"), Track, 6);
              H.FillRoundedPath(G, Color.WhiteSmoke, Track, 6);
              H.DrawRoundedPath(G, Color.FromArgb(60, Color.DarkGray), 1, Track, 6);
              G.DrawLine(Pens.Gray, P1, P2);
              G.DrawLine(Pens.Gray, P3, P4);
              G.DrawLine(Pens.Gray, P5, P6);

          e.Graphics.DrawImage((Image)B.Clone(), 0, 0);
          G.Dispose();
          B.Dispose();
          }

      }
      #endregion

      #region Mouse & Other Events

      public event ScrollEventHandler Scroll;
      public delegate void ScrollEventHandler(object sender);

      protected override void OnMouseMove(MouseEventArgs e)
      {
          if (Variable && e.X > -1 && e.X < Width + 1)
          {
              Value = Minimum + (int)Math.Round((double)(Maximum - Minimum) * (double)e.X / Width);
          }
             
          base.OnMouseMove(e);
      }
      
      protected override void OnMouseDown(MouseEventArgs e)
      {
          if (e.Button == MouseButtons.Left && Height > 0)
          {
              RenewCurrentValue();
              if (Width > 0 && Height > 0) Track = new Rectangle(Convert.ToInt32(CurrentValue + 0.8), 0, 25, 24);
        
              Variable = new Rectangle(CurrentValue, 0, 24, Height).Contains(e.Location);
          }
          base.OnMouseDown(e);
      }

      protected override void OnMouseUp(MouseEventArgs e)
      {
          Variable = false;
          base.OnMouseUp(e);
      }

      protected override void OnKeyDown(KeyEventArgs e)
      {
          if (e.KeyCode == Keys.Subtract || e.KeyCode == Keys.Down || e.KeyCode==Keys.Left)
          {
              if (Value != 0)             
              {
                  Value -= 1;

              }
             
          }
          else if (e.KeyCode == Keys.Add || e.KeyCode == Keys.Up || e.KeyCode == Keys.Right)
          {
              if (Value != Maximum)
              {
                  Value += 1;
              }
             
          }
          base.OnKeyDown(e);
      }

      protected override void OnResize(EventArgs e)
      {
               if (Width > 0 && Height > 0) 
               {
            RenewCurrentValue();
            MoveTrack();
            Height = 25;
               }

               Invalidate();
          base.OnResize(e);
      }

      public void MoveTrack()
          {
        if (Height > 0 && Width > 0) 
          {
      Track = new Rectangle(Convert.ToInt32(CurrentValue - 0.4), 0, 25, 23);
          }
        P1 = new Point(CurrentValue + 9, Track.Y + 5);
        P2 = new Point(CurrentValue + 9, Track.Height - 5);
        P3 = new Point(CurrentValue + 12, Track.Y + 5);
        P4 = new Point(CurrentValue + 12, Track.Height - 5);
        P5 = new Point(CurrentValue + 15, Track.Y + 5);
        P6 = new Point(CurrentValue + 15, Track.Height - 5);
          }

      public void RenewCurrentValue()
          {
              
              CurrentValue = (int)Math.Round((double)(Value - Minimum) / (double)(Maximum - Minimum) * (double)(Width - 25));
          }

      #endregion

  }

    #endregion

    #region Button

    public class GreenUIButton : Control
  {


      #region Variables

      private HelperMethods.MouseMode State;
      private static HelperMethods H = new HelperMethods();

      #endregion

      #region Initialization

      public GreenUIButton()
      {
          SetStyle(ControlStyles.DoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.ResizeRedraw | ControlStyles.UserPaint | 
          ControlStyles.Selectable | ControlStyles.SupportsTransparentBackColor, true);
          DoubleBuffered = true;
          UpdateStyles();
          Font = new Font("Proxima Nova", 10, FontStyle.Bold);
      }

      #endregion

      #region DrawControl

      protected override void OnPaint(PaintEventArgs e)
      {

          Rectangle Rect = new Rectangle(0, 0, Width -1, Height -1);
          Bitmap B = new Bitmap(Width, Height);
          using (Graphics G = Graphics.FromImage(B))
          {
              G.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
              G.SmoothingMode = SmoothingMode.HighQuality;

              switch (State)
              {
                  case HelperMethods.MouseMode.NormalMode:
                    H.FillRoundedPath(G, H.GetHTMLColor("097A74"), Rect, 12);
                    H.DrawRoundedPath(G, H.GetHTMLColor("e0e0e0"), 1, Rect, 12);
                    H.CentreString(G, Text, Font, Brushes.White, Rect);
                    break;
                  case HelperMethods.MouseMode.Hovered:
                    Cursor = Cursors.Hand;
                    H.FillRoundedPath(G, H.GetHTMLColor("06514C"), Rect, 12);
                    H.DrawRoundedPath(G, H.GetHTMLColor("9d92a8"), 1, Rect, 12);
                    H.CentreString(G, Text, Font, H.SolidBrushHTMlColor("ffffff"), Rect);
                    break;
                  case HelperMethods.MouseMode.Pushed:
                    H.FillRoundedPath(G, H.GetHTMLColor("21272c"), Rect, 12);
                    H.DrawRoundedPath(G, H.GetHTMLColor("444444"), 1, Rect, 12);
                    H.CentreString(G, Text, Font, H.SolidBrushHTMlColor("444444"), Rect);
                    break;
              }
             e.Graphics.DrawImage((Image)(B.Clone()), 0, 0);
             G.Dispose();
             B.Dispose();
          }
          
          
      }

      #endregion

      #region Mouse Events

      protected override void OnMouseEnter(EventArgs e)
      {
          
          base.OnMouseEnter(e);
          State = HelperMethods.MouseMode.Hovered;
          Invalidate();
      }

      protected override void OnMouseUp(MouseEventArgs e)
      {
          
        base.OnMouseUp(e);
        State = HelperMethods.MouseMode.Hovered;
        Invalidate();
      }

      protected override void OnMouseDown(MouseEventArgs e)
      {
          
         base.OnMouseDown(e);
         State = HelperMethods.MouseMode.Pushed;
         Invalidate();
      }

      protected override void OnMouseLeave(EventArgs e)
      {
          
         base.OnMouseLeave(e);
         State = HelperMethods.MouseMode.NormalMode;
         Invalidate();
      }

      #endregion

  }
    
    #endregion

    #region CheckBox

  [DefaultEvent("CheckedChanged")]  public class GreenUICheckBox : Control
  {
      
      #region Variables

         private bool _Checked;
         protected HelperMethods.MouseMode State = HelperMethods.MouseMode.NormalMode;
         private  Color _BackColor = Color.WhiteSmoke;
         private Color  _ForeColor = Color.Gray;
         private static HelperMethods H = new HelperMethods();

      #endregion

      #region Properties

         protected override void OnTextChanged(EventArgs e)
         {
             base.OnTextChanged(e);
             Invalidate();
         }

         public bool Checked
         {
             get
             {
                 return _Checked;
             }
             set
             {
                 _Checked = value;
                 Invalidate();
             }
         }

         #endregion

      #region Initialization

         public GreenUICheckBox()
      {
          SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | 
                   ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer 
                   | ControlStyles.SupportsTransparentBackColor | ControlStyles.UseTextForAccessibility,true);
          DoubleBuffered = true;
          UpdateStyles();
                  Cursor = Cursors.Hand;
        Size = new Size(200, 20);
        Font = new Font("Proxima Nova", 11, FontStyle.Regular);
      }

      #endregion

      #region DrawControl

      protected override void OnPaint(PaintEventArgs e)
      {
          base.OnPaint(e);
          Bitmap B = new Bitmap(Width, Height);
          using (Graphics G = Graphics.FromImage(B))
          {

                G.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                G.PixelOffsetMode = PixelOffsetMode.Half;

             if (Checked)
              {
                    H.FillRoundedPath(G, H.GetHTMLColor("097A74"), new Rectangle(4, 4, 12, 12), 2);
                    H.DrawRoundedPath(G, H.GetHTMLColor("097A74"), 2, new Rectangle(1, 1, 18, 18), 2);
                    G.DrawString(Text, Font, H.SolidBrushHTMlColor("097A74"), new Rectangle(22, +(int)1.6, Width, Height - 2), new StringFormat { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center });
              }
             else
              {
                    H.FillRoundedPath(G, Color.Silver, new Rectangle(4, 4, 12, 12), 2);
                    H.DrawRoundedPath(G, Color.Silver, 2, new Rectangle(1, 1, 18, 18), 2);
                    G.DrawString(Text, Font, Brushes.DimGray, new Rectangle(22, +(int) 1.6, Width, Height - 2), new StringFormat {Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center});
              }

            e.Graphics.DrawImage((Image)(B.Clone()), 0, 0);
            G.Dispose();
            B.Dispose();
          }
          
          
      }

      #endregion

      #region  Mouse & Other Events 

      public event CheckedChangedEventHandler CheckedChanged;
      public delegate void CheckedChangedEventHandler(object sender);

      protected override void OnMouseEnter(EventArgs e)
      {

          base.OnMouseEnter(e);
          State = HelperMethods.MouseMode.Hovered;
          Invalidate();
      }

      protected override void OnMouseUp(MouseEventArgs e)
      {

          base.OnMouseUp(e);
          State = HelperMethods.MouseMode.Hovered;
          Invalidate();
      }

      protected override void OnMouseDown(MouseEventArgs e)
      {

          base.OnMouseDown(e);
          State = HelperMethods.MouseMode.Pushed;
          Invalidate();
      }

      protected override void OnMouseLeave(EventArgs e)
      {

          base.OnMouseLeave(e);
          State = HelperMethods.MouseMode.NormalMode;
          Invalidate();
      }

      protected override void OnResize(EventArgs e)
      {
          Height = 20;
          Invalidate();
          base.OnResize(e);
      }

      protected override void OnClick(EventArgs e)
      {
          _Checked = !_Checked;
          if (CheckedChanged != null)
          {
              CheckedChanged(this);
          }
          base.OnClick(e);
      }

      protected override void OnCreateControl()
      {
          base.OnCreateControl();
          BackColor = Color.Transparent;
      }

     #endregion

  }
    
    #endregion

    #region RadioButton

  [DefaultEvent("CheckedChanged")] public class GreenUIRadioButton : Control
  {

      #region Variables

      private bool _Checked;
      private int _Group = 1;
      protected HelperMethods.MouseMode State = HelperMethods.MouseMode.NormalMode;
      private Color _BackColor = Color.WhiteSmoke;
      private Color _ForeColor = Color.Gray;
      private static HelperMethods H = new HelperMethods();
      public event CheckedChangedEventHandler CheckedChanged;
      public delegate void CheckedChangedEventHandler(object sender);

      #endregion

      #region Properties

      protected override void OnTextChanged(EventArgs e)
      {
          base.OnTextChanged(e);
          Invalidate();
      }

      public bool Checked
      {
          get
          {
              return _Checked;
          }
          set
          {
              _Checked = value;
              UpdateCheckState();
              Invalidate();
          }
      }

      public int Group
      {
          get
          {
              return _Group;
          }
          set
          {
              _Group = value;
          }
      }

      #endregion

      #region Initialization

      public GreenUIRadioButton()
      {
          SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint |
                   ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer
                   | ControlStyles.SupportsTransparentBackColor | ControlStyles.UseTextForAccessibility, true);
          DoubleBuffered = true;
          Cursor = Cursors.Hand;
          Size = new Size(200, 20);
          Font = new Font("Proxima Nova", 11, FontStyle.Regular);
          UpdateStyles();
      }

      #endregion

      #region DrawControl

      protected override void OnPaint(PaintEventArgs e)
      {
          //base.OnPaint(e);
          Bitmap B = new Bitmap(Width, Height);
          using (Graphics G = Graphics.FromImage(B))
          {

              G.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
              G.SmoothingMode = SmoothingMode.AntiAlias;

              if (Checked)
              {
                    G.DrawEllipse(H.PenHTMlColor("097A74", 2), 1, 1, 18, 18);
                    G.FillEllipse(H.SolidBrushHTMlColor("097A74"), new Rectangle(4, 4, 12, 12));
                    G.DrawString(Text, Font, H.SolidBrushHTMlColor("097A74"), new Rectangle(23, -1, Width, Height));
              }
              else
              {
                  G.DrawEllipse(new Pen(Color.Silver,2), 1, 1, 18, 18);
                  G.FillEllipse(Brushes.Silver, new Rectangle(4, 4, 12, 12));
                  G.DrawString(Text, Font, Brushes.Gray, new Rectangle(23, -1, Width, Height));
              }
               
              e.Graphics.DrawImage((Image)(B.Clone()), 0, 0);
              G.Dispose();
              B.Dispose();
          }


      }

      #endregion

      #region  Events

      private void UpdateCheckState()
      {
          if (!IsHandleCreated || !_Checked)
              return;

          foreach (Control C in Parent.Controls)
          {
              if (!object.ReferenceEquals(C, this) && C is GreenUIRadioButton && ((GreenUIRadioButton)C).Group == _Group)
              {
                  ((GreenUIRadioButton)C).Checked = false;
              }
          }
          if (CheckedChanged != null)
          {
              CheckedChanged(this);
          }
      }

      protected override void OnMouseDown(MouseEventArgs e)
      {
          if (!_Checked)
              Checked = true;
          base.OnMouseDown(e);
      }

      protected override void OnMouseHover(EventArgs e)
      {
          base.OnMouseHover(e);
          State = HelperMethods.MouseMode.Hovered;
          Invalidate();
      }

      protected override void OnMouseLeave(EventArgs e)
      {
          base.OnMouseLeave(e);
          State = HelperMethods.MouseMode.NormalMode;
          Invalidate();
      }

      protected override void OnResize(EventArgs e)
      {
          base.OnResize(e);
          Height = 21;
          Invalidate();
      }

      protected override void OnCreateControl()
      {
          base.OnCreateControl();
          UpdateCheckState();
      }

      #endregion

  }
    

    #endregion

    #region ProgressBar

  [DefaultEvent("ValueChanged")]  public class GreenUIProgressBar : Control
  {


      #region Variables

      private int _Maximum = 100;
      private int _Value;
      int CurrentValue;
      private static HelperMethods H = new HelperMethods();

      #endregion

      #region Initialization

      public GreenUIProgressBar()
      {
          SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.ResizeRedraw | 
                       ControlStyles.OptimizedDoubleBuffer | ControlStyles.SupportsTransparentBackColor, true);
                    DoubleBuffered = true;
                    UpdateStyles();
          Size = new Size(75, 23);
          CurrentValue = (int)Math.Round((double)(Value / Maximum) * (double)Width);
      }

      #endregion

      #region Properties

      public int Value
      {
          get
          {
              if (_Value < 0)
              {
                  return 0;
              }
              else
              {
                  return _Value;
              }            
          }
          set
          {
              if (value > Maximum)
              {
                  _Value = Maximum;
              }
              _Value = value;
              RenewCurrentValue();
              Invalidate();
              if (ValueChanged != null)
                  ValueChanged(this);
              
          }
      }

      public int Maximum
      {
          get
          {
              return _Maximum;
            
          }
          set
          {
              if (_Maximum < value)
              {
                  _Value =value ;
              }
              _Maximum = value;
              Invalidate();
          }
      }

      #endregion

      #region DrawControl

      protected override void OnPaint(PaintEventArgs e)
      {
          Rectangle Rect = new Rectangle(0, 0, Width, Height);
          Bitmap B = new Bitmap(Width, Height);
          using (Graphics G = Graphics.FromImage(B))
          {
                G.SmoothingMode = SmoothingMode.AntiAlias;
                G.PixelOffsetMode = PixelOffsetMode.HighQuality;
                G.InterpolationMode = InterpolationMode.HighQualityBicubic;
              
                H.FillRoundedPath(G, Color.Silver, Rect, 8);
                H.DrawRoundedPath(G, Color.FromArgb(200, Color.DarkGray), 1, Rect, 8);
              
              if (CurrentValue != 0)
                {
                   H.FillRoundedPath(G, H.GetHTMLColor("097A74"), new Rectangle(Rect.X, Rect.Y, CurrentValue, Rect.Height), 8);
                }

              e.Graphics.DrawImage((Image)(B.Clone()), 0, 0);
              G.Dispose();
              B.Dispose();
          }

      }

      #endregion

      #region Events

      public event ValueChangedEventHandler ValueChanged;
      public delegate void ValueChangedEventHandler(object sender);
      public void RenewCurrentValue()
      {

          CurrentValue = (int)Math.Round((double)(Value) / (double)(Maximum ) * (double)(Width));
      }

      #endregion

  }
    
    #endregion

    #region Textbox
    [DefaultEvent("TextChanged")]
  public class GreenUITextbox : Control
  {

      #region Variables

      private TextBox T = new TextBox();
      private HorizontalAlignment _TextAlign = HorizontalAlignment.Left;
      private int _MaxLength=32767;
      private bool _ReadOnly;
      private bool _UseSystemPasswordChar=false;
      private string _WatermarkText = "";
      private Image _SideImage; 
      private Color TBC = Color.Silver;
      private Color TFC = Color.Gray;
      private bool _Multiline = false;
      private static HelperMethods H = new HelperMethods();

      #endregion

      #region Initialization

      public GreenUITextbox()
      {
          SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | 
                  ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer | 
                  ControlStyles.SupportsTransparentBackColor, true);
          DoubleBuffered = true;
          UpdateStyles();
          Text = WatermarkText;
          Size = new Size(135, 30);
          Font = new Font("Proxima Nova", 11, FontStyle.Regular);
            T.Multiline = _Multiline;
            T.Cursor = Cursors.IBeam;
            T.BackColor = TBC;
            T.ForeColor = Color.Gray;
            T.Text = WatermarkText;
            T.BorderStyle = BorderStyle.None;
            T.Location = new Point(7, 7);
            T.Font = Font;
            T.Size = new Size(Width - 10, 30);
            T.UseSystemPasswordChar = _UseSystemPasswordChar;
            T.TextChanged += T_TextChanged;
            T.Leave += T_Leave;
            T.Enter+=T_Enter;
            T.KeyDown += T_KeyDown;
       }

      #endregion

      #region Properties

      [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
      public BorderStyle BorderStyle
      {
          get
          {
              return BorderStyle.None;
          }
      }

      [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
      public bool Multiline
      {
          get
          {
              return _Multiline;
          }
          set
          {
              _Multiline = value;
              if (T != null)
              {
                  T.Multiline = value;
              }

          }
      }

      public HorizontalAlignment TextAlign
      {
          get          
          {              
              return _TextAlign;          
          }
         set
          {
              _TextAlign=value;
              if(T !=null)
              { 
                  T.TextAlign = value;
              }
          }
      }

      public int MaxLength
      {
          get
          {
              return _MaxLength;
          }
          set
          {
              _MaxLength = value;
              if (T != null)
              {
                  T.MaxLength = value;
              }
          }
      }

      public bool ReadOnly
      {
          get
          {
              return _ReadOnly;
          }
          set
          {
              _ReadOnly = value;
              if (T != null)
              {
                  T.ReadOnly = value;
              }
          }
      }

      public bool UseSystemPasswordChar
      {
          get
          {
              return _UseSystemPasswordChar;
          }
          set
          {
              _UseSystemPasswordChar = value;
              if (T != null)
              {
                  T.UseSystemPasswordChar = value;
              }
          }
      }

      public string WatermarkText
      {
          get
          {
              return _WatermarkText;
          }
          set
          {
              _WatermarkText = value;
             
          }
      }

      public Image SideImage
      {
          get
          {
              return _SideImage;
          }
          set
          {
              _SideImage = value;
              Invalidate();
          }
      }

      [Browsable(false)]
      public override Image BackgroundImage
      {
          get
          {
              return base.BackgroundImage;
          }
          set
          {
              base.BackgroundImage = value;
          }
      }

      [Browsable(false)]
      public override ImageLayout BackgroundImageLayout
      {
          get
          {
              return base.BackgroundImageLayout;
          }
          set
          {
              base.BackgroundImageLayout = value;
          }
      }

      public override string Text
      {
          get
          {
              return base.Text;
          }
          set
          {
              base.Text = value;
          }
      }

      #endregion

      #region DrawControl

      protected override void OnPaint(PaintEventArgs e)
      {
    
          Rectangle Rect = new Rectangle(0, 0, Width-1, Height-1);
          Bitmap B = new Bitmap(Width, Height);
          using (Graphics G = Graphics.FromImage(B))
          {
              Height = 30;
                G.SmoothingMode = SmoothingMode.HighQuality;
                G.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                G.InterpolationMode = InterpolationMode.HighQualityBicubic;

                H.FillRoundedPath(G, TBC, Rect, 10);
                H.DrawRoundedPath(G, Color.FromArgb(200, Color.DarkGray), 1, new Rectangle(0, 0, Width - 1, Height - 1), 10);
            
              if ( SideImage != null)
              {
                    T.Location = new Point(33, Convert.ToInt32(4.5));
                    T.Width = Width - 65;
                    G.DrawImage(SideImage, new Rectangle(8, 7, 16, 16));
              }               
              else
              {
                  T.Location = new Point(7, Convert.ToInt32(4.5));
                    T.Width = Width - 10;
              }

                if (ContextMenuStrip != null) { T.ContextMenuStrip = ContextMenuStrip; }

              e.Graphics.DrawImage((Image)(B.Clone()), 0, 0);
              G.Dispose();
              B.Dispose();
          }

      }

      #endregion

      #region Events

      private void T_Leave(object sender,EventArgs e)
      {
          if (Text.Length == 0)
          {
            Text = WatermarkText;
            T.ForeColor = TFC;
          }
      }

      private void T_Enter(object sender, EventArgs e)
      {
          if (Text == WatermarkText)
          {
              Text = "";
              T.ForeColor = TFC;
          }
      }

      private void T_TextChanged(object sender, EventArgs e)
      {
          Text = T.Text;
      }

      protected override void OnTextChanged(EventArgs e)
      {
          base.OnTextChanged(e);
          T.Text = Text;
      }

      private void T_KeyDown(object sender, KeyEventArgs e)
      {
          if (e.Control && e.KeyCode == Keys.A) { e.SuppressKeyPress = true; }
          if(e.Control && e.KeyCode==Keys.C)
          {
                 T.Copy();
                 e.SuppressKeyPress = true;
          }
      }

      protected override void OnCreateControl()
      {
          base.OnCreateControl();
           if (!Controls.Contains(T))
           Controls.Add(T);
        if (T.Text == "" && WatermarkText != "") 
          
              T.Text = WatermarkText;
        
      }

      protected override void OnResize(EventArgs e)
      {
          base.OnResize(e);
          if (_Multiline == false)
              Height = 30;
      }

      #endregion


  }
    
    #endregion

    #region Close
    public class GreenUIClose : Control
  {


      #region Variables

      private string img = "iVBORw0KGgoAAAANSUhEUgAAACQAAAAkCAYAAADhAJiYAAAACXBIWXMAAAsTAAALEwEAmpwYAAAAIGNIUk0AAHolAACAgwAA+f8AAIDpAAB1MAAA6mAAADqYAAAXb5JfxUYAAAVDSURBVHjarJdPaxtHGMZ/78yuZlerXAyhBh/r1r0EArnmYOpG1EkoKeTUQD9AIZ+h0E8QWhoaSqFQ0kN7aqGkuAh88FVQMBQEuZoYDL5Uf3YlzUwPs7IlW5JXbhaGHLwZ/fZ53nnfZ0Tf/4gFTwN4CXwJ/M3bezaBb4DPgZPxH3/N/FEhMGc1RKlXEulPRKuWiNxe8N5KS0Q2Rat9ifTHolQL4eZFWjVPGVHqFaZ2l6wOSbKGVi1B7vwfWQTZRKt9kmQj7GtuiVKt6OG9m8uAZmCk0UAa2QRq77pQgmwFGLMhjbAvWR3MZSg1pWlDlH6FMXepZ0hahziGJCmh0jW0LqGq+xRgdIsk2ZCsASaFOEbSFBoNMMktUboVPWzeBBD94N6sMvV6eDnS55/oHBQFvtuHPD/F2qaH9tXKUMKYDckyMAb0lCnW4vMCuj0oikPv3I5W77/bED1lUz2FWM9+qBLQGtEKvE9x7rF43wLeLISREiZNNoLCBiJ1YV8V9tQKnHtHnLulRKuX1EqYi8rMVJsCY5DGpND1ngh3FsKoEibUyqwy04/WSGIgqx9hzFNFzXxFlp0GmGh5TSgNZrqmoj2R2ZoSkS1UtB9gMjAJaH3VvkdSM9ujX357rajFbTGmidanlY7MEqVKZfZJzfrcmpn/HOH8zvDFj6/DKbMW7LgNvolwWunwaAWJQW7UIU3WiPSeKPkMrfepm/WFNXN5HeHczvD5D50JXUQxxItCRNoY00SpPWDtaqVK+xA8ssZo9JI4ItRMUk0Z63aG357DBCBrIc/xAXpFqNI+BD8aIXEMtVplGIq8c2lLX/YD8hzf68OwaOPdivbVypqqVbaJYd7xvT5zZ1mAcjDIQ/Mrhm2cb4KcVurISoOOwr/L3z3G+h2KouO7A8gLFg5Xj5+C6kFRtHGuCZy+pdhxjHXbFHmn7Pjh95ZNe48PYyLP8b0SahX7Fq9jnNs+s6nIwbnwe5eBZv+n9xP7ilBT+Yr2zVvWP52xybrwOwhV8lAJZYN9vR7kRRvnvriWUWEwv/Dd/u1gky1hFhzcRR/lAVx5+vr9LYbDZ3i3ukB4vB2vMR61cO7OmTAyVyCWnlHvBazbYjja96PxOpd2q5Jb1aQ/lWPmYp6qYNns1Fb7xPG6xBGIrG7ZpHlm2ZUpYallIc+oFvV0XRpTEeI6da0VpAa5kYXZN4Gab9miDKxbpGmIEEl6dYSo0jyTJOTptIwuczK6WhDIW2d5Jqk0KKvbl5iQp0ql4kcPZqCUBHvKJZtEqiX1EDslTZBIMfvOpXUswhMRTq94LyytkNI+qYfoEn96DjX16eHeJEmyQZYh1ZQJHdjan1caM0qF2Bo+OihVQkn06H643modYBorwFi3zbDo+NEYiSMw5k7l6FI2zcmtww8Gp1jb1OqD964Dc4R1H/o879DtwSB0YJR6I1q3EHkMpFffIAXROijmfYq16xFaP5d0ZZgdX+Qdej38IAxK7BgBvNCWZIWQV9aUF38guCdKEvOERvbPSjClMgHGhuFnLX4wCPLnK0YXkQOJot3hT792FXF8InG8jdaHV/SSEqbo0O3jB0VQxk+Gn4QpPshLqMop4QDrdouvv++GUzYc4YvhCWO7AxwuVsaWMNPKXIwJ5fV4kEO3W0WpA8bj3eLZd93zTD0soNfDF/kJ1u4gHF7OwHbbF0WombyEWTRnz+7sOfS6+MUh7wA7C1Pey8qj928Xn+cXlTrC2m2fF6/PlLG2WlceL1XqgPF41/cH3fmjw82B8v7Pa8Msg/L+9zOYObeO/wYAIlO8U9aIgCsAAAAASUVORK5CYII=";
      private static HelperMethods H = new HelperMethods();

      #endregion

      #region Initialization

      public GreenUIClose()
      {
          SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint |
          ControlStyles.ResizeRedraw | ControlStyles.UserPaint |
          ControlStyles.SupportsTransparentBackColor, true);
          DoubleBuffered = true;
          UpdateStyles();
          BackColor = Color.Transparent;
          Size = new Size(22, 22);
      }

      #endregion

      #region DrawControl

      protected override void OnPaint(PaintEventArgs e)
      {
          base.OnPaint(e);
          Rectangle Rect = new Rectangle(0, 0, 22, 22);
          Bitmap B = new Bitmap(Width, Height);
          using (Graphics G = Graphics.FromImage(B))
          {
              G.Clear(Color.Transparent);
              H.DrawImageFromBase64(G, img, Rect);
              e.Graphics.DrawImage((Image)(B.Clone()), 0, 0);
              G.Dispose();
              B.Dispose();
          }

      }

      #endregion

      #region Events

      protected override void OnResize(EventArgs e)
      {
          base.OnResize(e);
          Size = new Size(22, 22);
      }

      protected override void OnClick(EventArgs e)
      {
          base.OnClick(e);
          Environment.Exit(0);
          Application.Exit();
      }

      #endregion

  }
    
    #endregion

    #region Seperator

    public class GreenUISeperator : Control
    {

        #region Variables

        private Style _SepStyle = Style.Horizental;
        private static HelperMethods H = new HelperMethods();

        #endregion

        #region Initialization

        public GreenUISeperator()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint |
            ControlStyles.ResizeRedraw | ControlStyles.UserPaint |
            ControlStyles.SupportsTransparentBackColor, true);
            DoubleBuffered = true;
            UpdateStyles();
            BackColor = Color.Transparent;
            ForeColor = H.GetHTMLColor("097A74");
        }

        #endregion

        #region Enumerators

        public enum Style
        {
            Horizental,
            Vertiacal
        };

        #endregion

        #region Properties

        public Style SepStyle
        {
            get
            {
                return _SepStyle;
            }
            set
            {
                _SepStyle = value;
                if (value == Style.Horizental)
                {
                    Height = 4;
                }
                else
                {
                    Width = 4;
                }
                Invalidate();
            }
        }

        #endregion

        #region DrawControl

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Bitmap B = new Bitmap(Width, Height);
            using (Graphics G = Graphics.FromImage(B))
            {

            G.SmoothingMode = SmoothingMode.AntiAlias;
            ColorBlend BL1 = new ColorBlend();
            BL1.Positions = new Single[] {0.0F, 0.15F, 0.85F, 1.0F};
            BL1.Colors = new Color[] { Color.Transparent, ForeColor, ForeColor, Color.Transparent };
            switch(SepStyle)
            {
                case Style.Horizental:
                    using (LinearGradientBrush lb1 =new LinearGradientBrush(ClientRectangle, Color.Empty, Color.Empty, 0.0F))
                    { 
                    lb1.InterpolationColors = BL1;
                    G.DrawLine(new Pen(lb1), 0, (int)0.7, Width, (int)0.7);
                    }
                    break;
                case Style.Vertiacal:
                    using (LinearGradientBrush lb1 = new LinearGradientBrush(ClientRectangle, Color.Empty, Color.Empty, 90.0F))
                    {
                        lb1.InterpolationColors = BL1;
                        G.DrawLine(new Pen(lb1), (int)0.7, 0, (int)0.7, Height);
                    }
                    break;
            }

                e.Graphics.DrawImage((Image)(B.Clone()), 0, 0);
                G.Dispose();
                B.Dispose();
            }

        }

        #endregion
               
    }

#endregion

    #region ComboBox

    public class GreenUIComboBox : ComboBox
    {

        #region Variables

        private int _StartIndex = 0;
        private static HelperMethods H = new HelperMethods();

        #endregion

        #region Constructors

        public GreenUIComboBox()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint |  ControlStyles.UserPaint |
                  ControlStyles.OptimizedDoubleBuffer | ControlStyles.SupportsTransparentBackColor, true);
            DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            DoubleBuffered = true;
            StartIndex = 0;
            DropDownHeight = 100;
            BackColor = Color.Transparent;
            Font = new Font("Proxima Nova", 12, FontStyle.Regular);
            DropDownStyle = ComboBoxStyle.DropDownList;
            UpdateStyles();

        }

        #endregion

        #region Properties

        public int StartIndex
        {
            get
            {
                return _StartIndex;
            }
            set
            {
                _StartIndex = value;
                try
                {
                    base.SelectedIndex = value;
                }
                catch
                {

                }
                Invalidate();
            }
        }

        #endregion

        #region Draw Control

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics G = e.Graphics;

                Rectangle Rect = new Rectangle(0, 0, Width - 1, Height - 1);

                G.SmoothingMode = SmoothingMode.AntiAlias;
                G.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

                H.FillRoundedPath(e.Graphics, Color.Silver, Rect, 8);
                H.FillRoundedPath(e.Graphics, Color.Gainsboro, new Rectangle(Width - 28, 0, Width - 1, Height - 1), 10, false, true, false, true);
                H.DrawRoundedPath(e.Graphics, Color.FromArgb(200, Color.DarkGray), 1, Rect, 8);

                G.DrawLine(new Pen(Color.DarkGray, 1), new Point(Width - 21, (Height / 2) - Convert.ToInt32(4.5)), new Point(Width - 7, (Height / 2) - Convert.ToInt32(4.5)));
                G.DrawLine(new Pen(Color.DarkGray, 1), new Point(Width - 21, (Height / 2) + Convert.ToInt32(0.5)), new Point(Width - 7, (Height / 2) + Convert.ToInt32(0.5)));
                G.DrawLine(new Pen(Color.DarkGray, 1), new Point(Width - 21, (Height / 2) + Convert.ToInt32(3.5)), new Point(Width - 7, (Height / 2) + Convert.ToInt32(3.5)));
                G.DrawLine(new Pen(Color.DarkGray, 1), new Point(Width - 28, 1), new Point(Width - 28, Height - 1));
                G.DrawString(Text, Font, Brushes.Gray, new Rectangle(5, 1, Width - 1, Height - 1), new StringFormat { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Near });

        }

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            try
            {
                e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
                e.Graphics.FillRectangle(Brushes.Silver, e.Bounds);
                Cursor = Cursors.Hand;
                if (System.Convert.ToInt32((e.State & DrawItemState.Selected)) == (int)DrawItemState.Selected)
                {
                    if (!(e.Index == -1))
                    {
                        e.Graphics.DrawString(GetItemText(Items[e.Index]), Font, H.SolidBrushHTMlColor("097A74"), new Rectangle(e.Bounds.X + 1, e.Bounds.Y + 3, e.Bounds.Width - 2, e.Bounds.Height - 2));
                    }
                }
                else
                {
                    if (!(e.Index == -1))
                    {
                        e.Graphics.DrawString(GetItemText(Items[e.Index]), Font, Brushes.Gray, new Rectangle(e.Bounds.X + 1, e.Bounds.Y + 3, e.Bounds.Width - 2, e.Bounds.Height - 2));
                    }
                }

            }
            catch
            {

            }
            Invalidate();
        }

        #endregion

        #region Events

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Invalidate();
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();

        }

        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);
            SuspendLayout();
            Update();
            ResumeLayout();
        }

        #endregion

    }

    #endregion

    #region TabControl

    public class GreenUITabControl : TabControl
    {

        #region Variables

        private static HelperMethods H = new HelperMethods();

        #endregion

        #region Draw Control

        protected override void OnPaint(PaintEventArgs e)
        {

            Graphics G = e.Graphics;

            Cursor = Cursors.Hand;

            G.Clear(Color.Gainsboro);
            Cursor = Cursors.Hand;

            G.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

            G.FillRectangle(Brushes.Silver, new Rectangle(0, 0, Width, ItemSize.Height + 2));
            G.DrawRectangle(new Pen(Color.FromArgb(200, Color.DarkGray), 1), new Rectangle(0, 0, Width - 1, Height - 1));
            G.DrawLine(new Pen(Color.FromArgb(200, Color.DarkGray), 1), new Point(1, ItemSize.Height + 2), new Point(Width - 1, ItemSize.Height + 2));

            for (int i = 0; i <= TabCount - 1; i++)
            {
               Rectangle R = GetTabRect(i);
                if (i == SelectedIndex)
                {
                    G.DrawString(TabPages[i].Text,Font, H.SolidBrushHTMlColor("097A74"), R.X + 35, R.Y + 15, new StringFormat() { Alignment = StringAlignment.Center });
                }
                else
                {
                    G.DrawString(TabPages[i].Text, Font, Brushes.DarkGray, R.X + 35, R.Y + 15, new StringFormat() {Alignment = StringAlignment.Center});
                }
             }

        }

        #endregion

        #region Constructors

        public GreenUITabControl()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.SupportsTransparentBackColor, true);
            DoubleBuffered = true;
            Alignment = TabAlignment.Top;
            SizeMode = TabSizeMode.Fixed;
            ItemSize = new Size(80, 45);
            Dock = DockStyle.None;
            Font = new Font("Proxima Nova Rg", 10, FontStyle.Bold);
            UpdateStyles();

        }

        #endregion

        #region Events

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            foreach (TabPage Tab in base.TabPages)
            {
                Tab.BackColor = Color.Gainsboro;
            }
        }

        #endregion

    }

    #endregion