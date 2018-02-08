/// <summary>
/// Acacia Theme
/// Author : THE LORD
/// Release Date : Tuesday, January 10, 2017
/// </summary>

#region Namespaces

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading;
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
    { G.DrawString(Text, font, brush, new Rectangle(0, Rect.Y + Convert.ToInt32(Rect.Height / 2) - Convert.ToInt32(G.MeasureString(Text, font).Height / 2) + 0, Rect.Width, Rect.Height), new StringFormat() { Alignment = StringAlignment.Center }); }

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

#region Form

public class AcaciaSkin : ContainerControl
{

    #region    Variables

    bool Movable = false;
    private TitlePostion _TitleTextPostion = TitlePostion.Left;
    private Point MousePoint = new Point(0, 0);
    private int MoveHeight = 50;
    private static HelperMethods H = new HelperMethods();

    #endregion

    #region Draw Control

    protected override void OnPaint(PaintEventArgs e)
    {
        using (Bitmap B = new Bitmap(Width, Height))
        using (Graphics G = Graphics.FromImage(B))
        {

            G.Clear(Color.Fuchsia);

            G.FillRectangle(H.SolidBrushHTMlColor("24273e"), new Rectangle(0, 0, Width, Height));

            G.FillRectangle(H.SolidBrushHTMlColor("1e2137"), new Rectangle(0, 0, Width, 55));

            G.DrawLine(H.PenHTMlColor("1d1f38", 1), new Point(0, 55), new Point(Width, 55));

            G.DrawRectangle(H.PenHTMlColor("1d1f38", 1), new Rectangle(0, 0, Width - 1, Height - 1));

            if (FindForm().ShowIcon)
            {
                if (FindForm().Icon != null)
                {
                    switch (TitleTextPostion)
                    {
                        case TitlePostion.Left:
                            G.DrawString(Text, Font, H.SolidBrushHTMlColor("e4ecf2"), 27, 16);
                            G.DrawIcon(FindForm().Icon, new Rectangle(5, 16, 20, 20));
                            break;
                        case TitlePostion.Center:
                            H.CentreString(G, Text, Font, H.SolidBrushHTMlColor("e4ecf2"), new Rectangle(0, 0, Width, 50));
                            G.DrawIcon(FindForm().Icon, new Rectangle(5, 16, 20, 20));
                            break;
                        case TitlePostion.Right:
                            H.RightString(G, Text, Font, H.SolidBrushHTMlColor("e4ecf2"), new Rectangle(0, 0, Width, 50));
                            G.DrawIcon(FindForm().Icon, new Rectangle(Width - 30, 17, 20, 20));
                            break;
                    }
                }

            }
            else
            {
                switch (TitleTextPostion)
                {
                    case TitlePostion.Left:
                        G.DrawString(Text, Font, H.SolidBrushHTMlColor("e4ecf2"), 5, 16);
                        break;
                    case TitlePostion.Center:
                        H.CentreString(G, Text, Font, H.SolidBrushHTMlColor("e4ecf2"), new Rectangle(0, 0, Width, 50));
                        break;
                    case TitlePostion.Right:
                        H.RightString(G, Text, Font, H.SolidBrushHTMlColor("e4ecf2"), new Rectangle(0, 0, Width, 50));
                        break;
                }
            }
            e.Graphics.DrawImage(B, 0, 0);
            G.Dispose();
            B.Dispose();
        }
    }

    #endregion

    #region  Initialization

    public AcaciaSkin()
    {

        SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor | ControlStyles.ContainerControl, true);
        DoubleBuffered = true;
        BackColor = Color.Transparent;
        Font = new Font("Arial", 12, FontStyle.Bold);
        UpdateStyles();

    }

    #endregion

    #region  Properties

    private bool _ShowIcon = false;
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
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
            MessageBox.Show(ex.StackTrace);
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

#region Button

public class AcaciaButton : Control
{

    #region Variables

    private HelperMethods.MouseMode State;
    private Image _SideImage;
    private SideAligin _SideImageAlign = SideAligin.Left;
    private static HelperMethods H = new HelperMethods();
    private int _RoundRadius = 10;

    #endregion

    #region Draw Control

