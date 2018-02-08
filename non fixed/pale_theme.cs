/// <summary>
/// Pale Theme
/// Author : THE LORD
/// Release Date : Thursday, January 12, 2017
/// Last Update : Friday, January 13, 2017
/// HF Account : https://hackforums.net/member.php?action=profile&uid=3304362
/// PM Me for any bug.
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
    { G.DrawString(Text, font, brush, new Rectangle(0, Rect.Y, Rect.Width, Rect.Height), new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });}


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

public class PaleSkin : ContainerControl
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
            G.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            Rectangle R = new Rectangle(-1, -1, Width, Height);
            using (LinearGradientBrush LGB = new LinearGradientBrush(R, H.GetHTMLColor("f7f7f7"), Color.White, 90.0F))
            {
                H.FillRoundedPath(G, LGB, R, 12);
                H.DrawRoundedPath(G, H.GetHTMLColor("f7f7f7"), 2, R, 12);
                G.DrawLine(H.PenHTMlColor("dadada", Convert.ToSingle(1.5)), new Point(12, 55), new Point(Width - 20, 55));
            }

            if (FindForm().ShowIcon)
            {
                if (FindForm().Icon != null)
                {
                    switch (TitleTextPostion)
                    {
                        case TitlePostion.Left:
                            G.DrawString(Text, Font, H.SolidBrushHTMlColor("2e8fc7"), 27, 14);
                            G.DrawIcon(FindForm().Icon, new Rectangle(6, 17, 20, 20));
                            break;
                        case TitlePostion.Center:
                            H.CentreString(G, Text, Font, H.SolidBrushHTMlColor("2e8fc7"), new Rectangle(0, 0, Width, 50));
                            G.DrawIcon(FindForm().Icon, new Rectangle(5, 17, 20, 20));
                            break;
                        case TitlePostion.Right:
                            H.RightString(G, Text, Font, H.SolidBrushHTMlColor("2e8fc7"), new Rectangle(0, 0, Width, 50));
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
                        G.DrawString(Text, Font, H.SolidBrushHTMlColor("2e8fc7"), 5, 14);
                        break;
                    case TitlePostion.Center:
                        H.CentreString(G, Text, Font, H.SolidBrushHTMlColor("2e8fc7"), new Rectangle(0, 0, Width, 50));
                        break;
                    case TitlePostion.Right:
                        H.RightString(G, Text, Font, H.SolidBrushHTMlColor("2e8fc7"), new Rectangle(0, 0, Width, 50));
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

    public PaleSkin()
    {

        SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor | ControlStyles.ContainerControl, true);
        DoubleBuffered = true;
        BackColor = Color.Transparent;
        Font = new Font("Segoe UI", 14, FontStyle.Bold);
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

public class PaleButton : Control
{

    #region Variables

    private HelperMethods.MouseMode State;
    private static HelperMethods H = new HelperMethods();
    private int _RoundRadius = 10;

    #endregion

    #region Draw Control

    protected override void OnPaint(PaintEventArgs e)
    {
        using (Bitmap B = new Bitmap(Width, Height))
        using (Graphics G = Graphics.FromImage(B))
        {

            Rectangle Rect = new Rectangle(1, 1, Width - 2, Height - 2);

            G.SmoothingMode = SmoothingMode.HighQuality;
            G.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            switch (State)
            {
                case HelperMethods.MouseMode.NormalMode:
                    using (LinearGradientBrush LGB = new LinearGradientBrush(Rect, Color.FromArgb(180, 250, 250, 250), Color.FromArgb(200, 250, 250, 250), 90f))
                    {
                        H.FillRoundedPath(G, LGB, Rect, RoundRadius);
                    }
                    H.DrawRoundedPath(G, H.GetHTMLColor("e0e0e0"), 1, Rect, RoundRadius);
                    H.CentreString(G, Text, Font, H.SolidBrushHTMlColor("6a6a6a"), Rect);
                    break;
                case HelperMethods.MouseMode.Hovered:
                    using (LinearGradientBrush LGB = new LinearGradientBrush(Rect, Color.FromArgb(20, H.GetHTMLColor("6ebeec")), Color.FromArgb(30, H.GetHTMLColor("6ebeec")), 120f))
                    {
                        G.DrawPath(new Pen(LGB, 2), H.RoundRec(Rect, RoundRadius));
                    }
                    H.CentreString(G, Text, Font, H.SolidBrushHTMlColor("6a6a6a"), Rect);
                    break;
                case HelperMethods.MouseMode.Pushed:
                    H.FillRoundedPath(G, H.SolidBrushHTMlColor("f3f3f3"), Rect, RoundRadius);
                    H.DrawRoundedPath(G, H.GetHTMLColor("e0e0e0"), 1, Rect, RoundRadius);
                    H.CentreString(G, Text, Font, H.SolidBrushHTMlColor("6a6a6a"), Rect);
                    break;

            }

            e.Graphics.DrawImage(B, 0, 0);
            G.Dispose();
            B.Dispose();
        }
    }

    #endregion

    #region Properties

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

    #region Constructors

    public PaleButton()
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

}

#endregion

#region Blue Button

public class PaleBlueButton : Control
{

    #region Variables

    private HelperMethods.MouseMode State;
    private static HelperMethods H = new HelperMethods();
    private int _RoundRadius = 10;

    #endregion

    #region Draw Control

    protected override void OnPaint(PaintEventArgs e)
    {
        using (Bitmap B = new Bitmap(Width, Height))
        using (Graphics G = Graphics.FromImage(B))
        {

            Rectangle Rect = new Rectangle(1, 1, Width - 2, Height - 2);

            G.SmoothingMode = SmoothingMode.HighQuality;
            G.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            switch (State)
            {
                case HelperMethods.MouseMode.NormalMode:
                    H.FillRoundedPath(G, H.SolidBrushHTMlColor("5db6ea"), Rect, RoundRadius);
                    using (LinearGradientBrush LGB = new LinearGradientBrush(Rect, Color.FromArgb(30, H.GetHTMLColor("5db6ea")), Color.FromArgb(30, 250, 250, 250), 90F))
                    {
                        H.FillRoundedPath(G, LGB, Rect, RoundRadius);
                    }
                    H.DrawRoundedPath(G, H.GetHTMLColor("4ca6db"), 1, Rect, RoundRadius);
                    H.CentreString(G, Text, Font, Brushes.White, Rect);
                    break;
                case HelperMethods.MouseMode.Hovered:
                    H.FillRoundedPath(G, H.SolidBrushHTMlColor("5db6ea"), Rect, RoundRadius);
                    H.CentreString(G, Text, Font, H.SolidBrushHTMlColor("4ca6db"), Rect);
                    break;
                case HelperMethods.MouseMode.Pushed:
                    H.FillRoundedPath(G, Brushes.White, Rect, RoundRadius);
                    H.DrawRoundedPath(G, H.GetHTMLColor("4ca6db"), 1, Rect, RoundRadius);
                    H.CentreString(G, Text, Font, Brushes.White, Rect);
                    break;

            }

            e.Graphics.DrawImage(B, 0, 0);
            G.Dispose();
            B.Dispose();
        }
    }

    #endregion

    #region Properties

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

    #region Constructors

    public PaleBlueButton()
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

}

#endregion

#region Textbox

[DefaultEvent("TextChanged")]public class PaleTextbox : Control
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
    private static Color TBC = H.GetHTMLColor("ffffff");
    private static Color TFC = H.GetHTMLColor("a5a5a5");
    private SideAligin _SideImageAlign = SideAligin.Left;
    private Color _BackColor = Color.Transparent;
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
                    H.FillRoundedPath(G, Brushes.White, Rect, 5);
                    H.DrawRoundedPath(G, H.GetHTMLColor("dfdfdf"), 1, Rect, 5);
                    break;
                case HelperMethods.MouseMode.Hovered:
                    H.FillRoundedPath(G, Brushes.White, Rect, 5);

                    using (LinearGradientBrush lgb = new LinearGradientBrush(Rect, Color.FromArgb(50, H.GetHTMLColor("6ebeec")), Color.FromArgb(50, H.GetHTMLColor("6ebeec")), 120f))
                    {
                        G.DrawPath(new Pen(lgb, 2), H.RoundRec(Rect, 8));
                    }

                    break;
                case HelperMethods.MouseMode.Pushed:
                    H.FillRoundedPath(G, Brushes.White, Rect, 5);
                    H.DrawRoundedPath(G, H.GetHTMLColor("dfdfdf"), 1, Rect, 5);
                    break;
            }


            if (SideImage != null)
            {
                if (SideImageAlign == SideAligin.Right)
                {
                    T.Location = new Point(7, Convert.ToInt32(4.5));
                    T.Width = Width - 60;
                    G.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    G.DrawImage(SideImage, new Rectangle(Rect.Width - 24, 6, 16, 16));
                }
                else
                {
                    T.Location = new Point(33, Convert.ToInt32(4.5));
                    T.Width = Width - 60;
                    G.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    G.DrawImage(SideImage, new Rectangle(8, 6, 16, 16));
                }
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

    #region Constructors

    public PaleTextbox()
    {
        SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint |
                ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.SupportsTransparentBackColor, true);
        DoubleBuffered = true;
        UpdateStyles();
        Font = new Font("Myriad Pro", 11, FontStyle.Bold);
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

#region CheckBox

[DefaultEvent("CheckedChanged")]
public class PaleCheckBox : Control
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

            G.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            G.SmoothingMode = SmoothingMode.AntiAlias;
            using (GraphicsPath CheckBorder = new GraphicsPath { FillMode = FillMode.Winding })
            {
                CheckBorder.AddArc(0, 0, 10, 8, 180, 90);
                CheckBorder.AddArc(8, 0, 8, 10, -90, 90);
                CheckBorder.AddArc(8, 8, 8, 8, 0, 70);
                CheckBorder.AddArc(0, 8, 10, 8, 90, 90);
                CheckBorder.CloseAllFigures();
                G.FillPath(Brushes.White, CheckBorder);
                G.DrawPath(H.PenHTMlColor("d9d9d9", Convert.ToSingle(1.5)), CheckBorder);
                if (Checked)
                {
                    H.FillRoundedPath(G, H.SolidBrushHTMlColor("5db5e9"), new Rectangle(Convert.ToInt32(3.5), Convert.ToInt32(3.5), Convert.ToInt32(8.5), Convert.ToInt32(8.5)), 2);
                    H.DrawRoundedPath(G, H.GetHTMLColor("3db3e5"), 1, new Rectangle(Convert.ToInt32(3.5), Convert.ToInt32(3.5), Convert.ToInt32(8.5), Convert.ToInt32(8.5)), 2);
                }
            }

            G.DrawString(Text, Font, H.SolidBrushHTMlColor("a5a5a5"), new Rectangle(18, Convert.ToInt32(1.4), Width, Height - 2), new StringFormat { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center });

            e.Graphics.DrawImage(B, 0, 0);
            G.Dispose();
            B.Dispose();
        }
    }

    #endregion

    #region Constructors

    public PaleCheckBox()
    {
        SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint |
                 ControlStyles.OptimizedDoubleBuffer | ControlStyles.SupportsTransparentBackColor, true);
        DoubleBuffered = true;
        Cursor = Cursors.Hand;
        BackColor = Color.Transparent;
        Font = new Font("Myriad Pro", 9, FontStyle.Regular);
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

#region Seperator

public class PaleSeperator : Control
{

    #region Variables

    private Style _SepStyle = Style.Horizental;
    private static HelperMethods H = new HelperMethods();

    #endregion

    #region Constructors

    public PaleSeperator()
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
                    G.DrawLine(new Pen(H.SolidBrushHTMlColor("d4d4d4")), 0, 1, Width, 1);
                    break;
                case Style.Vertiacal:
                    G.DrawLine(new Pen(H.SolidBrushHTMlColor("d4d4d4")), 1, 0, 1, Height);
                    break;
            }

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
        if (SepStyle == Style.Horizental) { Height = 4; } else { Width = 4; }
    }

    #endregion

}

