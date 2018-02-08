using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.ComponentModel;
using System.Windows.Forms;


namespace RecuperareIIC
{
    #region " Graphics Functions "
    static class Draw
    {
        public static GraphicsPath RoundRect(Rectangle Rectangle, int Curve)
        {
            GraphicsPath P = new GraphicsPath();
            int ArcRectangleWidth = Curve * 2;
            P.AddArc(new Rectangle(Rectangle.X, Rectangle.Y, ArcRectangleWidth, ArcRectangleWidth), -180, 90);
            P.AddArc(new Rectangle(Rectangle.Width - ArcRectangleWidth + Rectangle.X, Rectangle.Y, ArcRectangleWidth, ArcRectangleWidth), -90, 90);
            P.AddArc(new Rectangle(Rectangle.Width - ArcRectangleWidth + Rectangle.X, Rectangle.Height - ArcRectangleWidth + Rectangle.Y, ArcRectangleWidth, ArcRectangleWidth), 0, 90);
            P.AddArc(new Rectangle(Rectangle.X, Rectangle.Height - ArcRectangleWidth + Rectangle.Y, ArcRectangleWidth, ArcRectangleWidth), 90, 90);
            P.AddLine(new Point(Rectangle.X, Rectangle.Height - ArcRectangleWidth + Rectangle.Y), new Point(Rectangle.X, Curve + Rectangle.Y));
            return P;
        }
        public static GraphicsPath RoundRect(int X, int Y, int Width, int Height, int Curve)
        {
            Rectangle Rectangle = new Rectangle(X, Y, Width, Height);
            GraphicsPath P = new GraphicsPath();
            int ArcRectangleWidth = Curve * 2;
            P.AddArc(new Rectangle(Rectangle.X, Rectangle.Y, ArcRectangleWidth, ArcRectangleWidth), -180, 90);
            P.AddArc(new Rectangle(Rectangle.Width - ArcRectangleWidth + Rectangle.X, Rectangle.Y, ArcRectangleWidth, ArcRectangleWidth), -90, 90);
            P.AddArc(new Rectangle(Rectangle.Width - ArcRectangleWidth + Rectangle.X, Rectangle.Height - ArcRectangleWidth + Rectangle.Y, ArcRectangleWidth, ArcRectangleWidth), 0, 90);
            P.AddArc(new Rectangle(Rectangle.X, Rectangle.Height - ArcRectangleWidth + Rectangle.Y, ArcRectangleWidth, ArcRectangleWidth), 90, 90);
            P.AddLine(new Point(Rectangle.X, Rectangle.Height - ArcRectangleWidth + Rectangle.Y), new Point(Rectangle.X, Curve + Rectangle.Y));
            return P;
        }
    }
    enum MouseState : byte
    {
        None = 0,
        Over = 1,
        Down = 2,
        Block = 3
    }
    #endregion

    public class RecuperareIIButton : Control
    {

        #region " Control Help - MouseState & Flicker Control"
        private MouseState State = MouseState.None;
        protected override void OnMouseEnter(System.EventArgs e)
        {
            base.OnMouseEnter(e);
            State = MouseState.Over;
            Invalidate();
        }
        protected override void OnMouseDown(System.Windows.Forms.MouseEventArgs e)
        {
            base.OnMouseDown(e);
            State = MouseState.Down;
            Invalidate();
        }
        protected override void OnMouseLeave(System.EventArgs e)
        {
            base.OnMouseLeave(e);
            State = MouseState.None;
            Invalidate();
        }
        protected override void OnMouseUp(System.Windows.Forms.MouseEventArgs e)
        {
            base.OnMouseUp(e);
            State = MouseState.Over;
            Invalidate();
        }
        protected override void OnTextChanged(System.EventArgs e)
        {
            base.OnTextChanged(e);
            Invalidate();
        }

        #endregion