    protected override void OnPaint(PaintEventArgs e)
    {
        using (Bitmap B = new Bitmap(Width, Height))
        using (Graphics G = Graphics.FromImage(B))
        {
            G.SmoothingMode = SmoothingMode.AntiAlias;
            G.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            Rectangle R = new Rectangle(2, 2, Width - 5, Height - 5);

            switch(State)
            {
                case HelperMethods.MouseMode.NormalMode:
                       using(PathGradientBrush HB   = new PathGradientBrush(H.RoundRec(new Rectangle(0, 0, Width, Height), 2)))
                          {
                        H.FillRoundedPath(G, H.SolidBrushHTMlColor("fc3955"), R, 2);
                        HB.WrapMode = WrapMode.Clamp;
                        ColorBlend CB=new ColorBlend(4);                        
                        CB.Colors = new Color[] {Color.FromArgb(220, H.GetHTMLColor("fc3955")), Color.FromArgb(220, H.GetHTMLColor("fc3955")), Color.FromArgb(220, H.GetHTMLColor("fc3955")), Color.FromArgb(220, H.GetHTMLColor("fc3955"))};
                        CB.Positions = new Single[] {0.0F, 0.2F, 0.8F, 1.0F};
                        HB.InterpolationColors = CB;
                        H.FillRoundedPath(G, HB, new Rectangle(0, 0, Width - 1, Height - 1), 2);
                         }

                    break;
                case HelperMethods.MouseMode.Hovered:
                    using (PathGradientBrush HB = new PathGradientBrush(H.RoundRec(new Rectangle(0, 0, Width, Height), 2)))
                    {
                        H.FillRoundedPath(G,new SolidBrush(Color.FromArgb(150, H.GetHTMLColor("fc3955"))), R, 2);
                        HB.WrapMode = WrapMode.Clamp;
                        ColorBlend CB = new ColorBlend(4);
                        CB.Colors = new Color[] { Color.FromArgb(150, H.GetHTMLColor("fc3955")), Color.FromArgb(150, H.GetHTMLColor("fc3955")), Color.FromArgb(150, H.GetHTMLColor("fc3955")), Color.FromArgb(150, H.GetHTMLColor("fc3955")) };
                        CB.Positions = new Single[] { 0.0F, 0.2F, 0.8F, 1.0F };
                        HB.InterpolationColors = CB;
                        H.FillRoundedPath(G, HB, new Rectangle(0, 0, Width - 1, Height - 1), 2);
                    }

                    break;
                case HelperMethods.MouseMode.Pushed:
                    using (PathGradientBrush HB = new PathGradientBrush(H.RoundRec(new Rectangle(0, 0, Width, Height), 2)))
                    {
                        H.FillRoundedPath(G, H.SolidBrushHTMlColor("fc3955"), R, 2);
                        HB.WrapMode = WrapMode.Clamp;
                        ColorBlend CB = new ColorBlend(4);
                        CB.Colors = new Color[] { Color.FromArgb(220, H.GetHTMLColor("fc3955")), Color.FromArgb(220, H.GetHTMLColor("fc3955")), Color.FromArgb(220, H.GetHTMLColor("fc3955")), Color.FromArgb(220, H.GetHTMLColor("fc3955")) };
                        CB.Positions = new Single[] { 0.0F, 0.2F, 0.8F, 1.0F };
                        HB.InterpolationColors = CB;
                        H.FillRoundedPath(G, HB, new Rectangle(0, 0, Width - 1, Height - 1), 2);
                    }

                    break;
            }

               if (SideImage !=null)
               {
                if (SideImageAlign == SideAligin.Right)
                {
                    G.DrawImage(SideImage, new Rectangle(R.Width - 24, R.Y + 7, 16, 16));
                }
                else
                {
                    G.DrawImage(SideImage, new Rectangle(8, R.Y + 7, 16, 16));
                }
               }
               H.CentreString(G, Text, Font, H.SolidBrushHTMlColor("e4ecf2"), R);

            e.Graphics.DrawImage(B, 0, 0);
            G.Dispose();
            B.Dispose();
        }
    }

    #endregion

    #region Properties

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

    [Browsable(true)]
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

    #endregion

    #region Initialization

