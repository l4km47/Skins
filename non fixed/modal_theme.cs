/// <summary>
/// Modal Theme
/// Author : THE LORD
/// Release Date : Monday, January 16, 2017
/// HF Account : https://hackforums.net/member.php?action=profile&uid=3304362
/// PM Me for any bug.
/// </summary>

#region NampeSpaces

using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;

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

    public void FillRoundedPath(Graphics G, Color C, Rectangle Rect, int Curve)
    {
        G.FillPath(new SolidBrush(C), RoundRec(Rect, Curve));
    }

    public void FillRoundedPath(Graphics G, Brush B, Rectangle Rect, int Curve)
    {
        G.FillPath(B, RoundRec(Rect, Curve));
    }

    public void DrawRoundedPath(Graphics G, Color C, Single Size, Rectangle Rect, int Curve)
    {
        G.DrawPath(new Pen(C, Size), RoundRec(Rect, Curve));

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

public class ModalTheme : ContainerControl
{

    #region  Variables

    bool Movable = false;
    private TitlePostion _TitleTextPostion = TitlePostion.Left;
    private Point MousePoint = new Point(0, 0);
    private int MoveHeight = 50;
    private static HelperMethods H = new HelperMethods();
    private int _BorderThickness = 1;
    private bool _ShowIcon;

    #endregion

    #region Draw Control

    protected override void OnPaint(PaintEventArgs e)
    {
        using (Bitmap B = new Bitmap(Width, Height))
        using (Graphics G = Graphics.FromImage(B))
        {
            
            G.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            G.FillRectangle(H.SolidBrushHTMlColor("331f35"), new Rectangle(0, 0, Width, Height));

            G.FillRectangle(H.SolidBrushHTMlColor("241525"), new Rectangle(0, 0, Width, 55));

            G.DrawLine(H.PenHTMlColor("231024", 1), new Point(0, 55), new Point(Width, 55));

            G.DrawRectangle(H.PenHTMlColor("241525", BorderThickness), new Rectangle(0, 0, Width, Height));

            if (FindForm().ShowIcon)
            {
                if (FindForm().Icon != null)
                {
                    switch (TitleTextPostion)
                    {
                        case TitlePostion.Left:
                            G.DrawString(Text.ToUpper(), Font, H.SolidBrushHTMlColor("f3ebf3"), 27, 15);
                            G.DrawIcon(FindForm().Icon, new Rectangle(5, 16, 20, 20));
                            break;
                        case TitlePostion.Center:
                            H.CentreString(G, Text.ToUpper(), Font, H.SolidBrushHTMlColor("f3ebf3"), new Rectangle(0, 0, Width, 50));
                            G.DrawIcon(FindForm().Icon, new Rectangle(5, 16, 20, 20));
                            break;
                        case TitlePostion.Right:
                            H.RightString(G, Text.ToUpper(), Font, H.SolidBrushHTMlColor("f3ebf3"), new Rectangle(0, 0, Width, 50));
                            G.DrawIcon(FindForm().Icon, new Rectangle(Width - 30, 16, 20, 20));
                            break;
                    }
                }

            }
            else
            {
                switch (TitleTextPostion)
                {
                    case TitlePostion.Left:
                        G.DrawString(Text.ToUpper(), Font, H.SolidBrushHTMlColor("f3ebf3"), 5, 17);
                        break;
                    case TitlePostion.Center:
                        H.CentreString(G, Text.ToUpper(), Font, H.SolidBrushHTMlColor("f3ebf3"), new Rectangle(0, 2, Width, 50));
                        break;
                    case TitlePostion.Right:
                        H.RightString(G, Text.ToUpper(), Font, H.SolidBrushHTMlColor("f3ebf3"), new Rectangle(0, 2, Width, 50));
                        break;
                }
            }

            e.Graphics.DrawImage(B, 0, 0);
            G.Dispose();
            B.Dispose();
        }
    }

    #endregion

    #region Constructors

    public ModalTheme()
    {

        SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor | ControlStyles.ContainerControl, true);
        DoubleBuffered = true;
        Font = new Font("Ubuntu", 13, FontStyle.Bold);
        UpdateStyles();

    }

    #endregion

    #region  Properties
    
    public int BorderThickness
    {
        get
        {
            return _BorderThickness;
        }
        set
        {
            _BorderThickness = value;
            Invalidate();
        }
    }

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
        Center,
        Right
    };

    #endregion

    #region Events

    protected override void OnCreateControl()
    {

        base.OnCreateControl();
        ParentForm.FormBorderStyle = FormBorderStyle.None;
        ParentForm.Dock = DockStyle.None;
        Dock = DockStyle.Fill;
        Invalidate();

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
        try
        {
            base.OnMouseDown(e);

            if (e.Button == System.Windows.Forms.MouseButtons.Left && new Rectangle(0, 0, Width, MoveHeight).Contains(e.Location))
            {
                Movable = true;
                MousePoint = e.Location;
            }

        }
        catch 
        {

        }
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {

        base.OnMouseUp(e);

        Movable = false;


    }

    #endregion


}

#endregion

#region Flat Button

public class ModalFlatButton : Control
{

    #region Variables

    private HelperMethods.MouseMode State;
    private static HelperMethods H = new HelperMethods();

    #endregion

    #region Draw Control

    protected override void OnPaint(PaintEventArgs e)
    {
        using (Bitmap B = new Bitmap(Width, Height))
        using (Graphics G = Graphics.FromImage(B))
        {
            G.SmoothingMode = SmoothingMode.AntiAlias;
            G.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            Rectangle R = new Rectangle(0, 0, Width - 1, Height - 1);

            switch (State)
            {
                case HelperMethods.MouseMode.NormalMode:

                 H.FillRoundedPath(G, H.SolidBrushHTMlColor("291a2a"), R, 2);
                 H.DrawRoundedPath(G, H.GetHTMLColor("231625"), 1, R, 2);

                    break;
                case HelperMethods.MouseMode.Hovered:
                    Cursor=Cursors.Hand;

                    H.FillRoundedPath(G, H.SolidBrushHTMlColor("231625"), R, 2);
                    H.DrawRoundedPath(G, H.GetHTMLColor("231625"), 1, R, 2);

                    break;
                case HelperMethods.MouseMode.Pushed:

                    H.FillRoundedPath(G, H.SolidBrushHTMlColor("291a2a"), R, 2);
                    H.DrawRoundedPath(G, H.GetHTMLColor("231625"), 1, R, 2);

                    break;
            }

            H.CentreString(G, Text, Font, H.SolidBrushHTMlColor("e5d2e6"), new Rectangle(0,0,Width-2,Height-4));

            e.Graphics.DrawImage(B, 0, 0);
            G.Dispose();
            B.Dispose();
        }
    }

    #endregion

    #region Constructors

    public ModalFlatButton()
    {
        SetStyle(ControlStyles.DoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.ResizeRedraw | ControlStyles.UserPaint |
        ControlStyles.SupportsTransparentBackColor, true);
        DoubleBuffered = true;
        BackColor = Color.Transparent;
        Font = new Font("Ubuntu", 14, FontStyle.Regular, GraphicsUnit.Pixel);
        UpdateStyles();

    }

    #endregion

    #region Events

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

#region Button

public class ModalButton : Control
{

    #region Variables

    private HelperMethods.MouseMode State;
    private static HelperMethods H = new HelperMethods();
    
    #endregion

    #region Draw Control