#endregion

#region Label

public class PaleLabel : Control
{
    #region Vaeiables

    private Style _ColorStyle = Style.Style1;
    private static HelperMethods H = new HelperMethods();

    #endregion

    #region Constructors

    public PaleLabel()
    {
        SetStyle(ControlStyles.SupportsTransparentBackColor | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer, true);
        DoubleBuffered = true;
        Font = new Font("Myriad Pro", 9, FontStyle.Regular);
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
        Style1,
        Style2,
        Style3
    }

    #endregion

    #region DrawControl

    protected override void OnPaint(PaintEventArgs e)
    {

        Rectangle Rect = new Rectangle(0, 0, 22, 22);
        using (Bitmap B = new Bitmap(Width, Height))
        using (Graphics G = Graphics.FromImage(B))
        {

            G.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            switch (ColorStyle)
            {
                case Style.Style1:
                    G.DrawString(Text, Font, H.SolidBrushHTMlColor("898989"), ClientRectangle);
                    break;
                case Style.Style2:
                    G.DrawString(Text, Font, H.SolidBrushHTMlColor("606060"), ClientRectangle);
                    break;
                case Style.Style3:
                    G.DrawString(Text, Font, H.SolidBrushHTMlColor("2e8fc7"), ClientRectangle);
                    break;
            }

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
        Height = Font.Height;
    }

    #endregion;
}

#endregion

#region Close

public class PaleClose : Control
{