    public AcaciaButton()
    {
        SetStyle(ControlStyles.DoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.ResizeRedraw | ControlStyles.UserPaint |
        ControlStyles.Selectable | ControlStyles.SupportsTransparentBackColor, true);
        DoubleBuffered = true;
        BackColor = Color.Transparent;
        Font = new Font("Myriad Pro", 12, FontStyle.Bold);
        UpdateStyles();

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

    #region Enumerators

    public enum SideAligin
    {
        Left,
        Right
    };

    #endregion


}

#endregion

#region Textbox

[DefaultEvent("TextChanged")]public class AcaciaTextbox : Control
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
    private static Color TBC = H.GetHTMLColor("24273e");
    private static Color TFC = H.GetHTMLColor("585c73");
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

            G.SmoothingMode = SmoothingMode.HighQuality;
            G.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            Rectangle Rect = new Rectangle(0, 0, Width - 1, Height - 1);
            Height = 30;
            switch (State)
            {
                case HelperMethods.MouseMode.NormalMode:
                    G.DrawLine(H.PenHTMlColor("585c73", 1), new Point(0, 29), new Point(Width, 29));
                    break;
                case HelperMethods.MouseMode.Hovered:
                    G.DrawLine(H.PenHTMlColor("fc3955", 1), new Point(0, 29), new Point(Width, 29));
                    break;
                case HelperMethods.MouseMode.Pushed:
                    G.DrawLine(new Pen(Color.FromArgb(150, H.GetHTMLColor("fc3955")), 1), new Point(0, 29), new Point(Width, 29));
                    break;
            }


            if (SideImage != null)
            {
                
                    T.Location = new Point(33, Convert.ToInt32(4.5));
                    T.Width = Width - 60;
                    G.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    G.DrawImage(SideImage, new Rectangle(8, 6, 16, 16));
              
            }
            else
            {
                T.Location = new Point(7, Convert.ToInt32(4.5));
                T.Width = Width - 10;
            }

            if (ContextMenuStrip != null) { T.ContextMenuStrip = ContextMenuStrip; }

            e.Graphics.DrawImage(B, 0, 0);
            G.Dispose();
            B.Dispose();
        }
    }

    #endregion

    #region Initialization

    public AcaciaTextbox()
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

#region Panel

public class AcaciaPanel : ContainerControl
{

    #region Variables

    private static HelperMethods H = new HelperMethods();

    #endregion

    #region Initialization

    public AcaciaPanel()
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
            Rectangle Rect =  new Rectangle(0,0,Width-1,Height-1);

            G.FillRectangle(H.SolidBrushHTMlColor("24273e"), Rect);
            G.DrawRectangle(H.PenHTMlColor("1d1f38", 1), Rect);

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

#region CheckBox

[DefaultEvent("CheckedChanged")]public class AcaciaCheckBox : Control
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
            G.SmoothingMode = SmoothingMode.AntiAlias;
            G.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            if(Checked)
            {
                H.DrawRoundedPath(G, H.GetHTMLColor("fc3955"),Convert.ToSingle(2.5), new Rectangle(1, 1, 17, 17), 1);

                H.FillRoundedPath(G, H.SolidBrushHTMlColor("fc3955"), new Rectangle(5, 5, 9, 9), 1);

                G.DrawString(Text, Font, H.SolidBrushHTMlColor("e4ecf2"), new Rectangle(22, Convert.ToInt32(1.6), Width, Height - 2), new StringFormat { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center });
            }
            else
            {
                switch(State)
                {
                    case HelperMethods.MouseMode.NormalMode:

                        H.DrawRoundedPath(G, Color.FromArgb(150, H.GetHTMLColor("fc3955")), Convert.ToSingle(2.5), new Rectangle(1, 1, 17, 17), 1);

                        G.DrawString(Text, Font, new SolidBrush(Color.Silver), new Rectangle(22,Convert.ToInt32(1.6), Width, Height - 2), new StringFormat {Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center});
                        break;
                    case HelperMethods.MouseMode.Hovered:

                        H.DrawRoundedPath(G, H.GetHTMLColor("fc3955"), Convert.ToSingle(2.5), new Rectangle(1, 1, 17, 17), 1);

                        H.FillRoundedPath(G, H.SolidBrushHTMlColor("fc3955"), new Rectangle(5, 5, 9, 9), 1);

                        G.DrawString(Text, Font, H.SolidBrushHTMlColor("e4ecf2"), new Rectangle(22, Convert.ToInt32(1.6), Width, Height - 2), new StringFormat { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center });
                        break;
                   }

            }

