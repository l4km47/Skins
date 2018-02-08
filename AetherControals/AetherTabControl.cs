using System.Drawing;
using System.Windows.Forms;

namespace sampletest1.AetherControals
{
    public sealed class AetherTabControl : TabControl
    {

        private Graphics _g;
        private Rectangle _rect;
        private SizeF _ms1;

        private SizeF _ms2;
        public bool UpperText { get; set; }

        public AetherTabControl()
        {
            DoubleBuffered = true;
            Alignment = TabAlignment.Left;
            SizeMode = TabSizeMode.Fixed;
            ItemSize = new Size(40, 190);
            Dock = DockStyle.Fill;
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            SetStyle(ControlStyles.UserPaint, true);
        }

        protected override void OnControlAdded(ControlEventArgs e)
        {
            base.OnControlAdded(e);
            e.Control.BackColor = Color.White;
            e.Control.ForeColor = Helpers.ColorFromHex("343843");

            using (Font f1 = new Font("Segoe UI", 9))
            {
                e.Control.Font = f1;
            }

        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle = cp.ExStyle | 0x2000000;
                return cp;
            }
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            _g = e.Graphics;
            _g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            _g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            base.OnPaint(e);

            _g.Clear(Helpers.ColorFromHex("343843"));


            for (int I = 0; I <= TabPages.Count - 1; I++)
            {
                _rect = GetTabRect(I);


                if (SelectedIndex == I)
                {
                    using (SolidBrush b1 = new SolidBrush(Helpers.ColorFromHex("3A3E49")))
                    {
                        _g.FillRectangle(b1, new Rectangle(_rect.X - 4, _rect.Y + 1, _rect.Width + 6, _rect.Height));
                    }

                }

                using (SolidBrush b1 = new SolidBrush(Helpers.ColorFromHex("737A8A")))
                {


                    if (UpperText)
                    {
                        using (Font f1 = new Font("Segoe UI", (float)7.75, FontStyle.Bold))
                        {
                            _g.DrawString(TabPages[I].Text.ToUpper(), f1, b1, new Point(_rect.X + 50, _rect.Y + 13));
                        }


                    }
                    else
                    {
                        using (Font f1 = new Font("Segoe UI semibold", 9))
                        {
                            _g.DrawString(TabPages[I].Text, f1, b1, new Point(_rect.X + 50, _rect.Y + 11));
                        }

                    }

                }


                if (TabPages[I].Tag != null)
                {

                    if (UpperText)
                    {
                        using (Font f1 = new Font("Segoe UI", (float)7.75, FontStyle.Bold))
                        {
                            _ms1 = _g.MeasureString(TabPages[I].Text, f1);
                        }


                    }
                    else
                    {
                        using (Font f1 = new Font("Segoe UI semibold", 9))
                        {
                            _ms1 = _g.MeasureString(TabPages[I].Text, f1);
                        }

                    }

                    using (Font f1 = new Font("Segoe UI", 9))
                    {
                        _ms2 = _g.MeasureString((string)TabPages[I].Tag, f1);
                    }

                    using (SolidBrush b1 = new SolidBrush(Helpers.ColorFromHex("424452")))
                    {
                        using (Pen p1 = new Pen(Helpers.ColorFromHex("323541")))
                        {
                            using (SolidBrush b2 = new SolidBrush(Helpers.ColorFromHex("737A8A")))
                            {
                                _g.FillRectangle(b1, new Rectangle(_rect.X + (int)_ms1.Width + 72, _rect.Y + 13, (int)_ms2.Width + 5, 15));
                                Helpers.DrawRoundRect(_g, new Rectangle(_rect.X + (int)_ms1.Width + 72, _rect.Y + 13, (int)_ms2.Width + 5, 15), 3, p1);
                                int n;
                                bool isNumeric = int.TryParse(TabPages[I].Tag.ToString(), out n);
                                if (isNumeric)
                                {
                                    using (Font f1 = new Font("Segoe UI", 8, FontStyle.Bold))
                                    {
                                        _g.DrawString(TabPages[I].Tag.ToString(), f1, b2, new Point(_rect.X + (int)_ms1.Width + 75, _rect.Y + 14));
                                    }


                                }
                                else
                                {
                                    using (Font f1 = new Font("Segoe UI", 7, FontStyle.Bold))
                                    {
                                        _g.DrawString(TabPages[I].Tag.ToString().ToUpper(), f1, b2, new Point(_rect.X + (int)_ms1.Width + 75, _rect.Y + 14));
                                    }

                                }

                            }
                        }
                    }

                }


                if (I != 0)
                {
                    using (Pen p1 = new Pen(Helpers.ColorFromHex("3B3D49")))
                    {
                        using (Pen p2 = new Pen(Helpers.ColorFromHex("2F323C")))
                        {
                            _g.DrawLine(p1, new Point(_rect.X - 4, _rect.Y + 1), new Point(_rect.Width + 4, _rect.Y + 1));
                            _g.DrawLine(p2, new Point(_rect.X - 4, _rect.Y + 2), new Point(_rect.Width + 4, _rect.Y + 2));
                        }
                    }

                }

                if ((ImageList != null))
                {
                    if (!(TabPages[I].ImageIndex < 0))
                    {
                        _g.DrawImage(ImageList.Images[TabPages[I].ImageIndex], new Rectangle(_rect.X + 18, _rect.Y + ((_rect.Height / 2) - 8), 16, 16));
                    }
                }

            }

        }

    }
}