    #region Variables

    private static HelperMethods H = new HelperMethods();

    private string IMG =
        "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/GMzLdsAADoTaVRYdFhNTDpjb20uYWRvYmUueG1wAAAAAAA8P3hwYWNrZXQgYmVnaW49Iu+7vyIgaWQ9Ilc1TTBNcENlaGlIenJlU3pOVGN6a2M5ZCI/Pgo8eDp4bXBtZXRhIHhtbG5zOng9ImFkb2JlOm5zOm1ldGEvIiB4OnhtcHRrPSJBZG9iZSBYTVAgQ29yZSA1LjUtYzAxNCA3OS4xNTE0ODEsIDIwMTMvMDMvMTMtMTI6MDk6MTUgICAgICAgICI+CiAgIDxyZGY6UkRGIHhtbG5zOnJkZj0iaHR0cDovL3d3dy53My5vcmcvMTk5OS8wMi8yMi1yZGYtc3ludGF4LW5zIyI+CiAgICAgIDxyZGY6RGVzY3JpcHRpb24gcmRmOmFib3V0PSIiCiAgICAgICAgICAgIHhtbG5zOnhtcD0iaHR0cDovL25zLmFkb2JlLmNvbS94YXAvMS4wLyIKICAgICAgICAgICAgeG1sbnM6eG1wTU09Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC9tbS8iCiAgICAgICAgICAgIHhtbG5zOnN0RXZ0PSJodHRwOi8vbnMuYWRvYmUuY29tL3hhcC8xLjAvc1R5cGUvUmVzb3VyY2VFdmVudCMiCiAgICAgICAgICAgIHhtbG5zOmRjPSJodHRwOi8vcHVybC5vcmcvZGMvZWxlbWVudHMvMS4xLyIKICAgICAgICAgICAgeG1sbnM6cGhvdG9zaG9wPSJodHRwOi8vbnMuYWRvYmUuY29tL3Bob3Rvc2hvcC8xLjAvIgogICAgICAgICAgICB4bWxuczp0aWZmPSJodHRwOi8vbnMuYWRvYmUuY29tL3RpZmYvMS4wLyIKICAgICAgICAgICAgeG1sbnM6ZXhpZj0iaHR0cDovL25zLmFkb2JlLmNvbS9leGlmLzEuMC8iPgogICAgICAgICA8eG1wOkNyZWF0b3JUb29sPkFkb2JlIFBob3Rvc2hvcCBDQyAoV2luZG93cyk8L3htcDpDcmVhdG9yVG9vbD4KICAgICAgICAgPHhtcDpDcmVhdGVEYXRlPjIwMTYtMTItMjlUMDY6NTE6MTktMDg6MDA8L3htcDpDcmVhdGVEYXRlPgogICAgICAgICA8eG1wOk1ldGFkYXRhRGF0ZT4yMDE2LTEyLTI5VDA2OjUxOjE5LTA4OjAwPC94bXA6TWV0YWRhdGFEYXRlPgogICAgICAgICA8eG1wOk1vZGlmeURhdGU+MjAxNi0xMi0yOVQwNjo1MToxOS0wODowMDwveG1wOk1vZGlmeURhdGU+CiAgICAgICAgIDx4bXBNTTpJbnN0YW5jZUlEPnhtcC5paWQ6Nzk1OWQ2MDQtZTAyMC1iZDQ0LTkwYzItOTgyMzJlYTY4ZTg2PC94bXBNTTpJbnN0YW5jZUlEPgogICAgICAgICA8eG1wTU06RG9jdW1lbnRJRD54bXAuZGlkOmRmZjgyZmQzLTlhZmEtM2M0NC1hZjlhLTRjZGZmNzhiOWRmNzwveG1wTU06RG9jdW1lbnRJRD4KICAgICAgICAgPHhtcE1NOk9yaWdpbmFsRG9jdW1lbnRJRD54bXAuZGlkOmRmZjgyZmQzLTlhZmEtM2M0NC1hZjlhLTRjZGZmNzhiOWRmNzwveG1wTU06T3JpZ2luYWxEb2N1bWVudElEPgogICAgICAgICA8eG1wTU06SGlzdG9yeT4KICAgICAgICAgICAgPHJkZjpTZXE+CiAgICAgICAgICAgICAgIDxyZGY6bGkgcmRmOnBhcnNlVHlwZT0iUmVzb3VyY2UiPgogICAgICAgICAgICAgICAgICA8c3RFdnQ6YWN0aW9uPmNyZWF0ZWQ8L3N0RXZ0OmFjdGlvbj4KICAgICAgICAgICAgICAgICAgPHN0RXZ0Omluc3RhbmNlSUQ+eG1wLmlpZDpkZmY4MmZkMy05YWZhLTNjNDQtYWY5YS00Y2RmZjc4YjlkZjc8L3N0RXZ0Omluc3RhbmNlSUQ+CiAgICAgICAgICAgICAgICAgIDxzdEV2dDp3aGVuPjIwMTYtMTItMjlUMDY6NTE6MTktMDg6MDA8L3N0RXZ0OndoZW4+CiAgICAgICAgICAgICAgICAgIDxzdEV2dDpzb2Z0d2FyZUFnZW50PkFkb2JlIFBob3Rvc2hvcCBDQyAoV2luZG93cyk8L3N0RXZ0OnNvZnR3YXJlQWdlbnQ+CiAgICAgICAgICAgICAgIDwvcmRmOmxpPgogICAgICAgICAgICAgICA8cmRmOmxpIHJkZjpwYXJzZVR5cGU9IlJlc291cmNlIj4KICAgICAgICAgICAgICAgICAgPHN0RXZ0OmFjdGlvbj5zYXZlZDwvc3RFdnQ6YWN0aW9uPgogICAgICAgICAgICAgICAgICA8c3RFdnQ6aW5zdGFuY2VJRD54bXAuaWlkOjc5NTlkNjA0LWUwMjAtYmQ0NC05MGMyLTk4MjMyZWE2OGU4Njwvc3RFdnQ6aW5zdGFuY2VJRD4KICAgICAgICAgICAgICAgICAgPHN0RXZ0OndoZW4+MjAxNi0xMi0yOVQwNjo1MToxOS0wODowMDwvc3RFdnQ6d2hlbj4KICAgICAgICAgICAgICAgICAgPHN0RXZ0OnNvZnR3YXJlQWdlbnQ+QWRvYmUgUGhvdG9zaG9wIENDIChXaW5kb3dzKTwvc3RFdnQ6c29mdHdhcmVBZ2VudD4KICAgICAgICAgICAgICAgICAgPHN0RXZ0OmNoYW5nZWQ+Lzwvc3RFdnQ6Y2hhbmdlZD4KICAgICAgICAgICAgICAgPC9yZGY6bGk+CiAgICAgICAgICAgIDwvcmRmOlNlcT4KICAgICAgICAgPC94bXBNTTpIaXN0b3J5PgogICAgICAgICA8ZGM6Zm9ybWF0PmltYWdlL3BuZzwvZGM6Zm9ybWF0PgogICAgICAgICA8cGhvdG9zaG9wOkNvbG9yTW9kZT4zPC9waG90b3Nob3A6Q29sb3JNb2RlPgogICAgICAgICA8cGhvdG9zaG9wOklDQ1Byb2ZpbGU+c1JHQiBJRUM2MTk2Ni0yLjE8L3Bob3Rvc2hvcDpJQ0NQcm9maWxlPgogICAgICAgICA8dGlmZjpPcmllbnRhdGlvbj4xPC90aWZmOk9yaWVudGF0aW9uPgogICAgICAgICA8dGlmZjpYUmVzb2x1dGlvbj43MjAwMDAvMTAwMDA8L3RpZmY6WFJlc29sdXRpb24+CiAgICAgICAgIDx0aWZmOllSZXNvbHV0aW9uPjcyMDAwMC8xMDAwMDwvdGlmZjpZUmVzb2x1dGlvbj4KICAgICAgICAgPHRpZmY6UmVzb2x1dGlvblVuaXQ+MjwvdGlmZjpSZXNvbHV0aW9uVW5pdD4KICAgICAgICAgPGV4aWY6Q29sb3JTcGFjZT4xPC9leGlmOkNvbG9yU3BhY2U+CiAgICAgICAgIDxleGlmOlBpeGVsWERpbWVuc2lvbj4xNTwvZXhpZjpQaXhlbFhEaW1lbnNpb24+CiAgICAgICAgIDxleGlmOlBpeGVsWURpbWVuc2lvbj4xMzwvZXhpZjpQaXhlbFlEaW1lbnNpb24+CiAgICAgIDwvcmRmOkRlc2NyaXB0aW9uPgogICA8L3JkZjpSREY+CjwveDp4bXBtZXRhPgogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgIAo8P3hwYWNrZXQgZW5kPSJ3Ij8+t8wKZgAAACBjSFJNAAB6JQAAgIMAAPn/AACA6QAAdTAAAOpgAAA6mAAAF2+SX8VGAAAA5UlEQVR42oTSIUvDQRjH8c9ORJNYhL0BQbAYVnwNZuM/GUWYTUXYwtjEoGA2GtRg3CswuJcgglFkYFLEtFn+G+d5cE+65/n+vnfhnkY1HA9w5G/9YBOvWMEzmklmENDHewKWcVWfuxnxDf2ATxz7Xzs4xUGGneCrUQ3HEDBCS7lG2MY01IMJDjEtiFO0Z7kQgUfcF+QbPM2akMDbgnwXN7G8gE5B7sZOLO9hqyC3UKXyKnqZ8CQzO6sXZy53sJaEvrGbuaBZ/7OADexnXujhAdcZ1sZ6wCUWE/iCi2ibPhK+hPPfAQAi6StkWJPjCAAAAABJRU5ErkJggg==";