            e.Graphics.DrawImage(B, 0, 0);
            G.Dispose();
            B.Dispose();
        }
    }

    #endregion

    #region Initialization

    public AcaciaCheckBox()
    {
        SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint |
                 ControlStyles.OptimizedDoubleBuffer | ControlStyles.SupportsTransparentBackColor, true);
        DoubleBuffered = true;
        BackColor = Color.Transparent;
        Font = new Font("Arial", 11, FontStyle.Regular);
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

[DefaultEvent("CheckedChanged")]public class AcaciaRadioButton : Control
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

            G.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            if(Checked)
            {
                G.DrawEllipse(H.PenHTMlColor("fc3955", Convert.ToInt32(2.8)), new Rectangle(1, 1, 18, 18));

                G.FillEllipse(H.SolidBrushHTMlColor("fc3955"), new Rectangle(5, 5, 10, 10));

                G.DrawString(Text, Font, H.SolidBrushHTMlColor("e4ecf2"), new Rectangle(22, Convert.ToInt32(1.6), Width, Height - 2), new StringFormat {Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center});
            }
            else
            {
                switch(State)
                {
                case HelperMethods.MouseMode.NormalMode:

                        G.DrawEllipse(new Pen(Color.FromArgb(150, H.GetHTMLColor("fc3955")), Convert.ToInt32(2.8)), new Rectangle(1, 1, 18, 18));

                        G.DrawString(Text, Font, new SolidBrush(Color.Silver), new Rectangle(22, Convert.ToInt32(1.6), Width, Height - 2), new StringFormat {Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center});
                break;
                case HelperMethods.MouseMode.Hovered:

                        G.DrawEllipse(H.PenHTMlColor("fc3955",Convert.ToInt32(2.8)), new Rectangle(1, 1, 18, 18));

                        G.FillEllipse(H.SolidBrushHTMlColor("fc3955"), new Rectangle(5, 5, 10, 10));

                        G.DrawString(Text, Font, H.SolidBrushHTMlColor("e4ecf2"), new Rectangle(22, Convert.ToInt32(1.6), Width, Height - 2), new StringFormat { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center });
                break;
            }

            }


            e.Graphics.DrawImage(B, 0, 0);
            G.Dispose();
            B.Dispose();
        }
    }

    #endregion

    #region Initialization

    public AcaciaRadioButton()
    {
        SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint |
                 ControlStyles.OptimizedDoubleBuffer | ControlStyles.SupportsTransparentBackColor, true);
        DoubleBuffered = true;
        Cursor = Cursors.Hand;
        BackColor = Color.Transparent;
        Font = new Font("Arial", 11, FontStyle.Regular);
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
            if (!object.ReferenceEquals(C, this) && C is AcaciaRadioButton && ((AcaciaRadioButton)C).Group == _Group)
            {
                ((AcaciaRadioButton)C).Checked = false;
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

#region ControlButton

public class AcaciaControlButton : Control
{

    #region Variables 
    private HelperMethods.MouseMode State;
    private Style _ControlStyle = Style.Close;
    private static HelperMethods H = new HelperMethods();

#endregion

    #region Enumenators

    public enum Style
    {
        Close,
        Minimize,
        Maximize
    }

    #endregion

    #region Constructors

    public AcaciaControlButton()
    {
        SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.ResizeRedraw | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer |
       ControlStyles.SupportsTransparentBackColor, true);
        DoubleBuffered = true;
        BackColor = Color.Transparent;
        UpdateStyles();
        Anchor = AnchorStyles.Top | AnchorStyles.Right;
        Size = new Size(18, 18);
    }

   #endregion

    #region Draw Control

    protected override void OnPaint(PaintEventArgs e)
    {
        using (Bitmap B = new Bitmap(Width, Height))
        using (Graphics G = Graphics.FromImage(B))
        {

             G.SmoothingMode = SmoothingMode.HighQuality;

           switch(State)
            {

            case HelperMethods.MouseMode.NormalMode:
                       G.DrawEllipse(new Pen(Color.FromArgb(150, H.GetHTMLColor("fc3955")), 2), new Rectangle(1, 1, 15, 15));
                       G.FillEllipse(new SolidBrush(Color.FromArgb(150, H.GetHTMLColor("fc3955"))), new Rectangle(5, 5, 7, 7));
                      break;
            case HelperMethods.MouseMode.Hovered:
                      Cursor = Cursors.Hand;
                        G.DrawEllipse(H.PenHTMlColor("fc3955", 2), new Rectangle(1, 1, 15, 15));
                        G.FillEllipse(H.SolidBrushHTMlColor("fc3955"), new Rectangle(5, 5, 7, 7));
                        break;
            case HelperMethods.MouseMode.Pushed:
                        G.DrawEllipse(H.PenHTMlColor("24273e", 2), new Rectangle(1, 1, 15, 15));
                        G.FillEllipse(H.SolidBrushHTMlColor("24273e"), new Rectangle(5, 5, 7, 7));
                        break;
          }

            e.Graphics.DrawImage(B, 0, 0);
            G.Dispose();
            B.Dispose();
        }
    }

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

    #region Events

    protected override void OnClick(EventArgs e)
    {
        base.OnClick(e);
        if (ControlStyle == Style.Close)
        {
            Environment.Exit(0);
            Application.Exit();
        }
        else if(ControlStyle == Style.Minimize)
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

#region Trackbar

[DefaultEvent("Scroll")]public class AcaciaTrackBar : Control
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

    #region properties

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
                G.SmoothingMode = SmoothingMode.HighQuality;

                H.FillRoundedPath(G, H.SolidBrushHTMlColor("1e2137"), new Rectangle(0, Convert.ToInt32(5.5), Width, 8), 8);

                if (CurrentValue != 0) 
                {
                    H.FillRoundedPath(G, H.GetHTMLColor("fc3955"), new Rectangle(0, Convert.ToInt32(5.5), CurrentValue + 4, 8), 6);
                }

                G.FillEllipse(H.SolidBrushHTMlColor("fc3955"), Track);
                G.FillEllipse(H.SolidBrushHTMlColor("1e2137"), TrackSide);

            e.Graphics.DrawImage(B, 0, 0);
            G.Dispose();
            B.Dispose();
        }
    }

    #endregion

    #region Initialization

    public AcaciaTrackBar()
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
        Height = 23;
        Invalidate();
        base.OnResize(e);
    }

    private void MoveTrack()
    {
        if (Height > 0 && Width > 0) { Track = new Rectangle(CurrentValue + 1, 0, 21, 20); }
        TrackSide = new Rectangle(CurrentValue + Convert.ToInt32(8.5), 7, 6, 6);
    }

    public void RenewCurrentValue()
    {

        CurrentValue = Convert.ToInt32(Math.Round((double)(Value - Minimum) / (double)(Maximum - Minimum) * (double)(Width - 23.5)));
    }

    #endregion

}

