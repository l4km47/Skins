using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace sampletest1.AetherControals
{
    public sealed class AetherCircular : Control
    {

        private Graphics _g;
        private float _progressAngle;
        private float _remainderAngle;
        private bool _exceedingLimits;

        private string _exceedingSign;
        private float _progress;
        private float _max = 100;
        private float _min;
        public bool Percent { get; set; }


        public float Progress
        {
            get
            {
                return _progress;
            }
            set
            {
                if (value > Max)
                {
                    value = Max;
                    _exceedingSign = "+";
                    _exceedingLimits = true;
                    Invalidate();
                }
                else if (value < Min)
                {
                    value = Min;
                    _exceedingSign = "-";
                    _exceedingLimits = true;
                    Invalidate();
                }
                _progress = value;
                Invalidate();
            }
        }

        public float Max
        {
            get { return _max; }
            set
            {
                if (value < _progress)
                {
                    _progress = value;
                }
                _max = value;
                Invalidate();
            }
        }

        public float Min
        {
            get { return _min; }
            set
            {
                if (value > _progress)
                {
                    _progress = value;
                }
                _min = value;
                Invalidate();
            }
        }

        public Color Border { get; set; }
        public Color HatchPrimary { get; set; }
        public Color HatchSecondary { get; set; }

        protected override void OnCreateControl()
        {
            Percent = true;
            Border = Color.LightGray;
            HatchPrimary = Color.Green;
            HatchSecondary = Color.Red;
            DoubleBuffered = true;
        }

        public AetherCircular()
        {
            Percent = true;
            Border = Color.LightGray;
            HatchPrimary = Color.Green;
            HatchSecondary = Color.Red;
            DoubleBuffered = true;
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            _g = e.Graphics;
            _g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            _g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            base.OnPaint(e);

            _progressAngle = 360 / Max * Progress;
            _remainderAngle = 360 - _progressAngle;

            using (Pen p1 = new Pen(new System.Drawing.Drawing2D.HatchBrush(System.Drawing.Drawing2D.HatchStyle.LightUpwardDiagonal, HatchPrimary, HatchSecondary), 4))
            {
                using (Pen p2 = new Pen(Border, 4))
                {
                    _g.DrawArc(p1, new Rectangle(2, 2, Width - 5, Height - 5), -90, _progressAngle);
                    _g.DrawArc(p2, new Rectangle(2, 2, Width - 5, Height - 5), _progressAngle - 90, _remainderAngle);
                }
            }


            if (Percent)
            {
                using (Font f1 = new Font("Segoe UI", 9, FontStyle.Bold))
                {

                    if (_exceedingLimits)
                    {
                        Helpers.CenterString(_g, Progress + "%" + _exceedingSign, f1, Color.FromArgb(100, 100, 100), new Rectangle(1, 1, Width, Height + 1));
                    }
                    else
                    {
                        Helpers.CenterString(_g, Progress + "%", f1, Color.FromArgb(100, 100, 100), new Rectangle(1, 1, Width, Height + 1));
                    }

                }


            }
            else
            {
                if (_exceedingLimits)
                {
                    Helpers.CenterString(_g, Progress + _exceedingSign, new Font("Segoe UI", 9, FontStyle.Bold), Color.FromArgb(100, 100, 100), new Rectangle(1, 1, Width, Height + 1));
                }
                else
                {
                    Helpers.CenterString(_g, Progress.ToString(CultureInfo.InvariantCulture), 
                        new Font("Segoe UI", 9, FontStyle.Bold),
                        Color.FromArgb(100, 100, 100), new Rectangle(1, 1,
                            Width, Height + 1));
                }

            }

            _exceedingLimits = false;

            Helpers.CenterString(_g, Text.ToUpper(), new Font("Segoe UI", 6, FontStyle.Bold), Color.FromArgb(170, 170, 170), new Rectangle(2, 2, Width, Height + 27));

        }

    }
}