    protected override void OnPaint(PaintEventArgs e)
    {
        using (Bitmap B = new Bitmap(Width, Height))
        using (Graphics G = Graphics.FromImage(B))
        {
            G.SmoothingMode = SmoothingMode.AntiAlias;
            G.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            Rectangle R = new Rectangle(0, 0, Width - 1, Height - 1);

            switch (State)
            {
                case HelperMethods.MouseMode.NormalMode:

                    using (LinearGradientBrush LB = new LinearGradientBrush(R,Color.FromArgb(200, H.GetHTMLColor("431448")), Color.FromArgb(200, H.GetHTMLColor("5b2960")), 270f))
                    {
                        H.FillRoundedPath(G, LB, R, 2);
                        using (LinearGradientBrush LB2 = new LinearGradientBrush(R, Color.FromArgb(230, H.GetHTMLColor("431448")), Color.FromArgb(230, H.GetHTMLColor("5b2960")), 270f))
                        {
                            H.FillRoundedPath(G, LB2, R, 2);
                        }
                        H.DrawRoundedPath(G, H.GetHTMLColor("311833"), 1, R, 2);
                    }

                    break;

                case HelperMethods.MouseMode.Hovered:
                    Cursor = Cursors.Hand;
                    using (LinearGradientBrush LB = new LinearGradientBrush(R, Color.FromArgb(200, H.GetHTMLColor("241525")), Color.FromArgb(200, H.GetHTMLColor("241525")), 270f))
                    {
                        H.FillRoundedPath(G, LB, R, 2);
                        using (LinearGradientBrush LB2 = new LinearGradientBrush(R, Color.FromArgb(230, H.GetHTMLColor("431448")), Color.FromArgb(230, H.GetHTMLColor("5b2960")), 270f))
                        {
                            H.FillRoundedPath(G, LB2, R, 2);
                        }
                        H.DrawRoundedPath(G, H.GetHTMLColor("311833"), 1, R, 2);
                    }

                    break;
                case HelperMethods.MouseMode.Pushed:
                    using (LinearGradientBrush LB = new LinearGradientBrush(R, Color.FromArgb(200, H.GetHTMLColor("431448")), Color.FromArgb(200, H.GetHTMLColor("5b2960")), 270f))
                    {
                        H.FillRoundedPath(G, LB, R, 2);
                        using (LinearGradientBrush LB2 = new LinearGradientBrush(R, Color.FromArgb(230, H.GetHTMLColor("431448")), Color.FromArgb(230, H.GetHTMLColor("5b2960")), 270f))
                        {
                            H.FillRoundedPath(G, LB2, R, 2);
                        }
                        H.DrawRoundedPath(G, H.GetHTMLColor("311833"), 1, R, 2);
                    }

                    break;
            }

            H.CentreString(G, Text, Font, H.SolidBrushHTMlColor("e5d2e6"), new Rectangle(0,0,Width-2,Height-4));

            e.Graphics.DrawImage(B, 0, 0);
            G.Dispose();
            B.Dispose();
        }
    }

    #endregion
        
    #region Constructors

    public ModalButton()
    {
        SetStyle(ControlStyles.DoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.ResizeRedraw | ControlStyles.UserPaint |
        ControlStyles.Selectable | ControlStyles.SupportsTransparentBackColor, true);
        DoubleBuffered = true;
        BackColor = Color.Transparent;
        Font = new Font("Ubuntu", 14, FontStyle.Regular, GraphicsUnit.Pixel);
        UpdateStyles();

    }

    #endregion

    #region Events

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

#region Textbox

[DefaultEvent("TextChanged")] public class ModalTextbox : Control
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
    private static HelperMethods H = new HelperMethods();
    private static Color TBC = H.GetHTMLColor("291a2a");
    private static Color TFC = H.GetHTMLColor("a89ea9");
    private SideAligin _SideImageAlign = SideAligin.Left;
    private Color _BackColor = TBC;
    #endregion

    #region  Native Methods
    [System.Runtime.InteropServices.DllImport("user32.dll")]
    private static extern Int32 SendMessage(IntPtr hWnd, int msg, int wParam, [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.LPWStr)]
string lParam);

    #endregion

    #region Draw Control

    protected override void OnPaint(PaintEventArgs e)
    {
        using (Bitmap B = new Bitmap(Width, Height))
        using (Graphics G = Graphics.FromImage(B))
        {
                     
            G.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            Rectangle Rect = new Rectangle(0, 0, Width - 1, Height - 1);
            Height = 30;

            G.FillRectangle(H.SolidBrushHTMlColor("291a2a"), Rect);

            G.DrawLine(H.PenHTMlColor("231625", 1), new Point(0, Height - 1), new Point(Width - 2, Height - 1));
                          


            if (SideImage != null)
            {
                if (SideImageAlign == SideAligin.Right) 
                {
                    T.Location = new Point(7, 5);
                    T.Width = Width - 60;
                    G.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    G.DrawImage(SideImage, new Rectangle(Rect.Width - 24, 6, 16, 16));
                }
                else
                {
                T.Location = new Point(33,5);
                T.Width = Width - 60;
                G.InterpolationMode = InterpolationMode.HighQualityBicubic;
                G.DrawImage(SideImage, new Rectangle(8, 6, 16, 16));
                }
                

            }
            else
            {
                T.Location = new Point(7,5);
                T.Width = Width - 10;
            }

            if (ContextMenuStrip != null) { T.ContextMenuStrip = ContextMenuStrip; }

            e.Graphics.DrawImage(B, 0, 0);
            G.Dispose();
            B.Dispose();
        }
    }

    #endregion

    #region Constructors

    public ModalTextbox()
    {
        SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint |
                ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.SupportsTransparentBackColor, true);
        DoubleBuffered = true;
        UpdateStyles();
        Font = new Font("Arial", 11, FontStyle.Regular);
        Size = new Size(135, 30);
        T.Multiline = _Multiline;
        T.Cursor = Cursors.IBeam;
        T.BackColor = TBC;
        T.ForeColor = TFC;
        T.BorderStyle = BorderStyle.None;
        T.Location = new Point(7, 7);
        T.Font = Font;
        T.Size = new Size(Width - 10, 30);
        T.UseSystemPasswordChar = _UseSystemPasswordChar;
        T.TextChanged += T_TextChanged;
        T.MouseDown += T_MouseDown;
        T.MouseEnter += T_MouseEnter;
        T.MouseUp += T_MouseUp;
        T.MouseLeave += T_MouseLeave;
        T.MouseHover += T_MouseHover;
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

    public enum SideAligin
    {
        Left,
        Right
    }

    public SideAligin SideImageAlign
    {
        get
        {
            return _SideImageAlign;
        }
        set
        {
            _SideImageAlign = value;
            Invalidate();
        }
    }

    override public Color BackColor
    {

        get { return _BackColor; }
        set
        {
            base.BackColor = value;
            _BackColor = value;
            T.BackColor = value;
            Invalidate();
        }
    }


    #endregion

    #region Events

    private void T_MouseHover(object sender, EventArgs e)
    {
        State = HelperMethods.MouseMode.Hovered;
        Invalidate();
    }

    private void T_MouseLeave(object sender, EventArgs e)
    {
        State = HelperMethods.MouseMode.NormalMode;
        Invalidate();
    }

    private void T_MouseUp(object sender, EventArgs e)
    {
        State = HelperMethods.MouseMode.Pushed;
        Invalidate();
    }

    private void T_MouseEnter(object sender, EventArgs e)
    {
        State = HelperMethods.MouseMode.Pushed;
        Invalidate();
    }

