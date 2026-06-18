using Project_CG_Paint.Forms.Animation;
using Project_CG_Paint.Forms.Transform;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project_CG_Paint.Forms.Paint
{
    public partial class PaintForm : Form
    {
        public PaintForm()
        {
            InitializeComponent();
        }

        private void canvas_MouseMove(object sender, MouseEventArgs e)
        {
            this.statusMousePos.Text = "Mouse Position: " + e.Location.ToString();
        }

        private void btnOpenAnimation_Click(object sender, EventArgs e)
        {
            AnimationForm animationForm = new AnimationForm();
            animationForm.Show();
        }

        private void btnOpenTransform_Click(object sender, EventArgs e)
        {
            TransformForm transformForm = new TransformForm();
            transformForm.Show();
        }

        private void btnExitApplication_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
