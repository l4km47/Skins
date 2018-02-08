// Author: TxTriton
// Name: StepProgress GDI Control
// (c) 2013

class StepProgress : ProgressBar
{
	private const int SaturationFactor = 85;

	public Color TopFillColor
	{
		get { return topFillColor; }
		set { topFillColor = value; }
	}

	public Color BottomFillColor
	{
		get { return bottomFillColor; }
		set { bottomFillColor = value; }
	}

	private Color topFillColor = Color.FromArgb(255, 255, 80, 80);
	private Color bottomFillColor = Color.FromArgb(255, 150, 0, 0);

	private static readonly Color BottomCircleColor = Color.FromArgb(255, 30, 30, 30);
	private static readonly Color BarColor = Color.FromArgb(255, 10, 10, 10);
	private static readonly Pen OutlinePen = new Pen(Color.FromArgb(255, 30, 30, 30));

	private int steps = 5;

	public int Steps // Configures the number of steps to create
	{
		get { return steps; }
		set { steps = value > 1 ? value : 2; }
	}

	public StepProgress()
	{
		Size = new Size(400, 20);
		SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer | ControlStyles.OptimizedDoubleBuffer |
			ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor | ControlStyles.UserPaint, true);
	}

	protected override void OnPaint(PaintEventArgs e)
	{
		base.OnPaint(e);
		e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
		e.Graphics.Clear(Parent.BackColor);

		const int pad = 1;
		int circleWidth = Height - (pad * 2);
		int circleDisp = (Width - circleWidth) / Steps;
		circleDisp += circleDisp / (Steps - 1);

		List<Point> numberPoints = new List<Point>(Steps);

		using (GraphicsPath progressPath = new GraphicsPath())
		{
			int barHeight = circleWidth / 4;
			Rectangle barRect = new Rectangle(circleWidth / 2, (Height / 2) - (barHeight / 2), circleDisp * (Steps - 1), barHeight);
			DrawBackgroundBar(e.Graphics, progressPath, pad, barRect);

			Region progressRegion = new Region(new Rectangle(Point.Empty,
		  new Size(Width * Value / 100, Height)));

			using (LinearGradientBrush progressFill =
				new LinearGradientBrush(ClientRectangle, TopFillColor, BottomFillColor, 90f))
			{
				FillClipRegion(e.Graphics, progressFill, progressRegion, progressPath);

				using (GraphicsPath circlePath = new GraphicsPath())
				{
		for (int i = 1, p = 0; i <= Steps; i++, p += circleDisp)
		{
		Rectangle rect = new Rectangle(new Point(p, (Height / 2) - (circleWidth / 2)),
		 new Size(circleWidth, circleWidth));
		circlePath.AddEllipse(rect);
		string stepNum = i.ToString(CultureInfo.InvariantCulture);
		SizeF fontSize = e.Graphics.MeasureString(stepNum, Font);
		numberPoints.Add(new Point(p + (circleWidth / 2) - (int)(fontSize.Width / 2),
		  (Height / 2) - (int)(fontSize.Height / 2)));
		}

		using (LinearGradientBrush circleFill =
		new LinearGradientBrush(ClientRectangle, RotateSaturation(BottomCircleColor, SaturationFactor), BottomCircleColor, 90f))
		{
		e.Graphics.FillPath(circleFill, circlePath);
		e.Graphics.DrawPath(OutlinePen, circlePath);
		}

		FillClipRegion(e.Graphics, progressFill, progressRegion, circlePath, progressPath);
				}
			}
		}

		DrawStepNumbers(e.Graphics, numberPoints, Font);
	}

	private static Color RotateSaturation(Color c, int factor)
	{
		return Color.FromArgb(c.A, 
			(c.R + factor) & 0xFF,
			(c.G + factor) & 0xFF,
			(c.B + factor) & 0xFF);
	}

	private static void FillClipRegion(Graphics g, Brush fillBrush, Region clipRegion, params GraphicsPath[] path)
	{
		g.Clip = clipRegion;
		foreach (var p in path) g.FillPath(fillBrush, p);
		g.ResetClip();
	}

	private static void DrawStepNumbers(Graphics g, IEnumerable<Point> numberPoints, Font font)
	{
		int n = 0;
		foreach (var p in numberPoints)
		{
			g.DrawString((++n).ToString(CultureInfo.InvariantCulture),
				font, Brushes.WhiteSmoke, p);
		}
	}

	private void DrawBackgroundBar(Graphics g, GraphicsPath progPath, int pad, Rectangle barRect)
	{
		using (LinearGradientBrush barFill = new LinearGradientBrush(ClientRectangle, BarColor, RotateSaturation(BarColor, SaturationFactor), 90f))
		{
			g.FillRectangle(barFill, barRect);
			g.DrawRectangle(OutlinePen, barRect);
		}
		progPath.AddRectangle(new Rectangle(barRect.X + pad, barRect.Y + pad, barRect.Width - (pad * 2),
		barRect.Height - (pad * 2)));
	}
}