    private void T_MouseDown(object sender, EventArgs e)
    {
        State = HelperMethods.MouseMode.Pushed;
        Invalidate();
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

#region ComboBox

public class ModalComboBox : ComboBox
{

    #region Variables

    private int _StartIndex = 0;
    private static HelperMethods H = new HelperMethods();

    #endregion

    #region Constructors

    public ModalComboBox()
    {
        SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.ResizeRedraw | ControlStyles.UserPaint |
              ControlStyles.OptimizedDoubleBuffer | ControlStyles.SupportsTransparentBackColor, true);
        DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
        DoubleBuffered = true;
        StartIndex = 0;
        DropDownHeight = 100;
        BackColor = H.GetHTMLColor("291a2a");
        Font = new Font("Ubuntu", 12, FontStyle.Regular);
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
        using (Bitmap B = new Bitmap(Width, Height))
        using (Graphics G = Graphics.FromImage(B))
        {
            Rectangle Rect = new Rectangle(0, 0, Width - 1, Height - 1);

            G.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

            G.FillRectangle(H.SolidBrushHTMlColor("291a2a"), Rect);
          
            G.DrawLine(H.PenHTMlColor("231625", 2), new Point(Width - 21, (Height / 2) - 3), new Point(Width - 7, (Height / 2) - 3));
          
            G.DrawLine(H.PenHTMlColor("231625", 2), new Point(Width - 21, (Height / 2)+1), new Point(Width - 7, (Height / 2)+1));
            
            G.DrawLine(H.PenHTMlColor("231625", 2), new Point(Width - 21, (Height / 2) + 5), new Point(Width - 7, (Height / 2) + 5));
           
            G.DrawLine(H.PenHTMlColor("231625", 1), new Point(1, Height - 1), new Point(Width - 2, Height - 1));
            
            G.DrawString(Text, Font, new SolidBrush(H.GetHTMLColor("a89ea9")), new Rectangle(5, 1, Width - 1, Height - 1), new StringFormat {LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Near});
            
            e.Graphics.DrawImage(B, 0, 0);
            G.Dispose();
            B.Dispose();
        }
    }

    protected override void OnDrawItem(DrawItemEventArgs e)
    {
        try
        {            
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            if (System.Convert.ToInt32((e.State & DrawItemState.Selected)) == (int)DrawItemState.Selected)
            {
                if (!(e.Index == -1))
                {
                    Cursor = Cursors.Hand;
                    e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(120, H.GetHTMLColor("291a2a"))), new Rectangle(e.Bounds.X + 1, e.Bounds.Y + 2, e.Bounds.Width - 2, e.Bounds.Height - 2));
                    e.Graphics.DrawString(GetItemText(Items[e.Index]), new Font("Ubuntu", 10, FontStyle.Bold), H.SolidBrushHTMlColor("a89ea9"), new Rectangle(e.Bounds.X + 1, e.Bounds.Y + 3, e.Bounds.Width - 2, e.Bounds.Height - 2));
                }
            }
            else
            {
                if (!(e.Index == -1))
                {
                    e.Graphics.FillRectangle(H.SolidBrushHTMlColor("291a2a"), new Rectangle(e.Bounds.X + 1, e.Bounds.Y + 2, e.Bounds.Width - 2, e.Bounds.Height - 2));
                    e.Graphics.DrawString(GetItemText(Items[e.Index]), new Font("Ubuntu", 10, FontStyle.Regular), Brushes.Gainsboro, new Rectangle(e.Bounds.X + 1, e.Bounds.Y + 3, e.Bounds.Width - 2, e.Bounds.Height - 2));
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

#region CheckBox

[DefaultEvent("CheckedChanged")] public class ModalCheckBox : Control
{

    #region Variables

    protected bool _Checked;
    protected HelperMethods.MouseMode State = HelperMethods.MouseMode.NormalMode;
    public event CheckedChangedEventHandler CheckedChanged;
    public delegate void CheckedChangedEventHandler(object sender);
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

    #region Draw Control

    protected override void OnPaint(PaintEventArgs e)
    {
        using (Bitmap B = new Bitmap(Width, Height))
        using (Graphics G = Graphics.FromImage(B))
        {

            Rectangle R = new Rectangle(1, 1, 18, 18);

            G.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

            G.FillRectangle(H.SolidBrushHTMlColor("291a2a"), R);
            G.DrawRectangle(H.PenHTMlColor("231625", (int)1.5), R);

            if (Checked)
            {
                G.DrawString("b", new Font("Marlett", 16, FontStyle.Regular), H.SolidBrushHTMlColor("5b2960"), new Rectangle(Convert.ToInt32(-2.7), 0, Width - 4, Height));                               
            }

            G.DrawString(Text, Font, H.SolidBrushHTMlColor("e4ecf2"), new Rectangle(22, Convert.ToInt32(1.6), Width, Height - 2), new StringFormat { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center });

            e.Graphics.DrawImage(B, 0, 0);
            G.Dispose();
            B.Dispose();
        }
    }

    #endregion

    #region Constructors

    public ModalCheckBox()
    {
        SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint |
                 ControlStyles.OptimizedDoubleBuffer | ControlStyles.SupportsTransparentBackColor, true);
        DoubleBuffered = true;
        BackColor = Color.Transparent;
        Font = new Font("Ubuntu", 11, FontStyle.Regular);
        Cursor = Cursors.Hand;
        UpdateStyles();
    }

    #endregion

    #region  Events

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
        Height = 20;
        Invalidate();
    }

    protected override void OnCreateControl()
    {
        base.OnCreateControl();
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

#region RadioButton

[DefaultEvent("CheckedChanged")] public class ModalRadioButton : Control
{

    #region Variables

    protected bool _Checked;
    protected HelperMethods.MouseMode State = HelperMethods.MouseMode.NormalMode;
    protected static int _Group = 1;
    public event CheckedChangedEventHandler CheckedChanged;
    public delegate void CheckedChangedEventHandler(object sender);
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

    #region Draw Control

    protected override void OnPaint(PaintEventArgs e)
    {
        using (Bitmap B = new Bitmap(Width, Height))
        using (Graphics G = Graphics.FromImage(B))
        {
            G.SmoothingMode = SmoothingMode.AntiAlias;

            G.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

            G.FillEllipse(H.SolidBrushHTMlColor("291a2a"), new Rectangle(1, 1, 18, 18));
            G.DrawEllipse(H.PenHTMlColor("231625", (int)2.8), new Rectangle(1, 1, 18, 18));
            
            if (Checked)
            {
                G.DrawString("-", Font, H.SolidBrushHTMlColor("5b2960"), new Rectangle(5, Convert.ToInt32(0.8), Width - 4, Height));
            }
            else
            {
                G.DrawString("+", Font, H.SolidBrushHTMlColor("5b2960"), new Rectangle((int)4.5, 3, Width - 4, Height));
            }

            G.DrawString(Text, Font, H.SolidBrushHTMlColor("a89ea9"), new Rectangle(22, Convert.ToInt32(1.6), Width, Height - 2), new StringFormat { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center });

            e.Graphics.DrawImage(B, 0, 0);
            G.Dispose();
            B.Dispose();
        }
    }

    #endregion

    #region Constructors

    public ModalRadioButton()
    {
        SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint |
                 ControlStyles.OptimizedDoubleBuffer | ControlStyles.SupportsTransparentBackColor, true);
        DoubleBuffered = true;
        Cursor = Cursors.Hand;
        BackColor = Color.Transparent;
        Font = new Font("Ubuntu", 11, FontStyle.Regular);
        UpdateStyles();
    }

    #endregion

    #region  Events

    private void UpdateCheckState()
    {
        if (!IsHandleCreated || !_Checked)
            return;

        foreach (Control C in Parent.Controls)
        {
            if (!object.ReferenceEquals(C, this) && C is ModalRadioButton && ((ModalRadioButton)C).Group == _Group)
            {
                ((ModalRadioButton)C).Checked = false;
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

#region Label

[DefaultEvent("TextChanged")] public class ModalLabel : Control
{
    #region Variables

    private static HelperMethods H = new HelperMethods();

    #endregion

    #region Constructors

    public ModalLabel()
    {
        SetStyle(ControlStyles.SupportsTransparentBackColor | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
        DoubleBuffered = true;
        BackColor = Color.Transparent;
        Font = new Font("Ubuntu", 12, FontStyle.Regular);
        UpdateStyles();
    }

    #endregion

    #region DrawControl

    protected override void OnPaint(PaintEventArgs e)
    {
        e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
        e.Graphics.DrawString(Text, Font, H.SolidBrushHTMlColor("a89ea9"), ClientRectangle);

    }

    #endregion

    #region Events

    protected override void OnResize(EventArgs e)
    {
        base.OnResize(e);
        Height = Font.Height;
    }

    protected override void OnTextChanged(EventArgs e)
    {
        base.OnTextChanged(e);
    }

    #endregion;

}

#endregion

#region Link Label

[DefaultEvent("TextChanged")] public class ModalLinkLabel : Control
{

    #region Variables

    private HelperMethods.MouseMode State;
    private static HelperMethods H = new HelperMethods();
    private string _URL = string.Empty;
    private Color _HoverColor = H.GetHTMLColor("311833");
    private Color _PushedColor = H.GetHTMLColor("431448");

    #endregion

    #region Properties

    public string URL
    {
        get
        {
            return _URL;
        }
        set
        {
            _URL = value;
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

    public Color PushedColor
    {
        get
        {
            return _PushedColor;
        }
        set
        {
            _PushedColor = value;
            Invalidate();
        }
    }

    #endregion

    #region Constructors

    public ModalLinkLabel()
    {
        SetStyle(ControlStyles.SupportsTransparentBackColor | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
        DoubleBuffered = true;
        BackColor = Color.Transparent;
        Font = new Font("Ubuntu", 12, FontStyle.Underline);
        UpdateStyles();
    }

    #endregion

    #region DrawControl

    protected override void OnPaint(PaintEventArgs e)
    {
        e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
         switch(State)
         {
             case HelperMethods.MouseMode.NormalMode:
                 e.Graphics.DrawString(Text, Font, H.SolidBrushHTMlColor("a89ea9"), ClientRectangle);
                    break;
             case HelperMethods.MouseMode.Hovered:
                    Cursor = Cursors.Hand;
                    e.Graphics.DrawString(Text, Font, new SolidBrush(HoverColor), ClientRectangle);
                    break;
             case HelperMethods.MouseMode.Pushed:
                    e.Graphics.DrawString(Text, Font, new SolidBrush(PushedColor), ClientRectangle);
                    break;
         }

    }

    #endregion

    #region Events

    protected override void OnResize(EventArgs e)
    {
        base.OnResize(e);
        Height = Font.Height+2;
    }

    protected override void OnClick(EventArgs e)
    {
        base.OnClick(e);
        if (URL !=null)
        {
            if (!URL.StartsWith("http://www."))
            {
                if (URL == "http://www.")
                {
                    MessageBox.Show("You must put the link to go");
                }
                else
                {
                    URL = "http://www." + URL;
                    System.Diagnostics.Process.Start(URL);
                }
            }
        }
    }

    protected override void OnTextChanged(EventArgs e)
    {
        base.OnTextChanged(e);
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

    #endregion;

}

#endregion

#region Seperator

public class ModalSeperator : Control
{

    #region Variables

    private Style _SepStyle = Style.Horizental;
    private static HelperMethods H = new HelperMethods();

    #endregion

    #region Constructors

    public ModalSeperator()
    {
        SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint |
        ControlStyles.ResizeRedraw | ControlStyles.UserPaint |
        ControlStyles.SupportsTransparentBackColor, true);
        DoubleBuffered = true;
        BackColor = Color.Transparent;       
        UpdateStyles();

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

        using (Bitmap B = new Bitmap(Width, Height))
        using (Graphics G = Graphics.FromImage(B))
        {

            G.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            switch (SepStyle)
            {
                case Style.Horizental:
                    using (System.Drawing.Drawing2D.LinearGradientBrush lb1 = new System.Drawing.Drawing2D.LinearGradientBrush(ClientRectangle, Color.Empty, Color.Empty, 0.0F))
                    {                        
                        G.DrawLine(H.PenHTMlColor("231625", 1), 0, 1, Width, 1);
                    }
                    break;
                case Style.Vertiacal:
                    using (System.Drawing.Drawing2D.LinearGradientBrush lb1 = new System.Drawing.Drawing2D.LinearGradientBrush(ClientRectangle, Color.Empty, Color.Empty, 90.0F))
                    {                       
                        G.DrawLine(H.PenHTMlColor("231625", 1), 1, 0, 1, Height);
                    }
                    break;
            }

            e.Graphics.DrawImage(B, 0, 0);
            G.Dispose();
            B.Dispose();
        }

    }

    #endregion

    #region Events

    protected override void OnResize(EventArgs e)
    {
        base.OnResize(e);
        if (SepStyle == Style.Horizental) { Height = 4; } else { Width = 4; }
    }

    #endregion

}

#endregion

#region Panel

public class ModalPanel : ContainerControl
{

    #region Variables

    private static HelperMethods H = new HelperMethods();

    #endregion

    #region Constructors

    public ModalPanel()
    {

        SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.ResizeRedraw |
            ControlStyles.SupportsTransparentBackColor, true);
        DoubleBuffered = true;
        BackColor = Color.Transparent;
        UpdateStyles();

    }

    #endregion

    #region Draw Control

    protected override void OnPaint(PaintEventArgs e)
    {
        using (Bitmap B = new Bitmap(Width, Height))
        using (Graphics G = Graphics.FromImage(B))
        {
            Rectangle Rect = new Rectangle(0, 0, Width - 1, Height - 1);

            H.FillRoundedPath(G, H.SolidBrushHTMlColor("291a2a"), Rect, 2);

            H.DrawRoundedPath(G, H.GetHTMLColor("231625"), 1, Rect, 2);

            e.Graphics.DrawImage(B, 0, 0);
            G.Dispose();
            B.Dispose();
        }
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
        Invalidate();
    }

    #endregion

}

#endregion

#region GroupBox

public class ModalGroupBox : ContainerControl
{

    #region Variables

    private static HelperMethods H = new HelperMethods();
    private Style _GroupBoxStyle = Style.I;

    #endregion

    #region Draw Control

    protected override void OnPaint(PaintEventArgs e)
    {
        using (Bitmap B = new Bitmap(Width, Height))
        using (Graphics G = Graphics.FromImage(B))
        {
            Rectangle Rect = new Rectangle(0, 0, Width - 1, Height - 1);

            G.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            if (GroupBoxStyle==Style.I)
            {
            H.FillRoundedPath(G, H.SolidBrushHTMlColor("291a2a"), Rect, 2);
            H.DrawRoundedPath(G, H.GetHTMLColor("231625"), 1, Rect, 2);

            G.DrawString(Text, Font, H.SolidBrushHTMlColor("a89ea9"), new Point(5, 6), StringFormat.GenericTypographic);
            G.DrawLine(H.PenHTMlColor("231625", 1), new Point(3, 32), new Point(130, 32));
            }
            else
            {
                H.FillRoundedPath(G, H.SolidBrushHTMlColor("291a2a"), new Rectangle(0, 0, Width - 1, 32), 2);
            H.DrawRoundedPath(G, H.GetHTMLColor("231625"), 1, Rect, 2);

            G.DrawString(Text, Font, H.SolidBrushHTMlColor("a89ea9"), new Point(5, 6), StringFormat.GenericTypographic);
            G.DrawLine(H.PenHTMlColor("231625", 1), new Point(1, 32), new Point(Width - 1, 32));
            }

            e.Graphics.DrawImage(B, 0, 0);
            G.Dispose();
            B.Dispose();
        }
    }

    #endregion

    #region Constructors

    public ModalGroupBox()
    {
        SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor | ControlStyles.ContainerControl, true);
        DoubleBuffered = true;
        Font = new Font("Ubuntu", 11, FontStyle.Regular);
        BackColor = Color.Transparent;
        UpdateStyles();

    }

    #endregion

    #region Properties

    public Style GroupBoxStyle
    {
        get
        {
            return _GroupBoxStyle;
        }
        set
        {
            _GroupBoxStyle = value;
            Invalidate();
        }
    }

    #endregion

    #region Enumerators

    public enum Style
    {
        I,
        O
    };

    #endregion

}

#endregion

#region Progress

public class ModalProgressBar : Control
{

    #region Variables

    private int _Maximum = 100;
    private int _Value;
    protected int CurrentValue;
    private static HelperMethods H = new HelperMethods();


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


    #endregion

    #region Draw Control

    protected override void OnPaint(PaintEventArgs e)
    {
        using (Bitmap B = new Bitmap(Width, Height))
        using (Graphics G = Graphics.FromImage(B))
        {
            Rectangle Rect = new Rectangle(0, 0, Width, Height);

            G.FillRectangle(H.SolidBrushHTMlColor("291a2a"), Rect);
            G.DrawRectangle(H.PenHTMlColor("231625", 1), new Rectangle(0, 0, Width - 1, Height - 1));

            if (CurrentValue != 0)
            {
                G.FillRectangle(new SolidBrush(Color.FromArgb(130, H.GetHTMLColor("5b2960"))), new Rectangle(Rect.X + 1, Rect.Y + 1, CurrentValue - 2, Rect.Height - 2));

            }
            e.Graphics.DrawImage(B, 0, 0);
            G.Dispose();
            B.Dispose();
        }

    }

    #endregion

    #region Constructors

    public ModalProgressBar()
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

#region Trackbar

[DefaultEvent("Scroll")] public class ModalTrackBar : Control
{

    #region Variables

    private int _Maximum = 100;
    private int _Minimum;
    private int _Value;
    private int CurrentValue;
    bool Variable;
    Rectangle Track, TrackSide;
    private static HelperMethods H = new HelperMethods();

    #endregion

    #region Properties

    public int Minimum
    {
        get
        {
            return _Minimum;

        }

        set
        {
            if (!(value < 0))
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

    #region Draw Control

    protected override void OnPaint(PaintEventArgs e)
    {
        using (Bitmap B = new Bitmap(Width, Height))
        using (Graphics G = Graphics.FromImage(B))
        {

            Cursor = Cursors.Hand;
            G.FillRectangle(H.SolidBrushHTMlColor("291a2a"), new Rectangle(0, (int)5.5, Width, 8));
            G.DrawRectangle(H.PenHTMlColor("231625", 1), new Rectangle(0, 5, Width - 1, 8));

            if (CurrentValue != 0)
            {
                G.FillRectangle(new SolidBrush(Color.FromArgb(130, H.GetHTMLColor("5b2960"))), new Rectangle(1, Convert.ToInt32(5.5), CurrentValue + 4, 7));
            }

            G.FillRectangle(H.SolidBrushHTMlColor("291a2a"), Track);
            G.DrawRectangle(H.PenHTMlColor("231625", 1), Track);
            G.FillRectangle(new SolidBrush(Color.FromArgb(130, H.GetHTMLColor("5b2960"))), TrackSide);
            G.DrawRectangle(H.PenHTMlColor("231625", 1), TrackSide);

            e.Graphics.DrawImage(B, 0, 0);
            G.Dispose();
            B.Dispose();
        }
    }

    #endregion

    #region Constructors

    public ModalTrackBar()
    {
        SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint |
        ControlStyles.ResizeRedraw | ControlStyles.UserPaint |
        ControlStyles.SupportsTransparentBackColor, true);
        DoubleBuffered = true;
        BackColor = Color.Transparent;
        UpdateStyles();
        CurrentValue = Convert.ToInt32((Math.Round(Convert.ToDouble(Value / Maximum - 2) * Convert.ToDouble(Width))));
    }

    #endregion

    #region Events

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
            if (Width > 0 && Height > 0) Track = new Rectangle(CurrentValue + Convert.ToInt32(0.8), 0, 25, 24);

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
        if (e.KeyCode == Keys.Subtract || e.KeyCode == Keys.Down || e.KeyCode == Keys.Left)
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

        }
        Height = 19;
        Invalidate();
        base.OnResize(e);
    }

    private void MoveTrack()
    {
        if (Height > 0 && Width > 0) {Track = new Rectangle(CurrentValue ,0, 21, (int)18.5);}
        TrackSide = new Rectangle(CurrentValue + Convert.ToInt32(5.6), 5, 8, 8);
    }

    public void RenewCurrentValue()
    {

        CurrentValue = Convert.ToInt32(Math.Round((double)(Value - Minimum) / (double)(Maximum - Minimum) * (double)(Width - Convert.ToInt32(22.5))));
    }

    #endregion

}

#endregion

#region Control Button

public class ModalControlButton : Control
{

    #region Variables

    private HelperMethods.MouseMode State;
    private static HelperMethods H = new HelperMethods();
    private Style _ControlStyle = Style.Close;

    #endregion

    #region Properties

    public Style ControlStyle
    {
        get
        {
            return _ControlStyle;
        }
        set
        {
            _ControlStyle = value;
            Invalidate();
        }
    }

    #endregion

    #region Enumenators

    public enum Style
    {
        Close,
        Minimize,
        Maximize
    }

    #endregion

    #region Draw Control

    protected override void OnPaint(PaintEventArgs e)
    {
        using (Bitmap B = new Bitmap(Width, Height))
        using (Graphics G = Graphics.FromImage(B))
        {
            G.SmoothingMode = SmoothingMode.AntiAlias;
            G.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            Rectangle R = new Rectangle(0, 0, Convert.ToInt32(18.5), 21);

            switch (State)
            {
                case HelperMethods.MouseMode.NormalMode:

                    H.FillRoundedPath(G, H.SolidBrushHTMlColor("291a2a"), R, 2);
                    H.DrawRoundedPath(G, H.GetHTMLColor("231625"), 1, R, 2);;

                      switch (ControlStyle)
                      {

                          case Style.Close:
                              H.FillRoundedPath(G, new SolidBrush(Color.FromArgb(130, H.GetHTMLColor("5b2960"))), new Rectangle(Convert.ToInt32(5.4), Convert.ToInt32(5.4), 8, 11), 2);
                              H.DrawRoundedPath(G, H.GetHTMLColor("231625"), 1, new Rectangle(Convert.ToInt32(5.4), Convert.ToInt32(5.4), 8, 11), 2);
                              break;
                          case Style.Maximize:
                              H.FillRoundedPath(G, new SolidBrush(Color.FromArgb(130, H.GetHTMLColor("5b2960"))), new Rectangle(Convert.ToInt32(4.5), 6, 9, 9), 2);
                              H.DrawRoundedPath(G, H.GetHTMLColor("231625"), 1, new Rectangle(Convert.ToInt32(4.5), 6, 9, 9), 2);
                              break;
                          case Style.Minimize:
                              H.FillRoundedPath(G, new SolidBrush(Color.FromArgb(130, H.GetHTMLColor("5b2960"))), new Rectangle(3, Convert.ToInt32(7.5), 12, 5), 2);
                              H.DrawRoundedPath(G, H.GetHTMLColor("231625"), 1, new Rectangle(3, Convert.ToInt32(7.5), 12, 5), 2);
                              break;
                      }

                    break;
                case HelperMethods.MouseMode.Hovered:
                    Cursor = Cursors.Hand;

                    H.FillRoundedPath(G, new SolidBrush(Color.FromArgb(130, H.GetHTMLColor("291a2a"))), R, 2);
                    H.DrawRoundedPath(G, Color.FromArgb(130, H.GetHTMLColor("291a2a")), 1, R, 2);;

                      switch (ControlStyle)
                      {

                          case Style.Close:
                              H.FillRoundedPath(G, new SolidBrush(Color.FromArgb(130, H.GetHTMLColor("5b2960"))), new Rectangle(Convert.ToInt32(5.4), Convert.ToInt32(5.4), 8, 11), 2);
                              H.DrawRoundedPath(G, H.GetHTMLColor("231625"), 1, new Rectangle(Convert.ToInt32(5.4), Convert.ToInt32(5.4), 8, 11), 2);
                              break;
                          case Style.Maximize:
                              H.FillRoundedPath(G, new SolidBrush(Color.FromArgb(130, H.GetHTMLColor("5b2960"))), new Rectangle(Convert.ToInt32(4.5), 6, 9, 9), 2);
                              H.DrawRoundedPath(G, H.GetHTMLColor("231625"), 1, new Rectangle(Convert.ToInt32(4.5), 6, 9, 9), 2);
                              break;
                          case Style.Minimize:
                              H.FillRoundedPath(G, new SolidBrush(Color.FromArgb(130, H.GetHTMLColor("5b2960"))), new Rectangle(3, Convert.ToInt32(7.5), 12, 5), 2);
                              H.DrawRoundedPath(G, H.GetHTMLColor("231625"), 1, new Rectangle(3, Convert.ToInt32(7.5), 12, 5), 2);
                              break;
                      }

                    break;
                case HelperMethods.MouseMode.Pushed:

                    H.FillRoundedPath(G, H.SolidBrushHTMlColor("291a2a"), R, 2);
                    H.DrawRoundedPath(G, H.GetHTMLColor("231625"), 1, R, 2);;

                      switch (ControlStyle)
                      {

                          case Style.Close:
                              H.FillRoundedPath(G, new SolidBrush(Color.FromArgb(130, H.GetHTMLColor("5b2960"))), new Rectangle(Convert.ToInt32(5.4), Convert.ToInt32(5.4), 8, 11), 2);
                              H.DrawRoundedPath(G, H.GetHTMLColor("231625"), 1, new Rectangle(Convert.ToInt32(5.4), Convert.ToInt32(5.4), 8, 11), 2);
                              break;
                          case Style.Maximize:
                              H.FillRoundedPath(G, new SolidBrush(Color.FromArgb(130, H.GetHTMLColor("5b2960"))), new Rectangle(Convert.ToInt32(4.5), 6, 9, 9), 2);
                              H.DrawRoundedPath(G, H.GetHTMLColor("231625"), 1, new Rectangle(Convert.ToInt32(4.5), 6, 9, 9), 2);
                              break;
                          case Style.Minimize:
                              H.FillRoundedPath(G, new SolidBrush(Color.FromArgb(130, H.GetHTMLColor("5b2960"))), new Rectangle(3, Convert.ToInt32(7.5), 12, 5), 2);
                              H.DrawRoundedPath(G, H.GetHTMLColor("231625"), 1, new Rectangle(3, Convert.ToInt32(7.5), 12, 5), 2);
                              break;
                      }

                    break;
            }

            e.Graphics.DrawImage(B, 0, 0);
            G.Dispose();
            B.Dispose();
        }
    }

    #endregion

    #region Constructors

    public ModalControlButton()
    {
        SetStyle(ControlStyles.DoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.ResizeRedraw | ControlStyles.UserPaint |
        ControlStyles.SupportsTransparentBackColor, true);
        DoubleBuffered = true;
        BackColor = Color.Transparent;
        Anchor = AnchorStyles.Top | AnchorStyles.Right;
        UpdateStyles();

    }

    #endregion

    #region Events

    protected override void OnClick(EventArgs e)
    {
        base.OnClick(e);
        if (ControlStyle == Style.Close)
        {
            Environment.Exit(0);
            Application.Exit();
        }
        else if (ControlStyle == Style.Minimize)
        {
            if (FindForm().WindowState == FormWindowState.Normal)
            {
                FindForm().WindowState = FormWindowState.Minimized;
            }
        }
        else if (ControlStyle == Style.Maximize)
        {
            if (FindForm().WindowState == FormWindowState.Normal)
            {
                FindForm().WindowState = FormWindowState.Maximized;
            }
            else if (FindForm().WindowState == FormWindowState.Maximized)
            {
                FindForm().WindowState = FormWindowState.Normal;
            }
        }
    }

    protected override void OnResize(EventArgs e)
    {
        base.OnResize(e);
        Size = new Size(20, 23);
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

#region Horizental TabControl

public class ModalHorizentalTabControl : TabControl
{

    #region Variables

    private static HelperMethods H = new HelperMethods();
    protected Color TabColor = H.GetHTMLColor("331f35");
    
    #endregion

    #region Draw Control

    protected override void OnPaint(PaintEventArgs e)
    {
        using (Bitmap B = new Bitmap(Width, Height))
        using (Graphics G = Graphics.FromImage(B))
        {

                G.Clear(H.GetHTMLColor("331f35"));
                G.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
                G.DrawLine(new Pen(Color.FromArgb(130, H.GetHTMLColor("5b2960")), 2), new Point(4, ItemSize.Height + 1), new Point(Width - 4, ItemSize.Height + 1));
                for (int i = 0; i <= TabCount - 1; i++)
                {
                    Rectangle R = GetTabRect(i);
                    if (i == SelectedIndex)
                    {
                        G.DrawLine(H.PenHTMlColor("5b2960", 2), new Point(R.X + 10, ItemSize.Height + 1), new Point(R.X - 10 + R.Width, ItemSize.Height + 1));
                        H.CentreString(G, TabPages[i].Text, Font, H.SolidBrushHTMlColor("5b2960"), R);
                        H.CentreString(G, TabPages[i].Text, Font, new SolidBrush(Color.FromArgb(30, Color.White)), R);
                    }
                    else
                    {
                        H.CentreString(G, TabPages[i].Text, Font, H.SolidBrushHTMlColor("a89ea9"), R);
                    }
                }

            e.Graphics.DrawImage(B, 0, 0);
            G.Dispose();
            B.Dispose();
        }
    }

    #endregion

    #region Constructors

    public ModalHorizentalTabControl()
    {
        SetStyle(ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.SupportsTransparentBackColor, true);
        DoubleBuffered = true;
        Alignment = TabAlignment.Top;
        SizeMode = TabSizeMode.Fixed;
        DrawMode = TabDrawMode.OwnerDrawFixed;
        ItemSize = new Size(110, 35);
        Font = new Font("Ubuntu", 13, FontStyle.Regular, GraphicsUnit.Pixel);
        Dock = DockStyle.None;
        UpdateStyles();

    }

    #endregion

    #region Events

    protected override void OnCreateControl()
    {
        base.OnCreateControl();
        foreach (TabPage Tab in base.TabPages)
        {
            Tab.BackColor = TabColor;
        }
    }

    #endregion

}

#endregion

#region Vertical TabControl

public class ModalVerticalTabControl : TabControl
{

    #region Variables
        
    private static HelperMethods H = new HelperMethods();
    private Color TabColor = H.GetHTMLColor("331f35");
    private bool _ShowBorder = true;

    #endregion

    #region Draw Control

    protected override void OnPaint(PaintEventArgs e)
    {
        using (Bitmap B = new Bitmap(Width, Height))
        using (Graphics G = Graphics.FromImage(B))
        {

            G.Clear(H.GetHTMLColor("331f35"));
            G.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
          
            for (int i = 0; i <= TabCount - 1; i++)
            {
                Rectangle R = GetTabRect(i);
                if (TabPages[i].Tag !=null)
                {
                    G.DrawString(TabPages[i].Text.ToUpper(), Font, new SolidBrush(Color.FromArgb(180, H.GetHTMLColor("5b2960"))), new Point(R.X + 5, R.Y + 9));
                }
                else if (i == SelectedIndex)
                {
                    G.FillRectangle(new SolidBrush(Color.FromArgb(130, H.GetHTMLColor("5b2960"))), R);
                    G.FillRectangle(new SolidBrush(Color.FromArgb(230, H.GetHTMLColor("291a2a"))), new Rectangle(R.X, R.Y, 5, R.Height));
                    H.CentreString(G, TabPages[i].Text, Font, H.SolidBrushHTMlColor("5b2960"), R);                   
                }
                else
                {
                    H.CentreString(G, TabPages[i].Text, Font, H.SolidBrushHTMlColor("a89ea9"), R);
                }
            }
               if (ShowBorder)
               {
                   G.DrawRectangle(H.PenHTMlColor("291a2a", 1), new Rectangle(0, 0, Width - 1, Height - 1));
               }
            e.Graphics.DrawImage(B, 0, 0);
            G.Dispose();
            B.Dispose();
        }
    }

    #endregion

    #region Constructors

    public ModalVerticalTabControl()
    {
        SetStyle(ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.SupportsTransparentBackColor, true);
        DoubleBuffered = true;
        SizeMode = TabSizeMode.Fixed;
        ItemSize = new Size(30, 120);
        DrawMode = TabDrawMode.OwnerDrawFixed;
        Alignment = TabAlignment.Left;
        Dock = DockStyle.None;
        Font = new Font("Ubuntu", 13, FontStyle.Regular, GraphicsUnit.Pixel);
        UpdateStyles();

    }

    #endregion

    #region Events

    protected override void OnCreateControl()
    {
        base.OnCreateControl();
        foreach (TabPage Tab in base.TabPages)
        {
            Tab.BackColor = TabColor;
        }
    }

    #endregion

    #region Properties

    public bool ShowBorder
    {
        get
        {
            return _ShowBorder;
        }
        set
        {
            _ShowBorder = value;
            Invalidate();
        }
    }

    #endregion

}

#endregion

#region RichTextbox

[DefaultEvent("TextChanged")] public class ModalRichTextbox : Control
{

    #region Variables

    private RichTextBox T = new RichTextBox();

    private bool _ReadOnly;
    protected HelperMethods.MouseMode State = HelperMethods.MouseMode.NormalMode;
    private static HelperMethods H = new HelperMethods();
    private static Color TBC = H.GetHTMLColor("291a2a");
    private static Color TFC = H.GetHTMLColor("a89ea9");
    private bool _WordWrap;
    private bool _AutoWordSelection;
    private Color _BackColor = TBC;
    #endregion

    #region  Native Methods
    [System.Runtime.InteropServices.DllImport("user32.dll")]
    private static extern Int32 SendMessage(IntPtr hWnd, int msg, int wParam, [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.LPWStr)]
string lParam);

    #endregion

    #region Draw Control

    protected override void OnPaint(PaintEventArgs e)
    {
        using (Bitmap B = new Bitmap(Width, Height))
        using (Graphics G = Graphics.FromImage(B))
        {

            G.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            Rectangle Rect = new Rectangle(0, 0, Width - 1, Height - 1);
            
            G.FillRectangle(H.SolidBrushHTMlColor("291a2a"), Rect);
            G.DrawRectangle(H.PenHTMlColor("231625", 1), Rect);

            if (ContextMenuStrip != null) { T.ContextMenuStrip = ContextMenuStrip; }

            e.Graphics.DrawImage(B, 0, 0);
            G.Dispose();
            B.Dispose();
        }
    }

    #endregion

    #region Constructors

    public ModalRichTextbox()
    {
        SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint |
                ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.SupportsTransparentBackColor, true);
        DoubleBuffered = true;
        UpdateStyles();
        Font = new Font("Arial", 11, FontStyle.Regular);
        Size = new Size(135, 30);
        T.Multiline = true;
        T.Cursor = Cursors.IBeam;
        T.BackColor = TBC;
        T.ForeColor = TFC;
        T.BorderStyle = BorderStyle.None;
        T.Location = new Point(7, 7);
        T.Font = Font;
        T.Size = new Size(Width - 10, 30);
        T.TextChanged += T_TextChanged;
        T.MouseDown += T_MouseDown;
        T.MouseEnter += T_MouseEnter;
        T.MouseUp += T_MouseUp;
        T.MouseLeave += T_MouseLeave;
        T.MouseHover += T_MouseHover;
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
            return true;
        }
       
    }

    public bool ReadOnly
    {
        get { return _ReadOnly; }
        set
        {
            _ReadOnly = value;
            if (T != null)
            {
                T.ReadOnly = value;
            }
        }
    }

    [Browsable(false)] public override Image BackgroundImage
    {
        get
        {
            return null;
        }      
    }

    [Browsable(false)] public override ImageLayout BackgroundImageLayout
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
            if (T != null)
            {
                T.Text = value;
            }
        }
    }

    override public Color BackColor
    {

        get { return Color.Transparent; }
    
    }

    override public Color ForeColor
    {

        get { return Color.Transparent; }

    }

    public bool WordWrap
    {
        get
        {
            return _WordWrap;
        }
        set
        {
            _WordWrap = value;
            if (T != null)
            {
                T.WordWrap = value;
            }
        }
    }

    public bool AutoWordSelection
    {
        get
        {
            return _AutoWordSelection;
        }
        set
        {
            _AutoWordSelection = value;
            if(T !=null)
            {
                T.AutoWordSelection = value;
            }
        }
    }

    #endregion

    #region Events

    private void T_MouseHover(object sender, EventArgs e)
    {
        State = HelperMethods.MouseMode.Hovered;
        Invalidate();
    }

    private void T_MouseLeave(object sender, EventArgs e)
    {
        State = HelperMethods.MouseMode.NormalMode;
        Invalidate();
    }

    private void T_MouseUp(object sender, EventArgs e)
    {
        State = HelperMethods.MouseMode.Pushed;
        Invalidate();
    }

    private void T_MouseEnter(object sender, EventArgs e)
    {
        State = HelperMethods.MouseMode.Pushed;
        Invalidate();
    }

    private void T_MouseDown(object sender, EventArgs e)
    {
        State = HelperMethods.MouseMode.Pushed;
        Invalidate();
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
        if (!Controls.Contains(T)) { Controls.Add(T); }
   

    }

    protected override void OnFontChanged(EventArgs e)
    {
        base.OnFontChanged(e);
        T.Font = Font;
    }

    protected override void OnSizeChanged(EventArgs e)
    {
        base.OnSizeChanged(e);
        T.Size = new Size(Width - 10, Height - 5);
    }

    #endregion


}

#endregion

#region Switch

[DefaultEvent("Switch")] public class ModalSwitch : Control
{

    #region Variables

    private bool _Switched = false;
    private static HelperMethods H = new HelperMethods();

    #endregion

    #region Constructors

    public ModalSwitch()
    {
        SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint |
        ControlStyles.OptimizedDoubleBuffer | ControlStyles.SupportsTransparentBackColor, true);
        DoubleBuffered = true;
        UpdateStyles();
        BackColor = Color.Transparent;
        Cursor = Cursors.Hand;
        Font = new Font("Ubuntu", 10, FontStyle.Regular);
        Size = new Size(70, 28);
    }

    #endregion

    #region Properties

    public bool Switched
    {
        get { return _Switched; }

        set 
        { _Switched = value;
        Invalidate();
        }
    }

    #endregion

    #region Draw Control

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

                G.FillRectangle(new SolidBrush(Color.FromArgb(130, H.GetHTMLColor("5b2960"))), new Rectangle(0, 0, 70, 27));

                G.FillRectangle(H.SolidBrushHTMlColor("291a2a"), new Rectangle(Width - Convert.ToInt32(28.5), Convert.ToInt32(1.5), 25, 23));
                G.DrawRectangle(H.PenHTMlColor("231625", 1), new Rectangle(Width - Convert.ToInt32(28.5), Convert.ToInt32(1.5), 25, 23));
                G.DrawLine(H.PenHTMlColor("5b2960", 1), Width - 13, 8, Width - 13, 18);
                G.DrawLine(H.PenHTMlColor("5b2960", 1), Width - 16, 7, Width - 16, 19);
                G.DrawLine(H.PenHTMlColor("5b2960", 1), Width - 19, 8, Width - 19, 18);

                G.DrawString("ON", Font, Brushes.Silver, new Point(Width - 62, 5));
            }
            else
            {
                G.FillRectangle(H.SolidBrushHTMlColor("291a2a"), new Rectangle(0, 0, 70, 27));

                G.FillRectangle(new SolidBrush(Color.FromArgb(130, H.GetHTMLColor("5b2960"))), new Rectangle(3, Convert.ToInt32(1.5), 25, 23));
                G.DrawRectangle(H.PenHTMlColor("231625", 1), new Rectangle(3, Convert.ToInt32(1.5), 25, 23));

                G.DrawLine(H.PenHTMlColor("231625", 1), 12, 8, 12, 18);
                G.DrawLine(H.PenHTMlColor("231625", 1), 15, 7, 15, 19);
                G.DrawLine(H.PenHTMlColor("231625", 1), 18, 8, 18, 18);

                G.DrawString("OFF", Font, H.SolidBrushHTMlColor("a89ea9"), new Point(33, 5));
            }

            G.DrawRectangle(H.PenHTMlColor("231625", 1), new Rectangle(0, 0, 69, 27));

        e.Graphics.DrawImage(B, 0, 0);
        G.Dispose();
        B.Dispose();
        }
   
    }

    #endregion

    #region Events

    public event CheckedChangedEventHandler Switch;
    public delegate void CheckedChangedEventHandler(object sender);

    protected override void OnClick(EventArgs e)
    {

        _Switched = !_Switched;
        if (Switch != null)
        {
            Switch(this);
        }
        base.OnClick(e);
        Invalidate();
    }

    protected override void OnResize(EventArgs e)
    {
        base.OnResize(e);
        Size = new Size(70, 28);
    }

    #endregion

}

#endregion

#region ContextMenuStrip

public class ModalContextMenuStrip : ContextMenuStrip
{

    #region Variables

    private ToolStripItemClickedEventArgs ClickedEventArgs;
    private static HelperMethods H = new HelperMethods();

    #endregion

    #region Constructors

    public ModalContextMenuStrip()
    {
        Renderer = new ModalToolStripRender();
        BackColor = H.GetHTMLColor("291a2a");
    }

    #endregion

    #region Events

    public event ClickedEventHandler Clicked;
    public delegate void ClickedEventHandler(object sender);

    protected override void OnItemClicked(ToolStripItemClickedEventArgs e)
    {
        if ((e.ClickedItem != null) && !(e.ClickedItem is ToolStripSeparator))
        {
            if (object.ReferenceEquals(e, ClickedEventArgs))
                OnItemClicked(e);
            else
            {
                ClickedEventArgs = e; if (Clicked != null)
                {
                    Clicked(this);
                }
            }
        }
    }

    protected override void OnMouseHover(EventArgs e)
    {
        base.OnMouseHover(e);
        Cursor = Cursors.Hand;
        Invalidate();
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
        base.OnMouseUp(e);
        Cursor = Cursors.Hand;
        Invalidate();
    }

    #endregion
    
    #region ModalToolStripMenuItem

public sealed class ModalToolStripMenuItem : ToolStripMenuItem
    {

        #region Constructors 

        public ModalToolStripMenuItem()
        {
            AutoSize = false;
            Size = new Size(160, 30);
        }

        #endregion

        #region Adding DropDowns 

        protected override ToolStripDropDown CreateDefaultDropDown()
        {
            if (DesignMode)
            { return base.CreateDefaultDropDown(); }
            ModalContextMenuStrip DP = new ModalContextMenuStrip();
            DP.Items.AddRange(base.CreateDefaultDropDown().Items);
            return DP;
        }

        #endregion

    }

    #endregion

    #region ModalToolStripRender

    public sealed class ModalToolStripRender : ToolStripProfessionalRenderer
    {

        #region Drawing Text

        protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
        {
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            Rectangle textRect = new Rectangle(25, e.Item.ContentRectangle.Y, e.Item.ContentRectangle.Width - (24 + 16), e.Item.ContentRectangle.Height - 4);
            using (Font F = new Font("Ubuntu", 11, FontStyle.Regular))
            {
                using (Brush B = e.Item.Enabled ? H.SolidBrushHTMlColor("a89ea9") : new SolidBrush(Color.FromArgb(70, H.GetHTMLColor("5b2960"))))
                {
                    using (StringFormat ST = new StringFormat { LineAlignment = StringAlignment.Center })
                    {
                        e.Graphics.DrawString(e.Text, F, B, textRect);
                    }
                }
            }
        }

        #endregion

        #region Drawing Backgrounds

        protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
        {
            base.OnRenderToolStripBackground(e);   
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.InterpolationMode = InterpolationMode.High;
            e.Graphics.Clear(H.GetHTMLColor("291a2a"));
        }

        protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
        {
            e.Graphics.InterpolationMode = InterpolationMode.High;
            e.Graphics.Clear(H.GetHTMLColor("291a2a"));
            Rectangle R = new Rectangle(0, e.Item.ContentRectangle.Y - 2, e.Item.ContentRectangle.Width + 4, e.Item.ContentRectangle.Height + 3);

            e.Graphics.FillRectangle(e.Item.Selected && e.Item.Enabled ? new SolidBrush(Color.FromArgb(130, H.GetHTMLColor("5b2960"))) : H.SolidBrushHTMlColor("291a2a"), R);

        }

        #endregion

        #region Set Image Margin 

        protected override void OnRenderImageMargin(ToolStripRenderEventArgs e)
        {
            //MyBase.OnRenderImageMargin(e) 
            //I Make above line comment which makes users to be able to add images to ToolStrips
        }

        #endregion

        #region Drawing Seperators & Borders 

        protected override void OnRenderSeparator(ToolStripSeparatorRenderEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.DrawLine(new Pen(Color.FromArgb(200, H.GetHTMLColor("231625")), 1), new Point(e.Item.Bounds.Left, e.Item.Bounds.Height / 2), new Point(e.Item.Bounds.Right - 5, e.Item.Bounds.Height / 2));
        }

        protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
        {
            e.Graphics.InterpolationMode = InterpolationMode.High;
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            H.DrawRoundedPath(e.Graphics, H.GetHTMLColor("231625"), 1, new Rectangle(e.AffectedBounds.X, e.AffectedBounds.Y, e.AffectedBounds.Width - 1, e.AffectedBounds.Height - 1), 4);
        }

        #endregion

        #region Drawing DropDown Arrows

        protected override void OnRenderArrow(ToolStripArrowRenderEventArgs e)
        {
            int ArrowX,ArrowY;
            ArrowX = e.ArrowRectangle.X + e.ArrowRectangle.Width / 2;
            ArrowY = e.ArrowRectangle.Y + e.ArrowRectangle.Height / 2;
            Point[] ArrowPoints = new Point[] {
				new Point(ArrowX - 5, ArrowY - 5),
				new Point(ArrowX, ArrowY),
				new Point(ArrowX - 5, ArrowY + 5)
			};
            if (e.Item.Enabled)
            {
                e.Graphics.FillPolygon(new SolidBrush(Color.FromArgb(130, H.GetHTMLColor("5b2960"))), ArrowPoints);
            }
            else
            {
                e.Graphics.FillPolygon(new SolidBrush(Color.FromArgb(40, H.GetHTMLColor("5b2960"))), ArrowPoints);
            }
        }

        #endregion

    }

    #endregion

}

#endregion

#region NumericalUpDown

public class ModalNumericUpDown : Control
{

    #region  Variables

    private int X, Y, _Value, _Maximum, _Minimum;
    private static HelperMethods H = new HelperMethods();

    #endregion

    #region Draw Control

    protected override void OnPaint(PaintEventArgs e)
    {
        using (Bitmap B = new Bitmap(Width, Height))
        using (Graphics G = Graphics.FromImage(B))
        {

                G.SmoothingMode = SmoothingMode.AntiAlias;
                G.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            Rectangle Rect = new Rectangle(0,0, Width - 1, Height - 1);
            
                G.FillRectangle(H.SolidBrushHTMlColor("291a2a"), Rect);

                G.FillPath(new SolidBrush(Color.FromArgb(130, H.GetHTMLColor("5b2960"))), H.RoundRec(new Rectangle(Width - 25, Convert.ToInt32(0.6), 23, Height - Convert.ToInt32(2.6)), 2));
                G.DrawLine(H.PenHTMlColor("231625", 1), new Point(Width - 25, 1), new Point(Width - 25, Height - 2));
                G.DrawRectangle(H.PenHTMlColor("231625", 1), Rect);
                using (GraphicsPath AboveWardTriangle = new GraphicsPath())
                 {
                AboveWardTriangle.AddLine(Width - 17, 10, Width - 2, 10);
                    AboveWardTriangle.AddLine(Width - 9, 10, Width - 13, 4);
                    AboveWardTriangle.CloseFigure();
                    G.FillPath(H.SolidBrushHTMlColor("291a2a"), AboveWardTriangle);
                 }

                using (GraphicsPath DownWardTriangle = new GraphicsPath())
                 {
                    DownWardTriangle.AddLine(Width - 17, 13, Width - 2, 13);
                    DownWardTriangle.AddLine(Width - 9, 13, Width - 13, 19);
                    DownWardTriangle.CloseFigure();
                    G.FillPath(H.SolidBrushHTMlColor("291a2a"), DownWardTriangle);
                 }

                H.CentreString(G, Value.ToString(), Font, H.SolidBrushHTMlColor("a89ea9"), new Rectangle(0, 0, Width - 18, Height - 1));
            

            e.Graphics.DrawImage(B, 0, 0);
            G.Dispose();
            B.Dispose();
        }
    }

    #endregion

    #region Properties

    public int Value
    {
        get
        {
            return _Value;
        }
        set
        {
            if (value <= Maximum & value >= Minimum) { _Value = value; }
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
            if (value > Minimum) { _Maximum = value; }
            if (value > _Maximum) { value = _Maximum; }
            Invalidate();
        }
    }

    public int Minimum
    {

        get
        {
            return _Minimum;
        }
        set
        {
            if (value < Maximum) { _Minimum = value; }
            if (value < _Minimum) { value = _Minimum; }
            Invalidate();
        }

    }


    #endregion

    #region Constructors

    public ModalNumericUpDown()
    {
        SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.SupportsTransparentBackColor, true);
        DoubleBuffered = true;
        BackColor = Color.Transparent;
        Font = new Font("Ubuntu", 10, FontStyle.Regular);
        UpdateStyles();
    }

    #endregion

    #region Events

    protected override void OnCreateControl()
    {
        base.OnCreateControl();

    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
        base.OnMouseMove(e);
        X = e.Location.X;
        Y = e.Location.Y;
        Invalidate();
        if (e.X < Width - 24) { Cursor = Cursors.IBeam; } else { Cursor = Cursors.Default; }
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
        base.OnMouseDown(e);
        if (X > Width - 18 && X < Width - 4)
        {
            if (Y < 14)
            {
                if ((Value + 1) <= Maximum) { Value += 1; }
            }
        }
        else
        {
            if ((Value - 1) >= Minimum) { Value -= 1; }

        }
        Invalidate();
    }

    protected override void OnResize(EventArgs e)
    {
        base.OnResize(e);

        Invalidate();
    }

    #endregion

}

#endregion

#region ListBox

public class ModalListBox : Control
{