        public RecuperareIIButton() : base()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.SupportsTransparentBackColor, true);
            BackColor = Color.Transparent;
            ForeColor = Color.FromArgb(27, 94, 137);
            DoubleBuffered = true;
            Size = new Size(75, 23);
            Font = new Font("Verdana", 6.75f, FontStyle.Bold);
        }

        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            Bitmap B = new Bitmap(Width, Height);
            Graphics G = Graphics.FromImage(B);
            object ClientRectangle = new Rectangle(0, 0, Width - 1, Height - 1);
            base.OnPaint(e);

            G.SmoothingMode = SmoothingMode.HighQuality;
            G.Clear(BackColor);

            switch (State)
            {
                case MouseState.None:
                    //Mouse None
                    LinearGradientBrush bodyGrad = new LinearGradientBrush(new Rectangle(0, 0, Width - 1, Height - 2), Color.FromArgb(245, 245, 245), Color.FromArgb(230, 230, 230), 90);
                    G.FillRectangle(bodyGrad, bodyGrad.Rectangle);
                    LinearGradientBrush bodyInBorder = new LinearGradientBrush(new Rectangle(1, 1, Width - 3, Height - 4), Color.FromArgb(252, 252, 252), Color.FromArgb(249, 249, 249), 90);
                    G.DrawRectangle(new Pen(bodyInBorder), new Rectangle(1, 1, Width - 3, Height - 4));
                    G.DrawRectangle(new Pen(Color.FromArgb(189, 189, 189)), new Rectangle(0, 0, Width - 1, Height - 2));
                    G.DrawLine(new Pen(Color.FromArgb(200, 168, 168, 168)), new Point(1, Height - 1), new Point(Width - 2, Height - 1));
                    ForeColor = Color.FromArgb(27, 94, 137);
                    G.DrawString(Text, Font, new SolidBrush(Color.FromArgb(200, Color.White)), new Rectangle(-1, 0, Width - 1, Height - 1), new StringFormat
                    {
                        LineAlignment = StringAlignment.Center,
                        Alignment = StringAlignment.Center
                    });
                    break;
                case MouseState.Over:
                    //Mouse Hover
                    LinearGradientBrush bodyGradOver = new LinearGradientBrush(new Rectangle(0, 0, Width - 1, Height - 2), Color.FromArgb(70, 153, 205), Color.FromArgb(53, 124, 170), 90);
                    G.FillRectangle(bodyGradOver, bodyGradOver.Rectangle);
                    LinearGradientBrush bodyInBorderOver = new LinearGradientBrush(new Rectangle(1, 1, Width - 3, Height - 4), Color.FromArgb(88, 168, 221), Color.FromArgb(76, 149, 194), 90);
                    G.DrawRectangle(new Pen(bodyInBorderOver), new Rectangle(1, 1, Width - 3, Height - 4));
                    G.DrawRectangle(new Pen(Color.FromArgb(38, 93, 131)), new Rectangle(0, 0, Width - 1, Height - 2));
                    G.DrawLine(new Pen(Color.FromArgb(200, 25, 73, 109)), new Point(1, Height - 1), new Point(Width - 2, Height - 1));
                    ForeColor = Color.White;
                    G.DrawString(Text, Font, new SolidBrush(Color.FromArgb(200, Color.Black)), new Rectangle(-1, -2, Width - 1, Height - 1), new StringFormat
                    {
                        LineAlignment = StringAlignment.Center,
                        Alignment = StringAlignment.Center
                    });
                    break;
                case MouseState.Down:
                    //Mouse Down
                    LinearGradientBrush bodyGradDown = new LinearGradientBrush(new Rectangle(0, 0, Width - 1, Height - 2), Color.FromArgb(70, 153, 205), Color.FromArgb(53, 124, 170), 270);
                    G.FillRectangle(bodyGradDown, bodyGradDown.Rectangle);
                    LinearGradientBrush bodyInBorderDown = new LinearGradientBrush(new Rectangle(1, 1, Width - 3, Height - 4), Color.FromArgb(88, 168, 221), Color.FromArgb(76, 149, 194), 270);
                    G.DrawRectangle(new Pen(bodyInBorderDown), new Rectangle(1, 1, Width - 3, Height - 4));
                    G.DrawRectangle(new Pen(Color.FromArgb(38, 93, 131)), new Rectangle(0, 0, Width - 1, Height - 2));
                    G.DrawLine(new Pen(Color.FromArgb(200, 25, 73, 109)), new Point(1, Height - 1), new Point(Width - 2, Height - 1));
                    ForeColor = Color.White;
                    G.DrawString(Text, Font, new SolidBrush(Color.FromArgb(200, Color.Black)), new Rectangle(-1, -2, Width - 1, Height - 1), new StringFormat
                    {
                        LineAlignment = StringAlignment.Center,
                        Alignment = StringAlignment.Center
                    });
                    break;
            }
            G.DrawString(Text, Font, new SolidBrush(ForeColor), new Rectangle(-1, -1, Width - 1, Height - 1), new StringFormat
            {
                LineAlignment = StringAlignment.Center,
                Alignment = StringAlignment.Center
            });

            e.Graphics.DrawImage(B, 0, 0);
            G.Dispose();
            B.Dispose();
        }
    }

    public class RecuperareIIComboBox : ComboBox
    {
        #region " Control Help - Properties & Flicker Control "
        private int _StartIndex = 0;
        public int StartIndex
        {
            get { return _StartIndex; }
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
        public void ReplaceItem(System.Object sender, System.Windows.Forms.DrawItemEventArgs e)
        {
            e.DrawBackground();
            try
            {
                if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                {
                    e.Graphics.FillRectangle(new SolidBrush(_highlightColor), e.Bounds);
                    LinearGradientBrush gloss = new LinearGradientBrush(e.Bounds, Color.FromArgb(15, Color.White), Color.FromArgb(0, Color.White), 90);
                    e.Graphics.FillRectangle(gloss, new Rectangle(new Point(e.Bounds.X, e.Bounds.Y), new Size(e.Bounds.Width, e.Bounds.Height)));
                    e.Graphics.DrawRectangle(new Pen(Color.FromArgb(50, Color.Black)) { DashStyle = DashStyle.Dot }, new Rectangle(e.Bounds.X, e.Bounds.Y, e.Bounds.Width - 1, e.Bounds.Height - 1));
                }
                else
                {
                    e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(255, 255, 255, 255)), e.Bounds);
                }
                using (SolidBrush b = new SolidBrush(Color.Black))
                {
                    e.Graphics.DrawString(base.GetItemText(base.Items[e.Index]), e.Font, b, new Rectangle(e.Bounds.X + 2, e.Bounds.Y, e.Bounds.Width - 4, e.Bounds.Height));
                }
            }
            catch
            {
            }
            e.DrawFocusRectangle();
        }
        protected void DrawTriangle(Color Clr, Point FirstPoint, Point SecondPoint, Point ThirdPoint, Graphics G)
        {
            List<Point> points = new List<Point>();
            points.Add(FirstPoint);
            points.Add(SecondPoint);
            points.Add(ThirdPoint);
            G.FillPolygon(new SolidBrush(Clr), points.ToArray());
        }
        private Color _highlightColor = Color.FromArgb(121, 176, 214);
        public Color ItemHighlightColor
        {
            get { return _highlightColor; }
            set
            {
                _highlightColor = value;
                Invalidate();
            }
        }
        #endregion

        public RecuperareIIComboBox() : base()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.ResizeRedraw | ControlStyles.UserPaint | ControlStyles.DoubleBuffer | ControlStyles.SupportsTransparentBackColor, true);
            DrawMode = DrawMode.OwnerDrawFixed;
            BackColor = Color.Transparent;
            ForeColor = Color.FromArgb(27, 94, 137);
            Font = new Font("Verdana", 6.75f, FontStyle.Bold);
            DropDownStyle = ComboBoxStyle.DropDownList;
            DoubleBuffered = true;
            Size = new Size(Width, 21);
            ItemHeight = 16;
            DrawItem += new DrawItemEventHandler(ReplaceItem);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Bitmap B = new Bitmap(Width, Height);
            Graphics G = Graphics.FromImage(B);
            G.SmoothingMode = SmoothingMode.HighQuality;


            G.Clear(BackColor);
            LinearGradientBrush bodyGradNone = new LinearGradientBrush(new Rectangle(0, 0, Width - 1, Height - 2), Color.FromArgb(245, 245, 245), Color.FromArgb(230, 230, 230), 90);
            G.FillRectangle(bodyGradNone, bodyGradNone.Rectangle);
            LinearGradientBrush bodyInBorderNone = new LinearGradientBrush(new Rectangle(0, 0, Width - 1, Height - 3), Color.FromArgb(252, 252, 252), Color.FromArgb(249, 249, 249), 90);
            G.DrawRectangle(new Pen(bodyInBorderNone), new Rectangle(1, 1, Width - 3, Height - 4));
            G.DrawRectangle(new Pen(Color.FromArgb(189, 189, 189)), new Rectangle(0, 0, Width - 1, Height - 2));
            G.DrawLine(new Pen(Color.FromArgb(200, 168, 168, 168)), new Point(1, Height - 1), new Point(Width - 2, Height - 1));
            DrawTriangle(Color.FromArgb(121, 176, 214), new Point(Width - 14, 8), new Point(Width - 7, 8), new Point(Width - 11, 12), G);
            G.DrawLine(new Pen(Color.FromArgb(27, 94, 137)), new Point(Width - 14, 8), new Point(Width - 8, 8));

            //Draw Separator line
            G.DrawLine(new Pen(Color.White), new Point(Width - 22, 1), new Point(Width - 22, Height - 3));
            G.DrawLine(new Pen(Color.FromArgb(189, 189, 189)), new Point(Width - 21, 1), new Point(Width - 21, Height - 3));
            G.DrawLine(new Pen(Color.White), new Point(Width - 20, 1), new Point(Width - 20, Height - 3));
            try
            {
                G.DrawString(Text, Font, new SolidBrush(ForeColor), new Rectangle(5, -1, Width - 20, Height), new StringFormat
                {
                    LineAlignment = StringAlignment.Center,
                    Alignment = StringAlignment.Near
                });
            }
            catch
            {
            }

            e.Graphics.DrawImage(B, 0, 0);
            G.Dispose();
            B.Dispose();
        }
    }

    public class RecuperareIITextBox : Control
    {

        TextBox txtbox = new TextBox();
        #region " Control Help - Properties & Flicker Control "
        private bool _passmask = false;
        public  bool UseSystemPasswordChar
        {
            get { return _passmask; }
            set
            {
                txtbox.UseSystemPasswordChar = UseSystemPasswordChar;
                _passmask = value;
                Invalidate();
            }
        }
        private int _maxchars = 32767;
        public  int MaxLength
        {
            get { return _maxchars; }
            set
            {
                _maxchars = value;
                txtbox.MaxLength = MaxLength;
                Invalidate();
            }
        }
        private HorizontalAlignment _align;
        public  HorizontalAlignment TextAlignment
        {
            get { return _align; }
            set
            {
                _align = value;
                Invalidate();
            }
        }
        private bool _multiline = false;
        public  bool MultiLine
        {
            get { return _multiline; }
            set
            {
                _multiline = value;
                Invalidate();
            }
        }


        protected override void OnTextChanged(System.EventArgs e)
        {
            base.OnTextChanged(e);
            Invalidate();
        }
        protected override void OnBackColorChanged(System.EventArgs e)
        {
            base.OnBackColorChanged(e);
            txtbox.BackColor = BackColor;
            Invalidate();
        }
        protected override void OnForeColorChanged(System.EventArgs e)
        {
            base.OnForeColorChanged(e);
            txtbox.ForeColor = ForeColor;
            Invalidate();
        }
        protected override void OnFontChanged(System.EventArgs e)
        {
            base.OnFontChanged(e);
            txtbox.Font = Font;
        }
        protected override void OnGotFocus(System.EventArgs e)
        {
            base.OnGotFocus(e);
            txtbox.Focus();
        }
        public void TextChngTxtBox(Object system, EventArgs e)
        {
            Text = txtbox.Text;
        }
        public void TextChng(Object system, EventArgs e)
        {
            txtbox.Text = Text;
        }
        public void NewTextBox()
        {
            {
                txtbox.Multiline = false;
                txtbox.BackColor = Color.FromArgb(43, 43, 43);
                txtbox.ForeColor = ForeColor;
                txtbox.Text = string.Empty;
                txtbox.TextAlign = HorizontalAlignment.Center;
                txtbox.BorderStyle = BorderStyle.None;
                txtbox.Location = new Point(5, 4);
                txtbox.Font = new Font("Trebuchet MS", 8.25f, FontStyle.Bold);
                txtbox.Size = new Size(Width - 10, Height - 11);
                txtbox.UseSystemPasswordChar = UseSystemPasswordChar;
            }

        }
        #endregion

        public RecuperareIITextBox() : base()
        {

            NewTextBox();
            Controls.Add(txtbox);

            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);

            Text = "";
            BackColor = Color.FromArgb(233, 233, 233);
            ForeColor = Color.FromArgb(27, 94, 137);
            Font = new Font("Verdana", 6.75f, FontStyle.Bold);
            Size = new Size(135, 35);
            DoubleBuffered = true;
            txtbox.TextChanged += new EventHandler(TextChngTxtBox);
            base.TextChanged += new EventHandler(TextChng);
        }

        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            Bitmap B = new Bitmap(Width, Height);
            Graphics G = Graphics.FromImage(B);
            G.SmoothingMode = SmoothingMode.HighQuality;

            Height = txtbox.Height + 10;
            {
                txtbox.Width = Width - 10;
                txtbox.TextAlign = TextAlignment;
                txtbox.UseSystemPasswordChar = UseSystemPasswordChar;
            }

            G.Clear(Color.Transparent);

            LinearGradientBrush innerBorderBrush = new LinearGradientBrush(new Rectangle(1, 1, Width - 3, Height - 3), Color.FromArgb(220, 220, 220), Color.FromArgb(228, 228, 228), 90);
            Pen innerBorderPen = new Pen(innerBorderBrush);
            G.DrawRectangle(innerBorderPen, new Rectangle(1, 1, Width - 3, Height - 3));
            G.DrawLine(new Pen(Color.FromArgb(191, 191, 191)), new Point(1, 1), new Point(Width - 3, 1));

            G.DrawRectangle(new Pen(Color.FromArgb(254, 254, 254)), new Rectangle(0, 0, Width - 1, Height - 1));
            e.Graphics.DrawImage(B, 0, 0);
            G.Dispose();
            B.Dispose();
        }
    }

    [DefaultEvent("CheckedChanged")]
    public class RecuperareIICheckBox : Control
    {

        #region " Control Help - MouseState & Flicker Control"
        private Point mouse = new Point(0, 0);

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            mouse = e.Location;
        }
        protected override void OnTextChanged(System.EventArgs e)
        {
            base.OnTextChanged(e);
            Invalidate();
        }
        private bool _Checked;
        public bool Checked
        {
            get { return _Checked; }
            set
            {
                _Checked = value;
                Invalidate();
            }
        }
        protected override void OnResize(System.EventArgs e)
        {
            base.OnResize(e);
            Height = 14;
        }
        protected override void OnClick(System.EventArgs e)
        {
            if (mouse.X <= Height - 1)
            {
                _Checked = !_Checked;
                Invalidate();
                if (CheckedChanged != null)
                {
                    CheckedChanged(this);
                }
            }

            base.OnClick(e);
        }
        public event CheckedChangedEventHandler CheckedChanged;
        public delegate void CheckedChangedEventHandler(object sender);
        #endregion

        public RecuperareIICheckBox() : base()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.SupportsTransparentBackColor, true);
            Font = new Font("Verdana", 6.75f, FontStyle.Bold);
            BackColor = Color.Transparent;
            ForeColor = Color.FromArgb(27, 94, 137);
            Size = new Size(145, 16);
            DoubleBuffered = true;
        }

        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            Bitmap B = new Bitmap(Width, Height);
            Graphics G = Graphics.FromImage(B);

            Rectangle checkBoxRectangle = new Rectangle(0, 0, Height - 1, Height - 1);
            G.SmoothingMode = SmoothingMode.HighQuality;

            G.Clear(Parent.FindForm().BackColor);

            LinearGradientBrush bodyGrad = new LinearGradientBrush(checkBoxRectangle, Color.FromArgb(245, 245, 245), Color.FromArgb(231, 231, 231), 90);
            G.FillRectangle(bodyGrad, bodyGrad.Rectangle);
            G.DrawRectangle(new Pen(Color.FromArgb(189, 189, 189)), new Rectangle(0, 0, Height - 1, Height - 2));
            G.DrawRectangle(new Pen(Color.FromArgb(252, 252, 252)), new Rectangle(1, 1, Height - 3, Height - 4));
            G.DrawLine(new Pen(Color.FromArgb(168, 168, 168)), new Point(1, Height - 1), new Point(Height - 2, Height - 1));

            if (Checked)
            {
                Rectangle chkPoly = new Rectangle(checkBoxRectangle.X + checkBoxRectangle.Width / 4, checkBoxRectangle.Y + checkBoxRectangle.Height / 4, checkBoxRectangle.Width / 2, checkBoxRectangle.Height / 2);
                Point[] Poly = {
				new Point(chkPoly.X, chkPoly.Y + chkPoly.Height / 2),
				new Point(chkPoly.X + chkPoly.Width / 2, chkPoly.Y + chkPoly.Height),
				new Point(chkPoly.X + chkPoly.Width, chkPoly.Y)
			};
                G.SmoothingMode = SmoothingMode.HighQuality;
                Pen P1 = new Pen(Color.FromArgb(27, 94, 137), 2);
                LinearGradientBrush chkGrad = new LinearGradientBrush(chkPoly, Color.FromArgb(200, 200, 200), Color.FromArgb(255, 255, 255), 0f);

                G.DrawString("a", new Font("Marlett", 10.75f), new SolidBrush(Color.FromArgb(220, ForeColor)), new Rectangle(-2, -1, Width - 1, Height - 1), new StringFormat
                {
                    Alignment = StringAlignment.Near,
                    LineAlignment = StringAlignment.Near
                });
            }

            G.DrawString(Text, Font, new SolidBrush(ForeColor), new Point(16, 0), new StringFormat
            {
                Alignment = StringAlignment.Near,
                LineAlignment = StringAlignment.Near
            });


            e.Graphics.DrawImage(B, 0, 0);
            G.Dispose();
            B.Dispose();

        }

    }

    [DefaultEvent("CheckedChanged")]
    public class RecuperareIIRadioButton : Control
    {

        #region " Control Help - MouseState & Flicker Control"
        private Point mouse = new Point(0, 0);
        protected override void OnResize(System.EventArgs e)
        {
            base.OnResize(e);
            Height = 14;
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            mouse = e.Location;
            base.OnMouseMove(e);
        }
        protected override void OnTextChanged(System.EventArgs e)
        {
            base.OnTextChanged(e);
            Invalidate();
        }
        private bool _Checked;
        public bool Checked
        {
            get { return _Checked; }
            set
            {
                _Checked = value;
                InvalidateControls();
                if (CheckedChanged != null)
                {
                    CheckedChanged(this);
                }
                Invalidate();
            }
        }
        protected override void OnClick(EventArgs e)
        {
            if (mouse.X <= Height - 1)
            {
                if (!_Checked)
                    Checked = true;
                Invalidate();
            }
            base.OnClick(e);
        }
        public event CheckedChangedEventHandler CheckedChanged;
        public delegate void CheckedChangedEventHandler(object sender);
        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            InvalidateControls();
        }
        private void InvalidateControls()
        {
            if (!IsHandleCreated || !_Checked)
                return;

            foreach (Control C in Parent.Controls)
            {
                if (!object.ReferenceEquals(C, this) && C is RecuperareIIRadioButton)
                {
                    ((RecuperareIIRadioButton)C).Checked = false;
                }
            }
        }
        #endregion

        public RecuperareIIRadioButton() : base()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.SupportsTransparentBackColor, true);
            BackColor = Color.Transparent;
            ForeColor = Color.FromArgb(27, 94, 137);
            Font = new Font("Verdana", 6.75f, FontStyle.Bold);
            Size = new Size(152, 14);
            DoubleBuffered = true;
        }

        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            Bitmap B = new Bitmap(Width, Height);
            Graphics G = Graphics.FromImage(B);

            G.SmoothingMode = SmoothingMode.HighQuality;
            G.Clear(Parent.FindForm().BackColor);

            G.DrawEllipse(new Pen(Color.FromArgb(168, 168, 168)), new Rectangle(0, 0, Height - 2, Height - 1));
            LinearGradientBrush bgGrad = new LinearGradientBrush(new Rectangle(0, 0, Height - 2, Height - 2), Color.FromArgb(245, 245, 245), Color.FromArgb(231, 231, 231), 90);
            G.FillEllipse(bgGrad, new Rectangle(0, 0, Height - 2, Height - 2));
            G.DrawEllipse(new Pen(Color.FromArgb(252, 252, 252)), new Rectangle(1, 1, Height - 4, Height - 4));

            if (Checked)
            {
                G.FillEllipse(new SolidBrush(Color.FromArgb(27, 94, 137)), new Rectangle(3, 3, Height - 8, Height - 8));
                G.FillEllipse(new SolidBrush(Color.FromArgb(150, 118, 177, 211)), new Rectangle(4, 4, Height - 10, Height - 10));
            }

            G.DrawString(Text, Font, new SolidBrush(ForeColor), new Point(16, 1), new StringFormat
            {
                Alignment = StringAlignment.Near,
                LineAlignment = StringAlignment.Near
            });

            e.Graphics.DrawImage(B, 0, 0);
            G.Dispose();
            B.Dispose();
        }

    }

    public class RecuperareIIStatusBar : Control
    {

        protected override void CreateHandle()
        {
            base.CreateHandle();
            Dock = DockStyle.Bottom;
        }
        protected override void OnResize(System.EventArgs e)
        {
            base.OnResize(e);
            Invalidate();
        }
        protected override void OnTextChanged(System.EventArgs e)
        {
            base.OnTextChanged(e);
            Invalidate();
        }

        public RecuperareIIStatusBar() : base()
        {
            Font = new Font("Verdana", 6.75f, FontStyle.Bold);
            ForeColor = Color.FromArgb(27, 94, 137);
            Size = new Size(Width, 20);
            DoubleBuffered = true;
        }

        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            Bitmap B = new Bitmap(Width, Height);
            Graphics G = Graphics.FromImage(B);
            base.OnPaint(e);

            LinearGradientBrush bodyGradNone = new LinearGradientBrush(new Rectangle(0, 1, Width, Height - 1), Color.FromArgb(245, 245, 245), Color.FromArgb(230, 230, 230), 90);
            G.FillRectangle(bodyGradNone, bodyGradNone.Rectangle);
            LinearGradientBrush bodyInBorderNone = new LinearGradientBrush(new Rectangle(1, 1, Width - 3, Height - 3), Color.FromArgb(200, 252, 252, 252), Color.FromArgb(200, 249, 249, 249), 90);
            G.DrawRectangle(new Pen(bodyInBorderNone), new Rectangle(1, 1, Width - 3, Height - 3));
            G.DrawRectangle(new Pen(Color.FromArgb(189, 189, 189)), new Rectangle(0, 0, Width - 1, Height - 1));

            G.DrawString(Text, Font, new SolidBrush(ForeColor), new Point(4, 4), StringFormat.GenericDefault);

            e.Graphics.DrawImage(B, 0, 0);
            G.Dispose();
            B.Dispose();
        }
    }

    public class RecuperareIINumericUpDown : Control
    {

        #region " Properties & Flicker Control "
        private int X;
        private int Y;
        private long _Value;
        private long _Max;
        private long _Min;
        private bool Typing;
        public long Value
        {
            get { return _Value; }
            set
            {
                if (value <= _Max & value >= _Min)
                    _Value = value;
                Invalidate();
            }
        }
        public long Maximum
        {
            get { return _Max; }
            set
            {
                if (value > _Min)
                    _Max = value;
                if (_Value > _Max)
                    _Value = _Max;
                Invalidate();
            }
        }
        public long Minimum
        {
            get { return _Min; }
            set
            {
                if (value < _Max)
                    _Min = value;
                if (_Value < _Min)
                    _Value = _Min;
                Invalidate();
            }
        }
        protected override void OnMouseMove(System.Windows.Forms.MouseEventArgs e)
        {
            base.OnMouseMove(e);
            X = e.Location.X;
            Y = e.Location.Y;
            Invalidate();
            if (e.X < Width - 23)
                Cursor = Cursors.IBeam;
            else
                Cursor = Cursors.Default;
        }
        protected override void OnResize(System.EventArgs e)
        {
            base.OnResize(e);
            this.Height = 30;
        }
        protected override void OnMouseDown(System.Windows.Forms.MouseEventArgs e)
        {
            base.OnMouseClick(e);
            if (X > this.Width - 21 && X < this.Width - 3)
            {
                if (Y < 15)
                {
                    if ((Value + 1) <= _Max)
                        _Value += 1;
                }
                else
                {
                    if ((Value - 1) >= _Min)
                        _Value -= 1;
                }
            }
            else
            {
                Typing = !Typing;
                Focus();
            }
            Invalidate();
        }
        protected override void OnKeyPress(System.Windows.Forms.KeyPressEventArgs e)
        {
            base.OnKeyPress(e);
            try
            {
                if (Typing)
                    _Value = Convert.ToInt32(Convert.ToString(Convert.ToString(_Value) + e.KeyChar.ToString()));
                if (_Value > _Max) { _Value = _Max; }

            }
            catch
            {
            }
        }
        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);
            if (e.KeyCode == Keys.Up)
            {
                if ((Value + 1) <= _Max)
                    _Value += 1;
                Invalidate();
            }
            else if (e.KeyCode == Keys.Down)
            {
                if ((Value - 1) >= _Min)
                    _Value -= 1;
            }
            else if (e.KeyCode == Keys.Back)
            {
                String tmp = _Value.ToString();
                tmp = tmp.Remove(Convert.ToInt32(tmp.Length - 1));
                if (tmp.Length == 0) { tmp = "0"; }
                _Value = Convert.ToInt32(tmp);
            }
            Invalidate();
        }
        protected void DrawTriangle(Color Clr, Point FirstPoint, Point SecondPoint, Point ThirdPoint, Graphics G)
        {
            List<Point> points = new List<Point>();
            points.Add(FirstPoint);
            points.Add(SecondPoint);
            points.Add(ThirdPoint);
            G.FillPolygon(new SolidBrush(Clr), points.ToArray());
        }
        #endregion

        public RecuperareIINumericUpDown()
        {
            _Max = 9999999;
            _Min = 0;
            Cursor = Cursors.IBeam;
            SetStyle(ControlStyles.UserPaint | ControlStyles.SupportsTransparentBackColor, true);
            BackColor = Color.FromArgb(233, 233, 233);
            ForeColor = Color.FromArgb(27, 94, 137);
            DoubleBuffered = true;
            Font = new Font("Verdana", 6.75f, FontStyle.Bold);

        }

        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            Bitmap B = new Bitmap(Width, Height);
            Graphics G = Graphics.FromImage(B);

            G.Clear(BackColor);
            G.SmoothingMode = SmoothingMode.HighQuality;

            LinearGradientBrush innerBorderBrush = new LinearGradientBrush(new Rectangle(1, 1, Width - 3, Height - 3), Color.FromArgb(220, 220, 220), Color.FromArgb(228, 228, 228), 90);
            Pen innerBorderPen = new Pen(innerBorderBrush);
            G.DrawRectangle(innerBorderPen, new Rectangle(1, 1, Width - 3, Height - 3));
            G.DrawLine(new Pen(Color.FromArgb(191, 191, 191)), new Point(1, 1), new Point(Width - 3, 1));

            G.DrawRectangle(new Pen(Color.FromArgb(254, 254, 254)), new Rectangle(0, 0, Width - 1, Height - 1));


            LinearGradientBrush buttonGradient = new LinearGradientBrush(new Rectangle(Width - 23, 4, 19, 21), Color.FromArgb(245, 245, 245), Color.FromArgb(232, 232, 232), 90);
            G.FillRectangle(buttonGradient, buttonGradient.Rectangle);
            G.DrawRectangle(new Pen(Color.FromArgb(252, 252, 252)), new Rectangle(Width - 22, 5, 17, 19));
            G.DrawRectangle(new Pen(Color.FromArgb(190, 190, 190)), new Rectangle(Width - 23, 4, 19, 21));
            G.DrawLine(new Pen(Color.FromArgb(200, 167, 167, 167)), new Point(Width - 22, Height - 4), new Point(Width - 5, Height - 4));
            G.DrawLine(new Pen(Color.FromArgb(188, 188, 188)), new Point(Width - 22, Height - 16), new Point(Width - 5, Height - 16));
            G.DrawLine(new Pen(Color.FromArgb(252, 252, 252)), new Point(Width - 22, Height - 15), new Point(Width - 5, Height - 15));


            //Top Triangle
            DrawTriangle(Color.FromArgb(27, 94, 137), new Point(Width - 17, 18), new Point(Width - 9, 18), new Point(Width - 13, 21), G);

            //Bottom Triangle
            DrawTriangle(Color.FromArgb(27, 94, 137), new Point(Width - 17, 10), new Point(Width - 9, 10), new Point(Width - 13, 7), G);

            G.DrawString(Value.ToString(), Font, new SolidBrush(ForeColor), new Rectangle(5, 0, Width - 1, Height - 1), new StringFormat { LineAlignment = StringAlignment.Center });

            e.Graphics.DrawImage(B, 0, 0);
            G.Dispose();
            B.Dispose();
        }

    }

    public class RecuperareIILabel : Label
    {
        public RecuperareIILabel() : base()
        {
            Font = new Font("Verdana", 6.75f, FontStyle.Bold);
            ForeColor = Color.FromArgb(27, 94, 137);
        }
    }

}