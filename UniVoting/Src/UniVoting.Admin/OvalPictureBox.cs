using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Color = System.Drawing.Color;

namespace UniVoting.Admin
{
    class OvalPictureBox : PictureBox
    {
        public OvalPictureBox()
        {
           BackColor=Color.DarkGray;
        }
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            using (var gp = new GraphicsPath())
            {
                gp.AddEllipse(new Rectangle(0, 0, Width - 1, Height - 1));
                Region = new Region(gp);
            }
        }
    }
}