    #endregion

    #region Constructors

    public PaleClose()
    {

        SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.ResizeRedraw |
            ControlStyles.SupportsTransparentBackColor, true);
        DoubleBuffered = true;
        BackColor = Color.Transparent;
        Cursor = Cursors.Hand;
        UpdateStyles();

    }

    #endregion

    #region DrawControl

    protected override void OnPaint(PaintEventArgs e)
    {

        using (Bitmap B = new Bitmap(Width, Height))
        using (Graphics G = Graphics.FromImage(B))
        {

            G.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            G.InterpolationMode = InterpolationMode.High;

            H.DrawImageFromBase64(G, IMG, ClientRectangle);

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
        Size = new Size(15, 13);
        Invalidate();
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

#region ComboBox

public class PaleComboBox : ComboBox
{

    #region Variables

    private int _StartIndex = 0;
    private static HelperMethods H = new HelperMethods();

    #endregion

    #region Constructors

    public PaleComboBox()
    {
        SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.ResizeRedraw | ControlStyles.UserPaint |
              ControlStyles.OptimizedDoubleBuffer | ControlStyles.SupportsTransparentBackColor, true);
        DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
        DoubleBuffered = true;
        BackColor = Color.Transparent;
        StartIndex = 0;
        DropDownHeight = 100;
        Font = new Font("Myriad Pro", 11);
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
            G.SmoothingMode = SmoothingMode.AntiAlias;

            H.FillRoundedPath(G, Brushes.White, Rect, 5);
            H.DrawRoundedPath(G, H.GetHTMLColor("d9d9d9"), (int)1.5, Rect, 5);
            H.FillRoundedPath(G, H.SolidBrushHTMlColor("5db5e9"), new Rectangle(Width - 30, Convert.ToInt32(1.4), 29, Height - Convert.ToInt32(2.5)), 5);
            H.DrawRoundedPath(G, H.GetHTMLColor("4ca6db"), (int)1.5, new Rectangle(Width - 30, Convert.ToInt32(1.4), 29, Height - Convert.ToInt32(2.5)), 5);

            H.DrawTriangle(G, Color.White, Convert.ToInt32(1.5),
                      new Point(Width - 20, 12), new Point(Width - 16, 16),
                      new Point(Width - 16, 16), new Point(Width - 12, 12),
                      new Point(Width - 16, 17), new Point(Width - 16, 16)
                      );
            G.DrawString(Text, Font, new SolidBrush(H.GetHTMLColor("a5a5a5")), new Rectangle(7, Convert.ToInt32(1.5), Width - 1, Height - 1), new StringFormat { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Near });

            e.Graphics.DrawImage(B, 0, 0);
            G.Dispose();
            B.Dispose();
        }
    }

    protected override void OnDrawItem(DrawItemEventArgs e)
    {
        try
        {

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            if (System.Convert.ToInt32((e.State & DrawItemState.Selected)) == (int)DrawItemState.Selected)
            {
                if (!(e.Index == -1))
                {
                    e.Graphics.FillRectangle(H.SolidBrushHTMlColor("5db5e9"), e.Bounds);
                    e.Graphics.DrawRectangle(H.PenHTMlColor("5db5e9", 1), e.Bounds);
                    e.Graphics.DrawString(GetItemText(Items[e.Index]), Font, Brushes.White, 1, e.Bounds.Y + 3);
                }
            }
            else
            {
                if (!(e.Index == -1))
                {
                    e.Graphics.FillRectangle(Brushes.WhiteSmoke, e.Bounds);
                    e.Graphics.DrawString(GetItemText(Items[e.Index]), Font, H.SolidBrushHTMlColor("a5a5a5"), 1, e.Bounds.Y + 3);
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

#region RadioButton

[DefaultEvent("CheckedChanged")]public class PaleRadioButton : Control
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

            if (Checked)
            {
                G.FillEllipse(H.SolidBrushHTMlColor("5db5e9"), new Rectangle(4, 4, 12, 12));
                G.DrawEllipse(H.PenHTMlColor("4ca6db", 2), new Rectangle(1, 1, 18, 18));
            }
            else
            {
                G.FillEllipse(Brushes.White, new Rectangle(1, 1, 18, 18));
                G.DrawEllipse(H.PenHTMlColor("d9d9d9", 2), new Rectangle(1, 1, 18, 18));
            }

            G.DrawString(Text, Font, H.SolidBrushHTMlColor("a5a5a5"), new Rectangle(21, Convert.ToInt32(1.5), Width, Height - 2), new StringFormat { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center });

            e.Graphics.DrawImage(B, 0, 0);
            G.Dispose();
            B.Dispose();
        }
    }

    #endregion

    #region Constructors

    public PaleRadioButton()
    {
        SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint |
                 ControlStyles.OptimizedDoubleBuffer | ControlStyles.SupportsTransparentBackColor, true);
        DoubleBuffered = true;
        Cursor = Cursors.Hand;
        BackColor = Color.Transparent;
        Font = new Font("Myriad Pro", 9, FontStyle.Regular);
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
            if (!object.ReferenceEquals(C, this) && C is PaleRadioButton && ((PaleRadioButton)C).Group == _Group)
            {
                ((PaleRadioButton)C).Checked = false;
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