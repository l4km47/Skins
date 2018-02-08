    /// <summary>
/// Ethereal Theme
/// Author : THE LORD
/// Credits : Aeonhack(Round Rectangle Function)
/// Credits : Mavamaarten(TabPage Mouse Hover & Mouse Leave Event)
/// Release Date : Tuesday, December 27, 2016
/// Last Update : Monday, January 9, 2017
/// Update Purpose : Customizable Colors
/// </summary>

    #region Namespaces
 
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

#endregion

    #region Helper Methods

    public class HelperMethods
    {

        public static System.Drawing.Drawing2D.GraphicsPath GP = null;

        public enum MouseMode
        {
            NormalMode,
            Hovered,
            Pushed
        };

        public static void DrawImageFromBase64(Graphics G, string Base64Image, Rectangle Rect)
        {
            Image IM = null;
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream(Convert.FromBase64String(Base64Image)))
            {
                IM = System.Drawing.Image.FromStream(ms);
            }
            G.DrawImage(IM, Rect);
        }

        public static System.Drawing.Drawing2D.GraphicsPath RoundRec(Rectangle r, int Curve)
        {
            System.Drawing.Drawing2D.GraphicsPath CreateRoundPath = new System.Drawing.Drawing2D.GraphicsPath(System.Drawing.Drawing2D.FillMode.Winding);
            CreateRoundPath.AddArc(r.X, r.Y, Curve, Curve, 180f, 90f);
            CreateRoundPath.AddArc(r.Right - Curve, r.Y, Curve, Curve, 270f, 90f);
            CreateRoundPath.AddArc(r.Right - Curve, r.Bottom - Curve, Curve, Curve, 0f, 90f);
            CreateRoundPath.AddArc(r.X, r.Bottom - Curve, Curve, Curve, 90f, 90f);
            CreateRoundPath.CloseFigure();
            return CreateRoundPath;
        }

        public static void FillRoundedPath(Graphics G, Color C, Rectangle Rect, int Curve)
        {
            G.FillPath(new SolidBrush(C), RoundRec(Rect, Curve));
        }

        public static void FillRoundedPath(Graphics G, Brush B, Rectangle Rect, int Curve)
        {
            G.FillPath(B, RoundRec(Rect, Curve));
        }
        
        public static void DrawRoundedPath(Graphics G, Color C, Single Size, Rectangle Rect, int Curve)
        {
            G.DrawPath(new Pen(C, Size), RoundRec(Rect, Curve));

        }

        public static void DrawTriangle(Graphics G, Color C, Single Size, Point P1_0, Point P1_1, Point P2_0, Point P2_1, Point P3_0, Point P3_1)
        {

            G.DrawLine(new Pen(C, Size), P1_0, P1_1);
            G.DrawLine(new Pen(C, Size), P2_0, P2_1);
            G.DrawLine(new Pen(C, Size), P3_0, P3_1);

        }

        public static Pen PenRGBColor(int R, int G, int B, Single size)
        { return new Pen(System.Drawing.Color.FromArgb(R, G, B), size); }

        public static Pen PenHTMlColor(String C_WithoutHash, Single size)
        { return new Pen(GetHTMLColor(C_WithoutHash), size); }

        public static SolidBrush SolidBrushRGBColor(int R, int G, int B, int A = 0)
        { return new SolidBrush(System.Drawing.Color.FromArgb(A, R, G, B)); }

        public static SolidBrush SolidBrushHTMlColor(String C_WithoutHash)
        { return new SolidBrush(GetHTMLColor(C_WithoutHash)); }

        public static Color GetHTMLColor(String C_WithoutHash)
        { return ColorTranslator.FromHtml("#" + C_WithoutHash); }

        public static String ColorToHTML(Color C)
        { return ColorTranslator.ToHtml(C); }

        public static Color SetARGB(int A, int R, int G, int B)
        { return System.Drawing.Color.FromArgb(A, R, G, B); }

        public static Color SetRGB(int R, int G, int B)
        { return System.Drawing.Color.FromArgb(R, G, B); }

        public static Font FontByName(String NameOfFont, Single size, FontStyle Style)
        { return new Font(NameOfFont, size, Style); }

        public static void CentreString(Graphics G, String Text, Font font, Brush brush, Rectangle Rect)
        { G.DrawString(Text, font, brush, new Rectangle(0, Rect.Y + Convert.ToInt32(Rect.Height / 2) - Convert.ToInt32(G.MeasureString(Text, font).Height / 2) + 0, Rect.Width, Rect.Height), new StringFormat() { Alignment = StringAlignment.Center }); }

        public static void LeftString(Graphics G, String Text, Font font, Brush brush, Rectangle Rect)
        {
            G.DrawString(Text, font, brush, new Rectangle(4, Rect.Y + Convert.ToInt32(Rect.Height / 2) - Convert.ToInt32(G.MeasureString(Text, font).Height / 2) + 0, Rect.Width, Rect.Height), new StringFormat { Alignment = StringAlignment.Near });
        }

        public static void FillRect(Graphics G, Brush Br, Rectangle Rect)
        { G.FillRectangle(Br, Rect); }

    }

    #endregion

    #region Skin

    public class EtherealTheme : ContainerControl
    {

        #region    Variables

        bool Movable = false;
        private TitlePostion _TitleTextPostion = TitlePostion.Left;
        static Point MousePoint = new Point(0, 0);
        private int MoveHeight = 50;
        private bool _ShowIcon = false;
        private Color _HeaderColor = HelperMethods.GetHTMLColor("3f2153");
        private Color _BackColor = Color.White;
        private Color _BorderColor = HelperMethods.GetHTMLColor("3f2153");

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
            get { return _TitleTextPostion; }

            set
            {
                _TitleTextPostion = value;
                Invalidate();
            }

        }


        public enum TitlePostion
        {
            Left,
            Center
        };

        public Color HeaderColor
        {
            get
            {
                return _HeaderColor;
            }
            set
            {
                _HeaderColor = value;
                Invalidate();
            }
        }

        override public Color BackColor
        {
            get
            {
                return _BackColor;
            }
            set
            {
                _BackColor = value;
                base.BackColor = value;
                Invalidate();
            }
        }

        public Color BorderColor
        {
            get
            {
                return _BorderColor;
            }
            set
            {
                _BorderColor = value;
                Invalidate();
            }
        }

        #endregion

        #region  Initialization

        public EtherealTheme()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor, true);
            DoubleBuffered = true;
            UpdateStyles();
            Font = new Font("Proxima Nova", 14, FontStyle.Bold);            

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
            base.OnPaint(e);
            Rectangle Rect = new Rectangle(1, 1, (int)(Width - 2.5), (int)(Height - 2.5));
            Bitmap B = new Bitmap(Width, Height);
            using (Graphics G = Graphics.FromImage(B))
            {
                G.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

                HelperMethods.FillRect(G, new SolidBrush(HeaderColor), new Rectangle(0, 0, Width, 50));
                G.DrawRectangle(new Pen(BorderColor, 2), new Rectangle(1, 1, Width - 2, Height - 2));
               

                if (FindForm().ShowIcon==true)
                {
                    G.DrawIcon(FindForm().Icon, new Rectangle(5, 13, 20, 20));
                    switch (TitleTextPostion)
                    {
                        case TitlePostion.Left:
                            G.DrawString(Text, Font, Brushes.White, 27, 10);
                            break;
                        case TitlePostion.Center:
                            HelperMethods.CentreString(G, Text, Font, Brushes.White, new Rectangle(0, 0, Width, 50));
                            break;
                    }
                }
                else
                {
                    switch (TitleTextPostion)
                    {
                        case TitlePostion.Left:
                            G.DrawString(Text, Font, Brushes.White, 5, 10);
                            break;
                        case TitlePostion.Center:
                            HelperMethods.CentreString(G, Text, Font, Brushes.White, new Rectangle(0, 0, Width, 50));
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

    #region TabControl

    public class EtherealTabControl : TabControl
    {

        #region  Variables 

        private MouseState State;
        private Color _TabsColor = HelperMethods.GetHTMLColor("432e58");
        private Color _SeletedTabTriangleColor = Color.White;
        private Color _LeftColor = HelperMethods.GetHTMLColor("4e3a62");
        private Color _RightColor = Color.White;
        private Color _LineColor = HelperMethods.GetHTMLColor("3b2551");
        private Color _NoneSelectedTabColors = HelperMethods.GetHTMLColor("432e58");
        private Color _HoverColor = HelperMethods.GetHTMLColor("3b2551");
        private Color _TextColor = Color.White;
        private Color _TabPageColor = Color.White;

        #endregion 

        #region  Stractures 

        public struct MouseState
        {
            public bool Hover;
            public Point Coordinates;
        };

        #endregion

        #region  Initialization 

        public EtherealTabControl()
        {

            SetStyle(ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.SupportsTransparentBackColor, true);
            DoubleBuffered = true;
            SizeMode = TabSizeMode.Fixed;
            Dock = DockStyle.None;
            ItemSize = new Size(40, 150);
            Alignment = TabAlignment.Left;
           
        }

        #endregion

        #region Properties

        public Color TabsColor
        {
            get
            {
                return _TabsColor;
            }
            set
            {
                _TabsColor = value;
                Invalidate();
            }
        }

        public Color SeletedTabTriangleColor
        {
            get
            {
                return _SeletedTabTriangleColor;
            }
            set
            {
                _SeletedTabTriangleColor = value;
                Invalidate();
            }
        }

        public Color LeftColor
        {
            get
            {
                return _LeftColor;
            }
            set
            {
                _LeftColor = value;
                Invalidate();
            }
        }

        public Color RightColor
        {
            get
            {
                return _RightColor;
            }
            set
            {
                _RightColor = value;
                Invalidate();
            }
        }

        public Color LineColor
        {
            get
            {
                return _LineColor;
            }
            set
            {
                _LineColor = value;
                Invalidate();
            }
        }

        public Color NoneSelectedTabColors
        {
            get
            {
                return _NoneSelectedTabColors;
            }
            set
            {
                _NoneSelectedTabColors = value;
                Invalidate();
            }
        }

        public Color HoverColor
        {
            get
            {
                return _HoverColor;
            }
            set
            {
                _HoverColor = value;
                Invalidate();
            }
        }

        public Color TextColor
        {
            get
            {
                return _TextColor;
            }
            set
            {
                _TextColor = value;
                Invalidate();
            }
        }

        public Color TabPageColor
        {
            get
            {
                return _TabPageColor;
            }
            set
            {
                _TabPageColor = value;
                Invalidate();
            }
        }

        #endregion

        #region  Events

        protected override void OnMouseEnter(EventArgs e)
        {
            State.Hover = true;
            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            State.Hover = false;
            foreach (TabPage Tab in base.TabPages)
            {
                if (Tab.DisplayRectangle.Contains(State.Coordinates))
                {
                    base.Invalidate();
                    break;
                }


            }
            base.OnMouseLeave(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            State.Coordinates = e.Location;
            foreach (TabPage Tab in base.TabPages)
            {
                if (Tab.DisplayRectangle.Contains(e.Location))
                {
                    base.Invalidate();
                    break;
                }

            }
            base.OnMouseMove(e);
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            foreach(TabPage T in TabPages)
            {
                T.BackColor = TabPageColor;
            }
        }

        #endregion

        #region  DrawControl 

        protected override void OnPaint(PaintEventArgs e)
        {
            Bitmap B = new Bitmap(Width, Height);
            using (Graphics G = Graphics.FromImage(B))
            {

                G.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

                G.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

                HelperMethods.FillRect(G, new SolidBrush(LeftColor), new Rectangle(0, 1, 150, Height));
            
                for (int i = 0; i <= TabPages.Count - 1; i++)
                {
                    Rectangle R = GetTabRect(i);
                    HelperMethods.FillRect(G, new SolidBrush(NoneSelectedTabColors), new Rectangle(R.X - 1, R.Y - 1, R.Width - 3, R.Height));
                    if (i == SelectedIndex)
                    {
                        G.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                        Point P1 = new Point(ItemSize.Height - 12, R.Location.Y + 20);
                        Point P2 = new Point(ItemSize.Height + 2, R.Location.Y + 10);
                        Point P3 = new Point(ItemSize.Height + 2, R.Location.Y + 30);
                        G.FillPolygon(new SolidBrush(SeletedTabTriangleColor), new Point[] { P1, P2, P3 });

                    }
                    else
                    {
                        if (State.Hover & R.Contains(State.Coordinates))
                        {
                            Cursor = Cursors.Hand;

                            HelperMethods.FillRect(G, new SolidBrush(HoverColor), new Rectangle(R.X, R.Y, R.Width - 3, R.Height));
                         
                        }
                       
                    }

                    G.DrawString(TabPages[i].Text, new Font("Segoe UI", 8, FontStyle.Bold), new SolidBrush(TextColor), R.X + 28, R.Y + 13);


                    if (!(ImageList == null))
                    { G.DrawImage(ImageList.Images[i], new Rectangle(R.X + 6, R.Y + 11, 16, 16)); }

                    G.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.Default;

                    G.DrawLine(new Pen(LineColor, 1), new Point(R.X - 1, R.Bottom - 2), new Point(R.Width - 2, R.Bottom - 2));

                }
                G.FillRectangle(new SolidBrush(RightColor), new Rectangle(150, Convert.ToInt32(1.3), Width, Height - 2));
                G.DrawRectangle(new Pen(LineColor, 1), new Rectangle(0, 0, Width - 1, Height - 1));

                e.Graphics.DrawImage((Image)B.Clone(), 0, 0);
                G.Dispose();
                B.Dispose();

            }
        }

        #endregion

    }

#endregion

    #region Button

    public class EtherealButton : Control 
    {
        #region   Variables 

    private HelperMethods.MouseMode State;
    private Style _ButtonStyle ;
    private Color NoneColor = HelperMethods.GetHTMLColor("222222");
    private int _RoundRadius = 5;

       #endregion

        #region  Properties 

        public int RoundRadius
        {
            get
            {
            return _RoundRadius;  
            }
            set
            {
            _RoundRadius=value;
            Invalidate();
            }
        }
  
        public Style ButtonStyle
        {
            get
            {
                return _ButtonStyle;
            }
            set
            {
                _ButtonStyle = value;
                Invalidate();
            }
        }

         #endregion

        #region  Initialization 

    public EtherealButton()
    {
        SetStyle(ControlStyles.DoubleBuffer | ControlStyles.AllPaintingInWmPaint |
        ControlStyles.ResizeRedraw | ControlStyles.UserPaint |
        ControlStyles.Selectable | ControlStyles.SupportsTransparentBackColor, true);
        DoubleBuffered = true;
        BackColor = Color.Transparent;
        Font = new Font("Segoe UI", 9, FontStyle.Bold);
    }

       #endregion

        #region  Enumerators 

    public enum Style
    {
        Clear,
        DarkClear,
        SemiBlack,
        DarkPink,
        LightPink
    };

        #endregion

        #region  Events

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

        #region  Draw Control 

     protected override void OnPaint(PaintEventArgs e)
        {
            Bitmap B = new Bitmap(Width, Height);
            using (Graphics G = Graphics.FromImage(B))
            {
Rectangle Rect=new Rectangle(0, 0, Width - 1, Height - 1);
System.Drawing.Drawing2D.GraphicsPath GP = HelperMethods.RoundRec(Rect, RoundRadius);
                G.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                G.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

                switch(State)
                {
                    case HelperMethods.MouseMode.NormalMode:
                        switch(ButtonStyle)
                        {
                            case Style.Clear:
                                NoneColor = HelperMethods.GetHTMLColor("ececec");
                                HelperMethods.DrawRoundedPath(G, NoneColor, 1, Rect, RoundRadius);
                                HelperMethods.CentreString(G, Text, Font, HelperMethods.SolidBrushHTMlColor("b9b9b9"), Rect);
                                break;
                            case Style.DarkClear:
                                NoneColor = HelperMethods.GetHTMLColor("444444");
                                HelperMethods.DrawRoundedPath(G, NoneColor, 1, Rect, RoundRadius);
                                HelperMethods.CentreString(G, Text, Font, HelperMethods.SolidBrushHTMlColor("444444"), Rect);
                                break;
                            case Style.SemiBlack:
                                HelperMethods.FillRoundedPath(G, NoneColor, Rect, RoundRadius);
                                HelperMethods.DrawRoundedPath(G, HelperMethods.GetHTMLColor("121212"), 1, Rect, RoundRadius);
                                HelperMethods.CentreString(G, Text, Font, Brushes.White, Rect);
                                break;
                            case Style.DarkPink:
                                NoneColor = HelperMethods.GetHTMLColor("3b2551");
                                HelperMethods.FillRoundedPath(G, NoneColor, Rect, RoundRadius);
                                HelperMethods.DrawRoundedPath(G, HelperMethods.GetHTMLColor("6d5980"), 1, Rect, RoundRadius);
                                HelperMethods.CentreString(G, Text, Font, Brushes.White, Rect);
                                break;
                            case Style.LightPink:
                                NoneColor = HelperMethods.GetHTMLColor("9d92a8");
                                HelperMethods.FillRoundedPath(G, NoneColor, Rect, RoundRadius);
                                HelperMethods.DrawRoundedPath(G, HelperMethods.GetHTMLColor("573d71"), 1, Rect, RoundRadius);
                                HelperMethods.CentreString(G, Text, Font, Brushes.White, Rect);
                                break;
                }
                        break;
                    case HelperMethods.MouseMode.Hovered:
                        NoneColor = HelperMethods.GetHTMLColor("444444");
                           switch(ButtonStyle)
                        {
                               case Style.Clear:
                                NoneColor = HelperMethods.GetHTMLColor("444444");
                                HelperMethods.DrawRoundedPath(G, NoneColor, 1, Rect, RoundRadius);
                                HelperMethods.CentreString(G, Text, Font, HelperMethods.SolidBrushHTMlColor("444444"), Rect);
                                break;
                               case Style.DarkClear:
                                NoneColor = HelperMethods.GetHTMLColor("ececec");
                                HelperMethods.DrawRoundedPath(G, NoneColor, 1, Rect, RoundRadius);
                                HelperMethods.CentreString(G, Text, Font, HelperMethods.SolidBrushHTMlColor("b9b9b9"), Rect);
                                break;
                               case Style.SemiBlack:
                                NoneColor = HelperMethods.GetHTMLColor("444444");
                                     HelperMethods.FillRect(G, new SolidBrush(Color.Transparent), Rect);
                                HelperMethods.DrawRoundedPath(G, NoneColor, 1, Rect, RoundRadius);
                                HelperMethods.CentreString(G, Text, Font, HelperMethods.SolidBrushHTMlColor("444444"), Rect);
                                break;
                               case Style.DarkPink:
                                NoneColor = HelperMethods.GetHTMLColor("444444");
                                HelperMethods.FillRect(G, new SolidBrush(Color.Transparent), Rect);
                                HelperMethods.DrawRoundedPath(G, NoneColor, 1, Rect, RoundRadius);
                                HelperMethods.CentreString(G, Text, Font, HelperMethods.SolidBrushHTMlColor("444444"), Rect);
                                break;
                               case Style.LightPink:
                                NoneColor = HelperMethods.GetHTMLColor("9d92a8");
                                HelperMethods.FillRect(G, new SolidBrush(Color.Transparent), Rect);
                                HelperMethods.DrawRoundedPath(G, NoneColor, 1, Rect, RoundRadius);
                                HelperMethods.CentreString(G, Text, Font, HelperMethods.SolidBrushHTMlColor("444444"), Rect);
                                break;
                           }
                           break;
                    case HelperMethods.MouseMode.Pushed:
                           switch(ButtonStyle)
                        {
                               case Style.Clear: case Style.DarkClear:
                                NoneColor = HelperMethods.GetHTMLColor("444444");
                                HelperMethods.FillRect(G, new SolidBrush(Color.Transparent), Rect);
                                HelperMethods.DrawRoundedPath(G, NoneColor, 1, Rect, 5);
                                HelperMethods.CentreString(G, Text, Font, HelperMethods.SolidBrushHTMlColor("444444"), Rect);
                                break;
                               case Style.DarkPink: case Style.LightPink: case Style.SemiBlack:
                                NoneColor = HelperMethods.GetHTMLColor("ececec");
                                HelperMethods.DrawRoundedPath(G, NoneColor, 1, Rect, 5);
                                HelperMethods.CentreString(G, Text, Font, HelperMethods.SolidBrushHTMlColor("b9b9b9"), Rect);
                                break;
                      }
                           break;
            }

            e.Graphics.DrawImage(B, 0, 0);
            G.Dispose() ;
            B.Dispose();
    }
        }

#endregion

    }

#endregion

    #region CheckBox

    [System.ComponentModel.DefaultEvent("CheckedChanged")]
    public class EtherealCheckBox : Control
    {

        #region Variables

        static bool _Checked;
        public event CheckedChangedEventHandler CheckedChanged;
        public delegate void CheckedChangedEventHandler(object sender);
        private Color _CheckColor = HelperMethods.GetHTMLColor("746188");
        private Color _BorderColor = HelperMethods.GetHTMLColor("746188");

        #endregion

        #region Properties

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

        public Color CheckColor
        {
            get
            {
                return _CheckColor;
            }
            set
            {
                _CheckColor = value;
                Invalidate();
            }
        }

        public Color BorderColor
        {
            get
            {
                return _BorderColor;
            }
            set
            {
                _BorderColor = value;
                Invalidate();
            }
        }

        #endregion

        #region Initialization

        public EtherealCheckBox()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint |
                     ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer
                     | ControlStyles.SupportsTransparentBackColor, true);
            DoubleBuffered = true;
            //UpdateStyles();
            Cursor = Cursors.Hand;
            Size = new Size(200, 20);
            Font = new Font("Proxima Nova", 11, FontStyle.Regular);
            BackColor = Color.Transparent;
        }

        #endregion

        #region DrawControl

        protected override void OnPaint(PaintEventArgs e)
        {
            Bitmap B = new Bitmap(Width, Height);
            using (Graphics G = Graphics.FromImage(B))
            {

                G.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                G.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                HelperMethods.DrawRoundedPath(G, BorderColor, 3, new Rectangle(1, 1, 16, 16), 3);
                if (Checked)
                {
                    HelperMethods.FillRoundedPath(G, CheckColor, new Rectangle(5, 5, (int)8.5, (int)8.5), 1);
                                       
                }
                G.DrawString(Text, Font, Brushes.Gray, new Rectangle(22,Convert.ToInt32(-1.2), Width, Height - 2));
                e.Graphics.DrawImage((Image)(B.Clone()), 0, 0);
                G.Dispose();
                B.Dispose();
            }


        }

        #endregion

        #region  Events
        
          protected override void OnResize(EventArgs e)
        {
            Height = 20;
            
            base.OnResize(e);
            Invalidate();
        }

        protected override void OnClick(EventArgs e)
        {
            _Checked = !_Checked;
            if (CheckedChanged != null)
            {
                CheckedChanged(this);
            }
            base.OnClick(e);
            Invalidate();
        }

        #endregion

    }

    #endregion

    #region Textbox
    [System.ComponentModel.DefaultEvent("TextChanged")]
    public class EtherealTextbox : Control
    {
        
        #region Variables

        private TextBox T = new TextBox();
        private HorizontalAlignment _TextAlign = HorizontalAlignment.Left;
        private int _MaxLength = 32767;
        private bool _ReadOnly;
        private bool _UseSystemPasswordChar = false;
        private string _WatermarkText = "";
        private Image _SideImage;
        private bool _Multiline = false;
        protected HelperMethods.MouseMode State = HelperMethods.MouseMode.NormalMode;
        private Color _ForeColor = Color.Gray;
        private Color _BackColor = Color.White;
        private Color _BorderColor = HelperMethods.GetHTMLColor("ececec");

        #endregion

        #region  Native Methods
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern Int32 SendMessage(IntPtr hWnd, int msg, int wParam, [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.LPWStr)]
string lParam);

        #endregion

        #region Initialization

        public EtherealTextbox()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint |
                    ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer |
                    ControlStyles.SupportsTransparentBackColor, true);
            DoubleBuffered = true;
            UpdateStyles();

            Font = new Font("Segoe UI", 10, FontStyle.Regular);
            Text = WatermarkText;
            Size = new Size(135, 30);

            T.Multiline = _Multiline;
            T.Cursor = Cursors.IBeam;
            T.BackColor = Color.White;
            T.ForeColor = Color.Gray;
            T.Text = WatermarkText;
            T.BorderStyle = BorderStyle.None;
            T.Location = new Point(7, 7);
            T.Font = Font;
            T.Size = new Size(Width - 10, 30);
            T.UseSystemPasswordChar = _UseSystemPasswordChar;
            T.TextChanged += T_TextChanged;
            T.KeyDown += T_KeyDown;
        }

        #endregion

        #region Properties

        [System.ComponentModel.Browsable(false), System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never), System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public BorderStyle BorderStyle
        {
            get
            {
                return BorderStyle.None;
            }
        }

        [System.ComponentModel.Browsable(false), System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never), System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
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
                _TextAlign = value;
                if (T != null)
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
                SendMessage(T.Handle, 0x1501, 0, value);
                Invalidate();
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

        [System.ComponentModel.Browsable(false)]
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

        [System.ComponentModel.Browsable(false)]
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

       override public Color BackColor
        {
            get
            {
                return _BackColor;
            }
            set
            {
                base.BackColor = value;
                T.BackColor = value;
                _BackColor = value;
                Invalidate();
            }
        }

       override public Color ForeColor
        {
            get
            {
                return _ForeColor;
            }
            set
            {
                base.ForeColor = value;
                T.ForeColor = value;
                _ForeColor = value;
                Invalidate();
            }
        }

        public Color BorderColor
        {
            get
            {
                return _BorderColor;
            }
            set
            {
                _BorderColor = value;
                Invalidate();
            }
        }

        

        #endregion

        #region DrawControl

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Rectangle Rect = new Rectangle(0, 0, Width - 1, Height - 1);
            Bitmap B = new Bitmap(Width, Height);
            using (Graphics G = Graphics.FromImage(B))
            {

                G.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                G.Clear(BackColor);                
               HelperMethods.DrawRoundedPath(G, BorderColor, (int)1.8, new Rectangle(0, 0, Width - 1, Height - 1), 4);
                if (SideImage != null)
                {
                    T.Location = new Point(45, 5);
                    T.Width = Width - 60;
                    G.DrawRectangle(new Pen(BorderColor, 1), new Rectangle(-1, -1, 35, Height + 1));
                    G.DrawImage(SideImage, new Rectangle(8, 7, 16, 16));
                }
                else
                {
                    T.Location = new Point(7, 5);
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

        private void T_MouseHover(object sender, EventArgs e)
        {
            State = HelperMethods.MouseMode.Hovered;

        }

        private void T_MouseLeave(object sender, EventArgs e)
        {
            State = HelperMethods.MouseMode.NormalMode;

        }

        private void T_MouseUp(object sender, EventArgs e)
        {
            State = HelperMethods.MouseMode.Pushed;

        }

        private void T_MouseEnter(object sender, EventArgs e)
        {
            State = HelperMethods.MouseMode.Pushed;

        }

        private void T_MouseDown(object sender, EventArgs e)
        {
            State = HelperMethods.MouseMode.Pushed;

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
            if (e.Control && e.KeyCode == Keys.C)
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

    #region RadioButton

    [System.ComponentModel.DefaultEvent("CheckedChanged")]
    public class EtherealRadioButton : Control
    {

        #region Variables

        private bool _Checked;
        private int _Group = 1;
        public event CheckedChangedEventHandler CheckedChanged;
        public delegate void CheckedChangedEventHandler(object sender);
        private Color _CheckColor = HelperMethods.GetHTMLColor("746188");
        private Color _BorderColor = HelperMethods.GetHTMLColor("746188");

        #endregion

        #region Properties

        public bool Checked
        {
            get 
            { return _Checked; 
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


        public Color CheckColor
        {
            get
            {
                return _CheckColor;
            }
            set
            {
                _CheckColor = value;
                Invalidate();
            }
        }

        public Color BorderColor
        {
            get
            {
                return _BorderColor;
            }
            set
            {
                _BorderColor = value;
                Invalidate();
            }
        }


        #endregion

        #region Initialization

        public EtherealRadioButton()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint |
                      ControlStyles.OptimizedDoubleBuffer | ControlStyles.SupportsTransparentBackColor , true);
            DoubleBuffered = true;
            UpdateStyles();
            Cursor = Cursors.Hand;
            Font = new Font("Proxima Nova", 11, FontStyle.Regular);
        }

        #endregion

        #region DrawControl

        protected override void OnPaint(PaintEventArgs e)
        {
            Bitmap B = new Bitmap(Width, Height);
            using (Graphics G = Graphics.FromImage(B))
            {

                G.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                G.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                G.DrawEllipse(new Pen(BorderColor, Convert.ToInt32(2.5)), 1, 1, 18, 18);
              
                if (Checked)
                {
                   G.FillEllipse(new SolidBrush(CheckColor), new Rectangle(5, 5, 10, 10));
                   
                }
                G.DrawString(Text, Font, Brushes.Gray, new Rectangle(23,Convert.ToInt32(-0.3), Width, Height));
                e.Graphics.DrawImage(B, 0, 0);
                G.Dispose();
                B.Dispose();
            }


        }

        #endregion

        #region  Events

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            Invalidate();
        }
        protected override void OnMouseDown(System.Windows.Forms.MouseEventArgs e)
        {
            if (!_Checked)
                Checked = true;
            base.OnMouseDown(e);
        }
        protected override void OnResize(EventArgs e)
        {
            Height = 21;
            Invalidate();
            base.OnResize(e);
        }

        private void UpdateCheckState()
        {
            if (!IsHandleCreated || !_Checked)
                return;

            foreach (Control C in Parent.Controls)
            {
                if (!object.ReferenceEquals(C, this) && C is EtherealRadioButton && ((EtherealRadioButton)C).Group == _Group)
                {
                    ((EtherealRadioButton)C).Checked = false;
                }
            }
            if (CheckedChanged != null)
            {
                CheckedChanged(this);
            }
        }
     
        protected override void OnCreateControl()
        {
           
            base.OnCreateControl();

        }

        #endregion

    }


    #endregion

    #region ComboBox

    public class EtherealComboBox : ComboBox
    {

        #region Variables

        private int _StartIndex = 0;
        private Color _BorderColor = HelperMethods.GetHTMLColor("ececec");
        private Color _TextColor = HelperMethods.GetHTMLColor("b8c6d6");
        private Color _TriangleColor = HelperMethods.GetHTMLColor("999999");

        #endregion

        #region Initialization

        public EtherealComboBox()
        {
 
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint |
                  ControlStyles.OptimizedDoubleBuffer | ControlStyles.SupportsTransparentBackColor, true);
        DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
        Size = new Size(200, 35);
        DropDownStyle = ComboBoxStyle.DropDownList;
        Font = new Font("Segoe UI", 15);
        BackColor = Color.Transparent;
        DoubleBuffered = true;

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

        public Color BorderColor
        {
            get
            {
                return _BorderColor;
            }
            set
            {
                _BorderColor = value;
                Invalidate();
            }
        }

        public Color TextColor
        {
            get
            {
                return _TextColor;
            }
            set
            {
                _TextColor = value;
                Invalidate();
            }
        }

        public Color TriangleColor
        {
            get
            {
                return _TriangleColor;
            }
            set
            {
                _TriangleColor = value;
                Invalidate();
            }
        }

        #endregion

        #region Draw Control

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Bitmap B = new Bitmap(Width, Height);
            using (Graphics G =Graphics.FromImage(B))
            {
                Rectangle Rect = new Rectangle(1, 1, Width - 2, Height - 2);
                G.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                G.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                G.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                HelperMethods.DrawTriangle(G, TriangleColor, 2, 
                new Point(Width - 20, 16), new Point(Width - 16, 20), 
                new Point(Width - 16, 20), new Point(Width - 12, 16), 
                new Point(Width - 16, 21), new Point(Width - 16, 20) 
                );
                HelperMethods.DrawRoundedPath(G, BorderColor,(int) 1.5, Rect, 4);
                G.DrawString(Text, Font, new SolidBrush(TextColor), new Rectangle(7, 0, Width - 1, Height - 1), new StringFormat {LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Near});
                
                e.Graphics.DrawImage(B, 0, 0);
                G.Dispose();
                B.Dispose();
            }
            
        }

        protected override void OnDrawItem(DrawItemEventArgs e)
        {

            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                  
                    if (System.Convert.ToInt32((e.State & DrawItemState.Selected)) == (int)DrawItemState.Selected)
                    {
                        if (!(e.Index == -1))
                        {
                            e.Graphics.FillRectangle(HelperMethods.SolidBrushHTMlColor("3b2551"), e.Bounds);
                            e.Graphics.DrawString(GetItemText(Items[e.Index]), e.Font, new SolidBrush(Color.WhiteSmoke), e.Bounds);
                        }
                    }
                    else
                    {
                        if (!(e.Index == -1))
                        {
                            e.Graphics.FillRectangle(new SolidBrush(e.BackColor), e.Bounds);
                            e.Graphics.DrawString(GetItemText(Items[e.Index]), e.Font, HelperMethods.SolidBrushHTMlColor("b8c6d6"), e.Bounds);
                        }
                    }

                    Invalidate();
            
        }

        #endregion

        #region Events

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            
        }
 
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            
        }
      
        #endregion

    }

    #endregion

    #region Switch

    [System.ComponentModel.DefaultEvent("CheckedChanged")]
    public class EtherealSwitch : Control
    {

        #region Variables

        protected bool _Switched;
        protected HelperMethods.MouseMode State = HelperMethods.MouseMode.NormalMode;
        public event CheckedChangedEventHandler CheckedChanged;
        public delegate void CheckedChangedEventHandler(object sender);
        private Color _SwitchColor = HelperMethods.GetHTMLColor("3f2153");


        #endregion

        #region Properties

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            Invalidate();
        }

        public bool Switched
        {
            get
            {
                return _Switched;
            }
            set
            {
                _Switched = value;
                Invalidate();
            }
        }

        public Color SwitchColor
        {
            get
            {
                return _SwitchColor;
            }
            set
            {
                _SwitchColor = value;
                Invalidate();
            }
        }
      
        #endregion

        #region Draw Control

        protected override  void OnPaint(PaintEventArgs e)
        {
            Bitmap B = new Bitmap(Width, Height);
            using (Graphics G =Graphics.FromImage(B))
            {

                G.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                 if (Switched)
                 {
                     HelperMethods.FillRoundedPath(G, new SolidBrush(SwitchColor), new Rectangle(1, 1, 42, 22), 22);
                     HelperMethods.DrawRoundedPath(G, HelperMethods.GetHTMLColor("ededed"),(Single)1.5, new Rectangle(1, 1, 42, 22), 20);

                     G.FillEllipse(HelperMethods.SolidBrushHTMlColor("fcfcfc"), new Rectangle(22, Convert.ToInt32(2.6), 19, 18));
                     G.DrawEllipse(HelperMethods.PenHTMlColor("e9e9e9", (Single)1.5), new Rectangle(22,Convert.ToInt32(2.6), 19, 18));
                 }
                 else
                 {
                     HelperMethods.FillRoundedPath(G, HelperMethods.GetHTMLColor("f8f8f8"), new Rectangle(1, 1, 42, 22), 22);
                     HelperMethods.DrawRoundedPath(G, HelperMethods.GetHTMLColor("ededed"), (Single)1.5, new Rectangle(1, 1, 42, 22), 20);

                     G.FillEllipse(HelperMethods.SolidBrushHTMlColor("fcfcfc"), new Rectangle(3, Convert.ToInt32(2.6), 19, 18));
                     G.DrawEllipse(HelperMethods.PenHTMlColor("e9e9e9", (Single)1.5), new Rectangle(3, Convert.ToInt32(2.6), 19, 18));

                 }
                 e.Graphics.DrawImage(B, 0, 0);
                 G.Dispose();
                 B.Dispose();
            }
        }

        #endregion

        #region Initialization

        public EtherealSwitch()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint |
                     ControlStyles.OptimizedDoubleBuffer | ControlStyles.SupportsTransparentBackColor, true);
            DoubleBuffered = true;
            Cursor = Cursors.Hand;
            UpdateStyles();
        }

        #endregion

        #region  Events

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
         
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            
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

        protected override void OnClick(EventArgs e)
        {
            _Switched = !_Switched;
            if (CheckedChanged != null)
            {
                CheckedChanged(this);
            }
            base.OnClick(e);
            Invalidate();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Size = new Size(46, 25);
        }

        #endregion

    }

    #endregion

    #region Progress

    public class EtherealProgressBar : Control
    {

        #region Variables
                
        private int _Maximum = 100;
        private int _Value;
        private int _RoundRadius=8;
        int CurrentValue;
        private Color _ProgressColor = HelperMethods.GetHTMLColor("4e3a62");
        private Color _BaseColor = HelperMethods.GetHTMLColor("f7f7f7");
        private Color _BorderColor = HelperMethods.GetHTMLColor("ececec");
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
                Invalidate();


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
                    _Value = value;
                }
                _Maximum = value;
                Invalidate();
            }
        }

        public int RoundRadius
        {

            get
            {
                return _RoundRadius;
            }
            set
            {
            _RoundRadius = value;
            Invalidate();
            }
        }

        public Color ProgressColor
        {
            get
            {
                return _ProgressColor;
            }
            set
            {
                _ProgressColor = value;
                Invalidate();
            }
        }

        public Color BaseColor
        {
            get
            {
                return _BaseColor;
            }
            set
            {
                _BaseColor = value;
                Invalidate();
            }
        }

        public Color BorderColor
        {
            get
            {
                return _BorderColor;
            }
            set
            {
                _BorderColor = value;
                Invalidate();
            }
        }

        #endregion

        #region Draw Control

        protected override void OnPaint(PaintEventArgs e)
        {
            Bitmap B = new Bitmap(Width, Height);
            using (Graphics G =Graphics.FromImage(B))
            {

                G.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                
                Rectangle Rect = new Rectangle(0, 0, Width - 1, Height - 1);

            HelperMethods.FillRoundedPath(G, new SolidBrush(BaseColor), Rect, RoundRadius);
            HelperMethods.DrawRoundedPath(G, BorderColor, 1, Rect, RoundRadius);

            if(CurrentValue != 0) 
            {
                HelperMethods.FillRoundedPath(G, ProgressColor, new Rectangle(Rect.X, Rect.Y, CurrentValue, Rect.Height), RoundRadius);

            }
                 e.Graphics.DrawImage(B, 0, 0);
                 G.Dispose();
                 B.Dispose();
            }
        }

        #endregion

        #region Initialization

        public EtherealProgressBar()
        {

            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.ResizeRedraw |
                           ControlStyles.OptimizedDoubleBuffer | ControlStyles.SupportsTransparentBackColor, true);
            DoubleBuffered = true;
            BackColor = Color.Transparent;
            UpdateStyles();
            CurrentValue = Convert.ToInt32(_Value / _Maximum * Width);
        }

        #endregion

        #region Events

        public event ValueChangedEventHandler ValueChanged;
        public delegate void ValueChangedEventHandler(object sender);
        public void RenewCurrentValue()
        {

            CurrentValue = (int)Math.Round((double)(Value) / (double)(Maximum) * (double)(Width));
        }

        #endregion

    }

    #endregion

    #region Seperator

    public class EtherealSeperator : Control
    {

        #region Variables

        private Style _SepStyle = Style.Horizental;

        #endregion

        #region Initialization

        public EtherealSeperator()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint |
            ControlStyles.ResizeRedraw | ControlStyles.UserPaint |
            ControlStyles.SupportsTransparentBackColor, true);
            DoubleBuffered = true;
            UpdateStyles();
            BackColor = Color.Transparent;
            ForeColor = HelperMethods.GetHTMLColor("3b2551");
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

                G.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                System.Drawing.Drawing2D.ColorBlend BL1 = new System.Drawing.Drawing2D.ColorBlend();
                BL1.Positions = new Single[] { 0.0F, 0.15F, 0.85F, 1.0F };
                BL1.Colors = new Color[] { Color.Transparent, ForeColor, ForeColor, Color.Transparent };
                switch (SepStyle)
                {
                    case Style.Horizental:
                        using (System.Drawing.Drawing2D.LinearGradientBrush lb1 = new System.Drawing.Drawing2D.LinearGradientBrush(ClientRectangle, Color.Empty, Color.Empty, 0.0F))
                        {
                            lb1.InterpolationColors = BL1;
                            G.DrawLine(new Pen(lb1), 0, Convert.ToInt32(0.7), Width, Convert.ToInt32(0.7));
                        }
                        break;
                    case Style.Vertiacal:
                        using (System.Drawing.Drawing2D.LinearGradientBrush lb1 = new System.Drawing.Drawing2D.LinearGradientBrush(ClientRectangle, Color.Empty, Color.Empty, 90.0F))
                        {
                            lb1.InterpolationColors = BL1;
                            G.DrawLine(new Pen(lb1), Convert.ToInt32(0.7), 0, Convert.ToInt32(0.7), Height);
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

    #region Close

    public class EtherealClose : Control
    {


        #region Variables

        private HelperMethods.MouseMode State;
        private Color _NormalColor = HelperMethods.GetHTMLColor("3f2153");
        private Color _HoverColor = HelperMethods.GetHTMLColor("f0f0f0");
        private Color _PushedColor = HelperMethods.GetHTMLColor("966bc1");
        private Color _TextColor = Color.White;

        #endregion

        #region Initialization

        public EtherealClose()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint |
            ControlStyles.ResizeRedraw | ControlStyles.UserPaint |
            ControlStyles.SupportsTransparentBackColor, true);
            DoubleBuffered = true;
            UpdateStyles();
            BackColor = Color.Transparent;
            Font = new Font("Marlett", 8);
            Size = new Size(20, 20);
        }

        #endregion

        #region Properties

        public Color NormalColor
        {
            get
            {
                return _NormalColor;
            }
            set
            {
                _NormalColor=value;
                Invalidate();
            }
            
        }

        public Color PushedColor
        {
            get
            {
                return _PushedColor;
            }
            set
            {
                PushedColor=value;
               Invalidate();
            }
            
        }

        public Color HoverColor
        {
            get
            {
                return _HoverColor;
            }
            set
            {
                _HoverColor=value;
                Invalidate();
            }
            
        }

        public Color TextColor
        {
            get
            {
                return _TextColor;
            }
            set
            {
                _TextColor=value;
                Invalidate();
            }
            
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
              
                G.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

                G.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

                G.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;

                switch(State)
                {
                    case HelperMethods.MouseMode.NormalMode:
                        G.FillEllipse(new SolidBrush(NormalColor), 1, 1, 19, 19);
                        break;
                    case HelperMethods.MouseMode.Hovered:
                        Cursor = Cursors.Hand;
                        G.FillEllipse(new SolidBrush(NormalColor), 1, 1, 19, 19);
                        G.DrawEllipse(new Pen(HoverColor, 2), 1, 1, 18, 18);
                        break;
                    case HelperMethods.MouseMode.Pushed:
                        G.FillEllipse(new SolidBrush(PushedColor), 1, 1, 19, 19);
                        break;
                }

                G.DrawString("r", Font, new SolidBrush(TextColor), new Rectangle(4, 6, 18, 18));

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
            Size = new Size(20, 20);
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            Environment.Exit(0);
            Application.Exit();
        }

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

    #region Maximize

    public class EtherealMaximize : Control
    {


        #region Variables

        private HelperMethods.MouseMode State;
        private Color _NormalColor = HelperMethods.GetHTMLColor("3f2153");
        private Color _HoverColor = HelperMethods.GetHTMLColor("f0f0f0");
        private Color _PushedColor = HelperMethods.GetHTMLColor("966bc1");
        private Color _TextColor = Color.White;

        #endregion

        #region Initialization

        public EtherealMaximize()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint |
            ControlStyles.ResizeRedraw | ControlStyles.UserPaint |
            ControlStyles.SupportsTransparentBackColor, true);
            DoubleBuffered = true;
            UpdateStyles();
            BackColor = Color.Transparent;
            Font = new Font("Marlett", 9);
            Size = new Size(20, 20);
        }

        #endregion

        #region Properties

        public Color NormalColor
        {
            get
            {
                return _NormalColor;
            }
            set
            {
                _NormalColor = value;
                Invalidate();
            }

        }

        public Color PushedColor
        {
            get
            {
                return _PushedColor;
            }
            set
            {
                PushedColor = value;
                Invalidate();
            }

        }

        public Color HoverColor
        {
            get
            {
                return _HoverColor;
            }
            set
            {
                _HoverColor = value;
                Invalidate();
            }

        }

        public Color TextColor
        {
            get
            {
                return _TextColor;
            }
            set
            {
                _TextColor = value;
                Invalidate();
            }

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

                G.Clear(HelperMethods.GetHTMLColor("3f2153"));

                G.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

                G.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

                G.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;

                switch (State)
                {
                    case HelperMethods.MouseMode.NormalMode:
                        G.FillEllipse(new SolidBrush(NormalColor), 1, 1, 19, 19);
                        break;
                    case HelperMethods.MouseMode.Hovered:
                        Cursor = Cursors.Hand;
                        G.FillEllipse(new SolidBrush(NormalColor), 1, 1, 19, 19);
                        G.DrawEllipse(new Pen(HoverColor, 2), 1, 1, 18, 18);
                        break;
                    case HelperMethods.MouseMode.Pushed:
                        G.FillEllipse(new SolidBrush(PushedColor), 1, 1, 19, 19);
                        break;
                }

                G.DrawString("v", Font, new SolidBrush(TextColor), new Rectangle(Convert.ToInt32(3.4), 5, 18, 18));

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
              if (FindForm().WindowState == FormWindowState.Normal)
              {
            FindForm().WindowState = FormWindowState.Maximized;
              }
              else
              {
            FindForm().WindowState = FormWindowState.Normal;
              }
       
        }

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

    #region Minimize

    public class EtherealMinimize : Control
    {


        #region Variables

        private HelperMethods.MouseMode State;
        private Color _NormalColor = HelperMethods.GetHTMLColor("3f2153");
        private Color _HoverColor = HelperMethods.GetHTMLColor("f0f0f0");
        private Color _PushedColor = HelperMethods.GetHTMLColor("966bc1");
        private Color _TextColor = Color.White;

        #endregion

        #region Initialization

        public EtherealMinimize()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint |
            ControlStyles.ResizeRedraw | ControlStyles.UserPaint |
            ControlStyles.SupportsTransparentBackColor, true);
            DoubleBuffered = true;
            UpdateStyles();
            BackColor = Color.Transparent;
            Font = new Font("Marlett", 9);
            Size = new Size(21, 21);
        }

        #endregion

        #region Properties

        public Color NormalColor
        {
            get
            {
                return _NormalColor;
            }
            set
            {
                _NormalColor = value;
                Invalidate();
            }

        }

        public Color PushedColor
        {
            get
            {
                return _PushedColor;
            }
            set
            {
                PushedColor = value;
                Invalidate();
            }

        }

        public Color HoverColor
        {
            get
            {
                return _HoverColor;
            }
            set
            {
                _HoverColor = value;
                Invalidate();
            }

        }

        public Color TextColor
        {
            get
            {
                return _TextColor;
            }
            set
            {
                _TextColor = value;
                Invalidate();
            }

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

                G.Clear(HelperMethods.GetHTMLColor("3f2153"));

                G.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

                G.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

                G.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;

                switch (State)
                {
                    case HelperMethods.MouseMode.NormalMode:
                        G.FillEllipse(new SolidBrush(NormalColor), 1, 1, 19, 19);
                        break;
                    case HelperMethods.MouseMode.Hovered:
                        Cursor = Cursors.Hand;
                        G.FillEllipse(new SolidBrush(NormalColor), 1, 1, 19, 19);
                        G.DrawEllipse(new Pen(HoverColor, 2), 1, 1, 18, 18);
                        break;
                    case HelperMethods.MouseMode.Pushed:
                        G.FillEllipse(new SolidBrush(PushedColor), 1, 1, 19, 19);
                        break;
                }

                G.DrawString("0", Font, new SolidBrush(TextColor), new Rectangle(Convert.ToInt32(4.5), Convert.ToInt32(2.6), 18, 18));

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
            if (FindForm().WindowState == FormWindowState.Normal)
              {
                  FindForm().WindowState = FormWindowState.Minimized;
              }
       
        }

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

    #region Label

    public class EtherealLabel :Control
    {
        #region Vaeiables
               
        private Style _ColorStyle=Style.DarkPink;

        #endregion

        #region Initialization

        public EtherealLabel()
        {
        SetStyle(ControlStyles.SupportsTransparentBackColor | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer, true);
        DoubleBuffered = true;
        Font = new Font("Montserrat", 10, FontStyle.Bold);
        }

        #endregion

        #region Properties

        public Style ColorStyle
        {
            get
            {
            return _ColorStyle;
            }
            set
            {
            _ColorStyle = value;
            Invalidate();
            }
        }
              
        #endregion
              
        #region Enumerators

        public enum Style
        {
        SemiBlack,
        DarkPink,
        LightPink
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

           G.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            switch (ColorStyle)
            {
                case Style.SemiBlack:
                    G.DrawString(Text, Font, HelperMethods.SolidBrushHTMlColor("222222"), ClientRectangle);
                        break;
                case Style.DarkPink:
                        G.DrawString(Text, Font, HelperMethods.SolidBrushHTMlColor("3b2551"), ClientRectangle);
                        break;
                case Style.LightPink:
                        G.DrawString(Text, Font, HelperMethods.SolidBrushHTMlColor("9d92a8"), ClientRectangle);
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