    #region Variables

    private ListBox LB = new ListBox();
    private static HelperMethods H = new HelperMethods();
    private string[] _Items = new string[]{string.Empty};

    #endregion

    #region Properties

    public string[] Items
    {
        get { return _Items; }
        set
        {
            _Items = value;
            LB.Items.Clear();
            LB.Items.AddRange(value);
            Invalidate();
        }
    }

    public string SelectedItem
    {
        get 
        { 
            return LB.SelectedItem.ToString();           
        }
    }

    public int SelectedIndex
    {
        get
        {
            
            if (LB.SelectedIndex < 0)
            {
               return 0;
            }
            else
            {
               return LB.SelectedIndex;   
            }
        }
    }

    public void Clear()
    {
        LB.Items.Clear();
    }

    #endregion

    #region Events

    public void ClearSelected()
    {
        for (int i = (LB.SelectedItems.Count - 1); i >= 0; i += -1)
        {
            LB.Items.Remove(LB.SelectedItems[i]);
        }
    }

    protected override void OnCreateControl()
    {
        base.OnCreateControl();
        LB.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
        LB.ScrollAlwaysVisible = false;
        LB.HorizontalScrollbar = false;
        LB.BorderStyle = BorderStyle.None;
        LB.ItemHeight = 20;
        LB.IntegralHeight = false;
        LB.DrawItem += OnDrawItem;
        if (!Controls.Contains(LB))
        {
            Controls.Add(LB);
        }
    }