#endregion

#region Progress

public class AcaciaProgressBar : Control
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

            G.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            Rectangle Rect = new Rectangle(0, 0, Width - 1, Height - 1);

            H.FillRoundedPath(G, H.GetHTMLColor("1e2137"), Rect, 1);           

            if (CurrentValue != 0)
            {
                H.FillRoundedPath(G, H.GetHTMLColor("fc3955"), new Rectangle(Rect.X, Rect.Y, CurrentValue, Rect.Height), 1);

            }
            e.Graphics.DrawImage(B, 0, 0);
            G.Dispose();
            B.Dispose();
        }

    }

    #endregion

    #region Initialization

    public AcaciaProgressBar()
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

#region ComboBox

public class AcaciaComboBox : ComboBox
{

    #region Variables

    private int _StartIndex = 0;
    private static HelperMethods H = new HelperMethods();

    #endregion

    #region Initialization

    public AcaciaComboBox()
    {
        SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.ResizeRedraw | ControlStyles.UserPaint |
              ControlStyles.OptimizedDoubleBuffer | ControlStyles.SupportsTransparentBackColor, true);
        DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
        DoubleBuffered = true;
        BackColor = Color.Transparent;
        StartIndex = 0;
        Font = new Font("Arial", 12);
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
            Rectangle Rect = new Rectangle(1, 1, Width - Convert.ToInt32(2.5), Height - Convert.ToInt32(2.5));
        
                G.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

