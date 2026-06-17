using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Project_CG_Paint.Data.Scene;
using Project_CG_Paint.Rendering;

namespace Project_CG_Paint.Forms.Paint
{
    public partial class PaintForm : Form
    {
        private Bitmap demoBitmap;

        public PaintForm()
        {
            InitializeComponent();
            Text = "Computer Graphics - Demo Scene";
            ClientSize = new Size(DemoSceneFactory.Width, DemoSceneFactory.Height);
            MinimumSize = new Size(720, 460);
            DoubleBuffered = true;

            demoBitmap = RenderService.RenderColoredPoints(
                DemoSceneFactory.Width,
                DemoSceneFactory.Height,
                DemoSceneFactory.CreateScene(),
                Color.FromArgb(134, 203, 236));
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (demoBitmap == null)
                return;

            e.Graphics.Clear(Color.FromArgb(134, 203, 236));
            e.Graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
            e.Graphics.PixelOffsetMode = PixelOffsetMode.Half;
            e.Graphics.DrawImage(demoBitmap, GetCanvasRectangle());
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Invalidate();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            if (demoBitmap != null)
            {
                demoBitmap.Dispose();
                demoBitmap = null;
            }

            base.OnFormClosed(e);
        }

        private Rectangle GetCanvasRectangle()
        {
            float scale = Math.Min(
                ClientSize.Width / (float)DemoSceneFactory.Width,
                ClientSize.Height / (float)DemoSceneFactory.Height);

            int width = (int)Math.Round(DemoSceneFactory.Width * scale);
            int height = (int)Math.Round(DemoSceneFactory.Height * scale);
            int x = (ClientSize.Width - width) / 2;
            int y = (ClientSize.Height - height) / 2;

            return new Rectangle(x, y, width, height);
        }
    }
}
