using System;
using System.Drawing;

namespace sampletest1.AetherControals
{
    internal static class Helpers
    {

        public enum MouseState : byte
        {
            None = 0,
            Over = 1,
            Down = 2
        }

        public static Rectangle FullRectangle(Size s, bool subtract)
        {
            if (subtract)
            {
                return new Rectangle(0, 0, s.Width - 1, s.Height - 1);
            }
            else
            {
                return new Rectangle(0, 0, s.Width, s.Height);
            }
        }

        public static Color GreyColor(uint g)
        {
            return Color.FromArgb((int)g, (int)g, (int)g);
        }

        public static void CenterString(Graphics g, string T, Font f, Color c, Rectangle r)
        {
            SizeF ts = g.MeasureString(T, f);

            using (SolidBrush b = new SolidBrush(c))
            {
                g.DrawString(T, f, b, new Point(r.Width / 2 - (int)(ts.Width / 2), r.Height / 2 - (int)(ts.Height / 2)));
            }

        }

        public static void FillRoundRect(Graphics g, Rectangle r, int curve, Brush b)
        {
            g.FillPie(b, r.X, r.Y, curve, curve, 180, 90);
            g.FillPie(b, r.X + r.Width - curve, r.Y, curve, curve, 270, 90);
            g.FillPie(b, r.X, r.Y + r.Height - curve, curve, curve, 90, 90);
            g.FillPie(b, r.X + r.Width - curve, r.Y + r.Height - curve, curve, curve, 0, 90);
            g.FillRectangle(b, Convert.ToInt32(r.X + curve / 2), r.Y, r.Width - curve, Convert.ToInt32(curve / 2));
            g.FillRectangle(b, r.X, Convert.ToInt32(r.Y + curve / 2), r.Width, r.Height - curve);
            g.FillRectangle(b, Convert.ToInt32(r.X + curve / 2), Convert.ToInt32(r.Y + r.Height - curve / 2), r.Width - curve, Convert.ToInt32(curve / 2));
        }

        public static void DrawRoundRect(Graphics g, Rectangle r, int curve, Pen pp)
        {
            g.DrawArc(pp, r.X, r.Y, curve, curve, 180, 90);
            g.DrawLine(pp, Convert.ToInt32(r.X + curve / 2), r.Y, Convert.ToInt32(r.X + r.Width - curve / 2), r.Y);
            g.DrawArc(pp, r.X + r.Width - curve, r.Y, curve, curve, 270, 90);
            g.DrawLine(pp, r.X, Convert.ToInt32(r.Y + curve / 2), r.X, Convert.ToInt32(r.Y + r.Height - curve / 2));
            g.DrawLine(pp, Convert.ToInt32(r.X + r.Width), Convert.ToInt32(r.Y + curve / 2), Convert.ToInt32(r.X + r.Width), Convert.ToInt32(r.Y + r.Height - curve / 2));
            g.DrawLine(pp, Convert.ToInt32(r.X + curve / 2), Convert.ToInt32(r.Y + r.Height), Convert.ToInt32(r.X + r.Width - curve / 2), Convert.ToInt32(r.Y + r.Height));
            g.DrawArc(pp, r.X, r.Y + r.Height - curve, curve, curve, 90, 90);
            g.DrawArc(pp, r.X + r.Width - curve, r.Y + r.Height - curve, curve, curve, 0, 90);
        }

        public enum Direction : byte
        {
            Up = 0,
            Down = 1,
            Left = 2,
            Right = 3
        }

        public static void DrawTriangle(Graphics g, Rectangle rect, Direction d, Color c)
        {
            int halfWidth = rect.Width / 2;
            int halfHeight = rect.Height / 2;
            Point p0 = Point.Empty;
            Point p1 = Point.Empty;
            Point p2 = Point.Empty;

            switch (d)
            {
                case Direction.Up:
                    p0 = new Point(rect.Left + halfWidth, rect.Top);
                    p1 = new Point(rect.Left, rect.Bottom);
                    p2 = new Point(rect.Right, rect.Bottom);

                    break;
                case Direction.Down:
                    p0 = new Point(rect.Left + halfWidth, rect.Bottom);
                    p1 = new Point(rect.Left, rect.Top);
                    p2 = new Point(rect.Right, rect.Top);

                    break;
                case Direction.Left:
                    p0 = new Point(rect.Left, rect.Top + halfHeight);
                    p1 = new Point(rect.Right, rect.Top);
                    p2 = new Point(rect.Right, rect.Bottom);

                    break;
                case Direction.Right:
                    p0 = new Point(rect.Right, rect.Top + halfHeight);
                    p1 = new Point(rect.Left, rect.Bottom);
                    p2 = new Point(rect.Left, rect.Top);

                    break;
            }

            using (SolidBrush b = new SolidBrush(c))
            {
                g.FillPolygon(b, new[] {
                    p0,
                    p1,
                    p2
                });
            }

        }

        public static Color ColorFromHex(string hex)
        {
            return ColorTranslator.FromHtml("#" + hex);
        }

    }
}