                H.DrawRoundedPath(G, H.GetHTMLColor("585c73"), (int)(1.7), Rect, 1);

                G.SmoothingMode = SmoothingMode.AntiAlias;
                H.DrawTriangle(G, H.GetHTMLColor("fc3955"), Convert.ToInt32(1.5),
                          new Point(Width - 20, 12), new Point(Width - 16, 16), 
                          new Point(Width - 16, 16), new Point(Width - 12, 12), 
                          new Point(Width - 16, 17), new Point(Width - 16, 16) 
                          );
                G.SmoothingMode = SmoothingMode.None;
                G.DrawString(Text, Font, new SolidBrush(H.GetHTMLColor("585c73")), new Rectangle(7, 1, Width - 1, Height - 1), new StringFormat {LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Near});
            

            e.Graphics.DrawImage(B, 0, 0);
            G.Dispose();
            B.Dispose();
        }
    }

    protected override void OnDrawItem(DrawItemEventArgs e)
    {
        try
        {

            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            if (System.Convert.ToInt32((e.State & DrawItemState.Selected)) == (int)DrawItemState.Selected)
            {
                if (!(e.Index == -1))
                {
                    e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(120, H.GetHTMLColor("fc3955"))), e.Bounds);
                    e.Graphics.DrawString(GetItemText(Items[e.Index]), Font, H.SolidBrushHTMlColor("585c73"), e.Bounds);
                }
            }
            else
            {
                if (!(e.Index == -1))
                {
                    e.Graphics.FillRectangle(Brushes.WhiteSmoke, e.Bounds);
                    e.Graphics.DrawString(GetItemText(Items[e.Index]), Font, H.SolidBrushHTMlColor("585c73"), e.Bounds);
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


    }

    protected override void OnCreateControl()
    {
        base.OnCreateControl();

    }

    #endregion

}

#endregion

#region Seperator

public class AcaciaSeperator : Control
{

    #region Variables

    private Style _SepStyle = Style.Horizental;
    private static HelperMethods H = new HelperMethods();

    #endregion

    #region Initialization

    public AcaciaSeperator()
    {
        SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint |
        ControlStyles.ResizeRedraw | ControlStyles.UserPaint |
        ControlStyles.SupportsTransparentBackColor, true);
        DoubleBuffered = true;
        BackColor = Color.Transparent;
        ForeColor = Color.FromArgb(150, H.GetHTMLColor("fc3955"));
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
            System.Drawing.Drawing2D.ColorBlend BL1 = new System.Drawing.Drawing2D.ColorBlend();
            BL1.Positions = new Single[] { 0.0F, 0.15F, 0.85F, 1.0F };
            BL1.Colors = new Color[] { Color.Transparent, ForeColor, ForeColor, Color.Transparent };
            switch (SepStyle)
            {
                case Style.Horizental:
                    using (System.Drawing.Drawing2D.LinearGradientBrush lb1 = new System.Drawing.Drawing2D.LinearGradientBrush(ClientRectangle, Color.Empty, Color.Empty, 0.0F))
                    {
                        lb1.InterpolationColors = BL1;
                        G.DrawLine(new Pen(lb1), 0, 1, Width, 1);
                    }
                    break;
                case Style.Vertiacal:
                    using (System.Drawing.Drawing2D.LinearGradientBrush lb1 = new System.Drawing.Drawing2D.LinearGradientBrush(ClientRectangle, Color.Empty, Color.Empty, 90.0F))
                    {
                        lb1.InterpolationColors = BL1;
                        G.DrawLine(new Pen(lb1), 1, 0, 1, Height);
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

#region Label

[DefaultEvent("TextChanged")] public class AcaciaLabel : Control
{
    #region Variables

    private static HelperMethods H = new HelperMethods();

    #endregion

    #region Initialization

    public AcaciaLabel()
    {
        SetStyle(ControlStyles.SupportsTransparentBackColor | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
        DoubleBuffered = true;
        BackColor = Color.Transparent;
        Font = new Font("Arial", 10, FontStyle.Bold);
        UpdateStyles();
    }

    #endregion

    #region DrawControl

    protected override void OnPaint(PaintEventArgs e)
    {
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
            e.Graphics.DrawString(Text, Font, H.SolidBrushHTMlColor("e4ecf2"), ClientRectangle);

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