    public void AddRange(object[] items)
    {
        LB.Items.Remove(string.Empty);
        LB.Items.AddRange(items);
    }

    public void AddItem(object item)
    {
        LB.Items.Remove(string.Empty);
        LB.Items.Add(item);
    }

    private void LB_SelectIndexChanged(object sender, EventArgs e)
    {
        LB.Invalidate();
    }

    #endregion

    #region Constructors

    public ModalListBox()
    {
        SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer | ControlStyles.SupportsTransparentBackColor, true);
        DoubleBuffered = true;
        Font = new Font("Ubuntu", 10, FontStyle.Regular);
        BackColor = Color.Transparent;
        LB.BackColor = H.GetHTMLColor("291a2a");
        LB.ForeColor = H.GetHTMLColor("a89ea9");
        LB.Location = new Point(2, 2);
        LB.SelectedIndexChanged += LB_SelectIndexChanged;
        LB.Font = Font;
        LB.Items.Clear();
        Size = new Size(130, 100);
    }

    #endregion

    #region Draw Control

    protected void OnDrawItem(object sender, DrawItemEventArgs e)
    {
        //e.DrawBackground();

        using (Bitmap B = new Bitmap(Width, Height))
        {
            using (Graphics G = Graphics.FromImage(B))
            {
                G.SmoothingMode = SmoothingMode.HighQuality;
                G.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

                if (e.Index < 0 | Items.Length < 1)
                    return;


                Rectangle MainRect = new Rectangle(e.Bounds.X, e.Bounds.Y, e.Bounds.Width + 2, e.Bounds.Height - 1);
                Rectangle ItemRect = new Rectangle(e.Bounds.X-1, e.Bounds.Y + 4, e.Bounds.Width, e.Bounds.Height +2);

                G.FillRectangle(H.SolidBrushHTMlColor("291a2a"), ItemRect);
                

                if (e.State > 0)
                {
                    G.FillRectangle(new SolidBrush(Color.FromArgb(130, H.GetHTMLColor("5b2960"))), ItemRect);
                    G.DrawRectangle(H.PenHTMlColor("231625", 1), ItemRect);
                    G.DrawString(Items[e.Index].ToString(), Font, H.SolidBrushHTMlColor("a89ea9"), 3, e.Bounds.Y + 4);
                    
                }
                else
                {
                    G.DrawString(Items[e.Index].ToString(), Font, H.SolidBrushHTMlColor("a89ea9"), 3, e.Bounds.Y + 4);

                }

                
                e.Graphics.DrawImage(B, 0, 0);
                G.Dispose();
                B.Dispose();
          
            }
        }

    }
    
    protected override void OnPaint(PaintEventArgs e)
    {
        using (Bitmap B = new Bitmap(Width, Height))
        {
            using (Graphics G = Graphics.FromImage(B))
            {

                G.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

                Rectangle MainRect = new Rectangle(0, 0, Width - 1, Height - 1);

                LB.Size = new Size(Width - 6, Height - 5);
                G.FillRectangle(H.SolidBrushHTMlColor("291a2a"), MainRect);
                G.DrawRectangle(H.PenHTMlColor("231625", 1), MainRect);


                e.Graphics.DrawImage(B, 0, 0);
                G.Dispose();
                B.Dispose();

            }
        }
    }

    #endregion

}

#endregion