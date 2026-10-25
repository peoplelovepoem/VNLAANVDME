using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Tetris.GameUI
{
    public class OutlinedLabel : Label
    {
        internal static Label Replace(Label original)
        {
            if (original is OutlinedLabel || original.Parent == null)
                return original;
            var parent = original.Parent;
            var index = parent.Controls.GetChildIndex(original);
            var title = new OutlinedLabel
            {
                Name = original.Name,
                Text = original.Text,
                Font = original.Font,
                ForeColor = original.ForeColor,
                BackColor = original.BackColor,
                AutoSize = original.AutoSize,
                TextAlign = original.TextAlign,
                Padding = original.Padding,
                Margin = original.Margin,
                TabIndex = original.TabIndex,
                TabStop = original.TabStop,
                AccessibleName = original.AccessibleName
            };
            parent.SuspendLayout();
            try
            {
                parent.Controls.Add(title);
                title.Bounds = original.Bounds;
                title.Anchor = original.Anchor;
                title.Dock = original.Dock;
                parent.Controls.SetChildIndex(title, index);
                parent.Controls.Remove(original);
                original.Dispose();
            }
            finally { parent.ResumeLayout(false); }
            return title;
        }

        private Color outlineColor = Color.Cornsilk;
        private float outlineWidth = 4f;
        public OutlinedLabel()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor, true);
        }
        [DefaultValue(typeof(Color), "Cornsilk")]
        public Color OutlineColor
        {
            get
            {
                return outlineColor;
            }
            set
            {
                outlineColor = value;
                Invalidate();
            }
        }
        [DefaultValue(4f)]
        public float OutlineWidth
        {
            get
            {
                return outlineWidth;
            }
            set
            {
                outlineWidth = System.Math.Max(0, value);
                Invalidate();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (string.IsNullOrEmpty(Text))
                return;
            using (var path = new GraphicsPath())
            using (var format = new StringFormat(StringFormat.GenericDefault))
            using (var brush = new SolidBrush(ForeColor))
            using (var pen = new Pen(OutlineColor, OutlineWidth * e.Graphics.DpiX / 96f))
            {
                format.FormatFlags |= StringFormatFlags.NoWrap;
                format.Alignment = TextAlign == ContentAlignment.TopCenter ||
                    TextAlign == ContentAlignment.MiddleCenter || TextAlign == ContentAlignment.BottomCenter
                    ? StringAlignment.Center : StringAlignment.Near;
                format.LineAlignment = TextAlign == ContentAlignment.MiddleCenter
                    ? StringAlignment.Center : StringAlignment.Near;
                var area = new RectangleF(Padding.Left + OutlineWidth, Padding.Top + OutlineWidth,
                    System.Math.Max(1, ClientSize.Width - Padding.Horizontal - 2 * OutlineWidth),
                    System.Math.Max(1, ClientSize.Height - Padding.Vertical - 2 * OutlineWidth));
                path.AddString(Text, Font.FontFamily, (int)Font.Style,
                    Font.SizeInPoints * e.Graphics.DpiY / 72f, area, format);
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                pen.LineJoin = LineJoin.Round;
                if (OutlineWidth > 0)
                    e.Graphics.DrawPath(pen, path);
                e.Graphics.FillPath(brush, path);
            }
        }
    }
}
